using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.ExtractionResults;

namespace Aero.FlightExtractor.Core.Interfaces.Services
{
    /// <summary>
    /// Chapter processor interface
    /// </summary>
    public interface IChapterExtractor
    {
        IChapterExtractor Initialize(IChapterSpecification chapterSpecification);
        IChapterExtractor ExtractFieldDataFrom(IPage page);
        ChapterExtractionResult Finalize();
    }
}
