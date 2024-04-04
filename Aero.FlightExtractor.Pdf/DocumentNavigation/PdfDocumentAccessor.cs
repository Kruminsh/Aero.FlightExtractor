using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    /// <summary>
    /// PDF Document accessor
    /// </summary>
    public sealed class PdfDocumentAccessor : IDocumentAccessor
    {
        public IDocument Open(string accessPath)
        {
            return new PdfDocumentObject(accessPath);
        }
    }
}
