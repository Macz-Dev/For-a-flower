using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExitLevelButton : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        this.button.onClick.AddListener(() => ExitLevel());
    }

    void ExitLevel()
    {
        GameManager.Instance.ChangeState(GameState.LEVEL_MENU);
        UIManager.Instance.SetCurrentPanel("LevelMenu");
    }
}
