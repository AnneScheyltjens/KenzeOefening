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
        notFullSized.ForEach(word => _notFullSizedWords.Add(word));
    }

    public static void MakeCombinations()
    {
        //make every combo of 2 words
        _notFullSizedWords.ForEach(word =>
        {
            //add new word to combo
            _notFullSizedWords.ForEach(word2 =>
            {
                if (word2 != word)
                {
                    //initialize new combo
                    var combo = new CombinationWord() { Value = word, WordsNeeded = new List<string>() { word } };

                    combo.AddWord(word2);

                    //save new combo
                    _CombinationWords.Add(combo);
                }
            });
        });

        FilterOnCriteria();

        //repeat for number of word needed
        for (int currentNrOfCombinations = 2; currentNrOfCombinations < NrOfWordsNeeded; currentNrOfCombinations++)
        {
            AddAnotherWordToCombos();
            FilterOnCriteria();
        }

        //check if combo in file
        _CombinationWords = _CombinationWords.Where(combo => _fullSizedWords.Contains(combo.Value)).ToList();
    }

    public static void FilterOnCriteria()
    {
        //check the max letter length
        _CombinationWords = _CombinationWords.Where(combo => combo.Value.Length <= MaxNrOfLetters).ToList();

        //check if there is a possibility that it might be in the file
        _CombinationWords = _CombinationWords.Where(combo => _fullSizedWords.Any(word => word.Contains(combo.Value)))
            .ToList();
    }

    public static void AddAnotherWordToCombos()
    {
        var newComboList = new List<CombinationWord>();
        _CombinationWords.ForEach(combo =>
        {
            _notFullSizedWords.ForEach(word =>
            {
                var newcombo = new CombinationWord(combo);

                //if word is already in this combo => skip
                if (!combo.WordsNeeded.Contains(word))
                {
                    newcombo.AddWord(word);
                    newComboList.Add(newcombo);
                }
            });
        });
        _CombinationWords = newComboList;
    }
}