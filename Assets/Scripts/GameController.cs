using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CardStack player;
    public CardStack dealer;
    public CardStack deck;
    public Button hitButton;
    public Button stickButton;
    public Button playAgainButton;
    public Text winnerText;

    int dealerFirstCard = -1;

    public void Hit()
    {
        player.Push(deck.Pop());
        if(player.HandValue()>21)
        {
            hitButton.interactable = false;
            stickButton.interactable = false;
            StartCoroutine(DealerTurn());
        }
    }

    public void Stick()
    {
        hitButton.interactable = false;
        stickButton.interactable = false;
        StartCoroutine(DealerTurn());
    }

    public void PlayAgain()
    {
        playAgainButton.interactable = false;

        player.GetComponent<CardStackView>().Clear();
        dealer.GetComponent<CardStackView>().Clear();
        deck.GetComponent<CardStackView>().Clear();
        deck.CreateDeck();

        hitButton.interactable = true;
        stickButton.interactable = true;
        winnerText.text = "";
        dealerFirstCard = -1;
        StartGame();
    }
    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        for(int i=0;i<2;i++)
        {
            player.Push(deck.Pop());
            HitDealer();
        }
    }

    public void HitDealer()
    {
        int card = deck.Pop();
        if (dealerFirstCard < 0)
            dealerFirstCard = card;
        dealer.Push(card);
        if(dealer.cardCount>=2)
        {
            CardStackView view = dealer.GetComponent<CardStackView>();
            view.Toggle(card, true);
        }
    }
	
    IEnumerator DealerTurn()
    {
        hitButton.interactable = false;
        stickButton.interactable = false;

        CardStackView view = dealer.GetComponent<CardStackView>();
        view.Toggle(dealerFirstCard, true);
        view.ShowCards();
        yield return new WaitForSeconds(1f);

        while (dealer.HandValue()<17)
        {
            HitDealer();
            yield return new WaitForSeconds(1f);
        }

        if(player.HandValue()>21 || (dealer.HandValue()>=player.HandValue() && dealer.HandValue()<=21))
        {
            winnerText.text = "Sorry!! You lose. :(";
        }
        else if(dealer.HandValue() > 21 || (player.HandValue() > dealer.HandValue() && player.HandValue() <= 21))
        {
            winnerText.text = "Winner Winner!! Chiken Dinner!! :)";
        }
        else
        {
            winnerText.text = "The house wins!!!";
        }

        yield return new WaitForSeconds(1f);

        playAgainButton.interactable = true;

    }
}
