using System.Collections.Generic;
using WebApi.Models;
using WebApi.Utility;
using System.Net;
using WebHttp = System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Hosting;

namespace WebApi.Controllers
{
    [Route("api/emission/")]
    [ApiController]
    public class EmissionController : ControllerBase
    {
        private static List<Consumption> _consumptionData;
        private readonly int tyreProduced = 350; 

        [HttpPost]
        [Route("upload")]
        public ActionResult UploadConsumption(List<Consumption> consumptionData) {

            _consumptionData = new List<Consumption>();
            foreach (var consumption in consumptionData) { 
                _consumptionData.Add(consumption);
            }
            return Ok(consumptionData);
        }
        
        [HttpGet]
        [Route("scope")]
        public ActionResult GetScopeEmissions()
        {
            if (_consumptionData == null) return NotFound("No Consumption data found");

            var scopeEmissions = new ScopeEmission() { 
                ScopeOneEmission = GetScopeOneEmissions(),
                ScopeTwoEmission = GetScopeTwoEmissions(),
                ScopeThreeEmission = GetScopeThreeEmissions()
            };
            
            return Ok(scopeEmissions);
        }

        [HttpGet]
        [Route("total")]
        public ActionResult GetTotalEmissions()
        {
            if (_consumptionData == null) return NotFound("No Consumption data found");

            var totalEmmision = CalculateTotalEmissions();

            return Ok(totalEmmision);
        }

        [HttpGet]
        [Route("total/ton")]
        public ActionResult GetTotalEmissionsPerToneTyre()
        {
            if (_consumptionData == null) return NotFound("No Consumption data found");

            var totalEmmisionPerToneType = CalculateTotalEmissions()/tyreProduced;

            return Ok(totalEmmisionPerToneType);
        }

        [HttpGet]
        [Route("sites")]
        public ActionResult GetEmissionsBySite()
        {
            if (_consumptionData == null) return NotFound("No Consumption data found");

            var sitesEmission = _consumptionData.GroupBy(consumption => consumption.Site, consumption => Util.CalculateTotalEmmision(consumption))
                .ToDictionary(group => group.Key, group => group.ToList().Sum());
            return Ok(sitesEmission);
        }

        [HttpGet]
        [Route("months")]
        public ActionResult GetEmissionByMonth()
        {
            if (_consumptionData == null) return NotFound("No Consumption data found");

            var monthsEmission = _consumptionData.GroupBy(x => x.Month, x => Util.CalculateTotalEmmision(x))
                .ToDictionary(group => group.Key, group => group.ToList().Sum());

            return Ok(monthsEmission);
        }

        private float GetScopeThreeEmissions()
        {
            var scopeThreeEmissions = 0.0f;
            foreach(var consumption in _consumptionData)
            {
                scopeThreeEmissions += Util.CalculateScopeTreeEmissions(consumption);
            }

            return scopeThreeEmissions;
        }

        private float GetScopeTwoEmissions()
        {
            var scopeTwoEmissions = 0.0f;
            foreach (var consumption in _consumptionData)
            {
                scopeTwoEmissions += Util.CalculateScopeTwoEmissions(consumption);
            }

            return scopeTwoEmissions;
        }

        private float GetScopeOneEmissions()
        {
            var scopeOneEmissions = 0.0f;
            foreach (var consumption in _consumptionData)
            {
                scopeOneEmissions += Util.CalculateScopeOneEmissions(consumption);
            }

            return scopeOneEmissions;
        }

        private float CalculateTotalEmissions()
        {
            var totalEmmision = 0.0f;
            foreach (var consumption in _consumptionData)
            {
                totalEmmision += Util.CalculateTotalEmmision(consumption);
            }

            return totalEmmision;
        }

    }
}
