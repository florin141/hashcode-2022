using System.Collections.Generic;

namespace HashCode.Console
{
	public class Input
	{
        public Input()
        {
            Contributors = new List<Contributor>();
            Projects = new List<Project>();
        }

        public int ContributorCount { get; set; }

		public int ProjectCount { get; set; }

        public List<Contributor> Contributors { get; set; }

        public List<Project> Projects { get; set; }
    }

	public class Contributor
    {
        public Contributor()
        {
            Skills = new List<Skill>();
        }

        public string Name { get; set; }

		public int SkillCount { get; set; }

        public List<Skill> Skills { get; set; }

        public override string ToString()
        {
            return $"{Name} {Skills}";
        }
    }

	public class Skill
    {
        public string Name { get; set; }
		public int Level { get; set; }

        public override string ToString()
        {
            return $"{Name} {Level}";
        }
	}

    public class Project
    {
        public Project()
        {
            Skills = new List<Skill>();
        }

        public string Name { get; set; }

        public int Duration { get; set; }
        public int Score { get; set; }
        public int BestBefore { get; set; }
        public int NumberOfRoles { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
