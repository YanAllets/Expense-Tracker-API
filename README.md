# Expense Tracker Api

A REST API built with C# and ASP.NET core to administer personal expenses.

## Technologies
- C#
- Asp.Net Core (NET 9)
- Mysql
- MysqlConnector
- Swagger/OpenAPI

## Existing Features

- Create Expenses
- List Expenses
- Search expenses by and ID
- Filter all expenses by name,value,category and date
- Pagination
- Update expenses
- Delete expenses
- CAlculate spending by category
- Calculate total Spending

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Expense` | Get expenses |
| GET | `/api/Expense/{id}` | Get an expense by ID |
| GET | `/api/Expense/category` | Get spending grouped by category |
| GET | `/api/Expense/total` | Get total spending |
| POST | `/api/Expense` | Create an expense |
| PUT | `/api/Expense/{id}` | Update an expense |
| DELETE | `/api/Expense?id={id}` | Delete an expense |

## FIltering and Pagination

The GET endpoint supports optional filters by all expenses values and forms of dispose those values

Example:

`Get /api/Expense?category=Food&page=1&pageSize=10`

## Running the Project 
### 1) Create the DataBase

Create the database and the expenses table using the following SQL commands:

```sql
CREATE DATABASE expensetracker;

USE expensetracker;

CREATE TABLE expenses (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Value DECIMAL(20,2) NOT NULL,
    Date DATETIME NOT NULL,
    Category VARCHAR(100) NOT NULL
);
```


### 2) Configure the MySQL connection
Create `DBConfig.cs` file inside the `DataBase` folder.

Example:
```csharp
using MySqlConnector;

namespace ExpenseTrackerApi.DataBase;

public class Config
{
    public static string conexao = "Server=localhost;Database=expensetracker;User ID=root;Password=YourPASSWORD;";
    public static MySqlConnection conn = new MySqlConnection(conexao);
}

```

### 3) Run the project

```bash
dotnet run
```
