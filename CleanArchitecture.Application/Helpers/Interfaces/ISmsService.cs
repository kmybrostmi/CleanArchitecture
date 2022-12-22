namespace CleanArchitecture.Application.Helpers.Interfaces;
public interface ISmsService
{
    Task SendVirificationCode(string mobile, string activeCode);
}
