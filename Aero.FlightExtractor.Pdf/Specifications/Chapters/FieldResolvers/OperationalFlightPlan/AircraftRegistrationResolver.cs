﻿using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Specifications;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.OperationalFlightPlan
{
    /// <summary>
    /// Aircraft Registration field resolver for Operational Flight Plan chapter
    /// </summary>
    internal sealed class AircraftRegistrationResolver : FieldResolverBase<string?>
    {
        public override string? ResolveFrom(IPage page)
        {
            var words = page.GetPageElements().ToList();
            if (words.FirstOrDefault(x => x.Text == "Reg.:") is IPageElement regLabel)
            {
                var idx = words.IndexOf(regLabel);
                return words[idx + 1].Text;
            }

            return default;
        }
    }
}
