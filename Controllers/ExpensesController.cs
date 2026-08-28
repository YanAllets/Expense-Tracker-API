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
    
    public List<ExpenseClass> GetShowList(
        int? page,
        int? pageSize,
        int? id,
        string? name,
        decimal? value,
        string? category,
        DateTime? date
    )
    {
        return ExpenseService.GetEveryExpense(page,pageSize,id,name,value,category,date);
    }

    [HttpGet("category")]
    public List<CategoryClass> GetByCategory()
    {
        return ExpenseService.SpendByCategory();
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
        var result = ExpenseService.CreateExpense(expense);
        if(result.boolean)
        {
            return Ok(result.obj);
        }
        else
        {
            return NotFound("Invalid Values");
        }
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
