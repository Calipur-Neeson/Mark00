using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class RayHitBox : MonoBehaviour
{
     
    [SerializeField] private GameObject thistrigger;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject boxprefab;
    [SerializeField] private ParticleSystem vfx;
    private Vector3 positionborn;
    public AudioClip explore;
    private AudioSource audioSource;

    public AudioMixerSnapshot snapshot1;
    private void Start()
    {
    
        Transform parent = transform.parent;
        positionborn = parent.position;
        audioSource=GetComponent<AudioSource>();
        audioSource.clip = explore;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            float delatTime = Time.deltaTime;
            Invoke(nameof(Destroy), 3f);          
        }
        
    }
    private void Destroy()
    {
        cube.SetActive(false);
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        vfx.gameObject.SetActive(true);
        Vector3 respawnposition = new Vector3(positionborn.x, positionborn.y +3, positionborn.z);
        
        //respawn(cube, respawnposition);
        StartCoroutine(Respawn(cube,thistrigger, respawnposition));
    }



    private IEnumerator Respawn(GameObject ob, GameObject tr, Vector3 pos)
    {
        yield return new WaitForSeconds(1f);
        ob.SetActive(true);
        ob.transform.position = pos;
        tr.transform.position = pos;
        

        //Instantiate(ob, pos, Quaternion.identity);
        
    }
}
