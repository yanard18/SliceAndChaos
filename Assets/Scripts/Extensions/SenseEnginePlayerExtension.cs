namespace DenizYanar.External.Sense_Engine.Scripts.Core
{
    public static class SenseEnginePlayerExtension
    {
        public static void PlayIfExist(this SenseEnginePlayer sense)
        {
            if(sense != null)
                sense.Play();
        }
    }
}
