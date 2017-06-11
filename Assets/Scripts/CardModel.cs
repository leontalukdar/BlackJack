using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour {

    public Sprite[] faces;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    public int cardIndex;

    public void ToggleFace(bool showFace)
    {
        if(showFace)
        {
            spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
