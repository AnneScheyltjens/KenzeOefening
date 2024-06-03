namespace Kenze6LetterWordChallenge;

public class CombinationWord
{
    public List<Word> WordsNeeded { get; set; } = new List<Word>();
    public string Value { get; set; }

    public override string ToString()
    {
        var result = "";
        WordsNeeded.ForEach(word => result += $"{word.Value} + ");

        result = result.Remove(result.Length - 2, 2);

        result += $"= {Value}";

        return result;
    }
}