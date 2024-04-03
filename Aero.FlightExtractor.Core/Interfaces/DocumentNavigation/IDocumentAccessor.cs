namespace Aero.FlightExtractor.Core.Interfaces.DocumentNavigation
{
    /// <summary>
    /// Document accessor
    /// </summary>
    public interface IDocumentAccessor
    {
        /// <summary>
        /// Open a document
        /// </summary>
        /// <param name="accessPath"></param>
        /// <returns></returns>
        public IDocument Open(string accessPath);
    }
}
