using ConsoleApp1;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestProject1;

public class TestConsole : IConsole
{
    List<string> _lines = ["1", "1", "1", "2", "1", "1"];
    public void WriteLine(string? message = null)
    {
    }

    public string? ReadLine()
    {
        if (_lines.Count > 1)
        {
            var line = _lines[0];
            _lines.RemoveAt(0);
            return line;
        }

        return "5";
    }
}

[TestFixture]
public class Tests
{
    private ICalculator _calculator = new Calculator(new TestConsole());

    [Test]
    public void TestInitCalculator()
    {
        _calculator = new Calculator(new TestConsole());
        _calculator.Should().NotBeNull();
    }
    
    [Test]
    public void TestAdd()
    {
        var result = _calculator.Add(1, 2);
        using (new AssertionScope())
        {
            result.Should().Be(3);
        }
    }
    
    [Test]
    public void TestSubtract()
    {
        var result = _calculator.Subtract(2, 1);
        using (new AssertionScope())
        {
            result.Should().Be(1);
        }
    }
    
    [Test]
    public void TestMultiply()
    {
        var result = _calculator.Multiply(2, 3);
        using (new AssertionScope())
        {
            result.Should().Be(6);
        }
    }
    
    [Test]
    public void TestDivide()
    {
        var result = _calculator.Divide(6, 3);
        using (new AssertionScope())
        {
            result.Should().Be(2);
        }
    }
    
    [Test]
    public void TestConsolePrint()
    {
        _calculator.
            PrintRows();
    }
    
    [Test]
    public void TestCalculatorRun()
    {
        _calculator.RunCalculator();
    }
    
}