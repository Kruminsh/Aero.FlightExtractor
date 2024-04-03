using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters;
using Aero.FlightExtractor.Core.Services;
using Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.OperationalFlightPlan;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters
{
    /// <summary>
    /// Specification for Operational Flight Plan Chapter
    /// </summary>
    public class OperationalFlightPlanSpec : IChapterSpecification<OperationalFlightPlan>
    {
        public bool BeginsIn(IPage page)
        {
            if (page.Text.Contains("Operational Flight Plan") )
            {
                var elements = page.GetPageElements().ToList();
                if (elements.FirstOrDefault(x => x.Text == "Page") is IPageElement pageName)
                {
                    return elements[elements.IndexOf(pageName) + 1].Text == "1";
                }
            }

            return false;
        }

        public IReadOnlyDictionary<string, IFieldResolver> GetFieldResolvers(OperationalFlightPlan chapter)
        {
            return new Dictionary<string, IFieldResolver>()
            {
                { nameof(chapter.Flight), new FlightIdentityResolver() },
                { nameof(chapter.AircraftRegistration), new AirrcraftRegistrationResolver() },
                { nameof(chapter.Route), new RouteFieldResolver() },
                { nameof(chapter.ATC), new FirstAndLastPointResolver() },
                { nameof(chapter.FuelToDestination), new FuelToDestinationResolver() }
            };
        }

        public ChapterProcessor<OperationalFlightPlan> CreateProcessor() => ChapterProcessor<OperationalFlightPlan>.Initialize(this);

        IChapterProcessor IChapterSpecification.CreateProcessor() => CreateProcessor();
    }
}
