using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    public class PdfDocumentAccessor : IDocumentAccessor
    {
        public IDocument Open(string accessPath)
        {
            return new PdfDocumentObject(accessPath);
        }
    }
}
