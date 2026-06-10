using System;
using System.Collections.Generic;

class Expense
{
    public string Category { get; set; } = "";
    public double Amount { get; set; }
    public DateTime Date { get; set; }
}

class Program
{
    static List<Expense> expenses = new List<Expense>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== Expense Tracker ===");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. Delete Last Expense");
            Console.WriteLine("4. Show Total");
            Console.WriteLine("5. Exit");
            Console.WriteLine("6. filter by category");
            Console.WriteLine("7. Show total by category");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddExpense();
                    break;
                case "2":
                    ViewExpenses();
                    break;
                case "3":
                    DeleteLast();
                    break;
                case "4":
                    ShowTotal();
                    break;
                case "5":
                    return;
                case "6":
                    FilterByCategory();
                    break;
                case "7":
                    ShowTotalByCategory();
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }



    }

    static void FilterByCategory()
    {
        Console.WriteLine("Enter category to filter:");
        string category = Console.ReadLine();

        var filtered = expenses.FindAll(e => e.Category.ToLower() == category.ToLower());

        Console.WriteLine($"\n--- Expenses for {category} ---");

        if (filtered.Count == 0)
        {
            Console.WriteLine("No expenses found.");
            return;
        }

        foreach (var e in filtered)
        {
            Console.WriteLine($"{e.Category} - {e.Amount} - {e.Date}");
        }
    }

    static void ShowTotalByCategory()
    {
        Console.WriteLine("\n--- Total by Category ---");

        var categories = expenses.Select(e => e.Category).Distinct();

        foreach (var category in categories)
        {
            double total = expenses
                .Where(e => e.Category == category)
                .Sum(e => e.Amount);

            Console.WriteLine($"{category} = {total}");
        }
    }

    static void AddExpense()
    {
        Console.WriteLine("Choose category:");
        Console.WriteLine("1. Food");
        Console.WriteLine("2. Shopping");
        Console.WriteLine("3. Transport");
        Console.WriteLine("4. Bills");
        Console.Write("Enter choice: ");

        string category = "";

        switch (Console.ReadLine())
        {
            case "1": category = "Food"; break;
            case "2": category = "Shopping"; break;
            case "3": category = "Transport"; break;
            case "4": category = "Bills"; break;
            default:
                category = "Other";
                break;
        }

        double amount;

        Console.Write("Enter amount: ");
        while (!double.TryParse(Console.ReadLine(), out amount))
        {
            Console.Write("❌ Invalid number. Enter amount again: ");
        }

        expenses.Add(new Expense
        {
            Category = category,
            Amount = amount,
            Date = DateTime.Now
        });

        Console.WriteLine("Expense added ✔");
    }

    static void ViewExpenses()
    {
        Console.WriteLine("\n--- Expenses ---");

        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses yet.");
            return;
        }

        for (int i = 0; i < expenses.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {expenses[i].Category} - {expenses[i].Amount} - {expenses[i].Date}");
        }
    }

    static void DeleteLast()
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("Nothing to delete.");
            return;
        }

        expenses.RemoveAt(expenses.Count - 1);
        Console.WriteLine("Last expense deleted 🗑️");
    }

    static void ShowTotal()
    {
        double total = 0;

        foreach (var e in expenses)
        {
            total += e.Amount;
        }

        Console.WriteLine($"Total Expenses: {total}");
    }
}