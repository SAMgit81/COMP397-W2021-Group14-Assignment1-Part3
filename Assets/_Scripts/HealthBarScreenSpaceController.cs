using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScreenSpaceController : MonoBehaviour
{
    [Header("Health Properties")]
    [Range(0,100)]
    public int currentHealth = 100;
    [Range(1,100)]
    public int MaxHealth = 100;

    private Slider healthBarSlider;
    // Start is called before the first frame update
    void Start()
    {
        healthBarSlider = GetComponent<Slider>();
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            takeDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        
    }

    public void takeDamage(int damage)
    {
        healthBarSlider.value -= damage;
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            healthBarSlider.value = 0;
            currentHealth = 0;
        }
    }

    public void Reset()
    {
        healthBarSlider.value = MaxHealth;
        currentHealth = MaxHealth;
    }
}
