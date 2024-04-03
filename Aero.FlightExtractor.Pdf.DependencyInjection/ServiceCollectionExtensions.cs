using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Services;
using Aero.FlightExtractor.Pdf.DocumentNavigation;
using Aero.FlightExtractor.Pdf.Specifications.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Aero.FlightExtractor.Pdf.DependencyInjection
{
    /// <summary>
    /// IServiceCollection extension methods for registering PDF Flight Extractor in IoC Container
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigurePdfFlightExtractor(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDocumentAccessor, PdfDocumentAccessor>();
            serviceCollection.AddTransient<IChapterSpecProvider, PdfChapterSpecProvider>();
            serviceCollection.AddTransient<IFlightExtractorService, FlightExtractorService>();
            return serviceCollection;
        }
    }
}
