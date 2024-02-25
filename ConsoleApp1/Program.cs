// See https://aka.ms/new-console-template for more information


using ConsoleApp1;


RunCalculator();

void RunCalculator()
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

