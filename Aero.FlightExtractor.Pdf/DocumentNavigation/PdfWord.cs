using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using UglyToad.PdfPig.Content;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    public sealed class PdfWord(Word word) : IPageElement
    {
        private readonly ILocation _location = new PdfWordLocation(word.BoundingBox);

        public string Text => word.Text;
        public ILocation Location => _location;
    }
}
