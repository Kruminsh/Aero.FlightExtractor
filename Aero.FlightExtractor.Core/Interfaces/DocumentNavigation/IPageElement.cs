namespace Aero.FlightExtractor.Core.Interfaces.DocumentNavigation
{
    /// <summary>
    /// Abstraction of a page element (e.g. Word)
    /// </summary>
    public interface IPageElement
    {
        /// <summary>
        /// Page element text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Element location in page
        /// </summary>
        public ILocation Location { get; }
    }
}
