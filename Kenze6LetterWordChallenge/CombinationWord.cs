namespace Kenze6LetterWordChallenge;

public class CombinationWord
{
    public List<string> WordsNeeded { get; set; }
    public string Value { get; set; }

    public CombinationWord()
    {
        Value = "";
        WordsNeeded = new List<string>();
    }

    public CombinationWord(CombinationWord combo)
    {
        Value = combo.Value;
        WordsNeeded = new List<string>();
        combo.WordsNeeded.ForEach(word => WordsNeeded.Add(word));
    }

    public void AddWord(string word)
    {
        Value += word;
        WordsNeeded.Add(word);
    }

    public override string ToString()
    {
        var result = "";
        WordsNeeded.ForEach(word => result += $"{word} + ");

        result = result.Remove(result.Length - 2, 2);

        result += $"= {Value}";

        return result;
    }
}