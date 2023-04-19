using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOpener : MonoBehaviour
{

    public GameObject inventory;
    
    private Text display;

    public void openInventory() {
    
        bool isActive = inventory.activeSelf;
        if (inventory != null) {
            inventory.SetActive(!isActive);
        }
        
//        display.text = "You better work or I'm going to scream";
    
    }

}
