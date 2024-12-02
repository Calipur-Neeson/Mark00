using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour
{
    public TextMeshProUGUI inputField;
    private string Password;
    public void Output()
    {
        
        Debug.Log(Password);
    }
    public void Enter1()
    {
        Password += "1";
        Output();
    }
    // Start is called before the first frame update
    void Start()
    {
        Output();
    }

    
}
