using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecurityCameraHazard : MonoBehaviour
{
    [Header("Health Properties")]
    [Range(0, 100)]
    public int currentHealth = 100;
    [Range(1, 100)]
    public int MaxHealth = 100;

    public bool GetCaught;
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
        if (GetCaught == true)
        {
            if (stopDealDamage == false)
            {
                stopDealDamage = true;
                StartCoroutine(SecurityCamHazard());
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void Reset()
    {
        healthBarSlider.value = MaxHealth;
        currentHealth = MaxHealth;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Security"))
        {
            GetCaught = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Security")
        {
            GetCaught = false;
        }
    }

    IEnumerator SecurityCamHazard()
    {
        yield return new WaitForSeconds(1);
        healthBarSlider.value -= 25;
        currentHealth -= 25;
        stopDealDamage = false;
        if (currentHealth < 0)
        {
            healthBarSlider.value = 0;
            currentHealth = 0;
        }
    }
}

