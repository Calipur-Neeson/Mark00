using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace TextInspectSystem
{
    public class TextInspectUIManager : MonoBehaviour
    {
        [Header("Object Name UI")]
        [SerializeField] private TMP_Text objectNameText;
        [SerializeField] private GameObject objectNameBG;

        [Header("Object Name Customisation")]
        [SerializeField] private int nameTextSize = 20;
        [SerializeField] private TMP_FontAsset nameFontType;
        [SerializeField] private FontStyles nameFontStyle;
        [SerializeField] private Color nameFontColor;

        [Header("Object Details Settings")]
        [SerializeField] private TMP_Text objectDetailsText;
        [SerializeField] private GameObject objectDetailsBG;

        [Header("Object Details Customisation")]
        [SerializeField] private int detailsTextSize = 30;
        [SerializeField] private TMP_FontAsset detailsFontType;
        [SerializeField] private FontStyles detailsFontStyle;
        [SerializeField] private Color detailsFontColor;

        [Header("Timer")]
        [SerializeField] private float onScreenTimer = 5f;
        [SerializeField] private float fadeInDuration = 1f;
        [SerializeField] private float fadeOutDuration = 1f;

        [Header("Crosshair")]
        [SerializeField] private Image crosshair;

        [Header("Should persist?")]
        [SerializeField] private bool persistAcrossScenes = true;

        private CanvasGroup objectDetailsCanvasGroup;
        private bool startTimer;
        private float timer;

        public static TextInspectUIManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                if (persistAcrossScenes)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }

            objectDetailsCanvasGroup = objectDetailsBG.GetComponent<CanvasGroup>();

            objectNameBG.SetActive(false);
            objectDetailsCanvasGroup.alpha = 0;

            SetTextSettings();
        }

        void SetTextSettings()
        {
            objectNameText.fontSize = nameTextSize;
            objectNameText.font = nameFontType;
            objectNameText.fontStyle = nameFontStyle;
            objectNameText.color = nameFontColor;

            objectDetailsText.fontSize = detailsTextSize;
            objectDetailsText.font = detailsFontType;
            objectDetailsText.fontStyle = detailsFontStyle;
            objectDetailsText.color = detailsFontColor;
        }

        private void Update()
        {
            if (startTimer)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = 0;
                    ClearObjectDetails();
                    startTimer = false;
                }
            }
        }

        public IEnumerator FadeUI(bool fadeIn, float duration)
        {
            float startAlpha = fadeIn ? 0f : 1f;
            float endAlpha = 1f - startAlpha;
            float elapsedTime = 0f;

            objectDetailsCanvasGroup.alpha = startAlpha;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / duration;
                objectDetailsCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                yield return null;
            }
            objectDetailsCanvasGroup.alpha = endAlpha;
        }

        public void ShowName(string objectName, bool show)
        {
            if (show)
            {
                objectNameBG.SetActive(true);
                objectNameText.text = objectName;
            }
            else
            {
                objectNameBG.SetActive(false);
                objectNameText.text = "";
            }
        }

        public void ShowObjectDetails(string newInfo)
        {
            objectDetailsText.text = newInfo;
            StartCoroutine(FadeUI(true, fadeInDuration));
            timer = onScreenTimer;
            startTimer = true;
        }

        void ClearObjectDetails()
        {
            StartCoroutine(FadeUI(false, fadeOutDuration));
        }

        public void HighlightCrosshair(bool on)
        {
            crosshair.color = on ? Color.red : Color.white;
        }
    }
}
