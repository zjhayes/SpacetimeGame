using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    [SerializeField]
    private Text tipText;
    [SerializeField]
    private RectTransform background;
    [SerializeField]
    private float textPadding = 4.0f;

    public void ShowTooltip(string tip)
    {
        gameObject.SetActive(true);
        tipText.text = tip;
        Vector2 backgroundSize = new Vector2(tipText.preferredWidth + textPadding * 2, tipText.preferredHeight + textPadding * 2);
        background.sizeDelta = backgroundSize;
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
