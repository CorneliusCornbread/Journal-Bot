using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using System.Text;

namespace Journal_Bot
{
    class Program
    {
        public const string QUOTES_FILE_PATH = "Quotes.json"; 

        static void Main(string[] args)
        {
            Random rand = new Random();

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("JOURNAL BOT 1.0");
            Console.WriteLine("Bot start");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine();

            if (!File.Exists(QUOTES_FILE_PATH))
            {
                throw new FileNotFoundException("Quotes json file not found.");
            }

            //string[] test = new string[]{"test1", "test2"};
            //Console.WriteLine(JsonSerializer.Serialize(test));
            //return;

            StreamReader jsonStream = File.OpenText(QUOTES_FILE_PATH);
            string jsonData = jsonStream.ReadToEnd();
            string[] quotes = JsonSerializer.Deserialize<string[]>(jsonData);

            int promptCount = 1;
            List<string> prompts = new List<string>(3);
            
            while (true)
            {
                Console.WriteLine($"Input empty input to exit. Input prompt {promptCount}:");
                promptCount++;

                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) {
                    break;
                }

                prompts.Add(input);
            }

            StringBuilder str = new StringBuilder();

            for (int i = 0; i < prompts.Count; i++)
            {
                str.Append(prompts[i]);
                str.AppendLine(":");
                str.Append(quotes[rand.Next(0, quotes.Length)]);

                if (i + 1 != prompts.Count)
                {
                    str.AppendLine("\n"); //Appends two lines
                }
            }
            
            Directory.CreateDirectory("Output");
            string outputFile = $"Output/Output {DateTime.Today.ToString().Replace('/', '-')}.txt";
            File.WriteAllText(outputFile, str.ToString());
        }
    }
}
