using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace HashCode.Console
{
	public static class FileHelper
	{
		private static string InputPath => Path.Combine(Directory.GetCurrentDirectory(), "Input");

		private static string OutputPath => Path.Combine(Directory.GetCurrentDirectory());

		public static Simulation ReadFromFile(string fileName)
		{
			var input = new Simulation();

            int counter = 0;
			string line;
			StreamReader file = new StreamReader(Path.Combine(InputPath, fileName));
			while ((line = file.ReadLine()) != null)
			{
				counter++;

				if (counter == 1)
				{
					var parts = line.Split(' ');

					input.Duration = int.Parse(parts[0]);
					input.NumberOfIntersections = int.Parse(parts[1]);
					input.NumberOfStreets = int.Parse(parts[2]);
					input.NumberOfCars = int.Parse(parts[3]);
					input.Bonus = int.Parse(parts[4]);
				}

				if (counter != 1 && counter <= input.NumberOfStreets + 1)
				{
					var parts = line.Split(' ');

                    input.Streets.Add(new Street
					{
						StartIntersection = int.Parse(parts[0]),
						EndIntersection = int.Parse(parts[1]),
						Name = parts[2],
						Duration = int.Parse(parts[3])
					});
				}

				if (counter > input.NumberOfStreets + 1)
				{
					var parts = line.Split(' ');

                    input.Paths.Add(new CarPath
					{
						Id = input.Paths.Count + 1,
						TotalStreets = int.Parse(parts[0]),
						StreetNames = parts.Skip(1).Select(x => x).ToList()
					}); ;
				}
			}

			file.Close();

			return input;
		}

		public static void WriteFileContents(string fileName, string content, string outputPath = null)
		{
            var path = Path.Combine(OutputPath);

			if (outputPath != null)
            {
                path = outputPath;
            }

            var exists = Directory.Exists(path);

			if (!exists)
				Directory.CreateDirectory(path);

			File.WriteAllText(Path.Combine(path, fileName), content);
		}

        public static void CreateZipFile(IEnumerable<FileInfo> files, string archiveName)
        {
            if (files == null) throw new ArgumentNullException(nameof(files));

            using (var stream = File.OpenWrite(archiveName))
            using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create))
            {
                foreach (var item in files)
                {
                    archive.CreateEntryFromFile(item.FullName, item.Name, CompressionLevel.Optimal);
                }
            }
        }
	}
}
