using System;
using MelonLoader;
using UnityEngine;
using Il2CppTMPro;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private TMP_FontAsset _clockFont;
        private GameObject _clockObject;
        private TextMeshProUGUI _clockText;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Granny Legacy Clock loaded.");
        }

        public override void OnUpdate()
        {
            try
            {
                if (_clockText == null)
                    CreateClock();

                if (_clockText != null)
                    _clockText.text = DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.ToString());
            }
        }

        private void CreateClock()
        {
            if (_clockFont == null)
            {
                TMP_FontAsset[] fonts =
                    Resources.FindObjectsOfTypeAll<TMP_FontAsset>();

                foreach (TMP_FontAsset font in fonts)
                {
                    if (font == null)
                        continue;

                    MelonLogger.Msg($"FONT: {font.name}");

                    if (font.name == "MTCORSVA SDF")
                    {
                        _clockFont = font;

                        MelonLogger.Msg(
                            "Found MTCORSVA SDF font.");

                        break;
                    }
                }
            }

            Canvas canvas =
                UnityEngine.Object.FindObjectOfType<Canvas>();

            if (canvas == null)
                return;

            _clockObject =
                new GameObject("GrannyLegacyClock");

            _clockObject.transform.SetParent(
                canvas.transform,
                false);

            _clockText =
                _clockObject.AddComponent<TextMeshProUGUI>();

            _clockText.text = "00:00:00";
            _clockText.fontSize = 36;
            _clockText.color = Color.white;
            _clockText.alignment =
                TextAlignmentOptions.TopRight;

            if (_clockFont != null)
            {
                _clockText.font = _clockFont;

                MelonLogger.Msg(
                    "Applied MTCORSVA SDF font.");
            }

            RectTransform rect =
                _clockText.rectTransform;

            rect.anchorMin =
                new Vector2(1f, 1f);

            rect.anchorMax =
                new Vector2(1f, 1f);

            rect.pivot =
                new Vector2(1f, 1f);

            rect.anchoredPosition =
                new Vector2(-20f, -20f);

            rect.sizeDelta =
                new Vector2(300f, 60f);

            MelonLogger.Msg(
                "Clock created.");
        }
    }
}
