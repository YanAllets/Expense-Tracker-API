using ExpenseTrackerApi.Models;
using MySqlConnector;

namespace ExpenseTrackerApi.DataBase;
public class Service
{
    public static void SqlNonQuery(string query)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        Config.conn.Open();
        comando.ExecuteNonQuery();
        Config.conn.Close();
    }
    public static void SqlNonQueryExp(string query,ExpenseClass? expense)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        comando.Parameters.AddWithValue("@id",expense.Id);
        comando.Parameters.AddWithValue("@name",expense.Name);
        comando.Parameters.AddWithValue("@value",expense.Value);
        comando.Parameters.AddWithValue("@date",expense.Date);
        comando.Parameters.AddWithValue("@category",expense.Category);

        Config.conn.Open();
        comando.ExecuteNonQuery();
        Config.conn.Close();
    }
    public static List<ExpenseClass> SqlReadExpense(string query)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);
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
    public static int SqlScalar(string query)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);
        Config.conn.Open();
        object ScalarObj = comando.ExecuteScalar();
        int i = Convert.ToInt32(ScalarObj);
        Config.conn.Close();
        return i;
    }
}