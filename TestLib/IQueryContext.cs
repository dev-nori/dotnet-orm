using System.Linq;

namespace TestLib
{
    public interface IQueryContext
    {
        IQueryProvider Provider { get; }
    }
}
