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
    public GameObject player;
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

        for(int i = 0; i < requiredItems; i++)
        {

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
