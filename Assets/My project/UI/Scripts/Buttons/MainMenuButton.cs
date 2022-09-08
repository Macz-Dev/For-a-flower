using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private TMP_Text buttonText;
    private float originalTextSize;
    [SerializeField] private Color disabledTextColor;
    [SerializeField] private GameObject decorationBackgroundImage;

    private bool isEnabled;
    void Awake()
    {
        // Get components
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();

        // Identify button State
        isEnabled = button.interactable;

        SetInitialStyles();

    }

    void OnDisable()
    {
        if (!isEnabled) return;
        buttonText.fontSize = originalTextSize;
        decorationBackgroundImage.SetActive(false);
    }

    void SetInitialStyles()
    {
        originalTextSize = buttonText.fontSize;
        if (!isEnabled) buttonText.color = disabledTextColor;
        decorationBackgroundImage.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isEnabled) return;
        buttonText.fontSize = originalTextSize + 10;
        decorationBackgroundImage.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isEnabled) return;
        buttonText.fontSize = originalTextSize;
        decorationBackgroundImage.SetActive(false);
    }
}
