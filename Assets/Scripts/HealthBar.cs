using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Damageable playerDamagableComponent;
    public TMP_Text healthBarText;
    public Slider healthSlider;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamagableComponent = player.GetComponent<Damageable>();

        if (player == null)
        {
            Debug.Log("Player not found, make sure it has player tag");
        }else{
            Debug.Log("Player found");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        healthSlider.value = CaluclateSliderPercentage(playerDamagableComponent.health, playerDamagableComponent.maxHealth);
        healthBarText.text = "HP" + playerDamagableComponent.health + "/" + playerDamagableComponent.maxHealth;
    }

    private float CaluclateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
    public void OnEnable()
    {
        playerDamagableComponent.healthChanged.AddListener(onHealthChanged);
    }

   
    // Update is called once per frame
    void Update()
    {

    }

    private void onHealthChanged(int newHealth, int maxHealth)
    {
        Debug.Log("Health changed: " + newHealth + " / " + maxHealth);
        healthSlider.value = CaluclateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP" + newHealth + "/" + maxHealth;
    }



}
