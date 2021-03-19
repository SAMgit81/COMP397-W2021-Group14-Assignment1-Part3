using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool InventoryPresent = false;
    public GameObject inventoryUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryPresent == true)
            {

                Close();

            }
            else
            {
                Open();
            }
        }
    }

    public void Close()
    {
        inventoryUI.SetActive(false);
        InventoryPresent = false;
    }
    void Open()
    {
        inventoryUI.SetActive(true);
        InventoryPresent = true;
    }

    public void OnInventoryButtonPressed()
    {
        if (InventoryPresent == true)
        {

            Close();

        }
        else
        {
            Open();
        }
    }
}
