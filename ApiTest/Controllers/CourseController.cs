using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTest.Models;
using Microsoft.AspNetCore.Mvc;
//using ApiTest.Models;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ContosouniversityContext _db;

        public CourseController(ContosouniversityContext db)
        {
            _db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetTModels()
        {
            return _db.Course.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetTModelById(int id)
        {
            return _db.Course.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Course> PostTModel(Course model)
        {   
            _db.Course.Add(model);
            _db.SaveChanges();

            return Created("/api/Course/" + model.CourseId, model);
        }

        [HttpPut("{id}")]
        public IActionResult PutTModel(int id, Course model)
        {
            var c = _db.Course.Find(id);
            c.Credits = model.Credits;
            c.Title = model.Title;

            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteTModelById(int id)
        {
            var c = _db.Course.Find(id);
            _db.Course.Remove(c);

            _db.SaveChanges();

            return Ok(c);
        }
    }
}