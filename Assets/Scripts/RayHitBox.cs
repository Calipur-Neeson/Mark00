using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class RayHitBox : MonoBehaviour
{
    [SerializeField] private GameObject thistrigger;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject boxprefab;
    [SerializeField] private ParticleSystem vfx;
    private Vector3 positionborn;

    private void Start()
    {
    
        Transform parent = transform.parent;
        positionborn = parent.position;
            
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
        Vector3 respawnposition = new Vector3(positionborn.x, positionborn.y +3, positionborn.z);
        
        //respawn(cube, respawnposition);
        StartCoroutine(respawn(cube,thistrigger, respawnposition));
    }



    private IEnumerator respawn(GameObject ob, GameObject tr, Vector3 pos)
    {
        yield return new WaitForSeconds(1f);
        ob.SetActive(true);
        ob.transform.position = pos;
        tr.transform.position = pos;
        

        //Instantiate(ob, pos, Quaternion.identity);
        
    }
}
