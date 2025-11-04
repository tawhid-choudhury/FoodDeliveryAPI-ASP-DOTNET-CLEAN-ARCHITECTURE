using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Common.Interfaces
{
    public interface IDispatcher
    {
        Task<TResult> Send<TCommand, TResult>(TCommand command,
            CancellationToken cancellationToken= default) where TCommand:class;
        Task<TResult> Query<TQuery, TResult>(TQuery query,
            CancellationToken cancellationToken= default) where TQuery:class;
    }
}
