using Aero.FlightExtractor.Core.Interfaces.DocumentNavigation;

namespace Aero.FlightExtractor.Core.Interfaces.Specifications
{
    /// <summary>
    /// Base non-generic interface for field resolver
    /// </summary>
    public interface IFieldResolver
    {
        public object? ResolveFrom(IPage page);
    }

    /// <summary>
    /// Generic field resolver interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFieldResolver<T> : IFieldResolver
    {
        public new T? ResolveFrom(IPage page);
    }
}
