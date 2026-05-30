using System;
using MelonLoader;
using UnityEngine;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private GUIStyle _style;
        private bool _scanned;

        private static readonly string[] TargetNames =
        {
            "TextPro",
            "ProT",
            "Text",
            "DiffT",
            "CountCoins",
            "BuyText"
        };

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("=== TARGET TEXT SCANNER ===");
        }

        public override void OnUpdate()
        {
            if (_scanned)
                return;

            try
            {
                _scanned = true;

                GameObject[] objects =
                    UnityEngine.Object.FindObjectsOfType<GameObject>(true);

                foreach (GameObject obj in objects)
                {
                    if (obj == null)
                        continue;

                    bool match = false;

                    foreach (string target in TargetNames)
                    {
                        if (obj.name.Equals(
                            target,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            match = true;
                            break;
                        }
                    }

                    if (!match)
                        continue;

                    MelonLogger.Msg("");
                    MelonLogger.Msg("=================================");
                    MelonLogger.Msg($"OBJECT: {obj.name}");
                    MelonLogger.Msg($"ACTIVE: {obj.activeInHierarchy}");
                    MelonLogger.Msg($"PATH: {GetPath(obj.transform)}");

                    Component[] components =
                        obj.GetComponents<Component>();

                    foreach (Component component in components)
                    {
                        if (component == null)
                            continue;

                        try
                        {
                            MelonLogger.Msg(
                                $"COMPONENT: {component.GetType().FullName}");
                        }
                        catch
                        {
                            MelonLogger.Msg(
                                "COMPONENT: <failed to read type>");
                        }
                    }

                    MelonLogger.Msg("=================================");
                    MelonLogger.Msg("");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.ToString());
            }
        }

        private static string GetPath(Transform transform)
        {
            string path = transform.name;

            while (transform.parent != null)
            {
                transform = transform.parent;
                path = transform.name + "/" + path;
            }

            return path;
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

            GUI.Label(
                new Rect(
                    Screen.width - 260,
                    10,
                    250,
                    50),
                DateTime.Now.ToString("HH:mm:ss"),
                _style);
        }
    }
}
