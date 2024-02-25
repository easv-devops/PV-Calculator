using ConsoleApp1;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestProject1;

[TestFixture]
public class Tests
{
    private ICalculator _calculator = new Calculator();

    [Test]
    public void TestInitCalculator()
    {
        _calculator = new Calculator();
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
    public void TestRunCalculator()
    {
        Program.Run([]);
    }
}