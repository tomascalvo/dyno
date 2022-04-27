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
                StaffingCompanyOptions = _context.Companies.Where(c => c.IsStaffingCompany == true).ToList(),
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
                    StaffingCompanyId = formData.SelectedStaffingCompanyId,
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
                // AUTHORIZATION
                if (!User.IsInRole(RoleName.CanManageAll)) return RedirectToAction("Account", "Login");

                // QUERY DB FOR RECORD TO UPDATE
                var employmentListingInDb = _context.EmploymentListings
                    .Include(el => el.ClientCompany)
                    .Single(el => el.Id == formData.Id);

                // CHANGE PROPERTY VALUES
                employmentListingInDb.Title = formData.Title;
                employmentListingInDb.ClientCompanyId = formData.SelectedClientCompanyId;
                employmentListingInDb.StaffingCompanyId = formData.SelectedStaffingCompanyId;
                employmentListingInDb.PayQuantity = formData.PayQuantity;
                employmentListingInDb.WorkLocation = formData.WorkLocation;
                employmentListingInDb.FullText = formData.FullText;

                // CHANGE RELATED ENTITIES

                // ADD ENTITIES
                foreach (int skillId in formData.SelectedSkillIds)
                {
                    // CHECK TO SEE IF RELATED ENTITY ALREADY EXISTS
                    var elsExists = _context.EmploymentListingSkills
                        .Any(els => els.EmploymentListingId == employmentListingInDb.Id
                        && els.SkillId == skillId);
                    if (elsExists) continue;
                    EmploymentListingSkill newELS = new EmploymentListingSkill
                    {
                        EmploymentListingId = employmentListingInDb.Id,
                        SkillId = skillId
                    };
                    _context.EmploymentListingSkills.Add(newELS);
                }

                // REMOVE ENTITIES
                foreach (EmploymentListingSkill els in employmentListingInDb.EmploymentListingSkills.ToList())
                {
                    // CHECK TO SEE IF RELATED ENTITIES ARE CHOSEN FOR REMOVAL IN FORM
                    if (formData.SelectedSkillIds.Contains(els.SkillId)) continue;
                    _context.EmploymentListingSkills.Remove(els);
                }

            }

            _context.SaveChanges();
            return RedirectToAction("Index", "EmploymentListings");
        }

        public ActionResult Index()
        {
            // EAGER LOADING
            var employmentListings = _context.EmploymentListings
                .Include(el => el.ClientCompany)
                .Include(el => el.StaffingCompany)
                .Include(el => el.EmploymentListingSkills
                    .Select(els => els.Skill))
                .ToList();
            return View("List", employmentListings);
        }

        public ActionResult Details(int id)
        {
            // EAGER LOADING
            var employmentListing = _context.EmploymentListings
                .Include(el => el.ClientCompany)
                .Include(el => el.StaffingCompany)
                .FirstOrDefault(el => el.Id == id);
            if (employmentListing == null)
            {
                return HttpNotFound();
            }

            return View(employmentListing);
        }

        [Authorize(Roles = RoleName.CanManageAll)]
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
            var skillOptions = _context.Skills.ToList();

            // instantiate ViewModel
            var viewModel = new EmploymentListingFormViewModel(employmentListingInDb)
            {
                ClientCompanyOptions = _context.Companies.ToList(),
                StaffingCompanyOptions = _context.Companies.Where(c => c.IsStaffingCompany == true).ToList(),
                SkillOptions = skillOptions.Select(skill => new SelectListItem
                {
                    Text = skill.Title,
                    Value = skill.Id.ToString()
                }),
            };

            // return View passing in ViewModel
            return View("EmploymentListingForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageAll)]
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