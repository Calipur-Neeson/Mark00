using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteController : MonoBehaviour
{
    public Volume volume;
    private Vignette vignette;
    public static VignetteController instance;

    void Start()
    {
        // Ensure the Volume has a Vignette override
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            // Initial vignette intensity setup (optional)
            vignette.intensity.value = 0.3f;
        }
    }

    public void SetVignetteIntensity(float intensity)
    {
        if (vignette != null)
        {
            vignette.intensity.value = intensity; // Clamp intensity between 0 and 1
        }
    }
}
