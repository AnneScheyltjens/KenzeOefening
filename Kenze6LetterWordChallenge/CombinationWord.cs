namespace Kenze6LetterWordChallenge;

public class CombinationWord
{
    public List<string> WordsNeeded { get; set; }
    public string Value { get; set; }

    public override string ToString()
    {
        var result = "";
        WordsNeeded.ForEach(word => result += $"{word} + ");

        result = result.Remove(result.Length - 2, 2);

        result += $"= {Value}";

        return result;
    }
}