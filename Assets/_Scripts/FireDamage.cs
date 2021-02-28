using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireDamage : MonoBehaviour
{
    public bool beInFire;
    public bool stopDealDamage;

    private Slider healthBarSlider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (beInFire == true)
        {
            if (stopDealDamage == false)
            {
                stopDealDamage = true;
                StartCoroutine(DamageFromFire(10));
            }
        }
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

    IEnumerator DamageFromFire(int damage)
    {
        yield return new WaitForSeconds(1);
        healthBarSlider.value -= damage;
        stopDealDamage = false;
    }
}


