using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Services;
using Aero.FlightExtractor.Pdf.DocumentNavigation;
using Aero.FlightExtractor.Pdf.Specifications.Providers;
using Microsoft.Extensions.DependencyInjection;

const string filePath = "C:\\Users\\lvkru\\Desktop\\Personal\\capzlog\\sample-file.pdf";

var services = new ServiceCollection();
ConfigureServices(services);

using (var serviceProvider = services.BuildServiceProvider())
using (var scope = serviceProvider.CreateScope())
{
    var flightExtractorService = scope.ServiceProvider.GetRequiredService<IFlightExtractorService>();
    var result = flightExtractorService.ExtractFlightData(filePath);

    Console.WriteLine("Extracted Flights:");
    foreach (var item in result.Flights)
    {
        Console.WriteLine($"{item.Flight.FlightNumber} - {item.Flight.Date}");
    }

    Console.ReadLine();
}



static void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IDocumentAccessor, PdfDocumentAccessor>();
    services.AddTransient<IChapterSpecProvider, PdfChapterSpecProvider>();
    services.AddTransient<IFlightExtractorService, FlightExtractorService>();
}