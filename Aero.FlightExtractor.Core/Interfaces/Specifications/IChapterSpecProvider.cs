using Aero.FlightExtractor.Core.Models.Chapters;

namespace Aero.FlightExtractor.Core.Interfaces.Specifications
{
    public interface IChapterSpecProvider
    {
        public IReadOnlyCollection<IChapterSpecification> GetChapterSpecifications();
    }
}
