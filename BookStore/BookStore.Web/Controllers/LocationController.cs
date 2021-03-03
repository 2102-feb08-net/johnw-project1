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

        public LocationController(ILocationRepository locationRepository)
        {
            _repo = locationRepository;
        }

        [HttpGet("api/locations")]
        public IEnumerable<Domain.Location> GetAllLocations()
        {
            return _repo.GetAllLocations();
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
