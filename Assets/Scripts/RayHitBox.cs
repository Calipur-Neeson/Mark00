using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHitBox : MonoBehaviour
{

    [SerializeField] private GameObject cube;
    [SerializeField] private ParticleSystem vfx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            float delatTime = Time.deltaTime;
            Invoke("destory", 3f);
            
        }
        
    }
    private void destory()
    {
        Destroy(cube);
        vfx.gameObject.SetActive(true);
    }
    
}
