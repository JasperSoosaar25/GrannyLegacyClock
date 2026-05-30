using System;
using MelonLoader;
using UnityEngine;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private GUIStyle _style;
        private bool _logged;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("=== FULL OBJECT SCANNER ===");
        }

        public override void OnUpdate()
        {
            if (_logged)
                return;

            try
            {
                _logged = true;

                GameObject[] objects =
                    UnityEngine.Object.FindObjectsOfType<GameObject>(true);

                MelonLogger.Msg(
                    $"FOUND {objects.Length} GAMEOBJECTS");

                int count = 0;

                foreach (GameObject obj in objects)
                {
                    MelonLogger.Msg(
                        $"OBJECT: {obj.name}");

                    count++;

                    if (count >= 200)
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
