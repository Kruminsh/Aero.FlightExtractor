using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for ALTN1 field in Operational Flight Plan chapter
    /// </summary>
    internal sealed class FirstAlternateAirdromeResolver : FieldResolverBase<string?>
    {
        public override string? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "ALTN1:") is IPageElement altnLabel)
            {
                var idx = words.IndexOf(altnLabel);
                var firstAirdrome = words[idx + 1].Text;
                return !string.IsNullOrWhiteSpace(firstAirdrome) ? firstAirdrome : null;
            }

            return default;
        }
    }
}
