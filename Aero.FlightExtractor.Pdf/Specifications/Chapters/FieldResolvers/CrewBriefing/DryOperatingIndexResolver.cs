using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.CrewBriefing
{
    /// <summary>
    /// Resolver for DOI Field in Crew Briefing Chapter
    /// </summary>
    internal sealed class DryOperatingIndexResolver : FieldResolverBase<float?>
    {
        public override float? ResolveFrom(IPage page)
        {
            var elements = page.GetPageElements().ToList();
            if (elements.FirstOrDefault(x => x.Text == "DOI:") is IPageElement dowLabel)
            {
                var labelIndex = elements.IndexOf(dowLabel);
                var dowTextValue = elements[labelIndex + 1].Text;
                if (!string.IsNullOrEmpty(dowTextValue))
                {
                    if (float.TryParse(dowTextValue, out var value))
                    {
                        return value;
                    }
                }
            }

            return default;
        }
    }
}
