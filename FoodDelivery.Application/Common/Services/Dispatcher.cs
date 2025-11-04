using FoodDelivery.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Common.Services
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher( IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task<TResult> Query<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) 
            where TQuery : class
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return handler.HandleAsync(query, cancellationToken);
        }

        public async Task<TResult> Send<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : class
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return await handler.HandleAsync(command, cancellationToken);
        }
    }
}
