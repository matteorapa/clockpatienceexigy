using ClockPatience;
using ClockPatience.Common.Enums;
using ClockPatience.Common.Objects;

// read input into decks, stop reading at #

// Check if the filepath for the input is provided as a program argument
if (args.Length == 0)
{
    Console.WriteLine("Please specify a filename as a parameter.");
    return;
}

// Get filename from program arguments
var filename = args[0];
Console.WriteLine("Received the filepath: " + filename);

//Check that the path ends with .txt
if (!filename.EndsWith(".txt") ){
    Console.WriteLine("Invalid input, expected a .txt file.");
    return;
} 

// Read the txt file as lines and individual strings
var separator = " ";
var fileContentsLines =  File.ReadAllLines(filename)
    .Select(line => line.Split(separator, StringSplitOptions.RemoveEmptyEntries))
    .ToList();

// Check that there at least 1 deck (4 lines) and the stop character # (1 line), otherwise exit.
if (fileContentsLines.Count < 5){
    Console.WriteLine("Invalid input, expected at least 5 lines.");
    return;
}

// Check that the amount of lines is valid for multiple decks, 4d + 1, where d is the number of input decks.
if (fileContentsLines.Count % 4 != 1)
{
    Console.WriteLine("Invalid input, expected 4d + 1 lines, where d is the number of decks.");
    return;
}

List<Deck> decks = new List<Deck>();
const int fullDeckCardsLength = 52;
for (var index = 0;  index < fileContentsLines.Count; index++)
{
    //check if there enough lines, ie. 5 lines to add a new deck
    if (decks.Count == 0 || decks.Last().Cards.Count == fullDeckCardsLength)
    {
        var deck = new Deck();
        decks.Add(deck);
    }
    
    foreach (var inputText in fileContentsLines[index])
    {
        
        var lastDeck = decks.Last();

        // check if at end of input
        if (inputText.Equals("#"))
        {
            break;
        }
        var card = new Card(inputText);
        lastDeck.Cards.Add(card);
        
    }
}

var rankCount = Enum.GetValues(typeof(Rank)).Length;

// Play game for every deck
foreach(var deck in decks)
{
    // The deck is listed from bottom to top, reverse the cards in the decks.
    deck.Cards.Reverse();
    var piles = new List<Pile>();

    // create the piles, each pile references a rank of cards
    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
    {
        var newPile = new Pile(rank);
        piles.Add(newPile);
    }

    // for each deck place in piles using the correct order
    var currentPileAddIndex = 0;
    foreach (var deckCard in deck.Cards)
    {
        piles[currentPileAddIndex].Cards.Push(deckCard);
        
        // Check to reset the index once every pile is given a card
        if (currentPileAddIndex == (rankCount - 1))
        {
            // Reset index
            currentPileAddIndex = 0;
        }
        else
        {
            currentPileAddIndex++;
        }
    }


    var cardsExposedCount = 0;
    Card lastCardExposed = new Card("AD");
    
    // start at king pile
    var currentPileRankReferenceInt = (int)Rank.King;

    while (cardsExposedCount < deck.Cards.Count)
    {
        // Check if the top card of the pile is already exposed, then end the game for deck
        if (piles[currentPileRankReferenceInt].Cards.Count == 0 || piles[currentPileRankReferenceInt].Cards.Peek().IsExposed)
        {
            var cardsExposedCountText = cardsExposedCount.ToString();
            // When single digit, add leading zero character
            if (cardsExposedCount < 10)
            {
                cardsExposedCountText = "0" + cardsExposedCountText;
            }
            Console.WriteLine(cardsExposedCountText +" "+ lastCardExposed);
            break;
        }
        else
        {
            // set top card of pile to current pile
            var currentCard = piles[currentPileRankReferenceInt].Cards.Pop();
            
            // expose top card (current card),
            currentCard.IsExposed = true;
            lastCardExposed = currentCard;
            cardsExposedCount++;

            
            // place exposed card at bottom of pile with the same value,
            var destinationPileRankReferenceInt = (int)currentCard.Rank;
            piles[destinationPileRankReferenceInt].Cards = UtilityMethods
                .PushToBottom(piles[destinationPileRankReferenceInt].Cards, currentCard);
            
            // Set the working pile to the rank of the exposed card
            currentPileRankReferenceInt = destinationPileRankReferenceInt;
        }
    }
}