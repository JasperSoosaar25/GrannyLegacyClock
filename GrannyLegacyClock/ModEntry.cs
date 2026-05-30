using System;
using MelonLoader;
using UnityEngine;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private GUIStyle _style;
        private float _nextScan;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("=== COMPONENT SCANNER BUILD ===");
            MelonLogger.Msg("Granny Legacy Clock loaded.");
        }

        public override void OnUpdate()
        {
            try
            {
                if (Time.time > _nextScan)
                {
                    _nextScan = Time.time + 10f;

                    MelonLogger.Msg("=== SCANNING SCENE ===");

                    GameObject[] objects =
                        UnityEngine.Object.FindObjectsOfType<GameObject>(true);

                    foreach (GameObject obj in objects)
                    {
                        if (obj == null)
                            continue;

                        Component[] components =
                            obj.GetComponents<Component>();

                        foreach (Component component in components)
                        {
                            if (component == null)
                                continue;

                            string typeName =
                                component.GetType().FullName;

                            string lower =
                                typeName.ToLowerInvariant();

                            if (lower.Contains("text") ||
                                lower.Contains("font") ||
                                lower.Contains("tmp"))
                            {
                                MelonLogger.Msg(
                                    $"OBJECT: {obj.name}");

                                MelonLogger.Msg(
                                    $"COMPONENT: {typeName}");
                            }
                        }
                    }

                    MelonLogger.Msg("=== SCAN COMPLETE ===");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(
                    $"SCAN ERROR: {ex}");
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
