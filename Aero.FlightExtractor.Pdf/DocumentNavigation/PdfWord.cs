using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using UglyToad.PdfPig.Content;

namespace Aero.FlightExtractor.Pdf.DocumentNavigation
{
    /// <summary>
    /// PdfPig Word wrapper (considered as an page element)
    /// </summary>
    /// <param name="word"></param>
    public sealed class PdfWord(Word word) : IPageElement
    {
        private readonly ILocation _location = new PdfWordLocation(word.BoundingBox);

        public string Text => word.Text;
        public ILocation Location => _location;
    }
}
