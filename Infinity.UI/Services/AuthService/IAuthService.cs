namespace Infinity.UI.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<ResponseAuthentication>> Register(UserCredentials request);

        Task<ServiceResponse<ResponseAuthentication>> Login(UserLogin request);
    }
}
