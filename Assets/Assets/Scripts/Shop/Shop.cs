using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    //variable for current item selected
    public int currentItemSelected;
    public int currentItemCost;

    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamondAmount);
            }

            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        //0 - flame sword
        //1 - boots of flight
        //2 - key to castle
        Debug.Log("SelectItem()");

        switch (item)
        {
            case 0: //flame sword
                UIManager.Instance.UpdateShopSelection(86);
                currentItemSelected = 0;
                currentItemCost = 200;
                break;
            case 1: //boots
                UIManager.Instance.UpdateShopSelection(-21);
                currentItemSelected = 1;
                currentItemCost = 400;
                break;
            case 2: //key to castle
                UIManager.Instance.UpdateShopSelection(-128);
                currentItemSelected = 2;
                currentItemCost = 100;
                break;
        }
    }

    //butitem method
    //check if player gems is greater or equal to item cost
    //if it is, reward the player, subtract cost from player gems
    //else, cancel sale
    public void BuyItem()
    {
        //Player player = GetComponent<Player>();
        if (player.diamondAmount >= currentItemCost)
        {
            //award item
            if (currentItemSelected == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }

            player.diamondAmount -= currentItemCost;
            Debug.Log("Purchased " + currentItemSelected);
            Debug.Log("Remaining Gems: " + player.diamondAmount);
            shopPanel.SetActive(false) ;
        }
        else
        {
            Debug.Log("You do not have enough gems.");
            shopPanel.SetActive(false) ;
        }
    }
}
