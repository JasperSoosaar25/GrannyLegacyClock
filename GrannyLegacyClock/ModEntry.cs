using System;
using MelonLoader;
using UnityEngine;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private GUIStyle _style;
        private bool _scanned;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("=== TEXTPRO INSPECTOR ===");
        }

        public override void OnUpdate()
        {
            if (_scanned)
                return;

            try
            {
                GameObject[] objects =
                    UnityEngine.Object.FindObjectsOfType<GameObject>(true);

                foreach (GameObject obj in objects)
                {
                    if (obj == null)
                        continue;

                    if (obj.name != "TextPro")
                        continue;

                    _scanned = true;

                    MelonLogger.Msg("");
                    MelonLogger.Msg("=================================");
                    MelonLogger.Msg("TEXTPRO FOUND");
                    MelonLogger.Msg($"NAME: {obj.name}");
                    MelonLogger.Msg($"ACTIVE: {obj.activeInHierarchy}");

                    Transform transform = obj.transform;

                    MelonLogger.Msg($"CHILD COUNT: {transform.childCount}");

                    if (transform.parent != null)
                        MelonLogger.Msg($"PARENT: {transform.parent.name}");

                    for (int i = 0; i < transform.childCount; i++)
                    {
                        try
                        {
                            MelonLogger.Msg(
                                $"CHILD: {transform.GetChild(i).name}");
                        }
                        catch { }
                    }

                    Component[] components =
                        obj.GetComponents<Component>();

                    MelonLogger.Msg(
                        $"COMPONENT COUNT: {components.Length}");

                    for (int i = 0; i < components.Length; i++)
                    {
                        Component component = components[i];

                        if (component == null)
                            continue;

                        try
                        {
                            MelonLogger.Msg(
                                $"COMPONENT[{i}] TYPE: {component.GetType()}");

                            MelonLogger.Msg(
                                $"COMPONENT[{i}] NAME: {component.name}");
                        }
                        catch (Exception ex)
                        {
                            MelonLogger.Msg(
                                $"COMPONENT[{i}] ERROR: {ex.Message}");
                        }
                    }

                    MelonLogger.Msg("=================================");
                    break;
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.ToString());
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
