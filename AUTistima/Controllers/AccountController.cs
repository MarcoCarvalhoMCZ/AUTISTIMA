using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AUTistima.Models;
using AUTistima.ViewModels;
using AUTistima.Models.Enums;
using AUTistima.Data;
using AUTistima.Services;

namespace AUTistima.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AccountController> _logger;
    private readonly IPushNotificationService _pushService;
    private readonly IActivityTrackingService _activityService;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context,
        ILogger<AccountController> logger,
        IPushNotificationService pushService,
        IActivityTrackingService activityService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _logger = logger;
        _pushService = pushService;
        _activityService = activityService;
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
                
                // Rastrear atividade de login
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    await _activityService.RegistrarAtividadeComContexto(
                        user.Id, 
                        TipoAtividade.Login, 
                        HttpContext,
                        detalhes: $"Login via {(model.RememberMe ? "sessão persistente" : "sessão normal")}");
                }
                
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
                
                // Criar notificação de boas-vindas
                await NotificacoesController.CriarNotificacao(
                    _context,
                    user.Id,
                    "🌟 Bem-vinda ao AUTistima!",
                    $"Olá, {model.NomeCompleto.Split(' ').First()}! Estamos muito felizes em ter você aqui. Você não está sozinha nessa jornada! 💕",
                    TipoNotificacao.BoasVindas,
                    "/Home/Sobre"
                );
                
                await _signInManager.SignInAsync(user, isPersistent: false);
                
                // Rastrear atividade de cadastro
                await _activityService.RegistrarAtividadeComContexto(
                    user.Id,
                    TipoAtividade.Cadastro,
                    HttpContext,
                    detalhes: $"Novo cadastro: {model.TipoPerfil}");
                
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
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            await _activityService.RegistrarAtividade(user.Id, TipoAtividade.Logout);
        }
        
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
        
        var viewModel = new ProfileViewModel
        {
            Id = user.Id,
            NomeCompleto = user.NomeCompleto,
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber,
            TipoPerfil = user.TipoPerfil,
            SobreMim = user.SobreMim,
            DataNascimento = user.DataNascimento,
            CPF = user.CPF,
            RG = user.RG,
            CEP = user.CEP,
            Endereco = user.Endereco,
            NumeroEndereco = user.NumeroEndereco,
            Complemento = user.Complemento,
            Bairro = user.Bairro,
            Cidade = user.Cidade,
            Estado = user.Estado,
            CNPJ = user.CNPJ,
            NomeEmpresa = user.NomeEmpresa,
            RegistroProfissional = user.RegistroProfissional,
            Especialidade = user.Especialidade,
            FotoPerfilUrl = user.FotoPerfilUrl
        };
        
        return View(viewModel);
    }
    
    // POST: Account/Profile
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        
        // Atualizar dados básicos
        user.NomeCompleto = model.NomeCompleto;
        user.PhoneNumber = model.PhoneNumber;
        user.SobreMim = model.SobreMim;
        
        // Dados pessoais
        user.DataNascimento = model.DataNascimento;
        user.CPF = model.CPF;
        user.RG = model.RG;
        
        // Endereço
        user.CEP = model.CEP;
        user.Endereco = model.Endereco;
        user.NumeroEndereco = model.NumeroEndereco;
        user.Complemento = model.Complemento;
        user.Bairro = model.Bairro;
        user.Cidade = model.Cidade;
        user.Estado = model.Estado;
        
        // Dados de Empresa (se aplicável)
        if (user.TipoPerfil == TipoPerfil.Empresa)
        {
            user.CNPJ = model.CNPJ;
            user.NomeEmpresa = model.NomeEmpresa;
        }
        
        // Dados de Profissional (se aplicável)
        if (user.TipoPerfil == TipoPerfil.ProfissionalSaude || user.TipoPerfil == TipoPerfil.ProfissionalEducacao)
        {
            user.RegistroProfissional = model.RegistroProfissional;
            user.Especialidade = model.Especialidade;
        }
        
        user.FotoPerfilUrl = model.FotoPerfilUrl;
        
        var result = await _userManager.UpdateAsync(user);
        
        if (result.Succeeded)
        {
            TempData["Mensagem"] = "Perfil atualizado com sucesso! 💕";
            return RedirectToAction(nameof(Profile));
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        
        return View(model);
    }
    
    // GET: Account/ChangePassword
    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }
    
    // POST: Account/ChangePassword
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }
        
        var result = await _userManager.ChangePasswordAsync(user, model.SenhaAtual, model.NovaSenha);
        
        if (result.Succeeded)
        {
            await _signInManager.RefreshSignInAsync(user);
            TempData["Mensagem"] = "Senha alterada com sucesso! 🔐";
            return RedirectToAction(nameof(Profile));
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, TranslateError(error.Code));
        }
        
        return View(model);
    }

    // GET: Account/AccessDenied
    public IActionResult AccessDenied()
    {
        return View();
    }

    // POST: Account/UploadFoto
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadFoto(IFormFile fotoPerfilFile)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        if (fotoPerfilFile == null || fotoPerfilFile.Length == 0)
        {
            TempData["Erro"] = "Selecione uma imagem para fazer upload. 📸";
            return RedirectToAction(nameof(Profile));
        }

        // Validar tipo de arquivo
        var extensoesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extensao = Path.GetExtension(fotoPerfilFile.FileName).ToLowerInvariant();

        if (!extensoesPermitidas.Contains(extensao))
        {
            TempData["Erro"] = "Apenas imagens (JPG, PNG, GIF) são permitidas. 📸";
            return RedirectToAction(nameof(Profile));
        }

        // Validar tamanho (máximo 5MB)
        if (fotoPerfilFile.Length > 5 * 1024 * 1024)
        {
            TempData["Erro"] = "A imagem não pode exceder 5MB. 📸";
            return RedirectToAction(nameof(Profile));
        }

        try
        {
            // Criar diretório se não existir
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profile-photos");
            Directory.CreateDirectory(uploadDir);

            // Gerar nome único para o arquivo
            var nomeArquivo = $"{user.Id}{extensao}";
            var caminhoCompleto = Path.Combine(uploadDir, nomeArquivo);

            // Salvar arquivo
            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await fotoPerfilFile.CopyToAsync(stream);
            }

            // Atualizar URL no banco de dados
            user.FotoPerfilUrl = $"/uploads/profile-photos/{nomeArquivo}";
            var resultado = await _userManager.UpdateAsync(user);

            if (resultado.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                TempData["Mensagem"] = "Foto de perfil atualizada com carinho! 💕";
            }
            else
            {
                TempData["Erro"] = "Erro ao salvar a foto. Tente novamente. 🤗";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao fazer upload de foto");
            TempData["Erro"] = "Erro ao processar a imagem. Tente novamente. 🤗";
        }

        return RedirectToAction(nameof(Profile));
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
