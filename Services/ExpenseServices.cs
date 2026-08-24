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
    public static bool ChangeExpense(int id,Expense expense)
    {
        if (ExpenseIsReal(id) == true)
        {
            expense.Id = id;
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