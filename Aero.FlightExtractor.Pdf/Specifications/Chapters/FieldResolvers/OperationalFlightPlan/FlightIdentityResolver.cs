using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models;
using System.Globalization;
using System.Security.Cryptography;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.OperationalFlightPlan
{
    /// <summary>
    /// Flight Identity Resolver for Operational Flight Plan
    /// </summary>
    public class FlightIdentityResolver : FieldResolverBase<FlightIdentity>
    {
        public override FlightIdentity? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "FltNr:") is IPageElement flightNrLabel)
            {
                var labelIndex = words.IndexOf(flightNrLabel);
                var flightNumber = words[labelIndex + 1].Text;
                if (words.FirstOrDefault(x => x.Text == "Date:") is IPageElement dateLabel)
                {
                    labelIndex = words.IndexOf(dateLabel);
                    var dateText = words[labelIndex + 1].Text;
                    var dateTime = DateTime.ParseExact(dateText, "ddMMMyy", CultureInfo.InvariantCulture);
                    var flightDate = DateOnly.FromDateTime(dateTime);

                    return new FlightIdentity(flightNumber, flightDate);

                }
            }

            return null;
        }
    }
}
