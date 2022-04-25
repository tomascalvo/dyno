using DevPath.Models;
using DevPath.ViewModels.Companies;
using System;
using System.Web.Mvc;

namespace DevPath.Controllers
{
    public class CompaniesController : Controller
    {
        private ApplicationDbContext _context;

        public CompaniesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var viewModel = new CompanyFormViewModel()
            {

            };
            return View("CompanyForm", viewModel);
        }

        public ActionResult Save(CompanyFormViewModel formData)
        {

            // RETURN TO FORM IF INVALID

            if (!ModelState.IsValid)
            {
                return View("ProjectForm", formData);
            }

            if (formData.Id == 0) // CREATE NEW COMPANY
            {
                formData.DateAdded = DateTime.Now;
                var newCompany = new Company
                {
                    Title = formData.Title,
                    Description = formData.Description,
                    WebsiteUrl = formData.WebsiteUrl,
                };
                _context.Companies.Add(newCompany);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}