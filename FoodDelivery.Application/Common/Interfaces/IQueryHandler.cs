using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Common.Interfaces
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : class
    {
        Task<TResult> HandleAsync(TQuery query,
            CancellationToken cancellationToken = default);
    }
}
