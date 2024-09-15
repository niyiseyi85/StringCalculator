using StringCalculator.Entities;
using StringCalculator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Service
{
    public interface ICalculatorService
    {
        (int result, string formula) Evaluate(string input, OperationType operation);
    }
    public class CalculatorService : ICalculatorService
    {
        private readonly IParser _parser;
        private readonly IValidator _validator;

        public CalculatorService(IParser parser, IValidator validator) 
        {
            _parser = parser;
            _validator = validator;
        }
        public (int result, string formula) Evaluate(string input, OperationType operation)
        {
            // Parse numbers
            List<int> numbers = _parser.ParseInput(input, out List<string> formulaParts);

            // Validate numbers (e.g. handle negatives)
            _validator.ValidateNumbers(numbers);

            // Calculate result based on the operation
            int result = Calculate(numbers, operation);

            // Build the formula string
            string formula = string.Join(GetOperatorSymbol(operation), formulaParts) + $" = {result}";

            return (result, formula);
        }
        private int Calculate(List<int> numbers, OperationType operation)
        {
            int result = operation == OperationType.Multiply ? 1 : 0;

            foreach (var num in numbers)
            {
                switch (operation)
                {
                    case OperationType.Add:
                        result += num;
                        break;
                    case OperationType.Subtract:
                        result -= num;
                        break;
                    case OperationType.Multiply:
                        result *= num;
                        break;
                    case OperationType.Divide:
                        if (num != 0)
                            result /= num;
                        break;
                }
            }
            return result;
        }

        private string GetOperatorSymbol(OperationType operation)
        {
            switch (operation)
            {
                case OperationType.Add:
                    return "+";
                case OperationType.Subtract:
                    return "-";
                case OperationType.Multiply:
                    return "*";
                case OperationType.Divide:
                    return "/";
                default:
                    return "+";
            }
        }
    }
}
