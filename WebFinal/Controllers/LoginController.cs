using APiProjetoFinal.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebFinal.Models;

namespace WebFinal.Controllers
{
    public class LoginController : Controller
    {

        private readonly DataContext _context;

        public LoginController([FromServices] DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Pacientes");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public  IActionResult LoginUser([Bind("User, Password")] string user, string password)
        {
            var usuario = (from i in _context.Usuarios
                      .Where(u => u.User == user.ToLower() && u.Password == password.ToLower())
                      select i).FirstOrDefault();

            try
            {
                
                    if (usuario != null && (usuario.User == user.ToLower() && usuario.Password == password.ToLower()))
                    {
                        Login(usuario);
                        return RedirectToAction("Index", "Pacientes");
                    }
                    else
                    {
                        ViewBag.Erro = "Usuário e/ou senha incorretos!";
                    }
                
            }
            catch (Exception)
            {
                ViewBag.Erro = "Ocorreu algum erro ao tentar se logar, tente novamente!";
            }

            return RedirectToAction("Index", "Login");
        }

        public IActionResult NovoUsuario()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> NovoUsuario([Bind("User, Password")] string user, string password)
        {
            try
            {
                if (VerifarUsuario(user, password))
                {

                    await _context.Usuarios.AddAsync(new Usuario { User = user.ToLower(), Password = password.ToLower()}); ;
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            { }


            return View();
        }
        private bool VerifarUsuario(string user, string password)
        {
            var usuario = _context.Usuarios.Where(p => p.User == user).FirstOrDefault();

            if (String.IsNullOrWhiteSpace(user) || String.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErroUsuario = "Preencha todos os campos";
                ViewBag.ErroSenha = "Preencha todos os campos";
                return false;
            }
            if (password.Length < 8)
            {
                ViewBag.ErroSenha = "Senha deve ter 8 caractares";
                return false;
            }

            if(usuario != null)
            {
                ViewBag.ErroUsuario = "Nome de usuário já cadastrado";
                return false;
            }

            for (int i = 0; i < 10; i++)
            {
                if (user[i].ToString() == i.ToString())
                {
                    @ViewBag.ErroUsuario = "Digite apenas letras";
                    return false;
                }
            }

            return true;
        }
        private async void Login(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.User),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identidadeDeUsuario = new ClaimsIdentity(claims, "Index");
            ClaimsPrincipal claimPrincipal = new ClaimsPrincipal(identidadeDeUsuario);

            var propriedadesDeAutenticacao = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.ToLocalTime().AddMinutes(3),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal, propriedadesDeAutenticacao);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
