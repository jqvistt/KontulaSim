using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using JetBrains.Annotations;

public class MouseItemData : MonoBehaviour
{
    public Image ItemSprite;
    public TextMeshProUGUI ItemCount;
    public InventorySlot AssignedInventorySlot;
    public PlayerMove playerMove;

    private void Awake()
    {
        ItemSprite.color = Color.clear;
        ItemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        AssignedInventorySlot.AssignItem(invSlot);
        ItemSprite.sprite = invSlot.ItemData.Icon;
        ItemCount.text = invSlot.StackSize.ToString();
        ItemSprite.color = Color.white;
    }

    private void Update()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            /*  Debug.Log("Cursor is holding: " + AssignedInventorySlot.ItemData.name); // Debug log the held object's name */

            transform.position = Input.mousePosition;

            if (Mouse.current.leftButton.wasPressedThisFrame && !IsPointerOverUIObject() && !playerMove.isCursorOverTrigger)
            {
                ClearSlot();
            }
        }

        // Debug.Log statements to check values
        //Debug.Log("ItemCount.text: " + ItemCount.text);
        //Debug.Log("AssignedInventorySlot.StackSize: " + AssignedInventorySlot.StackSize);

        if (AssignedInventorySlot.StackSize >= 1)
        {
            ItemCount.text = AssignedInventorySlot.StackSize.ToString();
        }
        else return;
        
    }

    public void ClearSlot()
    {

        AssignedInventorySlot.ClearSlot();
        ItemCount.text = "";
        ItemSprite.color = Color.clear;
        ItemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public string GetAssignedItemName()
    {
        if (AssignedInventorySlot.ItemData != null)
        {
            return AssignedInventorySlot.ItemData.name;
        }
        else
        {
            return string.Empty;
        }
    }

    private void UpdateItemCount()
    {
        ItemCount.text = AssignedInventorySlot.StackSize.ToString();
    }

}
