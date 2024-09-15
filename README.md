# StringCalculator

## Description

The StringCalculator is a console application designed to perform mathematical operations based on formatted string inputs. The application supports various delimiters and handles multiple numbers, negative numbers, and values exceeding a specified upper limit. It also includes functionality for custom delimiters and arithmetic operations such as addition, subtraction, multiplication, and division.

## Features

- **Supports multiple delimiters:** Handles comma `,`, newline `\n`, and custom delimiters of any length.
- **Handles various input cases:**
  - Supports up to 2 numbers by default, with exceptions for more than 2 numbers.
  - Converts empty or missing numbers to 0.
  - Validates and converts invalid numbers to 0.
  - Throws exceptions for negative numbers.
  - Ignores numbers above a specified upper bound.
- **Custom delimiters:** Allows single and multi-character custom delimiters.
- **Arithmetic Operations:** Supports addition, subtraction, multiplication, and division.
- **Displays the formula:** Shows the calculation formula used to compute the result.

## Getting Started

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet) or compatible runtime (if updating from an older framework, ensure compatibility)
- A Git client (e.g., GitHub Desktop, Git command line)

### Installation

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/yourusername/StringCalculator.git
    cd StringCalculator
    ```

2. **Build the Solution:**

    Open the solution in Visual Studio or your preferred .NET IDE and build the solution. Alternatively, use the .NET CLI:

    ```bash
    dotnet build
    ```

### Running the Application

1. **Run the Console Application:**

    To run the console application, use the following command in the terminal:

    ```bash
    dotnet run --project StringCalculator.ConsoleApp
    ```

2. **Input Example:**

    Enter the formatted string when prompted. For example:

    ```text
    1\n2,3
    ```

    This will output the result of the calculation based on the provided input.

### Running Unit Tests

1. **Run Unit Tests:**

    To execute the unit tests, use the following command:

    ```bash
    dotnet test
    ```

    Ensure that the unit test project targets the same .NET version as the application to avoid compatibility issues.

### Configuration

- **Custom Delimiters:**

    To specify custom delimiters, use the format:

    ```text
    //[delimiter]\n{numbers}
    ```

    Example:

    ```text
    //[***]\n11***22***33
    ```

- **Negative Numbers:**

    By default, the application denies negative numbers. To modify this behavior, you need to update the configuration or implementation as required.

- **Upper Bound:**

    The default upper bound is set to 1000. This can be configured in the application settings or code.

## Contributing

Contributions are welcome! Please fork the repository and submit pull requests for any improvements or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
