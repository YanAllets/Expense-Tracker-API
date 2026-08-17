using MySqlConnector;

namespace ExpenseTrackerApi.DataBase;
public class Service
{
    public static void SqlNonQuery(string query)
    {
        Config.conn.Open();
        MySqlCommand command = new MySqlCommand(query,Config.conn);
        command.ExecuteNonQuery();
        Config.conn.Close();
    }
}