namespace DenizYanar
{
    public class MagnetConfigurations
    {
        public readonly EMagnetPolar Polar;
        public readonly float Power; 
        public readonly float Radius;
        public readonly float DistanceScale;

        public MagnetConfigurations(EMagnetPolar polar, float power, float radius, float distanceScale)
        {
            Polar = polar;
            Power = power;
            Radius = radius;
            DistanceScale = distanceScale;
        }
        
    }
}