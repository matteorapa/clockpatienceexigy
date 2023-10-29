using ClockPatience.Common.Enums;

namespace ClockPatience.Common.Objects;

public class Pile
{
    public Stack<Card> Cards { get; set; }
    public Rank PileReference { get; set; }

    public Pile(Rank inpRank)
    {
        PileReference = inpRank;
        Cards = new Stack<Card>();
    }
}