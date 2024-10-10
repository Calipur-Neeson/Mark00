using Microlight.MicroBar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(RepeatCollisionCheck());
                  
            }
        
        
    }
    private IEnumerator RepeatCollisionCheck()
    {
        while (true)
        {
            GameManager.instance.FireDamagePlayer();
            
              
            yield return new WaitForSeconds(0.5f);
        }
    }

   

}
