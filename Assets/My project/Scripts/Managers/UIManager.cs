using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject levelMenuPanel;

    [SerializeField] private UIPanel[] panels;
    private UIPanel currentPanel;

    void Start()
    {
        this.panels = GetComponentsInChildren<UIPanel>(true);
        SetCurrentPanel("MainMenu");
    }

    public void SetCurrentPanel(string panelName)
    {
        foreach (var panel in this.panels)
        {
            if (panel.name == panelName)
            {
                panel.Enable();
                this.currentPanel = panel;
            }
            else
            {
                panel.Disable();
            }
        }
    }

    public void ShowPanel(string panelName)
    {
        foreach (var panel in this.panels)
        {
            if (panel.name == panelName)
            {
                panel.Enable();
                return;
            }
        }
    }

    public void HidePanel(string panelName)
    {
        foreach (var panel in this.panels)
        {
            if (panel.name == panelName)
            {
                panel.Enable();
                return;
            }
        }
    }
}
