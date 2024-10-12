using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartSpikes : MonoBehaviour
{
    public SpikeMove spike1;
    public SpikeMove spike2;
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (spike1 == null || spike2==null)
            {
                spike1 = GetComponent<SpikeMove>();
                spike2 = GetComponent<SpikeMove>();
            }
            if (spike1 != null & spike2!= null)
            {
                spike1.autoStart = true;
                spike2.autoStart = true;
                float delatTime = spike2.moveTime/2;
                spike1.Run();
                Invoke("RunSpike2", delatTime);
            }
        }    
    }
    private void RunSpike2()
    {
        spike2.Run();
    }
}
