using WebApi.Models;

namespace WebApi.Utility
{
    public class Util
    {
        public static float CalculateTotalEmmision(Consumption consumption)
        {
            return CalculateScopeOneEmissions(consumption) + CalculateScopeTwoEmissions(consumption) + CalculateScopeTreeEmissions(consumption);
        }
        public static float CalculateScopeOneEmissions(Consumption consumption)
        {
            return CalculateCoalEmission(consumption.CoalConsumption) + CalculateBioMassEmission(consumption.BioMassConsumption);
        }

        public static float CalculateScopeTwoEmissions(Consumption consumption)
        {
            return CalculateElectricityEmission(consumption.ElectricityConsumption);
        }

        public static float CalculateScopeTreeEmissions(Consumption consumption)
        {
            return CalculateIncomingDieselEmission(consumption.RoadLogisticIncomingConsumption) + CalculateOutgoingDieselEmission(consumption.RoadLogisticOutgoingConsumption);
        }

        private static float CalculateIncomingDieselEmission(float roadLogisticIncomingConsumption)
        {
            return roadLogisticIncomingConsumption * Constants.IncomingDieselEmissionFactor;
        }

        private static float CalculateOutgoingDieselEmission(float roadLogisticOutgoingConsumption)
        {
            return roadLogisticOutgoingConsumption*Constants.OutgoingDieselEmissionFactor ;
        }

        private static float CalculateCoalEmission(float coalConsumption)
        {
            return coalConsumption * Constants.CoalEmissionFactor;
        }

        private static float CalculateBioMassEmission(float bioMassConsumption)
        {
            return bioMassConsumption * Constants.BioMassEmissionFactor;
        }

        private static float CalculateElectricityEmission(float electricityConsumption)
        {
            return electricityConsumption * Constants.ElectricityEmissionFactor;
        }

        

    }
}
