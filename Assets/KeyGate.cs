using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.name == "Player" && GameVariables.KeyCount > 0)
        {
            GameVariables.KeyCount--;
            Destroy(gameObject);
        }
    }
}
