using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.UnitTest.Service
{
    public class CalculatorServiceTest
    {
		//ivate readonly Mock
		private readonly Mock<IParser> _mockParser;
		private readonly Mock<IValidator> _mockValidator;
		private readonly CalculatorService _calculatorService;

		public CalculatorServiceTest()
		{
			_mockParser = new Mock<IParser>();
			_mockValidator = new Mock<IValidator>();

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
	}
}
