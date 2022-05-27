using CognitoSDK.Models;
using CognitoSDK.Services;
using Microsoft.AspNetCore.Mvc;

namespace CognitoSDK.Controllers
{
    public class AccountController : Controller
    {
        private ICognitoService _cognitoService;

        public AccountController(ICognitoService cognitoService)
        {
            _cognitoService = cognitoService;
        }

        [HttpGet("SignUp")]
        public async Task<IActionResult> SignUp()
        {
            var model = new SignUpModel();
            return View(model);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var result = await _cognitoService.CreateUser(model.Email, model.Password);

            if (result)
                return View("Confirm");

            return View();
        }

        [HttpGet("Confirm")]
        public async Task<IActionResult> Confirm(ConfirmModel model)
        {
            return View(model);
        }

        [HttpPost("Confirm")]
        public async Task<IActionResult> ConfirmPost(ConfirmModel model)
        {
            var result = await _cognitoService.ValidateUserCode(model.Email, model.Code);

            if (result)
                return RedirectToAction("Index", "Home");

            return View(model);
        }
    }
}
