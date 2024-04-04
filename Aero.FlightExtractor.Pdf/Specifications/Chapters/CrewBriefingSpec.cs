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
    internal sealed class CrewBriefingSpec : IChapterSpecification<CrewBriefing>
    {
        private const string _pageTitle = "Flight Assignment / Flight Crew Briefing";

        public bool BeginsIn(IPage page)
        {
            var firstTenWords = page.GetPageElements().Take(10);
            var firstTenWordText = string.Join(" ", firstTenWords.Select(x => x.Text));
            if (firstTenWordText.StartsWith(_pageTitle, StringComparison.CurrentCultureIgnoreCase))
            {
                // Check, if It is Page 1 of "Flight Assignment / Flight Crew Briefing"
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
                { nameof(chapter.DryOperatingWeight), new DryOperatingWeightResolver() },
                { nameof(chapter.DryOperatingIndex), new DryOperatingIndexResolver() },
                { nameof(chapter.Passengers), new PassengersResolver() },
                { nameof(chapter.Crew), new CrewMemberResolver() },
            };
        }

        public ChapterExtractor<CrewBriefing> CreateExtractor() => ChapterExtractor<CrewBriefing>.Initialize(this);

        IChapterExtractor IChapterSpecification.CreateExtractor() => CreateExtractor();
    }
}
