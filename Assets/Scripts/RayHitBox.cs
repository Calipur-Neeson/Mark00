using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RayHitBox : MonoBehaviour
{

    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject boxprefab;
    [SerializeField] private ParticleSystem vfx;
    private Vector3 position;

    private void Start()
    {
        Transform parent = transform.parent;
        position = parent.position;
        
    }

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
        cube.SetActive(false);
        vfx.gameObject.SetActive(true);
        Vector3 respawnposition = new Vector3(position.x, position.y +3, position.z);
        //respawn(cube, respawnposition);
        StartCoroutine(respawn(cube, respawnposition));
    }



    private IEnumerator respawn(GameObject ob, Vector3 pos)
    {
        yield return new WaitForSeconds(1f);
        ob.transform.position = pos;
        ob.SetActive(true);

        //Instantiate(ob, pos, Quaternion.identity);
        
    }
}
