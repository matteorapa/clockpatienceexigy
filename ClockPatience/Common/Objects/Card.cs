using ClockPatience.Common.Enums;

namespace ClockPatience.Common.Objects;

public class Card
{
    public Suits Suit { get; set; }
    public Rank Rank { get; set; }
    public bool IsExposed { get; set; }


    public Card(string rankSuitText)
    {
        var rankChar = rankSuitText.First();
        var suitChar = rankSuitText.Last();

        Rank = GetRankFromChar(rankChar);
        Suit = GetSuitFromChar(suitChar);
        IsExposed = false;
    }

    public override string ToString()
    {
        var suitChar = Suit.ToString();
        var rankChar = GetCharFromRank(Rank);
        return rankChar + suitChar;
    }

    private char GetCharFromRank(Rank rank)
    {
        switch (rank)
        {
            case Rank.Ace:
                return '1';
            case Rank.Two:
                return '2';
            case Rank.Three:
                return '3';
            case Rank.Four:
                return '4';
            case Rank.Five:
                return '5';
            case Rank.Six:
                return '6';
            case Rank.Seven:
                return '7';
            case Rank.Eight:
                return '8';
            case Rank.Nine:
                return '9';
            case Rank.Ten:
                return 'T';
            case Rank.Jack:
                return 'J';
            case Rank.Queen:
                return 'Q';
            case Rank.King:
                return 'K';
            default:
                return 'A';
        }
    }

    private Suits GetSuitFromChar(char inp)
    {
        switch (inp)
        {
            case 'H':
                return Suits.H;
            case 'D':
                return Suits.D;
            case 'C':
                return Suits.C;
            case 'S':
                return Suits.S;
            default:
                return Suits.H;
        }
    }

    private Rank GetRankFromChar(char inp)
    {
        switch (inp)
        {
            case 'A':
                return Rank.Ace;
            case '2':
                return Rank.Two;
            case '3':
                return Rank.Three;
            case '4':
                return Rank.Four;
            case '5':
                return Rank.Five;
            case '6':
                return Rank.Six;
            case '7':
                return Rank.Seven;
            case '8':
                return Rank.Eight;
            case '9':
                return Rank.Nine;
            case 'T':
                return Rank.Ten;
            case 'J':
                return Rank.Jack;
            case 'Q':
                return Rank.Queen;
            case 'K':
                return Rank.King;
            
            default:
                return Rank.Ace;
        }
    }
}