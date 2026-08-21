using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using MySqlConnector;


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
    public static bool ExpenseIsReal(int id)
    {
        string query = $"select count(*) from expenses where id = {id}";
        if (DataBase.Service.SqlScalar(query) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool ChangeExpense(int id,Expense expense)
    {
        if (ExpenseIsReal(id) == true)
        {
            expense.Id = id;
            string query = $"UPDATE expenses SET Name = @name,Value = @value,Data = @data,Category = @category WHERE id = @id";
            DataBase.Service.SqlNonQuery(query,expense);
            return true;
        }
        else
        {
            return false;
        }
        
    }
}