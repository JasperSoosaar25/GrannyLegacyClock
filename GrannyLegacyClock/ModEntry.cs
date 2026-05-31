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
            MelonLogger.Msg("=== TEXTPRO REFLECTION DUMPER ===");
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

                    Component[] components =
                        obj.GetComponents<Component>();

                    MelonLogger.Msg(
                        $"COMPONENT COUNT: {components.Length}");

                    for (int i = 0; i < components.Length; i++)
                    {
                        Component component = components[i];

                        if (component == null)
                            continue;

                        MelonLogger.Msg("");
                        MelonLogger.Msg($"----- COMPONENT {i} -----");

                        try
                        {
                            Type type = component.GetType();

                            MelonLogger.Msg(
                                $"TYPE: {type}");

                            MelonLogger.Msg(
                                $"ASSEMBLY: {type.Assembly.FullName}");

                            FieldInfo[] fields =
                                type.GetFields(
                                    BindingFlags.Public |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Instance);

                            MelonLogger.Msg(
                                $"FIELDS: {fields.Length}");

                            foreach (FieldInfo field in fields)
                            {
                                try
                                {
                                    object value =
                                        field.GetValue(component);

                                    MelonLogger.Msg(
                                        $"FIELD: {field.Name} = {value}");
                                }
                                catch
                                {
                                    MelonLogger.Msg(
                                        $"FIELD: {field.Name}");
                                }
                            }

                            PropertyInfo[] properties =
                                type.GetProperties(
                                    BindingFlags.Public |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Instance);

                            MelonLogger.Msg(
                                $"PROPERTIES: {properties.Length}");

                            foreach (PropertyInfo property in properties)
                            {
                                try
                                {
                                    if (!property.CanRead)
                                        continue;

                                    object value =
                                        property.GetValue(component);

                                    MelonLogger.Msg(
                                        $"PROPERTY: {property.Name} = {value}");
                                }
                                catch
                                {
                                    MelonLogger.Msg(
                                        $"PROPERTY: {property.Name}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MelonLogger.Error(
                                $"COMPONENT ERROR: {ex}");
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
