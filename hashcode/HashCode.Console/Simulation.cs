using System.Collections.Generic;

namespace HashCode.Console
{
	public class Simulation
	{
        public Simulation()
        {
            Streets = new List<Street>();
            Paths = new List<CarPath>();
        }

		public int Contributors { get; set; }

		public int Projects { get; set; }

		public int NumberOfStreets { get; set; }

		public int NumberOfCars { get; set; }

		public int Bonus { get; set; }

        public List<Street> Streets { get; set; }

        public List<CarPath> Paths { get; set; }
	}

	public class Contributor
    {
        public string Name { get; set; }

		public int Skills { get; set; }

        public override string ToString()
        {
            return $"{Name} {Skills}";
        }
    }

	public class Skill
    {
        public Skill()
        {
            StreetNames = new List<string>();
        }

		public int Id { get; set; }
		public int TotalStreets { get; set; }

		public List<string> StreetNames { get; set; }

        public override string ToString()
        {
            return $"street to travel: {TotalStreets}, from {string.Join(", ", StreetNames)}";
        }
	}
}
