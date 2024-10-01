using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private Transform MovePoint_1, Movepoint_2;
    [SerializeField] 
    private float MoveSpeed = 5f;

    private Transform targetPos;
    private bool firstMovePoint;

    [SerializeField]
    private float rotationSpeed = 200f;
    private Vector3 tempRotation = Vector3.zero;
    private float zAngle;

    private void Awake()
    {
        if (Random.Range(0, 2) > 0)
        {
            firstMovePoint = false;
            targetPos = Movepoint_2;

            rotationSpeed *= -1f;

        }
        else
        {
            firstMovePoint = true;
            targetPos = MovePoint_1;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }
    void HandleMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, MoveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, targetPos.position) < 0.1f)
        {
            if(firstMovePoint) 
            {
                firstMovePoint = false;
                targetPos = Movepoint_2;
            }
            else
            {
                firstMovePoint = true;
                targetPos = MovePoint_1;
            }
        }
    }

    void HandleRotation()
    {
        zAngle = rotationSpeed * Time.deltaTime;
        tempRotation.z = zAngle;
        transform.Rotate(tempRotation);
    }


}
