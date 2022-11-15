/******************************************
  2022 Trimester 3 INFT6900 Final Project
  Team   : Four Square
  Author : Chenrui Zhang
  Date   : 26/09/2022
******************************************/

using Microsoft.AspNetCore.Mvc;
using WebAPI.Entity;
using WebAPI.Model;
using WebAPI.Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LocationController : ControllerBase
    {
        /*
         * location.LocationID != 0: get exact location
         * location.Campus == null: get campus
         * location.Campus != null && location.building == null:
         * get buildings in the campus
         * Others: Get locations by campus and building or an exact location.
         */
        [HttpGet]
        public ResponseModel Get([FromQuery] Location location)
        {
            try
            {
                return new SuccessResponseModel()
                {
                    obj = LocationService.GetLocations(location),
                    Message = "success",
                };
            }
            catch (Exception e)
            {
                return new FailureResponseModel()
                {
                    Message = e.Message,
                };
            }
        }
    }
}
