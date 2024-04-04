using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.CrewBriefing
{
    /// <summary>
    /// Resolver for DOW Field in Crew Briefing Chapter
    /// </summary>
    public class DryOperatingWeightResolver : FieldResolverBase<int?>
    {
        public override int? ResolveFrom(IPage page)
        {
            var elements = page.GetPageElements().ToList();
            if (elements.FirstOrDefault(x => x.Text == "DOW:") is IPageElement dowLabel)
            {
                var labelIndex = elements.IndexOf(dowLabel);
                var dowTextValue = elements[labelIndex + 1].Text;
                if (!string.IsNullOrEmpty(dowTextValue))
                {
                    if (int.TryParse(dowTextValue.Replace("kg", "").Trim(), out var value))
                    {
                        return value;
                    }
                }
            }

            return default;
        }
    }
}
