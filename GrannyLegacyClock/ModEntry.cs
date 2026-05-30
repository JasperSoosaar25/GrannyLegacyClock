using System;
using MelonLoader;
using UnityEngine;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private GUIStyle _style;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Granny Legacy Clock loaded.");
        }

        public override void OnGUI()
        {
            if (_style == null)
            {
                _style = new GUIStyle();

                _style.fontSize = 30;
                _style.alignment = TextAnchor.UpperRight;
                _style.normal.textColor = Color.white;

                try
                {
                    Font[] fonts = Resources.FindObjectsOfTypeAll<Font>();

                    MelonLogger.Msg($"Found {fonts.Length} loaded fonts.");

                    foreach (Font font in fonts)
                    {
                        if (font == null)
                            continue;

                        MelonLogger.Msg($"FONT: {font.name}");
                    }

                    try
                    {
                        Font monotype =
                            Resources.GetBuiltinResource<Font>(
                                "Monotype-Corsiva-Regular");

                        if (monotype != null)
                        {
                            _style.font = monotype;

                            MelonLogger.Msg(
                                $"FOUND BUILTIN MONOTYPE: {monotype.name}");
                        }
                        else
                        {
                            MelonLogger.Warning(
                                "BUILTIN MONOTYPE NOT FOUND");
                        }
                    }
                    catch (Exception ex)
                    {
                        MelonLogger.Error(
                            $"Builtin font lookup failed: {ex}");
                    }
                }
                catch (Exception ex)
                {
                    MelonLogger.Error(
                        $"Font search failed: {ex}");
                }
            }

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
