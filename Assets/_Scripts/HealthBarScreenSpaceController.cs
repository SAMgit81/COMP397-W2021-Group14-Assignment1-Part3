using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScreenSpaceController : MonoBehaviour
{
    [Header("Health Properties")]
    [Range(0, 100)]
    public int currentHealth = 100;
    [Range(1, 100)]
    public int MaxHealth = 100;

    public bool beInFire;
    public bool stopDealDamage;

    public Slider healthBarSlider;

    // Start is called before the first frame update
    void Start()
    {
      //healthBarSlider = GetComponent<Slider>();
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (beInFire == true)
        {
            if (stopDealDamage == false)
            {
                stopDealDamage = true;
                StartCoroutine(DamageFromFire());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        if(currentHealth == 0)
        {
            FindObjectOfType<GamaManage>().EndGame();
        }
    }

    public void TakeDamage(int damage)
    {
        healthBarSlider.value -= damage;
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            healthBarSlider.value = 0;
            currentHealth = 0;
        }
    }
    public void SetHealth(int healthValue)
    {
        healthBarSlider.value = healthValue;
        currentHealth = healthValue;
    }
    public void Reset()
    {
        healthBarSlider.value = MaxHealth;
        currentHealth = MaxHealth;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fire"))
        {
            beInFire = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fire")
        {
            beInFire = false;
        }
    }

    IEnumerator DamageFromFire()
    {
        yield return new WaitForSeconds(1);
       healthBarSlider.value -= 10;
        currentHealth -= 10;
        stopDealDamage = false;
        if (currentHealth < 0)
        {
            healthBarSlider.value = 0;
            currentHealth = 0;
        }
    }
}




