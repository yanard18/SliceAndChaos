namespace DenizYanar
{
    public class MagnetConfigurations
    {
        public readonly EMagnetPolar m_Polar;
        public readonly float m_Power; 
        public readonly float m_Radius;
        public readonly float m_DistanceScale;

        public MagnetConfigurations(EMagnetPolar polar, float power, float radius, float distanceScale)
        {
            m_Polar = polar;
            m_Power = power;
            m_Radius = radius;
            m_DistanceScale = distanceScale;
        }
        
    }
}