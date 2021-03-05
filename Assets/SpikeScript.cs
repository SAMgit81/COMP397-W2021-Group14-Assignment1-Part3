using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpikeScript : MonoBehaviour
{
    [Header("Health Properties")]
    [Range(0, 100)]
    public int currentHealth = 100;
    [Range(1, 100)]
    public int MaxHealth = 100;

    public bool beInSpikes;
    public bool stopDealDamage;

    public Slider healthBarSlider;

    // Start is called before the first frame update
    void Start()
    {
        // healthBarSlider = GetComponent<Slider>();
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (beInSpikes == true)
        {
            if (stopDealDamage == false)
            {
                stopDealDamage = true;
                StartCoroutine(DamageFromSpikes());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    /*public void takeDamage(int damage)
    {
        healthBarSlider.value -= damage;
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            healthBarSlider.value = 0;
            currentHealth = 0;
        }
    }*/

    public void Reset()
    {
        healthBarSlider.value = MaxHealth;
        currentHealth = MaxHealth;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spikes"))
        {
            beInSpikes = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Spikes")
        {
            beInSpikes = false;
        }
    }

    IEnumerator DamageFromSpikes()
    {
        yield return new WaitForSeconds(1);
        healthBarSlider.value -= 20;
        currentHealth -= 20;
        stopDealDamage = false;
        if (currentHealth < 0)
        {
            healthBarSlider.value = 0;
            currentHealth = 0;
        }
    }
}