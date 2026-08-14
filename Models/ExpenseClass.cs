namespace ExpenseTrackerApi.Models;

public class Expense
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Value { get; set; }

    public string Category { get; set; }

    public DateTime Date { get; set; }
}