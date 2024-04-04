namespace Aero.FlightExtractor.Core.Models.Chapters
{
    /// <summary>
    /// Operational Flight Plan data object
    /// </summary>
    public class OperationalFlightPlan : ChapterBase
    {
        public string? AircraftRegistration { get; set; }
        public string? Route { get; set; }
        public TimeSpan? DepartureTime { get; set; }
        public TimeSpan? ArrivalTime { get; set; }
        public string? AlternateAirdrome1 { get; set; }
        public string? AlternateAirdrome2 { get; set; }
        public string? ATC { get; set; }
        public float? FuelToDestination { get; set; }
    }
}
