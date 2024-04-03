using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    private InventoryManager inventoryManager;
    void Start()
    {
        inventoryManager = GameObject.Find("inventory").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bouh");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("bouh");
            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(gameObject);
        }

    }

}


