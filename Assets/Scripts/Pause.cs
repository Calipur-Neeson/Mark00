using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausemenu;
    public bool ispause = false;
    public KeyCode togglekey = KeyCode.P;
   
    void Update()
    {
        if (Input.GetKeyDown(togglekey))
        {
            ToggleTheTarget();
        }
    }

    void ToggleTheTarget()
    {
        if(!ispause) 
        {
            ispause = true;
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            ispause = false;
            Time.timeScale = 1.0f;
            Cursor.visible = false;
        }
        pausemenu.SetActive(!pausemenu.activeSelf);
    }
}
