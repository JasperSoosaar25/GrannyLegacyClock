using System;
using MelonLoader;
using UnityEngine;
using Il2CppTMPro;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private TMP_FontAsset? _clockFont;
        private GameObject? _clockObject;
        private TextMeshProUGUI? _clockText;
        private int _lastSecond = -1;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Granny Legacy Clock loaded.");
        }

        public override void OnUpdate()
        {
            try
            {
                if (_clockObject != null &&
                    _clockObject.Equals(null))
                {
                    _clockObject = null;
                    _clockText = null;
                }

                if (_clockObject == null)
                    CreateClock();

                if (_clockText == null)
                    return;

                int second = DateTime.Now.Second;

                if (second != _lastSecond)
                {
                    _lastSecond = second;

                    _clockText.text =
                        DateTime.Now.ToString("HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.ToString());
            }
        }

        private void CreateClock()
        {
            try
            {
                GameObject existing =
                    GameObject.Find("GrannyLegacyClock");

                if (existing != null)
                {
                    _clockObject = existing;
                    _clockText =
                        existing.GetComponent<TextMeshProUGUI>();

                    return;
                }

                if (_clockFont == null)
                {
                    TMP_FontAsset[] fonts =
                        Resources.FindObjectsOfTypeAll<TMP_FontAsset>();

                    foreach (TMP_FontAsset font in fonts)
                    {
                        if (font == null)
                            continue;

                        if (font.name == "MTCORSVA SDF")
                        {
                            _clockFont = font;
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

                UnityEngine.Object.DontDestroyOnLoad(
                    _clockObject);

                _clockObject.transform.SetParent(
                    canvas.transform,
                    false);

                _clockText =
                    _clockObject.AddComponent<TextMeshProUGUI>();

                _clockText.text =
                    DateTime.Now.ToString("HH:mm:ss");

                _clockText.fontSize = 67;

                Color clockColor = Color.white;
                clockColor.a = 0.67f;

                _clockText.color = clockColor;

                _clockText.alignment =
                    TextAlignmentOptions.TopRight;

                if (_clockFont != null)
                    _clockText.font = _clockFont;

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
                    new Vector2(500f, 100f);
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.ToString());
            }
        }
    }
}
