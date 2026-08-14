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
        List<Expense> Lista = teste.expenses;
        teste.WriteList(Lista);
        return Lista;
    }

    [HttpGet("{id}")]

    public IActionResult GetExpense(int id)
    {
        return Ok(teste.ListIsReal(id).expense);
    }

    [HttpPost]
    public IActionResult CreateExpense(Expense expense)
    {
        teste.expenses.Add(expense);
        return Ok(teste.expenses);
    }
    [HttpDelete]

    public IActionResult DeleteExpense(int id)
    {
        List<Expense> Lista = teste.expenses;
        foreach(Expense expense in Lista)
        {
            if(expense.Id == id)
            {
                return Ok(Lista.Remove(expense));
            }
        }
        return NotFound();
    }
    [HttpPut("{id}")]
    public IActionResult ChangeExpense(int id,Expense ChangedExp)
    {
        List<Expense> Lista = teste.expenses;
        foreach(Expense OriginalExp in Lista)
        {
            if(OriginalExp.Id == id)
            {
                OriginalExp.Id = ChangedExp.Id;
                OriginalExp.Name = ChangedExp.Name;
                OriginalExp.Value = ChangedExp.Value;
                OriginalExp.Category = ChangedExp.Category;
                OriginalExp.Date = ChangedExp.Date;
                return Ok(ChangedExp);
            }
        }
        return NotFound();
    }
}
