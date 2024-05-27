using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotIndicator : MonoBehaviour
{
    [SerializeField] Image indicator;
    [SerializeField] float rateOfIndicatorFade = 1;

    private void Awake()
    {
        indicator.color = new Color(0, 0, 0, 0);
        enabled = false;
    }

    private void Update()
    {
        indicator.color = new Color(0, 1, 1, indicator.color.a - (rateOfIndicatorFade * Time.deltaTime));

        if (indicator.color.a < 0.05f)
        {
            indicator.color = new Color(0, 0, 0, 0);
            enabled = false;
        }
    }
    public void SetMovedIndicator()
    {
        enabled = true;

        indicator.color = new Color(0, 1, 1, 0.5f);
    }
}
