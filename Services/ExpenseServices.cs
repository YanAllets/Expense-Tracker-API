using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Services;

public class ExpenseService
{
    public List<Expense> CreateList()
    {
        List<Expense> expenses = new List<Expense>();

        Expense expense1 = new Expense();
        expense1.Name = "Bananas";
        expense1.Value = 10;
        expense1.Id = 1;
        expense1.Category = "Mercado";
        expense1.Date = new DateTime(2026,10,10);
        expenses.Add(expense1);

        Expense expense2 = new Expense();
        expense2.Name = "Maças";
        expense2.Value = 20;
        expense2.Id = 2;
        expense2.Category = "Feira";
        expense2.Date = new DateTime(2027,10,10);
        expenses.Add(expense2);
        return expenses;
    }
}