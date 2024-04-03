using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Pdf.Specifications.Chapters;

namespace Aero.FlightExtractor.Pdf.Specifications.Providers
{
    /// <summary>
    /// Provider for PDF file flight chapter specifications
    /// </summary>
    public class PdfChapterSpecProvider : IChapterSpecProvider
    {
        public IReadOnlyCollection<IChapterSpecification> GetChapterSpecifications()
        {
            return new IChapterSpecification[]
            {
                new OperationalFlightPlanSpec(),
                new CrewBriefingSpec()
            };
        }
    }
}
