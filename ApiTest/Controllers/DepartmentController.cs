using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using ApiTest.Models;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ContosouniversityContextProcedures _procedures;
        private readonly ContosouniversityContext _db;


        public DepartmentController(ContosouniversityContextProcedures procedures, ContosouniversityContext db)
        {
            _procedures = procedures;
            _db = db;
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetTModelById(int id)
        {
            return _db.Department.Find(id);
        }


        [HttpPost("")]
        public ActionResult<Department> PostTModel(Department model)
        {
            OutputParameter<int> outputParameter = new OutputParameter<int>();
            var result = _procedures.Department_Insert(model.Name, model.Budget, model.StartDate, model.InstructorId, outputParameter)
                .GetAwaiter().GetResult();

            return Created("api/Department", result);
        }

        [HttpPut("{id}")]
        public IActionResult PutTModel(int id, Department model)
        {
            var origin = _db.Department.Find(id);

            OutputParameter<int> outputParameter = new OutputParameter<int>();
            var result = _procedures.Department_Update(id, model.Name, model.Budget, model.StartDate, model.InstructorId, origin.RowVersion, outputParameter)
                    .GetAwaiter().GetResult();




            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteTModelById(int id)
        {
            var origin = _db.Department.Find(id);

            OutputParameter<int> outputParameter = new OutputParameter<int>();
            var result = _procedures.Department_Delete(id, origin.RowVersion, outputParameter)
                    .GetAwaiter().GetResult();

            return Ok();
        }


        [HttpGet("CourseCount/{id}")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetVwDepartmentCourseCountById(int id)
        {
            return await _db.VwDepartmentCourseCount.FromSqlInterpolated($"SELECT * FROM vwDepartmentCourseCount WHERE DepartmentID = {id}").ToListAsync();

        }
    }
}