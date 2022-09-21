using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionButton : MonoBehaviour
{
    public string id;
    public Image instructionSymbol;
    public Sprite instructionSymbolSprite;
    // Start is called before the first frame update
    void Start()
    {
        this.instructionSymbol.sprite = this.instructionSymbolSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
