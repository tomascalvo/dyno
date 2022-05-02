using DevPath.Models;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.Controllers
{
    [RequireHttps] // This attribute is part of SSL configuration. It prevents the browser from showing the user a security warning.
    [AllowAnonymous] // This attribute supercedes the global filter in App_Start/FilterConfig.cs that requires the user to be authenticated before accessing any controller actions. This attribute allows anonymous users to access HomeController actions.
    public class HomeController : Controller
    {

        private ApplicationDbContext _context = new ApplicationDbContext();

        public HomeController()
        {
            // the context is a disposable object that must be properly disposed of
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            return View("index_htmlrev_riga", _context.Users.Count());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}