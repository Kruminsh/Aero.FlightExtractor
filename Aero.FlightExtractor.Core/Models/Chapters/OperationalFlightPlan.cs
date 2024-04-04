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
        public string? AtcSign { get; set; }
        public string? FirstAndLastNavPoint { get; set; }
        public int? ZeroFuelMass { get; set; }
        public TimeSpan? TimeToDestination { get; set; }
        public float? FuelToDestination { get; set; }
        public TimeSpan? TimeToAlternate { get; set; }
        public float? FuelToAlternate { get; set; }
        public float? MinimumFuelRequired { get; set; }
        public int? Gain { get; set; }
    }
}
