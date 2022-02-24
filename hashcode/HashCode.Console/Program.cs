using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace HashCode.Console
{
    class Program
    {
        public static string[] FileNames = {
            "a_an_example.in.txt",
            "b_better_start_small.in.txt",
            "c_collaboration.in.txt",
            "d_dense_schedule.in.txt",
            "e_exceptional_skills.in.txt",
            "f_find_great_mentors.in.txt",
        };

        static void Main(string[] args)
        {
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent?.Parent;
            FileHelper.CreateZipFile(info?.GetFiles(), "source-code.zip");

            foreach (var fileName in FileNames)
            {
                var simulation = FileHelper.ReadFromFile(fileName);

                var intersections = Solve(simulation);

                GenerateSubmission(intersections, fileName);

                Trace.WriteLine($"Completed {fileName}...");
                System.Console.WriteLine($"Completed {fileName}...");
            }
        }

        private static List<Intersection> Solve(Simulation simulation)
        {
            var intersections = Enumerable
                .Range(0, simulation.NumberOfIntersections).Select(x => new Intersection { Id = x })
                .ToList();

            foreach (var street in simulation.Streets)
            {
                intersections[street.StartIntersection].Outgoing.Add(street);
                intersections[street.EndIntersection].Incoming.Add(street);
            }

            var streetsPerUseDescending = simulation.Paths
                .SelectMany(x => x.StreetNames)
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .ToDictionary(x => x.Key);
            foreach (var intersection in intersections)
            {
                intersection.Incoming = intersection.Incoming
                    .OrderByDescending(x => streetsPerUseDescending.ContainsKey(x.Name) ? streetsPerUseDescending[x.Name].Count() : 0)
                    .ToList();

                var streetsToDelete = new List<Street>();
                foreach (var incoming in intersection.Incoming)
                {
                    if (streetsPerUseDescending.ContainsKey(incoming.Name))
                    {
                        intersection.GreenSeconds.Add((streetsPerUseDescending[incoming.Name].Count() / 25) + 1);
                    }
                    else
                    {
                        streetsToDelete.Add(incoming);
                    }
                }

                foreach (var street in streetsToDelete)
                {
                    intersection.Incoming.Remove(street);
                }
            }

            foreach (var car in simulation.Paths)
            {
                intersections[simulation.Streets.Single(x => x.Name == car.StreetNames[0]).EndIntersection].Cars.Add(car);
            }

            return intersections;
        }

        private static void GenerateSubmission(List<Intersection> intersections, string fileName)
        {
            var sb = new StringBuilder();
            sb.AppendLine(intersections.Count(x => x.GreenSeconds.Any()).ToString());
            foreach (var intersection in intersections.Where(x => x.GreenSeconds.Any()))
            {
                sb.AppendLine(intersection.Id.ToString());
                sb.AppendLine(intersection.Incoming.Count.ToString());
                for (int i = 0; i < intersection.Incoming.Count; i++)
                {
                    sb.AppendLine(intersection.Incoming[i].Name + " " + intersection.GreenSeconds[i]);
                }
            }

            FileHelper.WriteFileContents(fileName.Insert(1, "_result"), sb.ToString());
        }
    }
}
