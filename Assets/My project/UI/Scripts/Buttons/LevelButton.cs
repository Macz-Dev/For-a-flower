using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int level;
    public int optimumsPieces;
    public int parchmentPiecesRequired;
    private int usedPieces;
    public State currentState;
    public Button button;
    private RectTransform frameRectTransform;
    [SerializeField] private GameObject lockedPanel;
    [SerializeField] private TMP_Text parchmentPiecesRequiredText;
    [SerializeField] private GameObject unlockedPanel;
    [SerializeField] private TMP_Text levelNumberText;
    [SerializeField] private GameObject completedPanel;
    [SerializeField] private RectTransform completedSymbolRectTransform;
    [SerializeField] private TMP_Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        frameRectTransform = GetComponent<RectTransform>();
        CleanPanelStates();
        PrepareRenderByState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PrepareRenderByState()
    {
        switch (currentState)
        {
            case State.LOCKED:
                parchmentPiecesRequiredText.text = parchmentPiecesRequired.ToString();
                button.interactable = false;
                lockedPanel.SetActive(true);
                break;
            case State.UNLOCKED:
                levelNumberText.text = level.ToString();
                button.interactable = true;
                unlockedPanel.SetActive(true);
                break;
            case State.COMPLETED:
                resultText.text = usedPieces + "/" + optimumsPieces;
                button.interactable = true;
                completedPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    void CleanPanelStates()
    {
        lockedPanel.SetActive(false);
        unlockedPanel.SetActive(false);
        completedPanel.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == State.LOCKED) return;
        if (currentState == State.UNLOCKED)
        {
            levelNumberText.fontSize = 95;
        }
        if (currentState == State.COMPLETED)
        {
            completedSymbolRectTransform.sizeDelta = new Vector2(110f, 90f);
            resultText.fontSize = 45;
        }
        frameRectTransform.sizeDelta = new Vector2(210f, 140f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == State.LOCKED) return;
        if (currentState == State.UNLOCKED)
        {
            levelNumberText.fontSize = 90;
        }
        if (currentState == State.COMPLETED)
        {
            completedSymbolRectTransform.sizeDelta = new Vector2(100f, 80f);
            resultText.fontSize = 40;
        }
        frameRectTransform.sizeDelta = new Vector2(200f, 130f);
        // buttonText.fontSize = originalTextSize;
    }

    public enum State
    {
        LOCKED,
        UNLOCKED,
        COMPLETED
    }
}
