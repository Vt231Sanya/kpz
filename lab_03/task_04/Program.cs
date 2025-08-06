using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

interface ISmartTextReader
{
    char[][] ReadFile(string path);
}

class SmartTextReader : ISmartTextReader
{
    public char[][] ReadFile(string path)
    {
        var lines = File.ReadAllLines(path);
        var result = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            result[i] = lines[i].ToCharArray();
        }
        return result;
    }
}

class SmartTextChecker : ISmartTextReader
{
    private ISmartTextReader _reader;

    public SmartTextChecker(ISmartTextReader reader)
    {
        _reader = reader;
    }

    public char[][] ReadFile(string path)
    {
        Console.WriteLine($"[Інфа] Відкриття файлу: {path}");
        var result = _reader.ReadFile(path);
        Console.WriteLine($"[Інфа] Файл успішно прочитано.");

        int lineCount = result.Length;
        int charCount = 0;
        foreach (var line in result)
            charCount += line.Length;

        Console.WriteLine($"[Інфа] Рядків: {lineCount}, Символів: {charCount}");
        Console.WriteLine($"[Інфа] Закриття файлу: {path}");
        return result;
    }
}

class SmartTextReaderLocker : ISmartTextReader
{
    private ISmartTextReader _reader;
    private Regex _blockedPattern;

    public SmartTextReaderLocker(ISmartTextReader reader, string pattern)
    {
        _reader = reader;
        _blockedPattern = new Regex(pattern, RegexOptions.IgnoreCase);
    }

    public char[][] ReadFile(string path)
    {
        if (_blockedPattern.IsMatch(path))
        {
            Console.WriteLine("Access denied!");
            return Array.Empty<char[]>();
        }
        return _reader.ReadFile(path);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;

        string testFile = "example.txt";
        File.WriteAllLines(testFile, new[] {
            "Аааллоо",
            "тест тест",
            "Аааа хочу спати!!"
        });

        Console.WriteLine("SmartTextReader з SmartTextChecker");
        ISmartTextReader checker = new SmartTextChecker(new SmartTextReader());
        checker.ReadFile(testFile);

        Console.WriteLine("\nSmartTextReader з SmartTextReaderLocker");
        ISmartTextReader locker = new SmartTextReaderLocker(new SmartTextReader(), @"secret|restricted|example\.txt");
        locker.ReadFile(testFile);

        Console.WriteLine("\nSmartTextReader з обома проксі");
        ISmartTextReader secureLogger = new SmartTextReaderLocker(
            new SmartTextChecker(new SmartTextReader()),
            @"forbidden|blocked"
        );
        secureLogger.ReadFile(testFile);
    }
}
