using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        this.button.onClick.AddListener(() => EnableSettings());
    }

    void EnableSettings()
    {
        UIManager.Instance.ShowPanel("OptionsMenu");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.audioSource.Stop();
        UIManager.Instance.audioSource.Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.audioSource.Stop();
    }
}
