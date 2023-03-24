using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherApplication.Data;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocalityController : ControllerBase
    {
        private readonly WeatherdbContext _weatherdbContext;

        public LocalityController(WeatherdbContext weatherdbContext)
        {
            _weatherdbContext = weatherdbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Locality>>> GetLocalities()
        {
            return Ok(await _weatherdbContext.Localities.ToListAsync());

        }

        [HttpPost]
        public async Task<ActionResult<List<Locality>>> AddLocation(Locality location)
        {
            try
            {
                _weatherdbContext.Localities.Add(location);
                await _weatherdbContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                
            }

            return Ok(await _weatherdbContext.Localities.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Locality>>> DeleteLocation(int id)
        {
            var locality = await _weatherdbContext.Localities.FindAsync(id);
            
            if (locality == null)
            {
                return NotFound();
            }
            var weatherDataForLocation = await _weatherdbContext.Weatherdata.Where(w => w.LocalityId== id).ToListAsync();
            if(weatherDataForLocation != null)
            {
                foreach(var data in weatherDataForLocation)
                {
                    _weatherdbContext.Weatherdata.Remove(data);
                }
            }

            _weatherdbContext.Localities.Remove(locality);
            await _weatherdbContext.SaveChangesAsync();

            return Ok(await _weatherdbContext.Localities.ToListAsync());
        }
    }
}
