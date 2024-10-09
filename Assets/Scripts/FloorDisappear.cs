using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDisappear : MonoBehaviour
{
    [SerializeField]
    private GameObject objectpick;

    [SerializeField] private ParticleSystem vfx;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            Destroy(objectpick);
            vfx.gameObject.SetActive(true);
        }
    }
}
