using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities.Account;
using CleanArchitecture.Infrastructure.Repositories.Entities.User;
using MediatR;

namespace CleanArchitecture.Application.Entities.UserCommands.Create;

public class CreateUserCommand : IBaseCommand
{
    public CreateUserCommand(string firstName, string lastName, string nationalCode, string avatar, string password, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        NationalCode = nationalCode;
        Avatar = avatar;
        Password = password;
        PhoneNumber = phoneNumber;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public string Avatar { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}

public class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;

    public CreateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = new Users(request.FirstName,request.LastName,request.NationalCode,request.Avatar,request.Password,request.PhoneNumber);
            if (user == null)
                throw new Exception();

            await _repository.AddAsync(user);
            await _repository.Save();
            return Unit.Value;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
