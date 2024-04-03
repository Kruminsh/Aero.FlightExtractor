namespace Aero.FlightExtractor.Core.ErrorHandling.Exceptions
{
    /// <summary>
    /// Exception type for throwing field extraction errors
    /// </summary>
    public class FieldExtractionException : Exception 
    {
        public int? PageNumber { get; private set; }
        public string? FieldName { get; private set; }
        public string? ChapterObject { get; private set; }

        public FieldExtractionException(string message): base(message) { }
        public FieldExtractionException(string message, Exception innerException) : base(message, innerException) { }
        public FieldExtractionException(string message, int pageNumber, string fieldName, Exception innerException) : base(message, innerException) 
        { 
            PageNumber = pageNumber;
            FieldName = fieldName;
        }
    }
}
