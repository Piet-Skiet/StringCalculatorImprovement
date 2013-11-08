using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Application
{
    public class StringCalculator
    {
        const string Message = "Negatives not Allowed!";
        public int Add(string input)
        {
            var delimiterList = new List<string> { ",", "\n" };
            if (string.IsNullOrEmpty(input)) return 0;
            input = HandleCustomDefinedDelimitersOfAnyLength(input, delimiterList);
            var numbers = input.Split(delimiterList.ToArray(),StringSplitOptions.None).Select(int.Parse).ToList();
            CheckForNumbersGreaterThan_aThousand(numbers); 
            CheckForNegativeNumbers(numbers);
            return numbers.Sum();
        }

        private static void CheckForNegativeNumbers(List<int> numbers)
        {
            var negativeNumbers = numbers.Where(x => x < 0);
            if (negativeNumbers.Any())
            {
                throw new Exception(Message + negativeNumbers);
            }
        }

        private void CheckForNumbersGreaterThan_aThousand(List<int> numbers)
        {
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                if (numbers[i] > 1000)
                {
                    numbers.RemoveAt(i);
                }
            }
        }

        private string HandleCustomDefinedDelimitersOfAnyLength(string input, List<string> delimiterList)
        {
            if (input.StartsWith("//["))
            {
                var stripTheStringToGetTheDelimiters = (input.Substring(3, input.IndexOf("]\n", StringComparison.Ordinal) - 3));
                if (input.Contains("]["))
                {
                    var variousDelimiterSeparators = new List<string> {"[", "][", "]"};
                    var variousDelimiters =
                        stripTheStringToGetTheDelimiters.Split(variousDelimiterSeparators.ToArray(), StringSplitOptions.None)
                            .ToList();
                    delimiterList.AddRange(variousDelimiters);
                }
                delimiterList.Add(stripTheStringToGetTheDelimiters);
                input = input.Substring(input.IndexOf("]\n", StringComparison.Ordinal) + 2);
            }
            else if (input.StartsWith("//"))
            {
                delimiterList.Add(input.Substring(2, input.IndexOf("\n", StringComparison.Ordinal) - 2));
                input = input.Substring(input.IndexOf("\n", StringComparison.Ordinal) + 1);
            }
            return input;
        }
    }
}