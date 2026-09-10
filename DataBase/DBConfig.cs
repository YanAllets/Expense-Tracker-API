using MySqlConnector;

namespace ExpenseTrackerApi.DataBase;
public class Config
{
    public static string conexao = "Server=localhost;Database=expensetracker;User ID=root;Password=Spooky-Velocity-Unlearned;";
    public static MySqlConnection conn = new MySqlConnection(conexao);
}