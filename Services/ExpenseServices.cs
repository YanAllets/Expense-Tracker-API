using ExpenseTrackerApi.Models;

namespace ExpenseTrackerApi.Services;
public class ExpenseService
{ 
    //checks wheter an expense with the specified ID exits in the database.
    public static bool ExpenseIsReal(int id)
    {
        string query = "select count(*) from expenses where id = @id";

        return DataBase.Service.SqlScalarExp(query,id) == 1;
    }

    //checks if expense is valid by rejecting null,empty or invalid values 
    // validates an expense before it is created or updated
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
    // retrieves expenses from the databse using optional filter and pagination
    public static (bool success,List<ExpenseClass> list) GetEveryExpense(
        int? page,
        int? pageSize,
        int? id,
        string? name,
        decimal? value,
        string? category,
        DateTime? date
    )
    
    {
        if(page < 1|| pageSize < 1||pageSize > 100)
        {
            return (false,null);
        }
        string query = "SELECT * FROM expenses WHERE 1=1";
        int? offset = (page - 1) * pageSize;

        if (id != null)
        {
            query += "\n AND Id = @id";
        }
        if (name != null)
        {
            query += "\n AND Name = @name";
        }
        if (value != null)
        {
            query += "\n AND Value = @value";
        }
        if (category != null)
        {
            query += "\n AND Category = @category";
        }
        if (date != null)
        {
            query += "\n AND Date = @date";
        }

        query += "\n order by id";

        if (page != null && pageSize != null)
        {
            query += $"\n LIMIT @pageSize offset @offset";
        }

        query = query + ";";

        return (true,DataBase.Service.SqlReadExpFilter(query,offset,pageSize,id,name,value,category,date));
    }
    // Retrieves a single expense by its ID.
    // Returns null when no expense is found in the database.
    public static (bool success,ExpenseClass? expenseClass) GetExpense(int id)
    {
        string query = "SELECT * FROM expenses where id = @id;";
        var result = DataBase.Service.SqlReadExpense(query,id);
        if(result == null)
        {
            return (false,null);
        }
        else
        {
            return (true,result);
        }
    }
    //create a new expense in the database (after validating)
    public static (bool success,ExpenseClass? expense) CreateExpense(ExpenseClass expense)
    {
        if(Validate(expense) == true)
        {
            string query = "Insert into expenses (Name,Value,Date,Category) Values (@name,@value,@date,@category)";
            DataBase.Service.SqlNonQueryExp(query,expense);
            return (true,expense);
        }
        else
        {
            return (false,null);
        }
    }
// updates an already existing expense using the ID provided.
    public static bool ChangeExpense(int id,ExpenseClass expense)
    {
        if (ExpenseIsReal(id) == true && Validate(expense))
        {
            expense.Id = id;
            string query = "UPDATE expenses SET Name = @name,Value = @value,Date = @date,Category = @category WHERE id = @id";
            DataBase.Service.SqlNonQueryExp(query,expense);
            return true;
        }
        else
        {
            return false;
        }
    }
    // calculates the amount spent ofr each category
    public static List<CategoryClass> GetByCategory()
    {
        string query = "Select category,Sum(Value) as Value FROM expenses group by category";
        return DataBase.Service.SqlReadCategory(query);
    }
    //calculate the total amount spent across all expenses
    public static CategoryClass GetTotal()
    {
        string query = "Select Sum(Value) as Value FROM expenses;";
        return DataBase.Service.SqlReadTotal(query);
    }
    //deletes an expense with the specified id (if it exists)
    public static (bool success,string message) DeleteExpense(int id)
    {
        if (ExpenseIsReal(id))
        {
            string query = "delete from expenses where id = @id;";
            DataBase.Service.SqlNonQuery(query,id);
            {
                return (true,"Expense Deleted");
            }
        }
        return (false,"there is no expense with this id");
    }
}