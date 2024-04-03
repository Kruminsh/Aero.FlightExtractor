using Aero.FlightExtractor.Core.ErrorHandling;

namespace Aero.FlightExtractor.Core.Models
{
    /// <summary>
    /// Result model for flight extraction
    /// </summary>
    public class FlightExtractionResult
    {
        public IReadOnlyCollection<FlightData> Flights { get; set; } = Array.Empty<FlightData>();
        public IReadOnlyCollection<ExtractionError> Errors { get; set; } = Array.Empty<ExtractionError>();
    }
}
