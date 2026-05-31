using System;
using System.Reflection;
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
            MelonLogger.Msg("=== ACHIEVEMENTS HUNTER ===");
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

                    MelonLogger.Msg("");
                    MelonLogger.Msg("=================================");
                    MelonLogger.Msg($"FOUND TEXTPRO OBJECT");
                    MelonLogger.Msg("=================================");

                    Component[] components =
                        obj.GetComponents<Component>();

                    foreach (Component component in components)
                    {
                        if (component == null)
                            continue;

                        try
                        {
                            Type type = component.GetType();

                            MelonLogger.Msg("");
                            MelonLogger.Msg(
                                $"CHECKING COMPONENT: {type}");

                            PropertyInfo[] properties =
                                type.GetProperties(
                                    BindingFlags.Public |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Instance);

                            foreach (PropertyInfo property in properties)
                            {
                                try
                                {
                                    if (!property.CanRead)
                                        continue;

                                    object value =
                                        property.GetValue(component);

                                    string valueText =
                                        value?.ToString() ?? "null";

                                    if (valueText.Contains(
                                        "ACHIEVEMENTS",
                                        StringComparison.OrdinalIgnoreCase))
                                    {
                                        MelonLogger.Msg("");
                                        MelonLogger.Msg(
                                            "=== ACHIEVEMENTS COMPONENT FOUND ===");

                                        MelonLogger.Msg(
                                            $"PROPERTY: {property.Name}");

                                        MelonLogger.Msg(
                                            $"VALUE: {valueText}");

                                        foreach (PropertyInfo p in properties)
                                        {
                                            try
                                            {
                                                if (!p.CanRead)
                                                    continue;

                                                object v =
                                                    p.GetValue(component);

                                                string name =
                                                    p.Name.ToLowerInvariant();

                                                if (name.Contains("font") ||
                                                    name.Contains("asset") ||
                                                    name.Contains("material") ||
                                                    name.Contains("text"))
                                                {
                                                    MelonLogger.Msg(
                                                        $"MATCHING PROPERTY: {p.Name} = {v}");
                                                }
                                            }
                                            catch
                                            {
                                            }
                                        }

                                        _scanned = true;
                                        return;
                                    }
                                }
                                catch
                                {
                                }
                            }

                            FieldInfo[] fields =
                                type.GetFields(
                                    BindingFlags.Public |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Instance);

                            foreach (FieldInfo field in fields)
                            {
                                try
                                {
                                    object value =
                                        field.GetValue(component);

                                    string valueText =
                                        value?.ToString() ?? "null";

                                    if (valueText.Contains(
                                        "ACHIEVEMENTS",
                                        StringComparison.OrdinalIgnoreCase))
                                    {
                                        MelonLogger.Msg("");
                                        MelonLogger.Msg(
                                            "=== ACHIEVEMENTS FIELD FOUND ===");

                                        MelonLogger.Msg(
                                            $"FIELD: {field.Name}");

                                        MelonLogger.Msg(
                                            $"VALUE: {valueText}");

                                        foreach (FieldInfo f in fields)
                                        {
                                            try
                                            {
                                                string name =
                                                    f.Name.ToLowerInvariant();

                                                if (name.Contains("font") ||
                                                    name.Contains("asset") ||
                                                    name.Contains("material") ||
                                                    name.Contains("text"))
                                                {
                                                    object v =
                                                        f.GetValue(component);

                                                    MelonLogger.Msg(
                                                        $"MATCHING FIELD: {f.Name} = {v}");
                                                }
                                            }
                                            catch
                                            {
                                            }
                                        }

                                        _scanned = true;
                                        return;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MelonLogger.Error(
                                $"COMPONENT ERROR: {ex}");
                        }
                    }
                }

                MelonLogger.Msg(
                    "ACHIEVEMENTS COMPONENT NOT FOUND YET");
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
