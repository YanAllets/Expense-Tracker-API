using ExpenseTrackerApi.Models;
using MySqlConnector;

namespace ExpenseTrackerApi.DataBase;
public class Service
{
    public static string text = null;
    public static void SqlNonQuery(string query)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);

        Config.conn.Open();
        comando.ExecuteNonQuery();
        Config.conn.Close();
    }
    public static void SqlNonQueryExp(string query,Expense? expense)
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
    public static string SqlRead(string query)
    {
        MySqlCommand comando = new MySqlCommand(query,Config.conn);
        Config.conn.Open();
        MySqlDataReader reader = comando.ExecuteReader();
        

        while (reader.Read())
        {
            string id = Convert.ToString(reader["Id"]);
            string name = Convert.ToString(reader["Name"]);
            string value = Convert.ToString(reader["Value"]);
            string category = Convert.ToString(reader["Category"]);
            string date = Convert.ToString(reader["Data"]);

            string line = ($"ID:{id} NAME:{name} VALUE:{value} CATEGORY:{category} DATE:{date} \n");
            text = text + line;
        }
        Config.conn.Close();
        return text;
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