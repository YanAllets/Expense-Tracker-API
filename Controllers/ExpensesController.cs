using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerApi.Services;
using MySqlConnector;
using ExpenseTrackerApi.DataBase;

namespace ExpenseTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly ExpenseService teste;

    public ExpenseController(ExpenseService service)
    {
        teste = service;
    }
    
    [HttpGet]

    public string GetShowList()
    {
        string query = "SELECT * FROM expensetracker.expenses;";
        return DataBase.Service.SqlRead(query);
    }

    [HttpGet("{id}")]

    public IActionResult GetExpense(int id)
    {
        if(ExpenseService.ExpenseIsReal(id) == false)
        {
            return NotFound();
        }
        else
        {
            string query = $"SELECT * FROM expensetracker.expenses where id = {id};";
            return Ok(DataBase.Service.SqlRead(query));
        }
    }

    [HttpPost]
    public IActionResult CreateExpense(Expense expense)
    {
        string query = $"Insert into expenses (Name,Value,Data,Category) Values (@name,@value,@date,@category)";
        DataBase.Service.SqlNonQueryExp(query,expense);
        return Ok();
    }
    [HttpDelete]

    public IActionResult DeleteExpense(int id)
    {
        if (ExpenseService.ExpenseIsReal(id) == false)
        {
            return NotFound();
        }
        else
        {
            string query = $"delete from expenses where id = {id};";
            DataBase.Service.SqlNonQuery(query);
            return Ok();
        }
    }
    [HttpPut("{id}")]
    public IActionResult ChangeExpense(int id,Expense ChangedExp)
    {
        ExpenseService.ChangeExpense(id,ChangedExp);
        return GetExpense(id);
    }
}
