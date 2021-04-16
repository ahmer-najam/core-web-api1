using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_web_api1.Context;
using core_web_api1.Models;
using Microsoft.AspNetCore.Authorization;
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
        public City Put(int id, [FromBody] City city)
        {
            city.RecDate = DateTime.Now;
            city.CityId = id;
            var data = _myDbContext.City.Update(city);
            return city;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            City city = _myDbContext.City.Where(item => item.CityId == id).FirstOrDefault();

            if(city != null){
                _myDbContext.City.Remove(city);
                _myDbContext.SaveChanges();
            }
           
            return Ok();
        }

        [HttpGet("GetById/{id}")]
        public City GetById(int id)
        {
            City city = _myDbContext.City.Where(item => item.CityId == id).FirstOrDefault();
            return city;
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
            // var newId = _myDbContext.City.Max(item => item.CityId);
            var _serial = _myDbContext.Serial.ToList();
            _serial[0].CityId = _serial[0].CityId + 1;
            _myDbContext.SaveChanges();

            int newId =  _serial[0].CityId;
            return newId;
        }
    }
}
