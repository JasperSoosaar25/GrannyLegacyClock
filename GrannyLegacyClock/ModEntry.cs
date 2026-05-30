using System;
using System.Linq;
using System.Reflection;
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

            var resources = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceNames();

            MelonLogger.Msg("Embedded resources:");

            foreach (var resource in resources)
                MelonLogger.Msg($" - {resource}");
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
                    Font corsiva =
                        Font.CreateDynamicFontFromOSFont(
                            "Monotype Corsiva",
                            30);

                    if (corsiva != null)
                    {
                        _style.font = corsiva;
                        MelonLogger.Msg(
                            "Monotype Corsiva found on system.");
                    }
                    else
                    {
                        MelonLogger.Warning(
                            "Monotype Corsiva not found on system.");
                    }
                }
                catch (Exception ex)
                {
                    MelonLogger.Error(
                        $"Font error: {ex}");
                }
            }

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
