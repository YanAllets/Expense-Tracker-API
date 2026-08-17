using MySqlConnector;

namespace ExpenseTrackerApi.DataBase;
public class Config
{
    public static string conexao = "Server=localhost;Database=expensetracker;User ID=root;Password=Hypnotize-Overrule-Luckiness7;";
    public static MySqlConnection conn = new MySqlConnection(conexao);
}