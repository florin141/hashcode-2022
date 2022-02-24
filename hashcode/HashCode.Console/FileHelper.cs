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

		public static Input ReadFromFile(string fileName)
		{
			var input = new Input();

            int counter = 0;
            int contributorCnt = 0;
            int projectCnt = 0;
            
            int skillCnt2 = 0;
            string line;
			StreamReader file = new StreamReader(Path.Combine(InputPath, fileName));
			while ((line = file.ReadLine()) != null)
			{
				counter++;

				if (counter == 1)
				{
					var parts = line.Split(' ');

					input.ContributorCount = int.Parse(parts[0]);
					input.ProjectCount = int.Parse(parts[1]);
                }

                if (counter != 1 && counter <= input.ContributorCount + 1)
                {
                    var parts = line.Split(' ');

                    var cont = new Contributor
                    {
                        Name = parts[0],
                        SkillCount = int.Parse(parts[1])
                    };
                    
                    int skillCnt1 = 0;
                    string skillLine = string.Empty;
                    while ((skillLine = file.ReadLine()) != null && skillCnt1 <= cont.SkillCount + 1)
                    {
                        skillCnt1++;

                        var skillParts = skillLine.Split(' ');

                        var skill = new Skill
                        {
                            Name = skillParts[0],
                            Level = int.Parse(skillParts[1])
                        };

                        cont.Skills.Add(skill);
                    }

                    input.Contributors.Add(cont);
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
