using System;
public class CardEventArgs : EventArgs
{
    public int cardIndex { get; private set; }

    public CardEventArgs(int cardIndex)
    {
        this.cardIndex = cardIndex;
    }
}