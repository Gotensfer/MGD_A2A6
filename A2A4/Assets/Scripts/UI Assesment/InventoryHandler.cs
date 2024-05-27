using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] InventoryItemHighlighter _inventoryItemHighlighter;
    public InventoryItemHighlighter inventoryItemHighlighter { get => _inventoryItemHighlighter; }

    public static InventoryHandler instance;

    // Inventory variables
    [SerializeField] InventorySlot[] inventorySlots;

    Vector2 firstTouchPosition;

    Vector2 _initialItemTouchPosition;
    Vector2 initialItemTouchPosition { get => _initialItemTouchPosition; set => _initialItemTouchPosition = value; }

    bool hasActiveTouch;

    bool monitoringItemTouch;
    bool itemGrabbed;

    InventorySlot initialInventorySlot;

    InventorySlot _currentHoveredItemSlot;
    public InventorySlot currentHoveredItemSlot { get => _currentHoveredItemSlot; set => _currentHoveredItemSlot = value; }

    [SerializeField] float requieredDistanceForItemGrab = 1;

    private void Awake()
    {
        instance = this;

        Input.multiTouchEnabled = false;
    }

    private void Update()
    {
        GetTouchInfo();

        if (monitoringItemTouch)
        {
            MonitorItemTouch();
        }    
    }

    void GetTouchInfo()
    {
        int touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            firstTouchPosition = Input.GetTouch(0).position;
            hasActiveTouch = true;
        }
        else
        {
            firstTouchPosition = Vector2.zero;
            hasActiveTouch = false;
        }
    }

    #region"Item grab"

    void MonitorItemTouch()
    {
        if (!hasActiveTouch)
        {
            if (itemGrabbed) ReleaseItem();

            itemGrabbed = false;
            StopItemTouchMonitoring();

            return;
        }

        
        if (!itemGrabbed)
        {
            float distanceFromInitialTouch = Vector2.Distance(_initialItemTouchPosition, firstTouchPosition);

            if (distanceFromInitialTouch > requieredDistanceForItemGrab)
            {
                GrabItem();
            }
        }
    }

    public void StartItemTouchMonitoring(Vector2 initialPosition, InventorySlot initialInventorySlot)
    {
        _initialItemTouchPosition = initialPosition;
        this.initialInventorySlot = initialInventorySlot;

        monitoringItemTouch = true;
    }

    void StopItemTouchMonitoring()
    {
        monitoringItemTouch = false;
    }

    void GrabItem()
    {
        itemGrabbed = true;

        inventoryItemHighlighter.Hide();
    }

    void ReleaseItem()
    {
        print("Releasing item");
        print($"Condition eval: 1st {currentHoveredItemSlot != null} , 2nd {currentHoveredItemSlot != initialInventorySlot}");

        if (currentHoveredItemSlot != null && currentHoveredItemSlot != initialInventorySlot)
        {
            SwapItemsOnSlots();
        }
    }

    void SwapItemsOnSlots()
    {
        print("Swapping slot items");

        Item transferItem = initialInventorySlot.item;

        if (currentHoveredItemSlot.item != null)
        {
            initialInventorySlot.SetSlotItem(currentHoveredItemSlot.item);
            initialInventorySlot.SetIndicatorOn();
        }
        else initialInventorySlot.SetSlotItem(null);
        
        currentHoveredItemSlot.SetSlotItem(transferItem);
        currentHoveredItemSlot.SetIndicatorOn();
    }
    #endregion

    public void SortItems()
    {
        Item[] nonNullItems = inventorySlots
                              .Where(slot => slot.item != null)
                              .Select(slot => slot.item)
                              .ToArray();

        Item[] sortedItems = nonNullItems
                             .OrderBy(item => item.itemType)
                             .ToArray();

        int index = 0;
        foreach (var slot in inventorySlots)
        {
            if (index < sortedItems.Length)
            {
                slot.SetSlotItem(sortedItems[index]);
                slot.SetIndicatorOn();
                index++;
            }
            else
            {
                slot.SetSlotItem(null);
            }
        }
    }
}
