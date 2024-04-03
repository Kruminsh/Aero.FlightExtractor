using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters.Fields;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.CrewBriefing
{
    /// <summary>
    /// Crew member field resolver for Crew Briefing chapter
    /// </summary>
    public class CrewMemberResolver : FieldResolverBase<IReadOnlyCollection<CrewMember>>
    {
        public override IReadOnlyCollection<CrewMember>? ResolveFrom(IPage page)
        {
            if (page.Text.Contains("Flight Crew Briefing"))
            {
                var words = page.GetPageElements().ToList();
                if (words.FirstOrDefault(x => x.Text == "Crew" && words[words.IndexOf(x) + 1].Text == "Func") is IPageElement firstWord)
                {
                    var startINdex = words.IndexOf(firstWord);
                    while (true)
                    {
                        if (words[startINdex++].Text == "X:")
                            break;
                    }
                };
            }

            return default;
        }
    }
}
