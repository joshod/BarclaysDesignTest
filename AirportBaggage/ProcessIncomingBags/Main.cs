using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AirportBaggage
{
    partial class ProcessIncomingBags
    {
        static void Main(string[] args)
        {
            Regex matchingFilesRegex = new Regex(FileNameMask);
            string[] matchingFiles = Directory.GetFiles(Path.Combine(WorkingDirectoryPath, InputDirectoryName)).Where(f => matchingFilesRegex.IsMatch(Path.GetFileName(f))).ToArray();

            foreach (var file in matchingFiles)
            {
                try
                {
                    var baggageData = ImportDataFromFlatFile(file);

                    if (baggageData != null && baggageData.IncomingBags != null && baggageData.IncomingBags.Any())
                    {
                        var routedBags = ProcessBagRoutes(baggageData);

                        if (routedBags != null && routedBags.Any())
                        {
                            var resultsFileName = $"{OutputFileNamePrefix}_{Path.GetFileNameWithoutExtension(file)}{Path.GetExtension(file)}";
                            WriteBagRouteResults(routedBags, resultsFileName);
                        }
                    }

                    File.Move(file, Path.Combine(WorkingDirectoryPath, SourceFileArchiveDirectoryName, Path.GetFileName(file)));
                }
                catch (Exception e)
                {
                    //TODO: Trigger alert that file could not be processed (log error, email notification, add to queue for review, etc.) depending on project requirements
                    Console.Write(e);

                    File.Move(file, Path.Combine(WorkingDirectoryPath, ProcessingFailedDirectoryName, Path.GetFileName(file)));
                }
            }
        }
    }
}
