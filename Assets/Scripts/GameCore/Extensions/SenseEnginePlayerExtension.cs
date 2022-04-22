namespace DenizYanar.SenseEngine
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
