using Aero.FlightExtractor.Core.ErrorHandling;
using Aero.FlightExtractor.Core.Models.Chapters;

namespace Aero.FlightExtractor.Core.Models.ExtractionResults
{
    /// <summary>
    /// Model for a Flight chapter extraction reuslt
    /// </summary>
    public sealed class ChapterExtractionResult
    {
        public ChapterBase? Chapter { get; set; }
        public IReadOnlyCollection<ExtractionError> Errors { get; set; } = new List<ExtractionError>();

        private ChapterExtractionResult() { }

        public static ChapterExtractionResult Create(ChapterBase? chapter, IReadOnlyCollection<ExtractionError> extractionErrors)
        {
            return new ChapterExtractionResult
            {
                Chapter = chapter,
                Errors = extractionErrors
            };
        }
    }
}
