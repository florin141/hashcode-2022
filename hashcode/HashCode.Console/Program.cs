using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace HashCode.Console
{
    class Program
    {
        public static string[] FileNames =
        {
            //"a_an_example.in.txt",
            "b_better_start_small.in.txt",
            //"c_collaboration.in.txt",
            //"d_dense_schedule.in.txt",
            //"e_exceptional_skills.in.txt",
            //"f_find_great_mentors.in.txt",
        };

        static void Main(string[] args)
        {
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent;
            FileHelper.CreateZipFile(info?.GetFiles(), "source-code.zip");

            foreach (var fileName in FileNames)
            {
                var input = FileHelper.ReadFromFile(fileName);

                var result = Solve(input);

                GenerateSubmission(result, fileName);

                Trace.WriteLine($"Completed {fileName}...");
                System.Console.WriteLine($"Completed {fileName}...");
            }
        }

        private static Output Solve(Input input)
        {
            var output = new Output();
            var day = 0;

            do
            {
                bool projectFinished = false;
                foreach (var currentProject in output.Projects.Where(x => x.Duration > 0))
                {
                    currentProject.Duration--;
                    if (currentProject.Duration == 0)
                    {
                        projectFinished = true;
                        for (int i = 0; i < currentProject.Skills.Count; i++)
                        {
                            var contributor = currentProject.Contributors[i];
                            var skill = contributor.Skills.FirstOrDefault(x => x.Name == currentProject.Skills[i].Name);
                            if (skill != null && (skill.Level == currentProject.Skills[i].Level || skill.Level == currentProject.Skills[i].Level - 1))
                            {
                                skill.Level++;
                            }
                            else if(skill == null)
                            {
                                contributor.Skills.Add(new Skill { Name = currentProject.Skills[i].Name, Level = 1 });
                            }
                        }
                    }
                }

                if(!projectFinished && day > 0)
                {
                    ++day;
                    continue;
                }

                bool projectAvailable = true;
                do
                {
                    var assignableContributors = input.Contributors.Where(x => !output.Projects.Any(y => y.Duration > 0 && y.Contributors.Contains(x))).ToList();
                    var project = SelectProject(input, assignableContributors);
                    if (project != null)
                    {
                        output.Projects.Add(project);
                    }
                    else
                    {
                        projectAvailable = false;
                    }
                }
                while (projectAvailable);
                ++day;
            }
            while (input.Projects.Any() && output.Projects.Any(x=>x.Duration > 0));

            return output;
        }

        private static Project SelectProject(Input input, List<Contributor> assignableContributors)
        {
            Project selectedProject = null;


            foreach (var project in input.Projects)
            {
                project.Contributors.Clear();

                bool skipProject = false;
                foreach (var skill in project.Skills)
                {
                    var candidates = assignableContributors.Where(x => x.Skills.Any(y => y.Name == skill.Name && y.Level >= skill.Level) && !project.Contributors.Contains(x));
                    var candidate = candidates.FirstOrDefault();

                    if (candidate != null)
                    {
                        project.Contributors.Add(candidate);
                    }
                    else
                    {
                        project.Contributors.Clear();
                        skipProject = true;
                        break;
                    }
                }

                if (!skipProject)
                {
                    input.Projects.Remove(project);
                    selectedProject = project;
                    break;
                }
            }

            return selectedProject;
        }

        private static void GenerateSubmission(Output output, string fileName)
        {
            var sb = new StringBuilder();
            sb.AppendLine(output.Projects.Count.ToString());

            foreach (var project in output.Projects)
            {
                sb.AppendLine(project.Name);
                sb.AppendLine(string.Join(" ", project.Contributors.Select(x => x.Name)));

            }

            FileHelper.WriteFileContents("result-" + fileName, sb.ToString());
        }
    }
}