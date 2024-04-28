using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    private Player rewiredPlayer;

    void Start()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if (rewiredPlayer.GetButtonDown("Inventory") && menuActivated)
        {
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (rewiredPlayer.GetButtonDown("Inventory") && !menuActivated)
        {
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(itemName, quantity, itemSprite);
                return;
            }
        }
    }
    public bool HasFuel()
    {
        foreach (ItemSlot slot in itemSlot)
        {
            if (slot.itemName == "fuel" && slot.quantity > 0)
            {
                return true; // Le joueur a au moins un fuel dans son inventaire
            }
        }
        return false; // Le joueur n'a pas de fuel dans son inventaire
    }

}
