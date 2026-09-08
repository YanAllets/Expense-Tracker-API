using System.Reflection.Metadata;
using ExpenseTrackerApi.Models;
using MySqlConnector;

namespace ExpenseTrackerApi.DataBase;
public class Service
{
    public static void SqlNonQuery(string query,int id)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        Config.conn.Open();
        comando.Parameters.AddWithValue("@id",id);
        comando.ExecuteNonQuery();
        Config.conn.Close();
    }
    public static void SqlNonQueryExp(string query,ExpenseClass expense)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        comando.Parameters.Add("@id", MySqlDbType.Int32).Value = expense.Id;
        comando.Parameters.Add("@name", MySqlDbType.VarChar).Value = expense.Name;
        comando.Parameters.Add("@value", MySqlDbType.Decimal).Value = expense.Value;
        comando.Parameters.Add("@date", MySqlDbType.DateTime).Value = expense.Date;
        comando.Parameters.Add("@category", MySqlDbType.VarChar).Value = expense.Category;

        Config.conn.Open();
        comando.ExecuteNonQuery();
        Config.conn.Close();
    }
    public static ExpenseClass SqlReadExpense(string query,int? id)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        comando.Parameters.Add("@id",MySqlDbType.Int32).Value = id;

        Config.conn.Open();
        MySqlDataReader reader = comando.ExecuteReader();

        ExpenseClass? expense = null;

        while (reader.Read())
        {   
            expense = new ExpenseClass()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                Value = Convert.ToDecimal(reader["Value"]),
                Category = Convert.ToString(reader["Category"]),
                Date = Convert.ToDateTime(reader["Date"])
            };
        }
        Config.conn.Close();
        return expense;
    }
    public static List<ExpenseClass> SqlReadExpFilter(
        string query,
        int? offset,
        int? pageSize,
        int? id,
        string? name,
        decimal? value,
        string? category,
        DateTime? date
    )
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        comando.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
        comando.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
        comando.Parameters.Add("@value", MySqlDbType.Decimal).Value = value;
        comando.Parameters.Add("@date", MySqlDbType.DateTime).Value = date;
        comando.Parameters.Add("@category", MySqlDbType.VarChar).Value = category;
        comando.Parameters.Add("@offset", MySqlDbType.Int32).Value = offset;
        comando.Parameters.Add("@pageSize", MySqlDbType.Int32).Value = pageSize;
    
        Config.conn.Open();
        MySqlDataReader reader = comando.ExecuteReader();

        List<ExpenseClass> list = new List<ExpenseClass>();

        while (reader.Read())
        {   
            ExpenseClass expense = new ExpenseClass()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                Value = Convert.ToDecimal(reader["Value"]),
                Category = Convert.ToString(reader["Category"]),
                Date = Convert.ToDateTime(reader["Date"])
            };
            list.Add(expense);
        }

        Config.conn.Close();
        return list;
    }
     public static List<CategoryClass> SqlReadCategory(string query)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        Config.conn.Open();
        MySqlDataReader reader = comando.ExecuteReader();

        List<CategoryClass> list = new List<CategoryClass>();

        while (reader.Read())
        {   
            CategoryClass expense = new CategoryClass()
            {
                Value = Convert.ToDecimal(reader["Value"]),
                Category = Convert.ToString(reader["Category"]),
            };
            list.Add(expense);
        }
        Config.conn.Close();
        return list;
    }
    public static CategoryClass SqlReadTotal(string query)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);
        Config.conn.Open();
        MySqlDataReader reader = comando.ExecuteReader();

        List<CategoryClass> list = new List<CategoryClass>();

        while (reader.Read())
        {   
            CategoryClass expense = new CategoryClass()
            {
                Value = Convert.ToDecimal(reader["Value"]),
                Category = "Total",
            };
            list.Add(expense);
        }
        Config.conn.Close();
        return list.FirstOrDefault();
    }
    public static int SqlScalar(string query)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);
        Config.conn.Open();
        object ScalarObj = comando.ExecuteScalar();
        int i = Convert.ToInt32(ScalarObj);
        Config.conn.Close();
        return i;
    }
    public static int SqlScalarExp(string query,int id)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        comando.Parameters.AddWithValue("@id",id);

        Config.conn.Open();

        object ScalarObj = comando.ExecuteScalar();
        int i = Convert.ToInt32(ScalarObj);

        Config.conn.Close();
        return i;
    }
}