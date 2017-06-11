using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlipper : MonoBehaviour {

    CardModel model;
    SpriteRenderer spriteRender;

    public AnimationCurve scaleCurve;
    public float duration = 0.5f;

    void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        model = GetComponent<CardModel>();
    }

    public void FlipCard(Sprite firstImage,Sprite endImage,int cardIndex)
    {
        StopCoroutine(Flip(firstImage, endImage, cardIndex));
        StartCoroutine(Flip(firstImage, endImage, cardIndex));
    }

    IEnumerator Flip(Sprite firstImage,Sprite endImage,int cardIndex)
    {
        spriteRender.sprite = firstImage;
        float time = 0;

        while (time <= 1)
        {
            float scale = scaleCurve.Evaluate(time);
            time = time + Time.deltaTime / duration;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if(time >= 0.5f)
            {
                spriteRender.sprite = endImage;
            }

            yield return new WaitForFixedUpdate();

        }

        if(cardIndex == -1)
        {
            model.ToggleFace(false);
        }
        else
        {
            model.cardIndex = cardIndex;
            model.ToggleFace(true);
        }
        
    }


	
}
