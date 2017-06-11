using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour
{
    CardStack deck;
    Dictionary<int,CardView> fetchedCards;
    public bool faceUp = false;
    public bool reverseLayerOrder=false;

    int lastCount;

    public Vector3 start;
    public float cardOffset;
    public GameObject cardPrefab;

    public void Toggle(int card,bool isFaceUp)
    {
        fetchedCards[card].isFaceUp = isFaceUp;
    }

    public void Clear()
    {
        deck.Reset();
        foreach(CardView view in fetchedCards.Values)
        {
            Destroy(view.card);
        }
        fetchedCards.Clear();
    }
	
    void Start()
    {
        fetchedCards = new Dictionary<int, CardView>();
        deck = GetComponent<CardStack>();
        ShowCards();
        lastCount = deck.cardCount;
        deck.cardRemoved += Deck_cardRemoved;
        deck.cardAdded += Deck_cardAdded;
    }

    private void Deck_cardAdded(object sender, CardEventArgs e)
    {
        float co = cardOffset * deck.cardCount;
        Vector3 temp = start + new Vector3(co, 0f);
        AddCard(temp, e.cardIndex, deck.cardCount);
    }   

    private void Deck_cardRemoved(object sender, CardEventArgs e)
    {
        if(fetchedCards.ContainsKey(e.cardIndex))
        {
            Destroy(fetchedCards[e.cardIndex].card);
            fetchedCards.Remove(e.cardIndex);
        }
    }

    void Update()
    {
        ShowCards();
    }

    public void ShowCards()
    {
        int cardCount = 0;
        if (deck.hasCards)
        {
            foreach (int i in deck.GetCards())
            {
                float co = cardOffset * cardCount;
                Vector3 temp = start + new Vector3(co, 0f);
                AddCard(temp, i, cardCount);
                cardCount++;
            }
        }
    }

    void AddCard(Vector3 position,int cardIndex,int positionalIndex)
    {
        if(fetchedCards.ContainsKey(cardIndex))
        {
            if(!faceUp)
            {
                CardModel model = fetchedCards[cardIndex].card.GetComponent<CardModel>();
                model.ToggleFace(fetchedCards[cardIndex].isFaceUp);
            }
            return;
        }

        GameObject cardCopy = Instantiate(cardPrefab);
        cardCopy.transform.position = position;

        CardModel cardModel = cardCopy.GetComponent<CardModel>();
        cardModel.cardIndex = cardIndex;
        cardModel.ToggleFace(faceUp);

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();

        if(reverseLayerOrder)
        {
            spriteRenderer.sortingOrder = 51 - positionalIndex;
        }
        else
        {
            spriteRenderer.sortingOrder = positionalIndex;
        }

        fetchedCards.Add(cardIndex,new CardView(cardCopy));
        Debug.Log("Hand Value = " + deck.HandValue());
    }

}
