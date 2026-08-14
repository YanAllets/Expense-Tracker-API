using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Services;

public class ExpenseService
{
    public List<Expense> expenses { get; set;} = new List<Expense>
    {
        new Expense
        {
        Id = 1,
        Name = "Bananas",
        Value = 10,
        Category = "Mercado",
        Date = new DateTime(2026, 10, 10)
        },

        new Expense
        {
        Id = 2,
        Name = "Maçãs",
        Value = 20,
        Category = "Feira",
        Date = new DateTime(2027, 10, 10)
        }
    };
    
    public void WriteList(List<Expense> List)
    {
        foreach(Expense expense in List)
        {
            System.Console.WriteLine(expense);
        }
    }
    public (bool,Expense? expense) ListIsReal(int id)
    {
        foreach(Expense expense in expenses)
        {
            if(expense.Id == id)
            {
                return (true,expense);
            }
        }
        return (false,null);
    }
}