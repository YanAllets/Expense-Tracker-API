using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerApi.Services;
using System.Security.Cryptography.X509Certificates;

namespace ExpenseTrackerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{

    
    [HttpGet]

    public List<Expense> GetShowList()
    {
        ExpenseService teste = new ExpenseService();
        List<Expense> Lista = teste.CreateList();
        return Lista;
    }

    [HttpGet("{id}")]

    public IActionResult GetExpense(int id)
    {
        ExpenseService teste = new ExpenseService();
        List<Expense> Lista = teste.CreateList();
        foreach(Expense expense in Lista)
        {
            if(expense.Id == id)
            {
                return Ok(expense);
            }
        }
        return NotFound();
    }
}
