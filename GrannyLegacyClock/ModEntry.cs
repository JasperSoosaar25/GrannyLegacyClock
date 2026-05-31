using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using Il2CppTMPro;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private TMP_FontAsset _clockFont;
        private GameObject _canvasObject;
        private GameObject _clockObject;
        private TextMeshProUGUI _clockText;
        private int _lastSecond = -1;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Granny Legacy Clock loaded.");
        }

        public override void OnUpdate()
        {
            try
            {
                if (_canvasObject == null ||
                    _clockObject == null ||
                    _clockText == null)
                {
                    CreateClock();
                }

                if (_canvasObject != null)
                {
                    _canvasObject.transform.localScale =
                        Vector3.one;
                }

                if (_clockObject != null)
                {
                    _clockObject.transform.localScale =
                        Vector3.one;
                }

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
                GameObject existingCanvas =
                    GameObject.Find("GrannyLegacyClockCanvas");

                if (existingCanvas != null)
                {
                    _canvasObject = existingCanvas;

                    GameObject existingClock =
                        GameObject.Find("GrannyLegacyClockText");

                    if (existingClock != null)
                    {
                        _clockObject = existingClock;
                        _clockText =
                            existingClock.GetComponent<TextMeshProUGUI>();
                    }

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

                _canvasObject =
                    new GameObject("GrannyLegacyClockCanvas");

                UnityEngine.Object.DontDestroyOnLoad(
                    _canvasObject);

                Canvas canvas =
                    _canvasObject.AddComponent<Canvas>();

                canvas.renderMode =
                    RenderMode.ScreenSpaceOverlay;

                _canvasObject.AddComponent<CanvasScaler>();
                _canvasObject.AddComponent<GraphicRaycaster>();

                _canvasObject.transform.localScale =
                    Vector3.one;

                _clockObject =
                    new GameObject("GrannyLegacyClockText");

                _clockObject.transform.SetParent(
                    _canvasObject.transform,
                    false);

                _clockObject.transform.localScale =
                    Vector3.one;

                _clockText =
                    _clockObject.AddComponent<TextMeshProUGUI>();

                _clockText.text =
                    DateTime.Now.ToString("HH:mm:ss");

                _clockText.fontSize = 50;

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
                    new Vector2(400f, 80f);
            }
            catch (Exception ex)
            {
                MelonLogger.Error(ex.ToString());
            }
        }
    }
}
