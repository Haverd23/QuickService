using Microsoft.Extensions.DependencyInjection;
using QUS.Core.Mediator.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Service.Application.Service
{
    public class QueryDispatcher : IQueryDispatcher
    {
        public IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();
            if (handler == null)
                throw new InvalidOperationException($"Handler não encontrado para a query: {typeof(TQuery).Name}");
            return await handler.HandleAsync(query);
        }
    }
}
