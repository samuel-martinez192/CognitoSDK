using Amazon.AspNetCore.Identity.Cognito;
using Amazon.Extensions.CognitoAuthentication;
using Microsoft.AspNetCore.Identity;

namespace CognitoSDK.Services
{
    public interface ICognitoService
    {
        Task<bool> CreateUser(string email, string password);
        Task<bool> ValidateUserCode(string email, string cofirmationCode);
    }

    public class CognitoService : ICognitoService
    {
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;

        public CognitoService(UserManager<CognitoUser> userManager, CognitoUserPool pool)
        {
            _userManager = userManager;
            _pool = pool;
        }

        public async Task<bool> CreateUser(string email, string password)
        {
            bool result = false;
            var user = _pool.GetUser(email);

            if (user != null && user.Status == null)
            {
                var userCreated = await _userManager.CreateAsync(user, password);
                result = userCreated.Succeeded;
            }

            return result;
        }

        public async Task<bool> ValidateUserCode(string email, string cofirmationCode)
        {
            bool result = false;
            CognitoUser user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var userConfirmed = await ((CognitoUserManager<CognitoUser>)_userManager)
                    .ConfirmSignUpAsync(user, cofirmationCode, true);

                result = userConfirmed.Succeeded;
            }

            return result;
        }
    }
}
