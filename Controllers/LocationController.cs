using IPDP_Stefan.Context;
using IPDP_Stefan.Interfaces;
using IPDP_Stefan.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IPDP_Stefan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;
        private readonly ContextDb _context;
        public LocationController(ILocationService locationService, ContextDb context)
        {
            this.locationService = locationService;
            _context = context;
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> AddLocation(Location location)
        {
            var existingLocation = _context.Location.Find(location.Id);
            if (existingLocation != null) return BadRequest("This Location already exists.");
            await locationService.AddLocation(location).ConfigureAwait(false);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + location.Id,
                location);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await locationService.GetLocationById(id).ConfigureAwait(false);
            if (location != null)
            {
                await locationService.DeleteLocation(location).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> EditLocation( Location location)
        {
            var existingLocation = await locationService.GetLocationById(location.Id).ConfigureAwait(false);
            if (existingLocation != null)
            {
                location.Id = existingLocation.Id;
                await locationService.EditLocation(location).ConfigureAwait(false);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await locationService.GetLocationById(id).ConfigureAwait(false);
            if (location != null)
            {
                return Ok(location);
            }
            return NotFound($"can not find Location with id:{id}");
        }
        [HttpGet]
        [Route("get/byName/{name}")]
        public async Task<IActionResult> GetLocationByName(string name)
        {
            try
            {
                var location = await locationService.GetLocationByName(name).ConfigureAwait(false);
                if (location != null)
                {
                    return Ok(location.Name);
                }
                return NotFound($"can not find Location with name:{name}");
            }
            catch (Exception e)
            {
                return NotFound($"can not find Location with name:{name}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAllLocations()
        {
            return Ok(await locationService.GetAllLocations().ConfigureAwait(false));
        }

    }
}
