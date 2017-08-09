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
        //START VARIABLE DEFINITIONS
        //

        const string WorkingDirectoryPath = @"C:\Temp\AirportBaggage\";

        const string InputDirectoryName = "Input";
        const string OutputDirectoryName = "Output";
        const string OutputFileNamePrefix = "PROCESSED";
        const string SourceFileArchiveDirectoryName = "Archived";
        const string ProcessingFailedDirectoryName = "Failed";

        const string SectionDeclaration = "# Section:";
        const string ConveyorSectionName = "Conveyor System";
        const string DeparturesSectionName = "Departures";
        const string BagsSectionName = "Bags";

        const string FileNameMask = @"^Denver_International_[0-9]+\.txt$";

        const string TerminatedFlightId = "ARRIVAL";
        const string BaggageClaimGateId = "BaggageClaim";

        //
        //END VARIABLE DEFINITIONS
    }
}
