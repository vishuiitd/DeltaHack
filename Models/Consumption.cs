using System.ComponentModel.DataAnnotations;
using WebApi.Utility;

namespace WebApi.Models
{
    public class Consumption
    {
        [Required]
        public string Site { get; set; }
        [Required]
        [Range(0,11)]
        public MonthEnum Month { get; set; }
        [Required]
        public float CoalConsumption { get; set; }
        [Required]
        public float BioMassConsumption { get; set; }
        [Required]
        public float ElectricityConsumption { get; set; }
        [Required]
        public float RoadLogisticIncomingConsumption { get; set; }
        [Required]
        public float RoadLogisticOutgoingConsumption { get; set; }
    }

}
