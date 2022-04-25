using DevPath.Models;
using DevPath.ViewModels.EmploymentListings;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.Controllers
{
    public class EmploymentListingsController : Controller
    {
        private ApplicationDbContext _context;

        public EmploymentListingsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var viewModel = new EmploymentListingFormViewModel()
            {
                ClientCompanyOptions = _context.Companies.ToList(),
                SkillOptions = _context.Skills.ToList().Select(skill => new SelectListItem
                {
                    Text = skill.Title,
                    Value = skill.Id.ToString()
                }),
            };
            return View("EmploymentListingForm", viewModel);
        }

        public ActionResult Save(EmploymentListingFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("EmploymentListingForm", formData);
            }

            if (formData.Id == 0) // CREATE NEW EmploymentListing
            {
                formData.DateAdded = DateTime.Now;
                var newEmploymentListing = new EmploymentListing
                {
                    Title = formData.Title,
                    PayQuantity = formData.PayQuantity,
                    WorkLocation = formData.WorkLocation,
                    FullText = formData.FullText,
                    Url = formData.Url,
                    DatePublished = formData.DatePublished,
                    EmploymentApplicationIds = formData.EmploymentApplicationIds,
                    ClientCompanyId = formData.SelectedClientCompanyId,
                };
                _context.EmploymentListings.Add(newEmploymentListing);
                _context.SaveChanges();

                // ADDING NEW EmploymentListingSkills TO REPRESENT THE MANY TO MANY RELATIONSIHP BETWEEN EmploymentListings and Skills
                if (formData.SelectedSkillIds != null)
                {
                    foreach (int skillId in formData.SelectedSkillIds)
                    {
                        EmploymentListingSkill newELS = new EmploymentListingSkill
                        {
                            EmploymentListingId = newEmploymentListing.Id,
                            SkillId = skillId
                        };
                        _context.EmploymentListingSkills.Add(newELS);
                    }
                }
            }
            else // UPDATE EXISTING EmploymentListing
            {

                _context.SaveChanges();
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "EmploymentListings");
        }

        public ActionResult Index()
        {
            // EAGER LOADING
            var employmentListings = _context.EmploymentListings
                .Include(el => el.ClientCompany)
                .Include(el => el.EmploymentListingSkills
                    .Select(els => els.Skill))
                .ToList();
            return View("List", employmentListings);
        }

        public ActionResult Edit(int id)
        {
            // find the record to edit (with eager loading)
            var employmentListingInDb = _context.EmploymentListings
                .Include(el => el.ClientCompany)
                .Include(el => el.EmploymentListingSkills
                    .Select(els => els.Skill))
                .SingleOrDefault(el => el.Id == id);

            // return error page if no record can be found
            if (employmentListingInDb == null)
            {
                return HttpNotFound();
            }

            // query db for select list options
            var clientCompanyOptions = _context.Companies.ToList();
            var skillOptions = _context.Skills.ToList();

            // instantiate ViewModel
            var viewModel = new EmploymentListingFormViewModel(employmentListingInDb)
            {
                ClientCompanyOptions = clientCompanyOptions,
                SkillOptions = skillOptions.Select(skill => new SelectListItem
                {
                    Text = skill.Title,
                    Value = skill.Id.ToString()
                }),
            };

            // return View passing in ViewModel
            return View("EmploymentListingForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            var ELInDb = _context.EmploymentListings.SingleOrDefault(el => el.Id == id);
            if (ELInDb == null)
            {
                return RedirectToAction("Index");
            }
            _context.EmploymentListings.Remove(ELInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}