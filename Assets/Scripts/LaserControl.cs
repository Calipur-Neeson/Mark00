using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserControl : MonoBehaviour
{
    [SerializeField] private Color color = new Color(191 / 255, 36 / 255, 0);
    [SerializeField] private float colorIntensity = 4.3f;
    private float beamColorEnhance = 1;
    [SerializeField] private float maxLength = 100;
    [SerializeField] private float thickness = 3;
    [SerializeField] private float noiseScale = 3.14f;
    [SerializeField] private GameObject startVFX;
    [SerializeField] private GameObject endVFX;
    [SerializeField] private Vector3 startposition;
    [SerializeField] private Vector3 endposition;
    [SerializeField] private GameObject area;

    private LineRenderer lineRenderer;
    private BoxCollider boxcollider;
    //private BoxCollider hitarea;
    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        boxcollider= GetComponent<BoxCollider>();
        //hitarea= area.GetComponent<BoxCollider>();

        lineRenderer.material.color = color * colorIntensity;
        lineRenderer.material.SetFloat("_LaserThickness", thickness);
        lineRenderer.material.SetFloat("_LaserScale", noiseScale);
        lineRenderer.SetPosition(0, startposition);
        lineRenderer.SetPosition(1, endposition);
        startVFX.transform.position = startposition;
        endVFX.transform.position = endposition;
        boxcollider.center = (startposition+endposition)/2;
        boxcollider.size = new Vector3(endposition.x - startposition.x, 0.1f, 0.1f);

        //hitarea.center = (startposition + endposition) / 2;
        //hitarea.size = new Vector3(endposition.x - startposition.x, 0.1f, 0.1f);

        ParticleSystem[] particle=transform.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem p in particle)
        {
            Renderer r = p.GetComponent<Renderer>();
            r.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnhance));

        }
    }
    
    private void Start()
    {
        UpdateEndPosition();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateEndPosition();
    }

    /*private void UpdatePosition(Vector2 starPosition, Vector2 direction)
    {
        direction = direction.normalized;
        transform.position = starPosition;
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ * Mathf.Rad2Deg);

    }
    */
    
    private void UpdateEndPosition()
    {
        Ray ray= new Ray(startposition, endposition-startposition);
        LayerMask lm = ~LayerMask.GetMask("Laser");
        RaycastHit hit;
        bool res=Physics.Raycast(ray, out hit, maxLength, lm);
        if (res)
        {
            endposition = hit.point;
        }
        endVFX.transform.position = endposition;
        lineRenderer.SetPosition(1, endposition);
        boxcollider.center = (startposition + endposition) / 2;
        boxcollider.size = new Vector3(endposition.x - startposition.x, 0.1f, 0.1f);

        
        if (hit.collider.gameObject.CompareTag("Player")) 
        {
            GameManager.instance.LaserDamagePlayer();
        }

        
    }
}
