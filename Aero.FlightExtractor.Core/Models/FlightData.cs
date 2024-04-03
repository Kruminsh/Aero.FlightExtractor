using Aero.FlightExtractor.Core.Models.Chapters;

namespace Aero.FlightExtractor.Core.Models
{
    public class FlightData(FlightIdentity flight, IReadOnlyCollection<ChapterBase> chapters) 
    {
        public FlightIdentity Flight => flight;
        public IReadOnlyCollection<ChapterBase> Chapters => chapters;
    }
}
