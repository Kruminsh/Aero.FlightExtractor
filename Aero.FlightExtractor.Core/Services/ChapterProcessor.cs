using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters;

namespace Aero.FlightExtractor.Core.Services
{
    public sealed class ChapterProcessor<T> : IChapterProcessor where T : ChapterBase, new()
    {
        private readonly IReadOnlyDictionary<string, IFieldResolver> _fieldResolvers;
        private readonly IDictionary<string, object?> _extractedData = new Dictionary<string, object?>();
        private readonly T _chapter;

        private ChapterProcessor(IChapterSpecification<T> specification)
        {
            _chapter = new T();
            _fieldResolvers = specification.GetFieldResolvers(_chapter);
        }

        public static ChapterProcessor<T> Initialize(IChapterSpecification<T> specification)
        {
            return new(specification);
        }

        public ChapterProcessor<T> ExtractFieldsIfAny(IPage page)
        {
            foreach (var resolver in _fieldResolvers.Where(x => !_extractedData.ContainsKey(x.Key)))
            {
                var fieldResolver = resolver.Value;
                var value = fieldResolver.ResolveFrom(page);
                if (value is not null)
                {
                    _extractedData.Add(resolver.Key, value);
                }
            }

            return this;
        }

        public bool AllFieldsExtracted()
        {
            return _extractedData.Count == _fieldResolvers.Count;
        }

        public T Finalize()
        {
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (_extractedData.TryGetValue(propertyInfo.Name, out var value))
                {
                    propertyInfo.SetValue(_chapter, value);
                }
            }
            return _chapter;
        }

        public IChapterProcessor Initialize(IChapterSpecification chapterSpecification) => Initialize(chapterSpecification);

        IChapterProcessor IChapterProcessor.ExtractFieldsIfAny(IPage page) => ExtractFieldsIfAny(page);

        ChapterBase IChapterProcessor.Finalize() => Finalize();
    }
}
