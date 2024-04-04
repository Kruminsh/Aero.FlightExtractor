using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for Minimum Fuel Required field in Operational Flight Plan chapter
    /// </summary>
    internal sealed class MinimumFuelRequiredResolver : FieldResolverBase<float?>
    {
        public override float? ResolveFrom(IPage page)
        {
            var pageElements = page.GetPageElements().ToList();
            if (pageElements.FirstOrDefault(x => x.Text == "MIN:") is IPageElement minLabel)
            {
                var lblIndex = pageElements.IndexOf(minLabel);
                var value = pageElements[lblIndex + 2].Text;
                if (float.TryParse(value, out var minimumFuel))
                {
                    return minimumFuel;
                }

                throw new FieldExtractionException("Failed to parse minimum fuel required", page.Number, "MinimumFuelRequired");
            }

            return default;
        }
    }
}
