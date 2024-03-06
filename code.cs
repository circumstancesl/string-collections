using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        static string ReplacePhoneNumber(string input, Match match)
        {
            string phoneNumber = match.Value;
            string replacedPhoneNumber = "+380 " + phoneNumber.Substring(2, 2) + " " + phoneNumber.Substring(6, 3)
                                         + " " + phoneNumber.Substring(10, 2) + " " + phoneNumber.Substring(13, 2);

            return input.Replace(match.Value, replacedPhoneNumber);
        }

        Dictionary<string, string> mistakesDictionary = new Dictionary<string, string>
        {
            {"первет", "привет"},
            {"пирвет", "привет"},
            {"п1рвет", "привет"},
            {"пкрвет", "привет"},
            {"первет", "привет"},
            {"пнрвет", "привет"},
            {"пгрвет", "привет"},
            {"пшрвет", "привет"},
            {"пщрвет", "привет"},
            {"пзрвет", "привет"},
            {"пррвет", "привет"},
            {"ппрвет", "привет"},
            {"парвет", "привет"},
            {"пмрвет", "привет"},
            {"пярвет", "привет"},
            {"пчрвет", "привет"},
            {"псрвет", "привет"},
            {"пмрвет", "привет"},
            {"пирвет", "привет"},
            {"птрвет", "привет"},
            {"пьрвет", "привет"},
            {"пбрвет", "привет"}
        };

        string phonePattern = @"\(\d{3}\) \d{3}-\d{2}-\d{2}";

        Console.Write("Введите путь к директории: ");
        string directoryPath = Console.ReadLine();

        if (!Directory.Exists(directoryPath))
        {
            Console.WriteLine("Указанная директория не существует.");
            return;
        }

        foreach (string filePath in Directory.EnumerateFiles(directoryPath, "*.txt"))
        {
            string textFile = File.ReadAllText(filePath);

            foreach (KeyValuePair<string, string> mistake in mistakesDictionary)
            {
                textFile = textFile.Replace(mistake.Key, mistake.Value);
            }

            MatchCollection matches = Regex.Matches(textFile, phonePattern);

            foreach (Match match in matches)
            {
                textFile = ReplacePhoneNumber(textFile, match);
            }

            File.WriteAllText(filePath, textFile);
        }

        Console.WriteLine("Замена успешно выполнена");
        Console.ReadKey();
    }
}
