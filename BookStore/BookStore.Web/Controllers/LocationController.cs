using BookStore.DataAccess;
using BookStore.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _repo;

        public LocationController()
        {
            _repo = new LocationRepository();
        }

        [HttpGet("api/locations")]
        public IEnumerable<Domain.Location> GetAllLocations()
        {
            var locations = _repo.GetAllLocations();
            //List<Models.Location> toReturn = new List<Models.Location>();

            //foreach(var loc in locations)
            //{
            //    var l = new Models.Location(loc.ID, loc.Name);
            //    foreach(var kv in loc.Inventory)
            //    {
            //        l.Inventory.Add(kv.Key, kv.Value);
            //    }
            //    toReturn.Add(l);
            //}

            //return toReturn;
            return locations;
        }

        [HttpGet("api/locations/{id?}")]
        public Domain.Location GetLocationByID(int id)
        {
            return _repo.GetLocationByID(id);
        }

        [HttpPost("api/locations")]
        public void AddLocation(Domain.Location l)
        {
            _repo.AddLocation(l);
        }

        [HttpPut("api/locations")]
        public void UpdateLocation(Domain.Location l)
        {
            _repo.UpdateLocation(l);
        }
    }
}
