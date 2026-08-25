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
    
    public string GetShowList(
        int? id,
        string? name,
        decimal? value,
        string? category,
        DateTime? date
    )
    {
        return ExpenseService.GetEveryExpense(id,name,value,category,date);
    }

    [HttpGet("{id}")]

    public IActionResult GetExpense(int id)
    {
        var result = ExpenseService.GetExpense(id);
        if(result == (false,null))
        {
            return NotFound();
        }
        else
        {
            return Ok(result.line);
        }
    }

    [HttpPost]
    public IActionResult CreateExpense(ExpenseClass expense)
    {
        return Ok(ExpenseService.CreateExpense(expense));
    }
    [HttpDelete]

    public IActionResult DeleteExpense(int id)
    {
        var Response = ExpenseService.DeleteExpense(id);
        if (Response.boolean == false)
        {
            return NotFound(Response.line);
        }
        else
        {
            return Ok(Response);
        }
    }
    [HttpPut("{id}")]
    public IActionResult ChangeExpense(int id,ExpenseClass ChangedExp)
    {
        if (ExpenseService.ChangeExpense(id, ChangedExp))
        {
            return Ok(GetExpense(id));
        }
        else
        {
            return NotFound("There is no expense with this id or invalid values");
        }
    }
}
