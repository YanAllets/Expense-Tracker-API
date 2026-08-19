using ExpenseTrackerApi.Models;
using MySqlConnector;

namespace ExpenseTrackerApi.DataBase;
public class Service
{
    public static void SqlNonQuery(string query,Expense expense)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        comando.Parameters.AddWithValue("@date",expense.Date);
        comando.Parameters.AddWithValue("@category",expense.Category);

        Config.conn.Open();
        comando.ExecuteNonQuery();
        Config.conn.Close();
    }
}