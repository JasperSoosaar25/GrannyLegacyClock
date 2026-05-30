using System;
using MelonLoader;
using UnityEngine;
using TMPro;

namespace GrannyLegacyClock
{
    public class ModEntry : MelonMod
    {
        private GameObject _clockObject;
        private TextMeshProUGUI _clockText;
        private float _nextScan;

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("=== TMP BUILD LOADED ===");
            MelonLogger.Msg("Granny Legacy Clock loaded.");
        }

        public override void OnUpdate()
        {
            MelonLogger.Msg("TMP UPDATE RUNNING");

            try
            {
                if (_clockText == null)
                    CreateClock();

                if (_clockText != null)
                    _clockText.text = DateTime.Now.ToString("HH:mm:ss");

                if (Time.time > _nextScan)
                {
                    _nextScan = Time.time + 5f;

                    TMP_FontAsset[] fonts =
                        Resources.FindObjectsOfTypeAll<TMP_FontAsset>();

                    MelonLogger.Msg(
                        $"Found {fonts.Length} TMP fonts.");

                    foreach (TMP_FontAsset font in fonts)
                    {
                        if (font == null)
                            continue;

                        MelonLogger.Msg(
                            $"TMP FONT: {font.name}");

                        string name =
                            font.name.ToLowerInvariant();

                        if (name.Contains("monotype") ||
                            name.Contains("corsiva"))
                        {
                            MelonLogger.Msg(
                                $"MONOTYPE TMP FOUND: {font.name}");

                            if (_clockText != null)
                                _clockText.font = font;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error(
                    $"TMP ERROR: {ex}");
            }
        }

        private void CreateClock()
        {
            try
            {
                MelonLogger.Msg("Attempting to create TMP clock...");

                Canvas canvas =
                    UnityEngine.Object.FindObjectOfType<Canvas>();

                if (canvas == null)
                {
                    MelonLogger.Warning(
                        "No Canvas found.");
                    return;
                }

                MelonLogger.Msg(
                    $"Canvas found: {canvas.name}");

                _clockObject =
                    new GameObject("GrannyLegacyClock");

                _clockObject.transform.SetParent(
                    canvas.transform,
                    false);

                _clockText =
                    _clockObject.AddComponent<TextMeshProUGUI>();

                _clockText.fontSize = 36;
                _clockText.color = Color.white;
                _clockText.alignment = TextAlignmentOptions.TopRight;
                _clockText.text = "00:00:00";

                RectTransform rect =
                    _clockText.rectTransform;

                rect.anchorMin = new Vector2(1f, 1f);
                rect.anchorMax = new Vector2(1f, 1f);
                rect.pivot = new Vector2(1f, 1f);

                rect.anchoredPosition =
                    new Vector2(-20f, -20f);

                rect.sizeDelta =
                    new Vector2(300f, 60f);

                MelonLogger.Msg(
                    "TMP clock created.");
            }
            catch (Exception ex)
            {
                MelonLogger.Error(
                    $"CLOCK CREATE ERROR: {ex}");
            }
        }
    }
}
