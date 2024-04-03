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

    foreach (var item in result.Flights)
    {
        Console.WriteLine($"Flight Number: {item.Flight.FlightNumber}");
        Console.WriteLine($"Flight Date: {item.Flight.Date}");

        foreach (var chapter in item.Chapters)
        {
            if (chapter is OperationalFlightPlan operationalFlightPlan)
            {
                Console.WriteLine($"Aircraft registration: {operationalFlightPlan.AircraftRegistration}");
                Console.WriteLine($"Route: {operationalFlightPlan.Route}");
                Console.WriteLine($"Alternate Airdrome 1: {operationalFlightPlan.AlternateAirdrome1}");
                Console.WriteLine($"Alternate Airdrome 2: {operationalFlightPlan.AlternateAirdrome2}");
                Console.WriteLine($"Route first and last navigation point: {operationalFlightPlan.ATC}");
                Console.WriteLine($"Fuel To Destination: {operationalFlightPlan.FuelToDestination}");
                Console.WriteLine();
            }
            else if (chapter is CrewBriefing crewBriefing) 
            {
                Console.WriteLine($"DOW: {crewBriefing.DryOperatingWeight}");
                Console.WriteLine($"DOI: {crewBriefing.DryOperatingIndex}");
                Console.WriteLine($"Passengers in Economy: {crewBriefing.Passengers?.Economy}");
                Console.WriteLine($"Passengers in Business: {crewBriefing.Passengers?.Business}");
                Console.WriteLine();
            }
        }
    }

    Console.ReadLine();
}