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
    public static List<ExpenseClass> GetEveryExpense(
        int? page,
        int? pageSize,
        int? id,
        string? name,
        decimal? value,
        string? category,
        DateTime? date
    )
    {
        string query = "SELECT * FROM expenses WHERE 1=1";
        int? offset = (page - 1) * pageSize;

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
        if (page != null && pageSize != null)
        {
            query += $"\n LIMIT {pageSize} offset {offset}";
        }

        query = query + ";";

        return DataBase.Service.SqlReadExpense(query);
    }
    public static (bool boolean,object obj) CreateExpense(ExpenseClass expense)
    {
        if(Validate(expense) == true)
        {
            string query = $"Insert into expenses (Name,Value,Date,Category) Values (@name,@value,@date,@category)";
            DataBase.Service.SqlNonQueryExp(query,expense);
            return (true,expense);
        }
        else
        {
            return (false,null);
        }
    }
    public static (bool boolean,List<ExpenseClass>? line) GetExpense(int id)
    {
        if(ExpenseService.ExpenseIsReal(id) == false)
        {
            return (false,null);
        }
        else
        {
            string query = $"SELECT * FROM expensetracker.expenses where id = {id};";
            return (true,DataBase.Service.SqlReadExpense(query));
        }
    }

    public static bool ChangeExpense(int id,ExpenseClass expense)
    {
        if (ExpenseIsReal(id) == true && expense.Value > 0)
        {
            string query = $"UPDATE expenses SET Name = @name,Value = @value,Date = @date,Category = @category WHERE id = {id}";
            DataBase.Service.SqlNonQueryExp(query,expense);
            return true;
        }
        else
        {
            return false;
        }
    }
    //checks if expense is valid by rejecting null,empty or invalid values 
    public static bool Validate(ExpenseClass expense)
    {
        if(
            string.IsNullOrWhiteSpace(expense.Name) ||
            string.IsNullOrWhiteSpace(expense.Category) ||
            expense.Value <= 0
        )
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public static List<CategoryClass> SpendByCategory()
    {
        string query = $"Select category,Sum(Value) as Value FROM expenses group by category";
        return DataBase.Service.SqlReadCategory(query);
    }
}