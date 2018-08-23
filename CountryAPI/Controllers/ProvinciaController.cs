using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountryAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Pais/{PaisId}/Provincia")]
    public class ProvinciaController : Controller
    {
        private readonly ApplicationDbContext context;

        public ProvinciaController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Provincia> GetAll(int PaisId)
        {
            return context.Provincias.Where(x => x.PaisId == PaisId).ToList();
        }

        [HttpGet("{id}", Name = "provinciaById")]
        public IActionResult GetById(int id)
        {
            var provincia = context.Provincias.FirstOrDefault(x => x.Id == id);

            if (provincia == null)
            {
                return NotFound();
            }

            return new ObjectResult(provincia);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Provincia pro, int PaisId)
        {
            pro.PaisId = PaisId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Provincias.Add(pro);
            context.SaveChanges();

            return new CreatedAtRouteResult("provinciaById", new { id = pro.Id }, pro);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Provincia pro, int id)
        {
            if (pro.Id != id)
            {
                return BadRequest();
            }

            context.Entry(pro).State = EntityState.Modified;    
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pro = context.Provincias.FirstOrDefault(x => x.Id == id);
            if (pro == null)
            {
                return NotFound();
            }

            context.Provincias.Remove(pro);
            context.SaveChanges();
            return Ok(pro);
        }
    }
}