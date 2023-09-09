using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly APIContext _context;
        public EmployeesController(APIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _context.Employees.ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            var e = await _context.Employees.FirstOrDefaultAsync(x => x.Name == name);
            if (e == null)
                return NotFound();
            return Ok(e);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee e)
        {
            e.Id = Guid.NewGuid();
            await _context.Employees.AddAsync(e);
            await _context.SaveChangesAsync();
            return Ok(e);
        }

        [HttpPut]
        [Route("{name}")]
        public async Task<IActionResult> Put([FromRoute] string name, Employee e)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Name == name);
            if(employee == null)
                return NotFound();

            employee.Name = e.Name;
            employee.Email = e.Email;
            employee.Phone = e.Phone;
            employee.Salary = e.Salary;
            employee.Department = e.Department;

            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> Delete([FromRoute] string name)
        {
            var e = await _context.Employees.FirstOrDefaultAsync(x => x.Name == name);
            if(e == null) return NotFound();
            _context.Employees.Remove(e);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
