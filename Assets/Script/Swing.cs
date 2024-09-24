using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    private bool turnRight;
    private bool turnLeft;
    private float turnDegree = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.localRotation.z == 0)
        {
            turnRight = true;
            turnLeft = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Right();
        Left();
    }

    private void Right()
    {
        if (turnRight == true)
        {
            if (turnDegree < 60.0f)
            {
                turnDegree += Time.deltaTime * 60;
                this.transform.localEulerAngles = new Vector3(turnDegree, 0, 0);
            }
            else
            {
                turnRight = false;
                turnLeft = true;
            }
        }
    }
    private void Left()
    {
        if (turnLeft == true)
        {
            if (turnDegree > -60.0f)
            {
                turnDegree -= Time.deltaTime * 60;
                this.transform.localEulerAngles = new Vector3(turnDegree, 0, 0);
            }
            else
            {
                turnRight = true;
                turnLeft = false;
            }
        }
    }


}
