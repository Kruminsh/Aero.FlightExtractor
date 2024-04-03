using Aero.FlightExtractor.Core.Models.Chapters.Fields;

namespace Aero.FlightExtractor.Core.Models.Chapters
{
    /// <summary>
    /// Operational Flight Plan data object
    /// </summary>
    public class CrewBriefing : ChapterBase
    {
        public Passengers? Passengers { get; set; }
        public int? DryOperatingWeight { get; set; }
        public float? DryOperatingIndex { get; set; }
        public IReadOnlyCollection<CrewMember>? Crew { get; set; }
    }
}
