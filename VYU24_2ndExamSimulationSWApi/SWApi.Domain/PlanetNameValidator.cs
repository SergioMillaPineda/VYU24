namespace SWApi.Domain
{
    public class PlanetNameValidator
    {
        public string Name { get; set; }

        public PlanetNameValidator(string name)
        {
            Name = name;
        }

        public bool IsValid(List<string> existingPlanets)
        {
            return existingPlanets.Contains(Name);
        }
    }
}
