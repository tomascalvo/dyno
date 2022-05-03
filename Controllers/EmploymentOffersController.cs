using DevPath.Models;
using DevPath.ViewModels.EmploymentOffers;
using Microsoft.AspNet.Identity;
using System;
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
        public ActionResult New()
        {
            var viewModel = new EmploymentOfferFormViewModel()
            {
                EmploymentListingOptions = _context.EmploymentListings
                    .Where(listing => listing.DateArchived == null)
                    .ToList(),
                RecruiterOptions = _context.Recruiters.ToList(),
                SelectedRecipientId = User.Identity.GetUserId()
            };
            return View("EmploymentOfferForm", viewModel);
        }
        public ActionResult Save(EmploymentOfferFormViewModel formData)
        {
            // Form Validation

            if (!ModelState.IsValid)
            {
                return View("EmploymentOfferForm", formData);
            }

            // Control Flow: Create New Record

            if (formData.Id == 0)
            {
                EmploymentOffer newOffer = new EmploymentOffer
                {
                    EmploymentListingId = formData.SelectedEmploymentListingId,
                    RecruiterId = formData.SelectedRecruiterId,
                    RecipientId = formData.SelectedRecipientId,
                    FullText = formData.FullText,
                    Url = formData.Url,
                    Terms = formData.Terms,
                    Benefits = formData.Benefits,
                    Rating = formData.Rating,
                    DateOffered = formData.DateOffered,
                    DateAdded = DateTime.Now,
                    StartDate = formData.StartDate,
                    Expiration = formData.Expiration,
                    Accepted = formData.Accepted,
                    Declined = formData.Declined,
                    PayQuantity = formData.PayQuantity,
                };
                _context.EmploymentOffers.Add(newOffer);
                _context.SaveChanges();
            }


            // Control Flow: Modify Existing Record

            // Action Result

            return RedirectToAction("Index", "EmploymentOffers");
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