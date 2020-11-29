using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions
{
    public interface IInjector<T>
    {
        Task Run(T item);
        Task Run(IEnumerable<T> items);
    }
}