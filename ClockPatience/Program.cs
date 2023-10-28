using System.Runtime.InteropServices;
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

for (var index = 0;  index < fileContentsLines.Count; index++)
{
    //check if there enough lines, ie. 5 lines to add a new deck
    if (fileContentsLines.Count - index >= 5)
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



var piles = new List<Pile>();
// for each deck place in piles using the correct order


//play game,
// start at king pile

var currentPileRankReference = Rank.King;
var currentPile = piles.Find(p => p.PileReference == currentPileRankReference);
    

    
    // expose top card (current card),
    // place exposed card at bottom of pile with the same value,
    // set top card of pile to current pile
    // repeat until no cards left to expose
    // return one line per deck, showing number of exposed card, and last exposed card

