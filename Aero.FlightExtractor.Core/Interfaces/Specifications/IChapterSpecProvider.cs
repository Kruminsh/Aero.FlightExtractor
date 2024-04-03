namespace Aero.FlightExtractor.Core.Interfaces.Specifications
{
    /// <summary>
    /// Chapter specification provider interface
    /// </summary>
    public interface IChapterSpecProvider
    {
        public IReadOnlyCollection<IChapterSpecification> GetChapterSpecifications();
    }
}
