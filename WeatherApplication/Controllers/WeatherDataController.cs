using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using WeatherApplication.Data;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherDataController : ControllerBase
    {
        private readonly WeatherdbContext _weatherdbContext;

        public WeatherDataController(WeatherdbContext weatherdbContext)
        {
            _weatherdbContext = weatherdbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Weatherdatum>>> GetWeatherData()
        {

            return Ok(await _weatherdbContext.Weatherdata.ToListAsync());
        }

        [HttpGet("{locationName}/{year}/{month}")]
        public async Task<ActionResult<List<Weatherdatum>>> GetMonthlyWeatherData(string locationName, int year, int month)
        {

            Locality location; 
            try {
                location = await _weatherdbContext.Localities.Where(l => l.LocalityName == locationName).FirstAsync();
            
            }catch(InvalidOperationException ex)
            {
                return NotFound("location not found");
            }

            //return Ok(location.LocalityId);
            //return Ok(await _weatherdbContext.Weatherdata.Where(w => w.LocalityId == location.LocalityId && w.Day.Year == year && w.Day.Month==month).ToListAsync());
            return Ok( await _weatherdbContext.Weatherdata.Where(w => w.LocalityId == location.LocalityId && w.Day.Year == year && w.Day.Month == month).ToListAsync());      
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Weatherdatum>>> GetWeatherData(int id)
        {
            var weather = await _weatherdbContext.Weatherdata.FindAsync(id);
            var data =  await _weatherdbContext.Weatherdata.ToListAsync();
            if(data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult<List<Locality>>> AddWeather(Weatherdatum weatherdata)
        {
            try
            {
               _weatherdbContext.Weatherdata.Add(weatherdata);
               await _weatherdbContext.SaveChangesAsync();

            }catch(Exception ex)
            {
                return BadRequest();
            }
            

            return Ok(await _weatherdbContext.Weatherdata.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Locality>>> UpdateWeather(Weatherdatum weatherdata)
        {
            var dbData = await _weatherdbContext.Weatherdata.FindAsync(weatherdata.WeatherdataId);
            if(dbData == null)
            {
                return BadRequest("Data not found.");
            }

            dbData.Temperature = weatherdata.Temperature;
            dbData.Rainfall = weatherdata.Rainfall;
            dbData.WindSpeed= weatherdata.WindSpeed;
  
            await _weatherdbContext.SaveChangesAsync();

            return Ok(await _weatherdbContext.Weatherdata.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Locality>>> DeleteWeatherData(int id)
        {
            var weatherdata = await _weatherdbContext.Weatherdata.FindAsync(id);
            if (weatherdata == null)
            {
                return BadRequest("Data not found");
            }

            _weatherdbContext.Weatherdata.Remove(weatherdata);
            await _weatherdbContext.SaveChangesAsync();

            return Ok(await _weatherdbContext.Weatherdata.ToListAsync());
        }
    }
}
