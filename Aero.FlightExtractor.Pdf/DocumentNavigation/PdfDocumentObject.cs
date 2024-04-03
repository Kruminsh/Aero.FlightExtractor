using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using UglyToad.PdfPig;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    public sealed class PdfDocumentObject(string filePath) : IDocument
    {
        private readonly PdfDocument _pdfDocument = PdfDocument.Open(filePath);

        public IEnumerable<IPage> GetPages()
        {
            foreach (var page in _pdfDocument.GetPages())
            {
                yield return new PdfPage(page);
            }
        }

        public void Dispose()
        {
            _pdfDocument.Dispose();
        }
    }
}
