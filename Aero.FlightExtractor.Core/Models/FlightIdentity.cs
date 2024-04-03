namespace Aero.FlightExtractor.Core.Models
{
    /// <summary>
    /// Model for a combination of fields that uniquely identify a flight
    /// </summary>
    public record FlightIdentity(string FlightNumber, DateOnly Date) 
    {
        public bool IsValid() 
        {
            return !string.IsNullOrWhiteSpace(FlightNumber);
        }
    }
}
