using DevPath.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.Controllers
{
    public class EmploymentOffersController : Controller
    {
        private ApplicationDbContext _context;
        public EmploymentOffersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var offers = _context.EmploymentOffers
                .Include(eo => eo.EmploymentListing)
                .Include(eo => eo.EmploymentListing.ClientCompany)
                .Include(eo => eo.EmploymentListing.StaffingCompany);

            // USER CAN ONLY SEE THEIR OWN OFFERS
            // ADMIN CAN SEE ALL OFFERS
            if (!User.IsInRole(RoleName.CanManageAll))
            {
                offers = offers.Where(eo => eo.RecipientId == userId);
            }

            //offers = offers.ToList();
            return View("List", offers);
        }
        public ActionResult Details(int id)
        {
            var offer = _context.EmploymentOffers
                .Include(eo => eo.EmploymentListing)
                .FirstOrDefault(eo => eo.Id == id);
            return View("Details", offer);
        }
    }
}