using System;
using MelonLoader;
using UnityEngine;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private GUIStyle _style;
        private Font _gameFont;
        private float _nextFontScan;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Granny Legacy Clock loaded.");
        }

        public override void OnUpdate()
        {
            if (Time.time < _nextFontScan)
                return;

            _nextFontScan = Time.time + 5f;

            try
            {
                Font[] fonts = Resources.FindObjectsOfTypeAll<Font>();

                foreach (Font font in fonts)
                {
                    if (font == null)
                        continue;

                    if (font == _gameFont)
                        continue;

                    MelonLogger.Msg($"FONT FOUND: {font.name}");

                    string name = font.name.ToLowerInvariant();

                    if (name.Contains("monotype") ||
                        name.Contains("corsiva"))
                    {
                        _gameFont = font;

                        MelonLogger.Msg(
                            $"MONOTYPE ACQUIRED: {font.name}");
                    }
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(
                    $"Font scan failed: {ex}");
            }
        }

        public override void OnGUI()
        {
            if (_style == null)
            {
                _style = new GUIStyle();

                _style.fontSize = 30;
                _style.alignment = TextAnchor.UpperRight;
                _style.normal.textColor = Color.white;
            }

            if (_gameFont != null)
                _style.font = _gameFont;

            string time = DateTime.Now.ToString("HH:mm:ss");

            GUI.Label(
                new Rect(
                    Screen.width - 260,
                    10,
                    250,
                    50),
                time,
                _style);
        }
    }
}
