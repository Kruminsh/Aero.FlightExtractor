using Aero.FlightExtractor.Core.ErrorHandling.Exceptions;
using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan
{
    /// <summary>
    /// Resolver for ZFM field in Operational Flight Plan chapter
    /// </summary>
    internal sealed class ZeroFuelMassResolver : FieldResolverBase<int?>
    {
        public override int? ResolveFrom(IPage page)
        {
            var pageElements = page.GetPageElements().ToList();
            if (pageElements.FirstOrDefault(x => x.Text == "ZFM:") is IPageElement atcLabel)
            {
                var lblIndex = pageElements.IndexOf(atcLabel);
                var value = pageElements[lblIndex + 1].Text;
                if (int.TryParse(value, out var zeroFuelMass))
                {
                    return zeroFuelMass;
                }

                throw new FieldExtractionException("Failed to parse zero fuel mass", page.Number, "ZeroFuelMass");
            }

            return default;
        }
    }
}
