namespace Kenze6LetterWordChallenge;

public static class Program
{
    public static List<string> _fullSizedWords = new List<string>();
    public static List<string> _notFullSizedWords = new List<string>();
    public static List<CombinationWord> _CombinationWords = new List<CombinationWord>();
    private const int MaxNrOfLetters = 6;
    private const int NrOfWordsNeeded = 2;

    private static void Main(string[] args)
    {
        const string filePath = "../../../input.txt";

        var words = new List<string>();

        try
        {
            words = ReadFile(filePath);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e);
            throw;
        }

        SplitWordList(words);

        _notFullSizedWords.ForEach(word => MakeCombinations(word));


        _notFullSizedWords.ForEach(word =>
        {
            if (word.Combinations.Count == 0)
            {
                return;
            }

            word.Combinations.ForEach(Console.WriteLine);
        });
    }

    private static List<string> ReadFile(string filePath)
    {
        var words = File.ReadAllLines(filePath).ToList();
        return words;
    }

    public static void SplitWordList(List<string> words)
    {
        _fullSizedWords.AddRange(words.Where(word => word.Length == MaxNrOfLetters).Distinct());

        var notFullSized = words.Where(word => word.Length != MaxNrOfLetters).Distinct().ToList();
        notFullSized.ForEach(word => _notFullSizedWords.Add(new Word() { Value = word }));
    }

    public static void MakeCombinations(Word word, int nrOfWordsInCombination = 2)
    {
        var nrOfAvailableLetters = MaxNrOfLetters - word.Value.Length;

        _notFullSizedWords.ForEach(part =>
        {
            if (part.Value == word.Value || part.Value.Length != nrOfAvailableLetters)
            {
                return;
            }

            var combo = part.Value + word.Value;

            //only add the combination if it's valid
            if (_fullSizedWords.Contains(combo))
            {
                word.Combinations.Add(new CombinationWord()
                {
                    Value = combo, WordsNeeded = new List<Word>() { part, word }
                });
            }
        });
    }
}