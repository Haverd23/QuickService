using Microsoft.Extensions.DependencyInjection;
using QUS.Core.Mediator.Commands;


namespace QUS.Users.Application.Services
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