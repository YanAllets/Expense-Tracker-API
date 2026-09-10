using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerApi.Services;

namespace ExpenseTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    [HttpGet]
    // returns all expenses with optional filtering and pagination

    public IActionResult GetShowList(
        int? page,
        int? pageSize,
        int? id,
        string? name,
        decimal? value,
        string? category,
        DateTime? date
    )
    {
        var result = ExpenseService.GetEveryExpense(page,pageSize,id,name,value,category,date);
        if(result == (false,null))
        {
            return NotFound("Invalid page or Page Size...");
        }
        else
        {
            return Ok(result.list);
        }
    }

    [HttpGet("{id}")]
    // Returns a single expense by ID

    public IActionResult GetExpense(int id)
    {
        var result = ExpenseService.GetExpense(id);
        if(result == (false,null))
        {
            return NotFound();
        }
        else
        {
            return Ok(result.expenseClass);
        }
    }

    [HttpGet("category")]
    // Returns the total amount spent for each category

    public List<CategoryClass> GetByCategory()
    {
        return ExpenseService.GetByCategory();
    }

    [HttpGet("total")]
    // Returns the total amount spent across all expenses
    public CategoryClass GetTotal()
    {
        return ExpenseService.GetTotal();
    }


    [HttpPost]
    // Creates a new Expense
    public IActionResult CreateExpense(ExpenseClass expense)
    {
        var result = ExpenseService.CreateExpense(expense);
        if(result.success)
        {
            return Ok(result.expense);
        }
        else
        {
            return NotFound("Invalid Values");
        }
    }
    [HttpPut("{id}")]
    //Updates an existing expense
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
    [HttpDelete]
    //Deletes an expense by provided ID

    public IActionResult DeleteExpense(int id)
    {
        var Response = ExpenseService.DeleteExpense(id);
        if (Response.success == false)
        {
            return NotFound(Response.message);
        }
        else
        {
            return Ok(Response.message);
        }
    }
}
