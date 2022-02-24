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
            "a_an_example.in.txt",
            //"b_better_start_small.in.txt",
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
                var simulation = FileHelper.ReadFromFile(fileName);

                Trace.WriteLine($"Completed {fileName}...");
                System.Console.WriteLine($"Completed {fileName}...");
            }
        }

        private static Output Solve(Input input)
        {
            var output = new Output();
            //var intersections = Enumerable
            //    .Range(0, simulation.NumberOfIntersections).Select(x => new Intersection { Id = x })
            //    .ToList();

            //foreach (var street in simulation.Streets)
            //{
            //    intersections[street.StartIntersection].Outgoing.Add(street);
            //    intersections[street.EndIntersection].Incoming.Add(street);
            //}

            //var streetsPerUseDescending = simulation.Paths
            //    .SelectMany(x => x.StreetNames)
            //    .GroupBy(x => x)
            //    .OrderByDescending(x => x.Count())
            //    .ToDictionary(x => x.Key);
            //foreach (var intersection in intersections)
            //{
            //    intersection.Incoming = intersection.Incoming
            //        .OrderByDescending(x => streetsPerUseDescending.ContainsKey(x.Name) ? streetsPerUseDescending[x.Name].Count() : 0)
            //        .ToList();

            //    var streetsToDelete = new List<Street>();
            //    foreach (var incoming in intersection.Incoming)
            //    {
            //        if (streetsPerUseDescending.ContainsKey(incoming.Name))
            //        {
            //            intersection.GreenSeconds.Add((streetsPerUseDescending[incoming.Name].Count() / 25) + 1);
            //        }
            //        else
            //        {
            //            streetsToDelete.Add(incoming);
            //        }
            //    }

            //    foreach (var street in streetsToDelete)
            //    {
            //        intersection.Incoming.Remove(street);
            //    }
            //}

            //foreach (var car in simulation.Paths)
            //{
            //    intersections[simulation.Streets.Single(x => x.Name == car.StreetNames[0]).EndIntersection].Cars.Add(car);
            //}

            return output;
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