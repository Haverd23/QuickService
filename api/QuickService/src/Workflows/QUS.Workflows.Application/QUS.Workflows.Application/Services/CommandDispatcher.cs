using QUS.Core.Mediator.Commands;
using Microsoft.Extensions.DependencyInjection;


namespace QUS.Workflows.Application.Services
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command)
            where TCommand : ICommand<TResult>
        {
            var handler = _serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();
            if (handler == null)
                throw new InvalidOperationException($"Handler não encontrado para o comando: {typeof(TCommand).Name}");
            return await handler.HandleAsync(command);
        }
    }
}