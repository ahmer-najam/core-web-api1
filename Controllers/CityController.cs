using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_web_api1.Context;
using core_web_api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace core_web_api1.Controllers
{
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        readonly MyDbContext _myDbContext;
        public CityController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        [HttpGet]
        public IEnumerable<City> Get()
        {
            var data = _myDbContext.City.ToList();
            return data;
        }

        [HttpPost]
        public City Post([FromBody] City City)
        {
            City.CityId = GetNewId();
            City.RecDate = DateTime.Now;
            var data = _myDbContext.City.Add(City);
            _myDbContext.SaveChanges();
            return City;
        }

        [HttpPut("{id}")]
        public City Put(int id, [FromBody] City City)
        {
            City.RecDate = DateTime.Now;
            var data = _myDbContext.City.Update(City);
            _myDbContext.SaveChanges();
            return City;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            City City = _myDbContext.City.Where(item => item.CityId == id).FirstOrDefault();
            _myDbContext.City.Remove(City);
            _myDbContext.SaveChanges();
            return Ok();
        }

        [HttpGet("GetById/{id}")]
        public City GetById(int id)
        {
            City City = _myDbContext.City.Where(item => item.CityId == id).FirstOrDefault();
            return City;
        }

        [HttpGet("IsDuplicate/{id}/{cityName}")]
        public int IsDuplicate(int id, string cityName)
        {
            var result = _myDbContext.City.Count(item => item.CityId != id && item.CityName == cityName);
            return result;
        }

        [HttpGet("GetNewId")]
        public int GetNewId()
        {
            var newId = _myDbContext.City.Max(item => item.CityId) + 1;
            return newId;
        }
    }
}
