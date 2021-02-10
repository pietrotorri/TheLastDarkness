using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool hovered;
    private bool empty;

    private GameObject item;
    private Texture itemIcon;


    private void Start()
    {
        hovered = false;
    }

    private void Update()
    {
        if (item)
            empty = false;
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
