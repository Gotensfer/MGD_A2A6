using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItemHighlighter : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] TextMeshProUGUI itemNameField;
    [SerializeField] TextMeshProUGUI itemDescField;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Set(Transform newParent, string itemName, string itemDesc)
    {
        transform.SetParent(newParent);

        rectTransform.anchoredPosition = Vector3.zero + new Vector3(25, -75, 0);

        itemNameField.text = itemName;
        itemDescField.text = itemDesc;
    }

    public void Hide()
    {
        print("Hiding hightlighter");
        rectTransform.anchoredPosition = new Vector3(-2000, -2000, 0);
    }
}
