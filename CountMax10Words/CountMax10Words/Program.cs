using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        int X = 10;
        string[] words = ReadFile();
        Dictionary<string, int> WordNumber = CountSymbols(words);
        ShowMaxXValue(WordNumber, X);
    }

    
    /// <summary>
    /// Reads text file and split words excluding punctuation symbols
    /// </summary>
    /// <returns></returns>
    private static string[] ReadFile()
    {
        string text = File.ReadAllText("C:\\CSharp\\SF\\Module13\\CountMax10Words\\Text1.txt");

        // keep delimeters to separate words
        var punctuationSymbols = new List<char>(text.Where(c => char.IsPunctuation(c)).ToList());
        var additionalSymbols = new List<char> { ' ', '\r', '\n' };
        punctuationSymbols.AddRange(additionalSymbols);
        char[] delimiters = punctuationSymbols.ToArray();

        // separate string in to words from file by delimeters
        var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        return words;
    }

    /// <summary>
    /// Counts number of different words from string array and compile pairs[word,number] to Dictionary
    /// </summary>
    /// <param name="words"></param>
    private static Dictionary<string, int> CountSymbols(string[] words)
    {
        var wordsNumber = words
                               .GroupBy(element => element)
                               .Select(count => new { Word = count.Key, Count = count.Count() })
                               .ToDictionary(dict => dict.Word, dict => dict.Count);

        return wordsNumber;
    }
    
    /// <summary>
    /// Shows max X values in Dictionary on console
    /// </summary>
    /// <param name="wordNumber"></param>
    /// <param name="x"></param>
    private static void ShowMaxXValue(Dictionary<string, int> wordNumber, int x)
    {
        int maxValue = 0;
        for (int i = 0; i < x; i++)
        {
            maxValue = wordNumber.Values.Max();
            var element = wordNumber.First(item => item.Value == maxValue).Key;
            Console.WriteLine($"Слово <{element}> встречается в тексте {maxValue} раз");
            wordNumber.Remove(element);
        }
    }
}
