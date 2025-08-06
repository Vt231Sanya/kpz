using System;
using System.IO;
using System.Text;

class Logger
{
    public void Log(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("[LOG] " + message);
        Console.ResetColor();
    }

    public void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[ERROR] " + message);
        Console.ResetColor();
    }

    public void Warn(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[WARNING] " + message);
        Console.ResetColor();
    }
}

class FileWriter
{
    private string filePath;

    public FileWriter(string path)
    {
        filePath = path;
    }

    public void Write(string text)
    {
        File.AppendAllText(filePath, text);
    }

    public void WriteLine(string text)
    {
        File.AppendAllText(filePath, text + Environment.NewLine);
    }
}

class FileLoggerAdapter
{
    private FileWriter writer;

    public FileLoggerAdapter(string filePath)
    {
        writer = new FileWriter(filePath);
    }

    public void Log(string message)
    {
        writer.WriteLine("[LOG] " + message);
    }

    public void Error(string message)
    {
        writer.WriteLine("[ERROR] " + message);
    }

    public void Warn(string message)
    {
        writer.WriteLine("[WARNING] " + message);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Logger consoleLogger = new Logger();
        consoleLogger.Log("Це інформаційне повідомлення)");
        consoleLogger.Warn("Це попередження :о");
        consoleLogger.Error("Це помилка(");

        Console.WriteLine("\nЗаписую у файлик log.txt\n");

        FileLoggerAdapter fileLogger = new FileLoggerAdapter("log.txt");
        fileLogger.Log("Це інформаційне повідомлення у файл)");
        fileLogger.Warn("Це попередження у файл :о");
        fileLogger.Error("Це помилка у файл(");
    }
}
