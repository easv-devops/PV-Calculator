using System.Data.SQLite;

namespace ConsoleApp1;

public interface ICalculator
{
    double Add(double n1, double n2);
    double Subtract(double n1, double n2);
    double Multiply(double n1, double n2);
    double Divide(double n1, double n2);
    void PrintRows();
    void RunCalculator();
}

public interface IConsole
{
    void WriteLine(string? message = null);
    string? ReadLine();
}

public class ConsoleWrapper : IConsole
{
    public void WriteLine(string? message = null)
    {
        Console.WriteLine(message);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}


public class Calculator : ICalculator
{
    private readonly SQLiteConnection _conn;
    private readonly IConsole _console;
    public Calculator(IConsole console)
    {
        _console = console;
        _conn = CreateConnection();

        using var cmd = new SQLiteCommand(_conn);
        cmd.CommandText =
            "CREATE TABLE IF NOT EXISTS calculator (id INTEGER PRIMARY KEY, operation TEXT, result REAL, n1 REAL, n2 REAL)";
        cmd.ExecuteNonQuery();
    }

    public double Add(double n1, double n2)
    {
        var result = n1 + n2;
        using var cmd = new SQLiteCommand(_conn);
        cmd.CommandText = "INSERT INTO calculator (operation, result, n1, n2) VALUES ('add', @result, @n1, @n2)";
        cmd.Parameters.AddWithValue("@result", result);
        cmd.Parameters.AddWithValue("@n1", n1);
        cmd.Parameters.AddWithValue("@n2", n2);
        cmd.ExecuteNonQuery();
        return result;
    }

    public double Subtract(double n1, double n2)
    {
        var result = n1 - n2;
        using var cmd = new SQLiteCommand(_conn);
        cmd.CommandText = "INSERT INTO calculator (operation, result, n1, n2) VALUES ('subtract', @result, @n1, @n2)";
        cmd.Parameters.AddWithValue("@result", result);
        cmd.Parameters.AddWithValue("@n1", n1);
        cmd.Parameters.AddWithValue("@n2", n2);
        cmd.ExecuteNonQuery();
        return result;
    }

    public double Multiply(double n1, double n2)
    {
        var result = n1 * n2;
        using var cmd = new SQLiteCommand(_conn);
        cmd.CommandText = "INSERT INTO calculator (operation, result, n1, n2) VALUES ('multiply', @result, @n1, @n2)";
        cmd.Parameters.AddWithValue("@result", result);
        cmd.Parameters.AddWithValue("@n1", n1);
        cmd.Parameters.AddWithValue("@n2", n2);
        cmd.ExecuteNonQuery();
        return result;
    }

    public double Divide(double n1, double n2)
    {
        var result = n1 / n2;
        using var cmd = new SQLiteCommand(_conn);
        cmd.CommandText = "INSERT INTO calculator (operation, result, n1, n2) VALUES ('divide', @result, @n1, @n2)";
        cmd.Parameters.AddWithValue("@result", result);
        cmd.Parameters.AddWithValue("@n1", n1);
        cmd.Parameters.AddWithValue("@n2", n2);
        cmd.ExecuteNonQuery();
        return result;
    }

    public void PrintRows()
    {
        using var cmd = new SQLiteCommand(_conn);
        cmd.CommandText = "SELECT * FROM calculator";
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            _console.WriteLine(
                $"id: {reader.GetInt32(0)}, operation: {reader.GetString(1)}, result: {reader.GetDouble(2)}, n1: {reader.GetDouble(3)}, n2: {reader.GetDouble(4)}");
        _console.WriteLine();
    }

    public void RunCalculator()
    {
        while (true)
        {
            PrintOptions();

            var operation = _console.ReadLine();
            if (operation == "5") break;

            if (operation == "0")
            {
                PrintRows(); 
                continue;
            }

            // ask for numbers
            _console.WriteLine("Enter the first number: ");
            var n1 = Convert.ToDouble(_console.ReadLine());
            _console.WriteLine("Enter the second number: ");
            var n2 = Convert.ToDouble(_console.ReadLine());

            PrintResult(Calculate(n1, n2, operation!));
        }

    }

    private void PrintResult(double result)
    {
        // print result
        _console.WriteLine("The result is: " + result);
        _console.WriteLine();
    }
    private void PrintOptions()
    {
        // ask for math operation switch
        _console.WriteLine("Enter the math operation you want to perform: ");
        _console.WriteLine("1. Add");
        _console.WriteLine("2. Subtract");
        _console.WriteLine("3. Multiply");
        _console.WriteLine("4. Divide");
        _console.WriteLine("5. Exit");
    }

    private double Calculate(double n1, double n2, string operation)
    {
        switch (operation)
        {
            case "1":
                return Add(n1, n2);
            case "2":
                return Subtract(n1, n2);
            case "3":
                return Multiply(n1, n2);
            case "4":
                return Divide(n1, n2);
            default:
                _console.WriteLine("Invalid operation");
                return 0;
        }
    }

    private SQLiteConnection CreateConnection()
    {
        var sqliteConn =
            new SQLiteConnection("Data Source=calculator.db");
        // Open the connection:
        sqliteConn.Open();
        return sqliteConn;
    }
}