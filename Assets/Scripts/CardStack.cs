using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardStack : MonoBehaviour {

    public bool isGameDeck=false;
    List<int> cards;
    public event CardEventHandler cardRemoved;
    public event CardEventHandler cardAdded;

    public bool hasCards
    {
        get
        {
            return cards != null && cards.Count > 0;
        }
    }
    public IEnumerable<int> GetCards()
    {
        foreach(int i in cards)
        {
            yield return i;
        }
    }

    public int cardCount
    {
        get
        {
            if (cards == null)
                return 0;
            else
                return cards.Count;
        }
    }

    public int Pop()
    {
        int temp = 0;
        try
        {
            temp = cards[0];
            cards.RemoveAt(0);
            cardRemoved(this, new CardEventArgs(temp));
        }
        catch(NullReferenceException ex)
        {
            Console.WriteLine(ex.Message);
        }
        return temp;
        
        
    }

    public void Push(int card)
    {
        cards.Add(card);
        if(cardAdded!=null)
        {
            cardAdded(this, new CardEventArgs(card));
        }
    }

    public int HandValue()
    {
        int total = 0;
        int aces = 0;

        foreach(int card in GetCards())
        {
            int cardRank = card % 13;
            if(cardRank>0 && cardRank<9)
            {
                cardRank++;
                total += cardRank;
            }
            else if(cardRank>=9 && cardRank<=12)
            {
                cardRank = 10;
                total += cardRank;
            }
            else
            {
                aces++;
            }
        }

        for(int i=0;i<aces;i++)
        {
            if (total + 11 <= 21)
            {
                total += 11;
            }
            else
                total += 1;
        }

        return total;
    }

    public void CreateDeck()
    {
        for(int i = 0;i<52;i++)
        {
            cards.Add(i);
        }

        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            int temp = cards[n];
            cards[n] = cards[k];
            cards[k] = temp;
        }

    }

    public void Reset()
    {
        cards.Clear();
    }

	void Awake ()
    {
        cards = new List<int>();
        if (isGameDeck)
        {
            CreateDeck();
        }
	}

}
