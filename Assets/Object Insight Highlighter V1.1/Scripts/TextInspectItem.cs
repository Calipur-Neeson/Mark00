using UnityEngine;

namespace TextInspectSystem
{
    public class TextInspectItem : MonoBehaviour
    {
        [Header("Show Information Selection")]
        [SerializeField] private bool showObjectName;
        [SerializeField] private bool showObjectDetails;
        [SerializeField] private bool playDetailsAudio;

        [Header("Contol KeyPad")]
        [SerializeField] private bool showKeyPad;
        [SerializeField] private GameObject keypad;

        [Header("Text Parameters")]
        [SerializeField] private string objectName = "Generic Object";

        [Space(10)] [TextArea] [SerializeField] private string objectDetails = "This is a description, please fill in the inspector";

        [Header("Audio Parameters")]
        [SerializeField] private AudioClip detailsAudioClip;
        private AudioSource audioSource;

        void Awake()
        {
            // Ensure there's an AudioSource component and set it up
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void ShowObjectName(bool showName)
        {
            if(showObjectName)
            {
                TextInspectUIManager.instance.ShowName(objectName, showName);
            }
        }

        public void ShowDetails()
        {
            if (showObjectDetails)
            {
                TextInspectUIManager.instance.ShowObjectDetails(objectDetails);

                // Play audio if enabled
                if (playDetailsAudio && detailsAudioClip != null)
                {
                    audioSource.clip = detailsAudioClip;
                    audioSource.Play();
                }
            }
        }

        public void ShowKeyPad()
        {
            if(showKeyPad)
            {
                keypad.SetActive(true);
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
