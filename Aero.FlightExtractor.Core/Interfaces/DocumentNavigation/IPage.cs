namespace Aero.FlightExtractor.Core.Interfaces.DocumentNavigation
{
    /// <summary>
    /// Abstraction of a document page
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// Page Number
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// Page text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Enumerable for page elements
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPageElement> GetPageElements();
    }
}
