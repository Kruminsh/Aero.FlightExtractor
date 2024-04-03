using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Models.Chapters;
using Aero.FlightExtractor.Core.Services;

namespace Aero.FlightExtractor.Core.Interfaces.Specifications
{
    /// <summary>
    /// Non-generic interface for chapter specification
    /// </summary>
    public interface IChapterSpecification {
        public bool BeginsIn(IPage page);
        public IChapterExtractor CreateExtractor();
    }

    /// <summary>
    /// Generic interface for chapter specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IChapterSpecification<T> : IChapterSpecification where T : ChapterBase, new()
    {
        public new ChapterExtractor<T> CreateExtractor();
        public IReadOnlyDictionary<string, IFieldResolver> GetFieldResolvers(T chapter);
    }
}
