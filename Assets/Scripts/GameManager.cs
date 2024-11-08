using System.Collections;
using System.Collections.Generic;
using Microlight.MicroBar;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    public static GameManager instance;
    public MicroBar microbar;

    public float playerMaxHealth = 100f;
    [HideInInspector] public float playerCurrentHealth;

    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        microbar.Initialize(playerMaxHealth);
    }

    public void GameOver()
    {
        print("Game Over");
        SceneManager.LoadScene(0);
    }

    public void DamagePlayer()
    {

        float damageAmount = Random.Range(5f, 15f);
        GameManager.instance.playerCurrentHealth -= damageAmount;
        if (GameManager.instance.playerCurrentHealth < 0f)
        {
            GameOver();
            GameManager.instance.playerCurrentHealth = 0f;
        }
        //hpLeft -= damageAmount;
        //if (hpLeft < 0f) hpLeft = 0f;
        //soundSource.clip = hurtSound;
        //if (soundOn) soundSource.Play();

        //// Update HealthBar
        //if (leftMicroBar != null) leftMicroBar.UpdateBar(hpLeft, false, UpdateAnim.Damage);
        //leftAnimator.SetTrigger("Damage");
        microbar?.UpdateBar(GameManager.instance.playerCurrentHealth, false, UpdateAnim.Damage);
    }
    public void FireDamagePlayer()
    {

        float damageAmount = Random.Range(20f, 30f);
        GameManager.instance.playerCurrentHealth -= damageAmount;
        if (GameManager.instance.playerCurrentHealth < 0f)
        {
            GameOver();
            GameManager.instance.playerCurrentHealth = 0f;
        }
        microbar?.UpdateBar(GameManager.instance.playerCurrentHealth, false, UpdateAnim.Damage);
    }

    public void LaserDamagePlayer()
    {

        float damageAmount = 100f;
        GameManager.instance.playerCurrentHealth -= damageAmount;
        if (GameManager.instance.playerCurrentHealth < 0f)
        {
            GameOver();
            GameManager.instance.playerCurrentHealth = 0f;
        }
        microbar?.UpdateBar(GameManager.instance.playerCurrentHealth, false, UpdateAnim.Damage);
    }
}
