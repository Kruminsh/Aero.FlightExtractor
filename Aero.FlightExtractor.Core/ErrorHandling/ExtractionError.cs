namespace Aero.FlightExtractor.Core.ErrorHandling
{

    /// <summary>
    /// Error Object model for extraction error information
    /// </summary>
    public class ExtractionError
    {
        public int PageNumber { get; set; }
        public string ChapterName { get; set; }
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
