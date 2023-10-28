using ClockPatience.Common.Enums;

namespace ClockPatience.Common.Objects;

public class Card
{
    public Suits Suit { get; set; }
    public Rank Rank { get; set; }
    public bool IsExposed { get; set; }
}