using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using MySqlConnector;


namespace ExpenseTrackerApi.Services;

public class ExpenseService
{ 
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
    public static (bool boolean,string line) DeleteExpense(int id)
    {
        if (ExpenseService.ExpenseIsReal(id) == false)
        {
            return (false,"There is no Expense with this id");
        }
        else
        {
            string query = $"delete from expenses where id = {id};";
            DataBase.Service.SqlNonQuery(query);
            return (true,"Expense Deleted");
        }
    }
    public static string GetEveryExpense(
        int? id,
        string? name,
        decimal? value,
        string? category,
        DateTime? date
    )
    {
        string query = "SELECT * FROM expenses WHERE 1=1";
        if (id != null)
        {
            query += $"\n AND Id = {id}";
        }
        if (name != null)
        {
            query += $"\n AND Name = '{name}'";
        }
        if (value != null)
        {
            query += $"\n AND Value = {value}";
        }
        if (category != null)
        {
            query += $"\n AND Category = '{category}'";
        }
        if (date != null)
        {
            query += $"\n AND Date = '{date}'";
        }
        query = query + ";";

        return DataBase.Service.SqlRead(query);
    }
    public static (bool boolean,object obj) CreateExpense(Expense expense)
    {
        if(expense.Value <= 0)
        {
            
        }
        string query = $"Insert into expenses (Name,Value,Data,Category) Values (@name,@value,@date,@category)";
        DataBase.Service.SqlNonQueryExp(query,expense);

        return (true,expense);
    }
    public static (bool boolean,string? line) GetExpense(int id)
    {
        if(ExpenseService.ExpenseIsReal(id) == false)
        {
            return (false,null);
        }
        else
        {
            string query = $"SELECT * FROM expensetracker.expenses where id = {id};";
            return (true,DataBase.Service.SqlRead(query));
        }
    }
    public static bool ChangeExpense(int id,Expense expense)
    {
        if (ExpenseIsReal(id) == true && expense.Id > 0)
        {
            string query = $"UPDATE expenses SET Name = @name,Value = @value,Data = @date,Category = @category WHERE id = @id";
            DataBase.Service.SqlNonQueryExp(query,expense);
            return true;
        }
        else
        {
            return false;
        }
    }
}