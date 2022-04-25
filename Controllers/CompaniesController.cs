﻿using DevPath.Models;
using DevPath.ViewModels.Companies;
using System;
using System.Data.Entity;
using System.Linq;
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
            }
            else // UPDATE EXISTING COMPANY
            {
                var companyInDb = _context.Companies.Include(c => c.EmploymentListings).Single(c => c.Id == formData.Id);
                companyInDb.Title = formData.Title;
                companyInDb.Description = formData.Description;
                companyInDb.Logo = formData.Logo;
                companyInDb.WebsiteUrl = formData.WebsiteUrl;
                companyInDb.Country = formData.Country;
                companyInDb.StateProvince = formData.StateProvince;
                companyInDb.City = formData.City;
                companyInDb.DateFounded = formData.DateFounded;
                companyInDb.DateAdded = formData.DateAdded;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Companies");
        }

        public ActionResult Index()
        {
            // EAGER LOADING
            var companies = _context.Companies.Include(c => c.EmploymentListings).ToList();
            return View("List", companies);
        }

        public ActionResult Edit(int id)
        {
            var companyInDb = _context.Companies.Include(c => c.EmploymentListings).SingleOrDefault(c => c.Id == id);
            if (companyInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new CompanyFormViewModel(companyInDb);
            return View("CompanyForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            var companyInDb = _context.Companies.SingleOrDefault(c => c.Id == id);
            if (companyInDb == null)
            {
                return RedirectToAction("Index");
            }
            _context.Companies.Remove(companyInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}