namespace Aero.FlightExtractor.Core.Interfaces.DocumentNavigation
{
    /// <summary>
    /// Interface for a page element location
    /// </summary>
    public interface ILocation
    {
        public double Bottom { get; }
        public double Top { get; }
        public double Left { get; }
        public double Right { get; }
    }
}
