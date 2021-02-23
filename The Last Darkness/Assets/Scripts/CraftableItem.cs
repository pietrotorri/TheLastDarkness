using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftableItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int requiredItems;
    public GameObject[] item;

    private bool hovered;
    private GameObject player;
    private GameObject itemManager;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        itemManager = GameObject.FindWithTag("ItemManager");
    }

    public void Update()
    {
        if(hovered == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckForRequiredItems();
            }
        } 
    }

    public void CheckForRequiredItems()
    {
        int itemsInManager = itemManager.transform.childCount;

        if(itemsInManager > 0)
        {

            int itemsFound = 0;
            for(int i = 0; i < itemsInManager; i++)
            {
                for (int z = 0; z < requiredItems; z++)
                {
                    if (itemManager.transform.GetChild(i).GetComponent<Item>().type == item[z].GetComponent<Item>().type)
                    {
                        print("Item found!");
                        itemsFound++;
                        break;
                    }
                }
            }

            if (itemsFound >= requiredItems)
                print("All items are found!");

            /*
            for (int i = 0; i < requiredItems; i++)
            {
                for(int z = 0; z < itemsInManager; z++)
                {
                    if (itemManager.transform.GetChild(z).GetComponent<Item>().type == item[i].GetComponent<Item>().type)
                    {
                        print("Item found!");
                    }
                }
            }
            */
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }
}
