using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator.Shared
{
    public interface IParser
    {
        List<int> ParseInput(string input, out List<string> formulaParts);
    }
    public class Parser : IParser
    {
        private readonly int _upperBound = 1000;
        public List<int> ParseInput(string input, out List<string> formulaParts)
        {
            input = input.Replace("\\n", "\n");
            // Always get delimiters (either custom or default ones)
            var delimiters = GetDelimiters(input);
            formulaParts = new List<string>();

            // If input starts with //, remove the custom delimiter section
            if (input.StartsWith("//"))
            {
                input = input.Substring(input.IndexOf('\n') + 1); // Remove custom delimiter part
            }

            // Split the input string by the delimiters (handles both default and custom delimiters)
            string[] numArray = input.Split(delimiters.ToArray(), StringSplitOptions.None);
            List<int> numbers = new List<int>();

            // Iterate through the numbers, validating and parsing each one
            foreach (var num in numArray)
            {
                if (int.TryParse(num, out int parsedNumber))
                {
                    // If number exceeds the upper bound, treat it as 0
                    if (parsedNumber > _upperBound)
                    {
                        parsedNumber = 0;
                    }
                    formulaParts.Add(parsedNumber.ToString()); // Add number to formula parts
                    numbers.Add(parsedNumber);                 // Add number to list
                }
                else
                {
                    // Invalid numbers are treated as 0
                    formulaParts.Add("0");
                    numbers.Add(0);
                }
            }

            return numbers;
        }
        private List<string> GetDelimiters(string input)
        {
            var delimiters = new List<string> { ",", "\n" }; // Default delimiters

            if (input.StartsWith("//"))
            {
                int delimiterEndIndex = input.IndexOf('\n');
                string delimiterSection = input.Substring(2, delimiterEndIndex - 2);

                if (delimiterSection.StartsWith("["))
                {
                    // Support multiple custom delimiters wrapped in []
                    var matches = Regex.Matches(delimiterSection, @"\[(.*?)\]");
                    foreach (Match match in matches)
                    {
                        delimiters.Add(match.Groups[1].Value); // Add custom delimiter
                    }
                }
                else
                {
                    // Support single character delimiter after //
                    delimiters.Add(delimiterSection);
                }
            }

            return delimiters;
        }
    }
}    
