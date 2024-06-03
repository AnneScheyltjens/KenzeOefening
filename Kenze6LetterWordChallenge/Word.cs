namespace Kenze6LetterWordChallenge;

public class Word
{
    public string Value { get; set; }
    public List<CombinationWord> Combinations { get; set; } = new List<CombinationWord>();
}