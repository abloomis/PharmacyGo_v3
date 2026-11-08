using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BlackjackPhase
{
    Dealing,
    PlayerTurn,
    Answering,
    ChoosingCard,
    DealerTurn,
    GameEnd
}

public enum BlackjackResult
{
    PlayerWin,
    DealerWin,
    Push
}

public class BlackjackCard
{
    public int rank;
    public int suit;

    public BlackjackCard(int rank, int suit)
    {
        this.rank = rank;
        this.suit = suit;
    }

    public int BaseValue
    {
        get
        {
            if (rank == 1) return 1;
            if (rank >= 10) return 10;
            return rank;
        }
    }

    public override string ToString()
    {
        string r;
        switch (rank)
        {
            case 1: r = "A"; break;
            case 11: r = "J"; break;
            case 12: r = "Q"; break;
            case 13: r = "K"; break;
            default: r = rank.ToString(); break;
        }
        string[] suits = { "S", "H", "D", "C" };
        return r + suits[suit];
    }
}

public class BlackjackMinigame : MonoBehaviour
{
    public MinigamePilot pilot;
    public GameObject Blackjack;

    public TextMeshProUGUI playerHandText;
    public TextMeshProUGUI dealerHandText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI questionText;

    public BlackjackPhase phase = BlackjackPhase.Dealing;
    public int minigameDifficulty = 0;
    public float dealerDrawDelay = 0.6f;
    public int dealerStandValue = 17;

    public Question currentQuestion;

    private List<BlackjackCard> deck = new List<BlackjackCard>();
    private List<BlackjackCard> playerHand = new List<BlackjackCard>();
    private List<BlackjackCard> dealerHand = new List<BlackjackCard>();
    private List<BlackjackCard> offeredCards = new List<BlackjackCard>();

    private int difficulty = 0;
    private int module = 0;
    private bool dealerHoleHidden = true;

    public void StartBlackjack(int d, int level)
    {
        module = level;
        minigameDifficulty = d;
        GameController.Instance.StartMinigame();
        Blackjack.SetActive(true);
        RestartPlay();
    }

    public void CloseWindow()
    {
        StopAllCoroutines();
        ClearHands();
        GameController.Instance.EndMinigame();
        Blackjack.SetActive(false);
    }

    public void RestartPlay()
    {
        StopAllCoroutines();
        ClearHands();
        BuildDeck();
        ShuffleDeck();
        currentQuestion = null;
        dealerHoleHidden = true;
        StartCoroutine(DealOpeningHands());
    }

    private void ClearHands()
    {
        playerHand.Clear();
        dealerHand.Clear();
        offeredCards.Clear();
    }

    private void BuildDeck()
    {
        deck.Clear();
        for (int s = 0; s < 4; s++)
        {
            for (int r = 1; r <= 13; r++)
            {
                deck.Add(new BlackjackCard(r, s));
            }
        }
    }

    private void ShuffleDeck()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            BlackjackCard temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }

    private BlackjackCard DrawFromDeck()
    {
        if (deck.Count == 0)
        {
            BuildDeck();
            ShuffleDeck();
        }
        BlackjackCard card = deck[deck.Count - 1];
        deck.RemoveAt(deck.Count - 1);
        return card;
    }

    public static int HandValue(List<BlackjackCard> hand)
    {
        int total = 0;
        int aces = 0;
        foreach (BlackjackCard c in hand)
        {
            total += c.BaseValue;
            if (c.rank == 1) aces++;
        }
        if (aces > 0 && total + 10 <= 21)
        {
            total += 10;
        }
        return total;
    }

    private IEnumerator DealOpeningHands()
    {
        phase = BlackjackPhase.Dealing;
        playerHand.Add(DrawFromDeck());
        dealerHand.Add(DrawFromDeck());
        playerHand.Add(DrawFromDeck());
        dealerHand.Add(DrawFromDeck());
        UpdateHandTexts();
        yield return null;

        if (HandValue(playerHand) == 21)
        {
            Stand();
            yield break;
        }
        phase = BlackjackPhase.PlayerTurn;
        SetStatus("Hit or Stand?");
    }

    public void Hit()
    {
        if (phase != BlackjackPhase.PlayerTurn) return;
        StartCoroutine(AskQuestion());
    }

    private IEnumerator AskQuestion()
    {
        phase = BlackjackPhase.Answering;
        yield return new WaitUntil(() => pilot.gotQuestions);
        currentQuestion = pilot.moduleManager.GetRandomQuestion(module, (int)(difficulty / 20));
        ShowQuestion(currentQuestion);
    }

    private void ShowQuestion(Question q)
    {
        questionText.text = q.question;
    }

    public void SubmitAnswer(int optionIndex)
    {
        if (phase != BlackjackPhase.Answering || currentQuestion == null) return;

        bool correct = optionIndex == currentQuestion.answerIndex;
        currentQuestion = null;

        if (correct)
        {
            OfferCardChoice();
        }
        else
        {
            GivePlayerCard(DrawFromDeck());
        }
    }

    private void OfferCardChoice()
    {
    }

    public void ChooseOfferedCard(int index)
    {
    }

    private void GivePlayerCard(BlackjackCard card)
    {
        playerHand.Add(card);
        UpdateHandTexts();

        int value = HandValue(playerHand);
        if (value > 21)
        {
            EndRound(BlackjackResult.DealerWin);
        }
        else if (value == 21)
        {
            Stand();
        }
        else
        {
            phase = BlackjackPhase.PlayerTurn;
            SetStatus("Hit or Stand?");
        }
    }

    public void Stand()
    {
        if (phase != BlackjackPhase.PlayerTurn && phase != BlackjackPhase.Dealing) return;
        StartCoroutine(DealerTurn());
    }

    private IEnumerator DealerTurn()
    {
        phase = BlackjackPhase.DealerTurn;
        dealerHoleHidden = false;
        UpdateHandTexts();

        while (HandValue(dealerHand) < dealerStandValue)
        {
            yield return new WaitForSeconds(dealerDrawDelay);
            dealerHand.Add(DrawFromDeck());
            UpdateHandTexts();
        }

        EndRound(DecideResult());
    }

    private BlackjackResult DecideResult()
    {
        int player = HandValue(playerHand);
        int dealer = HandValue(dealerHand);

        if (player > 21) return BlackjackResult.DealerWin;
        if (dealer > 21) return BlackjackResult.PlayerWin;
        if (player > dealer) return BlackjackResult.PlayerWin;
        if (dealer > player) return BlackjackResult.DealerWin;
        return BlackjackResult.Push;
    }

    private void EndRound(BlackjackResult result)
    {
        phase = BlackjackPhase.GameEnd;
        dealerHoleHidden = false;
        UpdateHandTexts();
        SetStatus(result.ToString());
        GiveRewards(result);
    }

    private void GiveRewards(BlackjackResult result)
    {
    }

    private void UpdateHandTexts()
    {
        playerHandText.text = HandToString(playerHand) + " (" + HandValue(playerHand) + ")";
        if (dealerHoleHidden && dealerHand.Count > 1)
        {
            dealerHandText.text = dealerHand[0] + " ??";
        }
        else
        {
            dealerHandText.text = HandToString(dealerHand) + " (" + HandValue(dealerHand) + ")";
        }
    }

    private string HandToString(List<BlackjackCard> hand)
    {
        List<string> parts = new List<string>();
        foreach (BlackjackCard c in hand)
        {
            parts.Add(c.ToString());
        }
        return string.Join(" ", parts);
    }

    private void SetStatus(string message)
    {
        statusText.text = message;
    }
}
