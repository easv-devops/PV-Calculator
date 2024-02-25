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

public class Calculator : ICalculator
{
    private readonly SQLiteConnection _conn;

    public Calculator()
    {
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
            Console.WriteLine(
                $"id: {reader.GetInt32(0)}, operation: {reader.GetString(1)}, result: {reader.GetDouble(2)}, n1: {reader.GetDouble(3)}, n2: {reader.GetDouble(4)}");
    }

    public void RunCalculator()
    {
while (true)
{
    ICalculator calculator = new Calculator();
    // ask for math operation switch
    Console.WriteLine("Enter the math operation you want to perform: ");
    Console.WriteLine("1. Add");
    Console.WriteLine("2. Subtract");
    Console.WriteLine("3. Multiply");
    Console.WriteLine("4. Divide");
    Console.WriteLine("5. Exit");
    var operation = Console.ReadLine();
    if (operation == "5") break;
    
    if (operation == "0")
    {
        calculator.PrintRows();
        Console.WriteLine();
        continue;
    }
    
    // ask for numbers
    Console.WriteLine("Enter the first number: ");
    var n1 = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("Enter the second number: ");
    var n2 = Convert.ToDouble(Console.ReadLine());
    // perform operation
    double result = 0;
    switch (operation)
    {
        case "1":
            result = calculator.Add(n1, n2);
            break;
        case "2":
            result = calculator.Subtract(n1, n2);
            break;
        case "3":
            result = calculator.Multiply(n1, n2);
            break;
        case "4":
            if (n2 == 0)
            {
                Console.WriteLine("Cannot divide by zero");
                break;
            }
    
            result = calculator.Divide(n1, n2);
            break;
    
        default:
            Console.WriteLine("Invalid operation");
            break;
    }
    
    //change text color
    Console.ForegroundColor = ConsoleColor.Green;
    // print result
    Console.WriteLine("The result is: " + result);
    // reset text color
    Console.ResetColor();
    Console.WriteLine();
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