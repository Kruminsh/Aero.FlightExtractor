using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters;

namespace Aero.FlightExtractor.Core.Interfaces.Services
{
    /// <summary>
    /// Chapter processor interface
    /// </summary>
    public interface IChapterProcessor
    {
        IChapterProcessor Initialize(IChapterSpecification chapterSpecification);
        IChapterProcessor ExtractFieldsIfAny(IPage page);
        public bool AllFieldsExtracted();
        ChapterBase Finalize();
    }
}
