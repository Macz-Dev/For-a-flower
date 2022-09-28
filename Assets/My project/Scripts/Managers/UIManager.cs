using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private UIPanel[] panels;
    private UIPanel currentPanel;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

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
