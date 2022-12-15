using MediatR;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IBaseCommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : IBaseCommand
{
}

public interface IBaseCommandHandler<TCommand, TResponseData> : IRequestHandler<TCommand, TResponseData> where TCommand : IBaseCommand<TResponseData>
{
}
