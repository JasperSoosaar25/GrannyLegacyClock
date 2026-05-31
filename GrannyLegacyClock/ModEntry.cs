using System;
using MelonLoader;
using UnityEngine;
using Il2CppTMPro;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private bool _scanned;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("TMP Font Scanner Loaded");
        }

        public override void OnUpdate()
        {
            if (_scanned)
                return;

            _scanned = true;

            try
            {
                TMP_FontAsset[] fonts =
                    Resources.FindObjectsOfTypeAll<TMP_FontAsset>();

                MelonLogger.Msg(
                    $"TMP FONT COUNT: {fonts.Length}");

                foreach (TMP_FontAsset font in fonts)
                {
                    if (font == null)
                        continue;

                    MelonLogger.Msg(
                        $"TMP FONT: {font.name}");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(
                    ex.ToString());
            }
        }
    }
}
