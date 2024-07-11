using EmployeePortal.Data;
using EmployeePortal.Models;
using EmployeePortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Controllers
{
  public class EmployeeController : Controller
  {
    private readonly ApplicationDbContext dbContext;
    public EmployeeController(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult Add()
    {
      return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
    {
      var employee = new Employee
      {
        No = viewModel.No,
        Name = viewModel.Name,
        Age = viewModel.Age,
        Department = viewModel.Department,
        Salary = viewModel.Salary,
      };

      await dbContext.Employees.AddAsync(employee);
      await dbContext.SaveChangesAsync();

      return RedirectToAction("List", "Employee");
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
      var employee = await dbContext.Employees.ToListAsync();
      return View(employee);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
      var employee = await dbContext.Employees.FindAsync(id);
      return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee viewModel)
    {
      var employee = await dbContext.Employees.FindAsync(viewModel.Id);

      if (employee is not null)
      {
        employee.Name = viewModel.Name;
        employee.Age = viewModel.Age;
        employee.Department = viewModel.Department;
        employee.Salary = viewModel.Salary;

        await dbContext.SaveChangesAsync();
      }

      return RedirectToAction("List", "Employee");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
      var employee = await dbContext.Employees
          .FirstOrDefaultAsync(x => x.Id == id);

      if (employee != null)
      {
        dbContext.Employees.Remove(employee);
        await dbContext.SaveChangesAsync();
      }

      return RedirectToAction("List", "Employee");
    }


  }
}
