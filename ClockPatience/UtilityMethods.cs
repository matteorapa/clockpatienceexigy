using ClockPatience.Common.Enums;
using ClockPatience.Common.Objects;

namespace ClockPatience;

public static class UtilityMethods
{
    public static Stack<Card> PushToBottom(Stack<Card> pile, Card currentCard)
    {
        // Temporary stack
        Stack<Card> temp = new Stack<Card>();
 
        // Pop cards in pile and push to temp stack
        while (pile.Count != 0) {
            
            temp.Push(pile.Pop());
        }
        
        // Add current card to bottom of emptied pile
        pile.Push(currentCard);
 
        // Pop cards in temp stack back to pile
        while (temp.Count != 0) {
            pile.Push(temp.Pop());
        }
        
        return pile;
    }
}