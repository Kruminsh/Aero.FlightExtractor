using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters;
using Aero.FlightExtractor.Core.Services;
using Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.CrewBriefing;
using Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.CrewBriefing;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters
{
    /// <summary>
    /// Crew Briefing chapter specification
    /// </summary>
    public class CrewBriefingSpec : IChapterSpecification<CrewBriefing>
    {
        public bool BeginsIn(IPage page)
        {
            if (page.Text.Contains("Flight Crew Briefing"))
            {
                var elements = page.GetPageElements().ToList();
                var pageOne = elements.FirstOrDefault(x => x.Text == "Page" && elements[elements.IndexOf(x) + 1].Text == "1");
                return pageOne != null;
            }

            return false;
        }

        public IReadOnlyDictionary<string, IFieldResolver> GetFieldResolvers(CrewBriefing chapter)
        {
            return new Dictionary<string, IFieldResolver>()
            {
                { nameof(chapter.Flight), new FlightIdentityResolver() },
                { nameof(chapter.Passengers), new PassengersResolver() },
                { nameof(chapter.Crew), new CrewMemberResolver() },
            };
        }

        public ChapterProcessor<CrewBriefing> CreateProcessor() => ChapterProcessor<CrewBriefing>.Initialize(this);

        IChapterProcessor IChapterSpecification.CreateProcessor() => CreateProcessor();
    }
}
