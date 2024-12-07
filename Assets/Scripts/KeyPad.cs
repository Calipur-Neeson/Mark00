using Gamekit3D.GameCommands;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    public TMP_Text displayText; 
    private string input = "";
    public GameObject keypadcanvas;
    public KeyCode exit = KeyCode.E;
    public GameObject door;
    public AudioClip buttonAudio;
    //public AudioClip correctAudio;
    public AudioClip wrongAudio;
    private AudioSource audioSource;
    public AudioSource opendoor;
    void UpdateDisplay()
    {
        displayText.text = input;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(exit))
        {
            keypadcanvas.SetActive(false);
            Time.timeScale = 1.0f;
            Cursor.visible = false;
        }
    }

    public void ButtonClick0(string number)
    {
        if (input.Length < 4)
        {
            number = "0";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick1(string number)
    {
        if (input.Length < 4)
        {
            number = "1";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick2(string number)
    {
        if (input.Length < 4)
        {
            number = "2";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick3(string number)
    {
        if (input.Length < 4)
        {
            number = "3";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick4(string number)
    {
        if (input.Length < 4)
        {
            number = "4";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick5(string number)
    {
        if (input.Length < 4)
        {
            number = "5";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick6(string number)
    {
        if (input.Length < 4)
        {
            number = "6";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick7(string number)
    {
        if (input.Length < 4)
        {
            number = "7";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick8(string number)
    {
        if (input.Length < 4)
        {
            number = "8";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    public void ButtonClick9(string number)
    {
        if (input.Length < 4)
        {
            number = "9";
            input += number;
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }
    
    public void OnBackspaceButtonClick()
    {
        if (input.Length > 0)
        {
            input = input.Substring(0, input.Length - 1);
            audioSource.clip = buttonAudio;
            audioSource.Play();
            UpdateDisplay();
        }
    }

    
    public void OnConfirmButtonClick()
    {
        if (input.Length == 4)
        {
            if (input == "1810")
            {
                
                opendoor.Play();
                keypadcanvas.SetActive(false);
                door.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.visible = false;
            }
            else
            {
                audioSource.clip = wrongAudio;
                audioSource.Play();
                input = "";
                UpdateDisplay();
            }
        }
        else
        {
            audioSource.clip = wrongAudio;
            audioSource.Play();
            input = "";
            UpdateDisplay();
        }
    }
}
