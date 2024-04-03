using Aero.FlightExtractor.Core.Models;

namespace Aero.FlightExtractor.Core.Interfaces.Services
{
    /// <summary>
    /// Flight Data extractor service
    /// </summary>
    public interface IFlightExtractorService
    {
        /// <summary>
        /// Extract flight data from the provided file
        /// </summary>
        /// <param name="documentPath">access path to document</param>
        /// <returns></returns>
        public FlightExtractionResult ExtractFlightData(string documentPath);
    }
}
