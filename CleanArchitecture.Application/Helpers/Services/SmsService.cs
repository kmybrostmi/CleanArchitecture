using CleanArchitecture.Application.Helpers.Interfaces;

namespace CleanArchitecture.Application.Helpers.Services;

public class SmsService : ISmsService
{
    public string apiKey = "6A5052717177346B6D385769586475665843567379764A67534C6E73496334716F6663536B38334F6D31633D";
    public async Task SendVirificationCode(string mobile, string activeCode)
    {
        Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);

        await api.VerifyLookup(mobile, activeCode, "Verfy");
    }
}