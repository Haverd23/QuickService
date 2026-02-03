using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Core.Mediator.Commands
{
    public interface ICommandHandler<TCommand, TResult>
        where TCommand: ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
