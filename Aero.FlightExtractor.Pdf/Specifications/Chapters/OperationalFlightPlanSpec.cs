﻿using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;
using Aero.FlightExtractor.Core.Interfaces.Services;
using Aero.FlightExtractor.Core.Interfaces.Specifications;
using Aero.FlightExtractor.Core.Models.Chapters;
using Aero.FlightExtractor.Core.Services;
using Aero.FlightExtractor.Pdf.Specifications.Chapters.FieldResolvers.OperationalFlightPlan;
using Aero.FlightExtractor.Pdf.Specifications.Chapters.Fields.OperationalFlightPlan;

namespace Aero.FlightExtractor.Pdf.Specifications.Chapters
{
    /// <summary>
    /// Specification for Operational Flight Plan Chapter
    /// </summary>
    internal sealed class OperationalFlightPlanSpec : IChapterSpecification<OperationalFlightPlan>
    {
        public bool BeginsIn(IPage page)
        {
            var firstWords = page.GetPageElements().Take(30);
            var firstWordsText = string.Join(" ", firstWords.Select(x => x.Text));
            return firstWordsText.Contains("Operational Flight Plan", StringComparison.CurrentCultureIgnoreCase) && firstWordsText.Contains("Page 1", StringComparison.CurrentCultureIgnoreCase);
        }

        public IReadOnlyDictionary<string, IFieldResolver> GetFieldResolvers(OperationalFlightPlan chapter)
        {
            return new Dictionary<string, IFieldResolver>()
            {
                { nameof(chapter.Flight), new FlightIdentityResolver() },
                { nameof(chapter.AircraftRegistration), new AircraftRegistrationResolver() },
                { nameof(chapter.Route), new RouteFieldResolver() },
                { nameof(chapter.DepartureTime), new DepartureTimeResolver() },
                { nameof(chapter.ArrivalTime), new ArrivalTimeResolver() },
                { nameof(chapter.AlternateAirdrome1), new FirstAlternateAirdromeResolver() },
                { nameof(chapter.AtcSign), new AtcSignResolver() },
                { nameof(chapter.FirstAndLastNavPoint), new FirstAndLastPointResolver() },
                { nameof(chapter.ZeroFuelMass), new ZeroFuelMassResolver() },
                { nameof(chapter.TimeToDestination), new TimeToDestination() },
                { nameof(chapter.FuelToDestination), new FuelToDestinationResolver() },
                { nameof(chapter.TimeToAlternate), new TimeToAlternateResolver() },
                { nameof(chapter.FuelToAlternate), new FuelToAlternateResolver() },
                { nameof(chapter.MinimumFuelRequired), new MinimumFuelRequiredResolver() },
                { nameof(chapter.Gain), new GainResolver() },
            };
        }

        public ChapterExtractor<OperationalFlightPlan> CreateExtractor() => ChapterExtractor<OperationalFlightPlan>.Initialize(this);

        IChapterExtractor IChapterSpecification.CreateExtractor() => CreateExtractor();
    }
}
