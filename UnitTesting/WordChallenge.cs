using Kenze6LetterWordChallenge;

namespace UnitTesting;

[TestClass]
public class WordChallenge
{
    [TestInitialize]
    public void Setup()
    {
        Program._fullSizedWords = new List<string>();
        Program._notFullSizedWords = new List<Word>();
    }

    [DataTestMethod]
    [DataRow("foo", "bar", "foobar", 1, 2)]
    [DataRow("giblet", "tromie", "leader", 3, 0)]
    [DataRow("g", "tr", "lead", 0, 3)]
    public void SplitWordList(string word1, string word2, string word3, int fullSize, int notFullSize)
    {
        //Arrange
        var words = new List<string>()
        {
            word1, word2, word3
        };

        //Act
        Program.SplitWordList(words);

        //Assert
        Assert.AreEqual(fullSize, Program._fullSizedWords.Count);
        Assert.AreEqual(notFullSize, Program._notFullSizedWords.Count);
    }

    [TestMethod]
    public void MakeCombinations()
    {
        //Arrange
        var wordToCheck = new Word() { Value = "bar" };
        const string fullSize = "foobar";
        
        Program._fullSizedWords.Add(fullSize);
        Program._notFullSizedWords.AddRange(new List<Word>()
        {
            new Word() {Value = "foo"},
            wordToCheck
        });
        
        //Act
        Program.MakeCombinations(wordToCheck);
        
        //Assert
        Assert.AreEqual(1 ,Program._notFullSizedWords.Count(word => word.Value == wordToCheck.Value));
        var combinations = Program._notFullSizedWords.Single(word => word.Value == wordToCheck.Value).Combinations;
        Assert.AreEqual(fullSize, combinations.First().Value);
    }
}