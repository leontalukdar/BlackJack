using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CardView
{
    public GameObject card { get; private set; }
    public bool isFaceUp { get; set; }

    public CardView(GameObject card)
    {
        this.card = card;
        isFaceUp = false;
    }
}
