using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedSocial.Models;

namespace RedSocial.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly RedSocialBDContext _context;

        public UsuariosController(RedSocialBDContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {

            return View(await _context.Usuario.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Email == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(String Email, String Password)
        {
            bool inicioSesion = false;
            var usuario = await _context.Usuario.FindAsync(Email);
            if (usuario != null)
            {
                if (usuario.Password == Password)
                {
                    Autenticacion.login(HttpContext, Email);
                    inicioSesion = true;
                }
                

            }
            if (inicioSesion)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Email", "Usuario y/o contraseña incorrectos");
                ModelState.AddModelError("Password", "Usuario y/o contraseña incorrectos");
                return View();
            }
            
        }
        public IActionResult Logout()
        {
            Autenticacion.logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Logout(String Email, String Password)
        {
            Autenticacion.logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }
        
        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Password,Nombre,Apellido,PreguntaSecreta,RespuestaSecreta,FechaCreacion")] Usuario usuario, string password2)
        {
            if (usuario.Password != password2) ModelState.AddModelError("password2", "Las contraseñas no coinciden");
            var userCheck = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Email == usuario.Email);
            if (userCheck != null)
            {
                ModelState.AddModelError("Email", "Usuario ya registrado");
            }
            if (ModelState.IsValid)
            {
                usuario.FechaCreacion = DateTime.Now;
                usuario.Email = usuario.Email.ToLower();
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,Password,Nombre,Apellido,PreguntaSecreta,RespuestaSecreta,FechaCreacion")] Usuario usuario)
        {
            if (id != usuario.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.Email == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        
        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(string id)
        {
            return _context.Usuario.Any(e => e.Email == id);
        }
    }
}
