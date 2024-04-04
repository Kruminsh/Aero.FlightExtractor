using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for ATC call sign field in Operational Flight Plan
    /// </summary>
    public class AtcSignResolver : FieldResolverBase<string?>
    {
        public override string? ResolveFrom(IPage page)
        {
            var pageElements = page.GetPageElements().ToList();
            if (pageElements.FirstOrDefault(x => x.Text == "ATC:") is IPageElement atcLabel)
            {
                var lblIndex = pageElements.IndexOf(atcLabel);
                var atcSign = pageElements[lblIndex + 1].Text;
                return !string.IsNullOrWhiteSpace(atcSign) ? atcSign : default;
            }

            return default;
        }
    }
}
