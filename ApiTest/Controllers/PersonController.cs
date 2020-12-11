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
    public class PersonController : ControllerBase
    {

        private readonly ContosouniversityContext _db;

        public PersonController(ContosouniversityContext db)
        {
            _db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetTModels()
        {
            return _db.Person.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Person>> GetTModelById(int id)
        {
            return _db.Person.Where(b => b.Id == id)
                             .Where(b => b.IsDeleted == false)
                             .ToList();
        }

        [HttpPost("")]
        public ActionResult<Person> PostTModel(Person model)
        {
            _db.Person.Add(model);
            _db.SaveChanges();

            return Created("/api/Person/" + model.Id, model);
        }

        [HttpPut("{id}")]
        public IActionResult PutTModel(int id, Person model)
        {
            var p = _db.Person.Find(id);
            p.LastName = model.LastName;
            p.FirstName = model.FirstName;
            p.HireDate = model.HireDate;
            p.EnrollmentDate = model.EnrollmentDate;
            p.Discriminator = model.Discriminator;

            _db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeleteTModelById(int id)
        {
            var c = _db.Person.Find(id);
            c.IsDeleted = true;

            _db.SaveChanges();

            return Ok(c);
        }
    }
}