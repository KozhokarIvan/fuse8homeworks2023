using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using Fuse8_ByteMinds.SummerSchool.Domain;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;

// В 2 из 25 сравнений способ без интернирования оказался быстрее на примерно 1/3.
// Причем это первые 2 слова из 4 в выборке слов из начала словаря
// В остальных случаях способ с интернированием оказался быстрее. В лучшем случае на 24%, в худшем на 14%
//BenchmarkRunner.Run<StringInternBenchmark>();

BenchmarkRunner.Run<AccountProcessorBenchmark>();

[MemoryDiagnoser(displayGenColumns: true)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class StringInternBenchmark
{
    private readonly List<string> _words = new();
    private readonly string[] _wordsWithStringBuilder;
    private readonly string[] _wordsNotFromDictionaryWithoutStringBuilder;
    private readonly string[] _wordsFromBeginning;
    private readonly string[] _wordsFromMiddle;
    private readonly string[] _wordsFromEnd;

    private const string _word1 = "wordasdasdsss";
    private const string _word2 = "wordkjjkdfjkgdfg";
    private const string _word3 = "wordioasoidfjasdofi";
    private const string _word4 = "wordllllsssasd";
    private const string _word5 = "wordiiiisdfsdfsdfs";
    public StringInternBenchmark()
    {
        foreach (var word in File.ReadLines(@".\SpellingDictionaries\ru_RU.dic"))
            _words.Add(string.Intern(word));
        _wordsWithStringBuilder = new[]
        {
            new StringBuilder().Append("sbword").Append("asdasdasdasd").ToString(),
            new StringBuilder().Append("sbword").Append("asdjkkaklsafd").ToString(),
            new StringBuilder().Append("sbword").Append("dflaaaaaak").ToString(),
            new StringBuilder().Append("sbword").Append("askldaskldklasd").ToString(),
            new StringBuilder().Append("sbword").Append("kslgllllll").ToString(),
            new StringBuilder().Append("sbword").Append("aaaaaassssdasd").ToString()
        };
        _wordsNotFromDictionaryWithoutStringBuilder = new[] { _word1, _word2, _word3, _word4, _word5 };
        _wordsFromBeginning = new[] { "Чэнду", "Чжэнчжоу", "Винчи", "Варфоломеевичем" };
        _wordsFromMiddle = new[] { "помурлыкивают", "помоложе", "помимо", "помечу", "помелешь" };
        _wordsFromEnd = new[] { "альбигойцев", "альбедо", "аляскинцев", "аляскинцем", "алло" };
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(SampleData))]
    public bool WordIsExists(string word)
        => _words.Any(item => word.Equals(item, StringComparison.Ordinal));

    [Benchmark]
    [ArgumentsSource(nameof(SampleData))]
    public bool WordIsExistsIntern(string word)
    {
        var internedWord = string.Intern(word);
        return _words.Any(item => ReferenceEquals(internedWord, item));
    }
    public IEnumerable<string> SampleData()
    {
        

        foreach (var word in _wordsFromBeginning)
        {
            yield return new StringBuilder().Append(word).ToString();
        }

        foreach (var word in _wordsFromMiddle)
        {
            yield return new StringBuilder().Append(word).ToString();
        }

        foreach (var word in _wordsFromEnd)
        {
            yield return new StringBuilder().Append(word).ToString();
        }

        foreach (var word in _wordsWithStringBuilder)
        {
            yield return word;
        }

        foreach (var word in _wordsNotFromDictionaryWithoutStringBuilder)
        {
            yield return word;
        }
    }
}



//| Method                | Mean      | Error    | StdDev   | Ratio  | Rank      | Gen0        | Allocated  | Alloc Ratio  |
//| -------------------   | ---------:| --------:| --------:| ------:| -----:    | -------    :| ----------:| ------------:|
//| BenchmarkPerformed    | 285.8 ns  | 2.47 ns  | 2.31 ns  | 0.47   | 1         | -           | -          | 0.00         |
//| Benchmark             | 603.6 ns  | 6.48 ns  | 6.06 ns  | 1.00   | 2         | 0.4015      | 6720 B     | 1.00         |
[MemoryDiagnoser(displayGenColumns: true)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class AccountProcessorBenchmark
{
    AccountProcessor accountProcessor;
    BankAccount bankAccount;
    public AccountProcessorBenchmark()
    {
        accountProcessor = new AccountProcessor();
        bankAccount = new BankAccount();
    }

    [Benchmark]
    [ArgumentsSource(nameof(bankAccount))]
    public void BenchmarkPerformed()
    {
        accountProcessor.CalculatePerformed(ref bankAccount);
    }

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(bankAccount))]
    public void Benchmark()
    {
        accountProcessor.Calculate(bankAccount);
    }
}