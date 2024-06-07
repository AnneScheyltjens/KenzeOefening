using Kenze6LetterWordChallenge;

namespace UnitTesting;

[TestClass]
public class WordChallenge
{
    [TestInitialize]
    public void Setup()
    {
        Program._fullSizedWords = new List<string>();
        Program._notFullSizedWords = new List<string>();
        Program._CombinationWords = new List<CombinationWord>();
    }

    [DataTestMethod]
    [DataRow("foo", "bar", "foobar", 1, 2)]
    [DataRow("giblet", "tromie", "leader", 3, 0)]
    [DataRow("g", "tr", "lead", 0, 3)]
    public void SplitWordListTest(string word1, string word2, string word3, int fullSize, int notFullSize)
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
    public void MakeCombinationsTest()
    {
        //Arrange
        const string fullSize = "foobar";

        Program._fullSizedWords.Add(fullSize);
        Program._notFullSizedWords.AddRange(new List<string>()
        {
            "foo",
            "bar"
        });

        //Act
        Program.MakeCombinations();

        //Assert
        Assert.AreEqual(1, Program._CombinationWords.Count);
        Assert.AreEqual(fullSize, Program._CombinationWords[0].Value);
    }
    
    [TestMethod]
    public void FilterCriteriaTest()
    {
        //Arrange
        var overMaxLength = new CombinationWord() {Value = "foobary", WordsNeeded = new List<string>() {"foobar", "y"}};
        
        Program._fullSizedWords.Add("foobar");
        Program._CombinationWords = new List<CombinationWord>()
        {
            new CombinationWord() {Value = "foo", WordsNeeded = new List<string>() {"fo", "o"}},
            new CombinationWord() {Value = "bar", WordsNeeded = new List<string>() {"b", "ar"}},
            new CombinationWord() {Value = "lepel", WordsNeeded = new List<string>() {"lep", "el"}},
            overMaxLength
        };

        //Act
        Program.FilterOnCriteria();

        //Assert
        Assert.AreEqual(2, Program._CombinationWords.Count);
        Assert.IsFalse(Program._CombinationWords.Contains(overMaxLength));
    }
    
     [TestMethod]
    public void AddAnotherWordToCombosTest()
    {
        //Arrange
        Program._notFullSizedWords = new List<string>()
        {
            "fo", "o", "b", "ar"
        };
        
        Program._CombinationWords = new List<CombinationWord>()
        {
            new CombinationWord() {Value = "foo", WordsNeeded = new List<string>() {"fo", "o"}},
            new CombinationWord() {Value = "bar", WordsNeeded = new List<string>() {"b", "ar"}},
        };

        //Act
        Program.AddAnotherWordToCombos();

        //Assert
        Assert.AreEqual(4, Program._CombinationWords.Count);
        Assert.AreEqual("foob", Program._CombinationWords[0].Value);
    }
    
    
}