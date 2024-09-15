using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StringCalculator.Shared
{
    public interface IValidator
    {
        void ValidateNumbers(List<int> numbers);
    }
    public class Validator : IValidator
    {
        private readonly bool _denyNegatives;
        public void ValidateNumbers(List<int> numbers)
        {
            if (_denyNegatives)
            {
                var negativeNumbers = numbers.Where(n => n < 0).ToList();
                if (negativeNumbers.Any())
                {
                    throw new ArgumentException($"Negatives not allowed: {string.Join(",", negativeNumbers)}");
                }
            }
        }
    }
}
