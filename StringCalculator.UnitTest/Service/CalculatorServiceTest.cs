using Moq;
using StringCalculator.Entities;
using StringCalculator.Service;
using StringCalculator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.UnitTest.Service
{
    public class CalculatorServiceTest
    {
        private readonly Mock<IParser> _mockParser = new Mock<IParser>();
        private readonly Mock<IValidator> _mockValidator = new Mock<IValidator>();
        private readonly CalculatorService _calculatorService;

        public CalculatorServiceTest()
        {
            _calculatorService = new CalculatorService(_mockParser.Object, _mockValidator.Object);
        }
        [Fact]
        public void Evaluate_ShouldReturnCorrectSum_WhenOperationIsAdd()
        {
            // Arrange
            var input = "1,2,3";
            var operation = OperationType.Add;
            var numbers = new List<int> { 1, 2, 3 };
            var formulaParts = new List<string> { "1", "2", "3" };
            var expectedResult = 6;
            var expectedFormula = "1+2+3 = 6";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }

        [Fact]
        public void Evaluate_ShouldReturnCorrectMultiplication_WhenOperationIsMultiply()
        {
            // Arrange
            var input = "2,3,4";
            var operation = OperationType.Multiply;
            var numbers = new List<int> { 2, 3, 4 };
            var formulaParts = new List<string> { "2", "3", "4" };
            var expectedResult = 24;
            var expectedFormula = "2*3*4 = 24";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }

        [Fact]
        public void Evaluate_ShouldThrowException_WhenNegativeNumbersArePresent()
        {
            // Arrange
            var input = "1,-2,3";
            var operation = OperationType.Add;
            var numbers = new List<int> { 1, -2, 3 };
            var formulaParts = new List<string> { "1", "-2", "3" };

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers)).Throws(new ArgumentException("Negatives not allowed: -2"));

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _calculatorService.Evaluate(input, operation));
            Assert.Equal("Negatives not allowed: -2", exception.Message);
        }
        [Fact]
        public void Evaluate_ShouldReturnSumOfTwoNumbers_WhenGivenTwoNumbers()
        {
            // Arrange
            var input = "20,5000";
            var operation = OperationType.Add;
            var numbers = new List<int> { 20, 5000 };
            var formulaParts = new List<string> { "20", "5000" };
            var expectedResult = 5020;
            var expectedFormula = "20+5000 = 5020";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }

        [Fact]
        public void Evaluate_ShouldThrowException_WhenMoreThanTwoNumbersAreProvided()
        {
            // Arrange
            var input = "1,2,3";
            var operation = OperationType.Add;
            var numbers = new List<int> { 1, 2, 3 };
            var formulaParts = new List<string> { "1", "2", "3" };

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _calculatorService.Evaluate(input, operation));
            Assert.Equal("More than 2 numbers are not allowed", exception.Message);
        }

        [Fact]
        public void Evaluate_ShouldConvertEmptyOrMissingNumbersToZero()
        {
            // Arrange
            var input = "5,,";
            var operation = OperationType.Add;
            var numbers = new List<int> { 5, 0 };
            var formulaParts = new List<string> { "5", "0" };
            var expectedResult = 5;
            var expectedFormula = "5+0 = 5";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }

        [Fact]
        public void Evaluate_ShouldConvertInvalidNumbersToZero()
        {
            // Arrange
            var input = "5,tytyt";
            var operation = OperationType.Add;
            var numbers = new List<int> { 5, 0 };
            var formulaParts = new List<string> { "5", "0" };
            var expectedResult = 5;
            var expectedFormula = "5+0 = 5";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }
        [Fact]
        public void Evaluate_ShouldReturnSum_WhenMoreThanTwoNumbersAreProvidedAfterRemovingConstraint()
        {
            // Arrange
            var input = "1,2,3,4,5,6,7,8,9,10,11,12";
            var operation = OperationType.Add;
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var formulaParts = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            var expectedResult = 78;
            var expectedFormula = "1+2+3+4+5+6+7+8+9+10+11+12 = 78";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }
        [Fact]
        public void Evaluate_ShouldReturnSum_WhenUsingNewlineCharacterAsDelimiter()
        {
            // Arrange
            var input = "1\n2,3";
            var operation = OperationType.Add;
            var numbers = new List<int> { 1, 2, 3 };
            var formulaParts = new List<string> { "1", "2", "3" };
            var expectedResult = 6;
            var expectedFormula = "1+2+3 = 6";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }
        [Fact]
        public void Evaluate_ShouldThrowException_WhenNegativeNumbersAreProvided()
        {
            // Arrange
            var input = "1,-2,3";
            var operation = OperationType.Add;
            var numbers = new List<int> { 1, -2, 3 };
            var formulaParts = new List<string> { "1", "-2", "3" };

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers)).Throws(new ArgumentException("Negatives not allowed: -2"));

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _calculatorService.Evaluate(input, operation));
            Assert.Equal("Negatives not allowed: -2", exception.Message);
        }
        [Fact]
        public void Evaluate_ShouldIgnoreNumbersGreaterThan1000()
        {
            // Arrange
            var input = "2,1001,6";
            var operation = OperationType.Add;
            var numbers = new List<int> { 2, 0, 6 };  // 1001 should be treated as 0
            var formulaParts = new List<string> { "2", "0", "6" };
            var expectedResult = 8;
            var expectedFormula = "2+0+6 = 8";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }
        [Fact]
        public void Evaluate_ShouldSupportSingleCharacterCustomDelimiter()
        {
            // Arrange
            var input = "//#\n2#5";
            var operation = OperationType.Add;
            var numbers = new List<int> { 2, 5 };
            var formulaParts = new List<string> { "2", "5" };
            var expectedResult = 7;
            var expectedFormula = "2+5 = 7";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }
        [Fact]
        public void Evaluate_ShouldSupportCustomDelimiterOfAnyLength()
        {
            // Arrange
            var input = "//[***]\n11***22***33";
            var operation = OperationType.Add;
            var numbers = new List<int> { 11, 22, 33 };
            var formulaParts = new List<string> { "11", "22", "33" };
            var expectedResult = 66;
            var expectedFormula = "11+22+33 = 66";

            _mockParser.Setup(p => p.ParseInput(input, out formulaParts)).Returns(numbers);
            _mockValidator.Setup(v => v.ValidateNumbers(numbers));

            // Act
            var (result, formula) = _calculatorService.Evaluate(input, operation);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedFormula, formula);
        }        

    }
}
