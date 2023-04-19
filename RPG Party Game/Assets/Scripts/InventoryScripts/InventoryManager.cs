using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;
    public List<Item> items = new List<Item>();
    
    public Transform ItemContent;
    public GameObject InventoryItem;
    
    private void awake() {
        instance = this;
    }
    
    public void add(Item item) {
        items.Add(item);
    }
    
    public void remove(Item item) {
        items.Remove(item);
    }
    
    public void ListItems() {
        foreach(var item in items) {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            //var itemName = obj.transform.Find("Item/itemName").GetComponent<Text>();
            //var itemIcon = obj.transform.Find("Item/itemIcon").GetComponent<Image>();
            
            //itemName.text = item.itemName;
            //itemIcon.sprite = item.icon;
        }
    }

}
