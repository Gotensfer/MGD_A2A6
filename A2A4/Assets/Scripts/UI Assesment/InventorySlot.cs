using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Item _item;
    public Item item { get => _item; }

    [SerializeField] Image spriteHolder;
    [SerializeField] InventorySlotIndicator inventorySlotIndicator;

    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        SetSlotVisual();
    }


    // Logical methods
    public void SetSlotItem(Item newItem)
    {
        if (newItem == null)
        {
            _item = null;
        }
        else
        {
            _item = newItem;
        }

        SetSlotVisual();
    }

    public void DisplayItemInfo()
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        print(corners[3]);
        InventoryHandler.instance.inventoryItemHighlighter.Set(transform, item.itemName, item.desc);
    }


    // Visual methods
    void SetSlotVisual()
    {
        if (_item != null)
        {
            spriteHolder.sprite = _item.sprite;
            spriteHolder.color = new Color(1, 1, 1, 1);
        }
        else
        {
            spriteHolder.sprite = null;
            spriteHolder.color = new Color(0, 0, 0, 0);
        }
        
    }

    public void SetIndicatorOn()
    {
        inventorySlotIndicator.SetMovedIndicator();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null)
        {
            DisplayItemInfo();
            InventoryHandler.instance.StartItemTouchMonitoring(eventData.position, this);
        }    
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryHandler.instance.inventoryItemHighlighter.Hide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryHandler.instance.currentHoveredItemSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Originally, I would use this to nullify the reference to the 'currentHoveredItemslot',
        // buuuut, this pointer event SOMETIMES got called when it shouldn't, which nullifies the reference before
        // the InventoryHandler can swap the items, this behaviour was always replicated when doing the following swap:
        // A -> B then A -> B triggers the event the moment the touch/click is lifted out
        // Funnily enough A -> B then B -> A doesn't trigger this pointer event, therefore allowing the swap
        // Possible unity bug?? or weird 'intended design'??
    }
}
