using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AUTistima.Models;
using AUTistima.ViewModels;
using AUTistima.Models.Enums;

namespace AUTistima.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    // GET: Account/Login
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email, 
                model.Password, 
                model.RememberMe, 
                lockoutOnFailure: false);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("Usuário logado com sucesso.");
                return RedirectToLocal(returnUrl);
            }
            
            ModelState.AddModelError(string.Empty, "E-mail ou senha incorretos.");
        }
        
        return View(model);
    }

    // GET: Account/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto,
                TipoPerfil = model.TipoPerfil,
                Cidade = model.Cidade,
                Estado = model.Estado,
                DataCadastro = DateTime.UtcNow,
                Ativo = true
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                _logger.LogInformation("Novo usuário criado.");
                
                // Adiciona claim do tipo de perfil
                await _userManager.AddClaimAsync(user, 
                    new System.Security.Claims.Claim("TipoPerfil", model.TipoPerfil.ToString()));
                
                await _signInManager.SignInAsync(user, isPersistent: false);
                
                TempData["Mensagem"] = $"Bem-vinda ao AUTistima, {model.NomeCompleto.Split(' ').First()}! 💕";
                return RedirectToAction("Index", "Home");
            }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, TranslateError(error.Code));
            }
        }
        
        return View(model);
    }

    // POST: Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("Usuário deslogado.");
        return RedirectToAction("Index", "Home");
    }

    // GET: Account/Profile
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: Account/AccessDenied
    public IActionResult AccessDenied()
    {
        return View();
    }

    private IActionResult RedirectToLocal(string? returnUrl)
    {
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction("Index", "Home");
    }

    private string TranslateError(string errorCode)
    {
        return errorCode switch
        {
            "DuplicateUserName" => "Este e-mail já está cadastrado.",
            "DuplicateEmail" => "Este e-mail já está cadastrado.",
            "InvalidEmail" => "E-mail inválido.",
            "PasswordTooShort" => "A senha deve ter pelo menos 6 caracteres.",
            "PasswordRequiresDigit" => "A senha deve conter pelo menos um número.",
            "PasswordRequiresLower" => "A senha deve conter pelo menos uma letra minúscula.",
            "PasswordRequiresUpper" => "A senha deve conter pelo menos uma letra maiúscula.",
            _ => "Erro ao criar conta. Tente novamente."
        };
    }
}
