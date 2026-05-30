using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MelonLoader;
using UnityEngine;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private GUIStyle? _style;
        private Font? _font;
        private bool _fontLoaded;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Granny Legacy Clock loaded.");
        }

        private void LoadFont()
        {
            if (_fontLoaded)
                return;

            _fontLoaded = true;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                var resourceName = assembly
                    .GetManifestResourceNames()
                    .First(x => x.EndsWith(".ttf"));

                using var stream =
                    assembly.GetManifestResourceStream(resourceName);

                if (stream == null)
                    return;

                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);

                string tempPath = Path.Combine(
                    Path.GetTempPath(),
                    "GrannyLegacyClock_MonotypeCorsiva.ttf");

                File.WriteAllBytes(tempPath, bytes);

                _font = Font.CreateDynamicFontFromOSFont(
                    "Arial",
                    32);

                try
                {
                    _font = new Font(tempPath);
                }
                catch
                {
                    MelonLogger.Warning(
                        "Custom font load failed, using fallback.");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.ToString());
            }
        }

        public override void OnGUI()
        {
            LoadFont();

            _style ??= new GUIStyle(GUI.skin.label);

            _style.fontSize = 30;
            _style.alignment = TextAnchor.UpperRight;
            _style.normal.textColor = Color.white;

            if (_font != null)
                _style.font = _font;

            string time =
                DateTime.Now.ToString("HH:mm:ss");

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
