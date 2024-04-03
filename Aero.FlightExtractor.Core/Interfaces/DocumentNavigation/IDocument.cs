namespace Aero.FlightExtractor.Core.Interfaces.DocumentNavigation
{
    /// <summary>
    /// Abstraction of a document
    /// </summary>
    public interface IDocument : IDisposable
    {
        public IEnumerable<IPage> GetPages();
    }
}
