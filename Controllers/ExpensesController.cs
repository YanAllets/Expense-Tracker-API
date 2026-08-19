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

    public List<Expense> GetShowList()
    {
        return teste.expenses;
    }

    [HttpGet("{id}")]

    public IActionResult GetExpense(int id)
    {
        var result = teste.ListIsReal(id);
        if(!result.Item1)
        {
            return NotFound();
        }
        else
        {
            return Ok(result.expense);
        }
    }

    [HttpPost]
    public IActionResult CreateExpense(Expense expense)
    {
        string query = $"Insert into expenses (Name,Value,Data,Category) Values ('{expense.Name}','{expense.Value}',@date,@category)";
        DataBase.Service.SqlNonQuery(query,expense);
        return Ok();
    }
    [HttpDelete]

    public IActionResult DeleteExpense(int id)
    {
        var result = teste.ListIsReal(id);
        if (!result.Item1)
        {
            return NotFound();
        }
        else
        {
            return Ok(teste.expenses.Remove(result.expense));
        }
    }
    [HttpPut("{id}")]
    public IActionResult ChangeExpense(int id,Expense ChangedExp)
    {
        if (!teste.ChangeExpense(id, ChangedExp))
        {
            return NotFound();
        }
        else
        {
            return Ok(ChangedExp);
        }
    }
}
