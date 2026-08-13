using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerApi.Services;
using System.Security.Cryptography.X509Certificates;

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
        List<Expense> Lista = teste.AddFakeExpenses();
        teste.WriteList(Lista);
        return Lista;
    }

    [HttpGet("{id}")]

    public IActionResult GetExpense(int id)
    {

        List<Expense> Lista = teste.AddFakeExpenses();
        foreach(Expense expense in Lista)
        {
            if(expense.Id == id)
            {
                return Ok(expense);
            }
        }
        return NotFound();
    }

    [HttpPost]
    public IActionResult CreateExpense(Expense expense)
    {
        teste.expenses.Add(expense);
        return Ok(teste.expenses);
    }
}
