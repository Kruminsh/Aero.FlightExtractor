using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Models.Chapters;
using Aero.FlightExtractor.Pdf.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

const string filePath = "C:\\Aero\\sample-file.pdf";

var services = new ServiceCollection();
services.ConfigurePdfFlightExtractor();

using (var serviceProvider = services.BuildServiceProvider())
using (var scope = serviceProvider.CreateScope())
{
    var flightExtractorService = scope.ServiceProvider.GetRequiredService<IFlightExtractorService>();
    var result = flightExtractorService.ExtractFlightData(filePath);

    foreach (var flightData in result.Flights)
    {
        Console.WriteLine("<-------------------------- FLIGHT -------------------------->");
        Console.WriteLine($"Flight Number: {flightData.Flight.FlightNumber}");
        Console.WriteLine($"Flight Date: {flightData.Flight.Date}");
        Console.WriteLine();

        foreach (var chapter in flightData.Chapters)
        {
            if (chapter is OperationalFlightPlan operationalFlightPlan)
            {
                Console.WriteLine($"Aircraft registration: {operationalFlightPlan.AircraftRegistration}");
                Console.WriteLine($"Route: {operationalFlightPlan.Route}");
                Console.WriteLine($"Departure Time: {operationalFlightPlan.DepartureTime}");
                Console.WriteLine($"Arrival Time: {operationalFlightPlan.ArrivalTime}");
                Console.WriteLine($"Alternate Airdrome 1: {operationalFlightPlan.AlternateAirdrome1}");
                Console.WriteLine($"Alternate Airdrome 2: {operationalFlightPlan.AlternateAirdrome2}");
                Console.WriteLine($"ATC Call Sign: {operationalFlightPlan.AtcSign}");
                Console.WriteLine($"Route first and last navigation point: {operationalFlightPlan.FirstAndLastNavPoint}");
                Console.WriteLine($"Zero Fuel Mass: {operationalFlightPlan.ZeroFuelMass}");
                Console.WriteLine($"Time To Destination: {operationalFlightPlan.TimeToDestination}");
                Console.WriteLine($"Fuel To Destination: {operationalFlightPlan.FuelToDestination}");
                Console.WriteLine($"Time To Alternate: {operationalFlightPlan.TimeToAlternate}");
                Console.WriteLine($"Fuel To Alternate: {operationalFlightPlan.FuelToAlternate}");
                Console.WriteLine($"Minimum Fuel Required: {operationalFlightPlan.MinimumFuelRequired}");
                Console.WriteLine($"GAIN/LOSS: {operationalFlightPlan.Gain}");
                Console.WriteLine();
            }
            else if (chapter is CrewBriefing crewBriefing)
            {
                if (crewBriefing.Crew is not null && crewBriefing.Crew.Any())
                {
                    Console.WriteLine("Crew:");
                    foreach(var member in crewBriefing.Crew)
                    {
                        Console.WriteLine($"{member.FullName}, {member.Function}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"DOW: {crewBriefing.DryOperatingWeight}");
                Console.WriteLine($"DOI: {crewBriefing.DryOperatingIndex}");
                Console.WriteLine($"Passengers in Economy: {crewBriefing.Passengers?.Economy}");
                Console.WriteLine($"Passengers in Business: {crewBriefing.Passengers?.Business}");
                Console.WriteLine();
            }
        }

        Console.WriteLine("<------------------------------------------------------------>");
        Console.WriteLine();
    }

    Console.WriteLine("Errors");
    if (result.Errors.Count > 0)
    {
        foreach (var error in result.Errors)
        {
            if (error.PageNumber.HasValue) Console.WriteLine($"Page: {error.PageNumber}");
            if (error.ChapterObject != null) Console.WriteLine($"Chapter: {error.ChapterObject}");
            if (error.FieldName != null) Console.WriteLine($"Field: {error.FieldName}");
            if (error.Message != null) Console.WriteLine($"Message: {error.Message}");
            Console.WriteLine();
        }
    }
    else
    {
        Console.WriteLine("None");
    }

    Console.ReadLine();
}