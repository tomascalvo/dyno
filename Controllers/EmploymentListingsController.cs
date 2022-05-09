using DevPath.Models;
using DevPath.ViewModels.EmploymentListings;
using Microsoft.AspNet.Identity;
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
                StaffingCompanyOptions = _context.Companies
                .Where(c => c.IsStaffingCompany == true)
                .ToList(),
                RecruiterOptions = _context.Recruiters.ToList(),
                SkillOptions = _context.Skills
                    .ToList()
                    .Select(skill => new SelectListItem
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
                    DateAdded = formData.DateAdded,
                    EmploymentApplicationIds = formData.EmploymentApplicationIds,
                    ClientCompanyId = formData.SelectedClientCompanyId,
                    StaffingCompanyId = formData.SelectedStaffingCompanyId,
                    CreatorId = User.Identity.GetUserId(),
                    RecruiterId = formData.SelectedRecruiterId
                };
                _context.EmploymentListings.Add(newEmploymentListing);
                _context.SaveChanges();

                // Create new EmploymentListingAccess record to allow creator to access EmploymentListing.

                var ela = new EmploymentListingAccess
                {
                    EmploymentListingId = newEmploymentListing.Id,
                    ApplicationUserId = User.Identity.GetUserId(),
                };
                _context.EmploymentListingAccesses.Add(ela);

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

                // ADD NEW EmploymentApplication
                if (formData.ApplicationSubmitted)
                {
                    EmploymentApplication newEA = new EmploymentApplication
                    {
                        EmploymentListingId = newEmploymentListing.Id,
                        ApplicantId = User.Identity.GetUserId(),
                    };
                    _context.EmploymentApplications.Add(newEA);
                }
            }
            else // UPDATE EXISTING EmploymentListing
            {
                // AUTHORIZATION
                bool isAdmin = User.IsInRole(RoleName.CanManageAll);
                string userId = User.Identity.GetUserId();
                bool isAuthorized = _context.EmploymentListingAccesses.Any(ela => (ela.EmploymentListingId == formData.Id) && (ela.ApplicationUserId == userId) && ela.CanEdit);
                if (!isAdmin && !isAuthorized)
                {
                    return RedirectToAction("Account", "Login");
                }

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
                employmentListingInDb.RecruiterId = formData.SelectedRecruiterId;
                if (employmentListingInDb.DateArchived == null && formData.IsArchived)
                {
                    employmentListingInDb.DateArchived = DateTime.Now;
                }
                else if (employmentListingInDb.DateArchived != null && !formData.IsArchived)
                {
                    employmentListingInDb.DateArchived = null;
                }

                // CHANGE RELATED ENTITIES

                // ADD ENTITIES
                foreach (int skillId in formData.SelectedSkillIds)
                {
                    // CHECK TO SEE IF RELATED ENTITY ALREADY EXISTS
                    var elsExists = _context.EmploymentListingSkills
                        .Any(els => els.EmploymentListingId == employmentListingInDb.Id
                        && els.SkillId == skillId);
                    if (elsExists) continue;
                    var newELS = new EmploymentListingSkill
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
                .Include(el => el.Creator)
                .Include(el => el.EmploymentListingSkills
                    .Select(els => els.Skill))
                .Include(el => el.EmploymentApplications
                    .Select(ea => ea.Applicant))
                .Include(el => el.EmploymentListingAccesses)
                .Include(el => el.Recruiter)
                .Include(el => el.EmploymentOffers);

            // AUTHORIZED TO SEE OTHERS' ARCHIVED LISTINGS
            if (!User.IsInRole(RoleName.CanManageAll))
            {
                string userId = User.Identity.GetUserId();
                employmentListings = employmentListings
                    .Where(el => el.DateArchived == null || el.EmploymentListingAccesses.Any(ela => ela.ApplicationUserId == userId));
            }

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

            // is user authorized to edit record?
            bool isAdmin = User.IsInRole(RoleName.CanManageAll);
            string userId = User.Identity.GetUserId();
            bool isAuthorized = _context.EmploymentListingAccesses.Any(ela => (ela.EmploymentListingId == employmentListingInDb.Id) && (ela.ApplicationUserId == userId) && ela.CanEdit);
            if (!isAdmin && !isAuthorized)
            {
                return RedirectToAction("Account", "Login");
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

        public ActionResult ToggleApplication(int id)
        {
            // query for the EmploymentListing record
            var employmentListingInDb = _context.EmploymentListings
                .Include(el => el.ClientCompany)
                .Include(el => el.EmploymentListingSkills
                    .Select(els => els.Skill))
                .Include(el => el.EmploymentListingAccesses
                    .Select(ela => ela.ApplicationUser))
                .SingleOrDefault(el => el.Id == id);

            // does EmploymentApplication exist?
            string userId = User.Identity.GetUserId();
            var eaInDb = _context.EmploymentApplications
                .SingleOrDefault(ea => ea.EmploymentListingId == id && ea.ApplicantId == userId);
            // if ea doesn't exist, create it
            if (eaInDb == null)
            {
                EmploymentApplication newEA = new EmploymentApplication
                {
                    EmploymentListingId = id,
                    ApplicantId = userId,
                };
                _context.EmploymentApplications.Add(newEA);
            }
            else
            // if ea exists, delete it
            {
                _context.EmploymentApplications.Remove(eaInDb);
            }
            // save changes
            _context.SaveChanges();

            // return view
            return RedirectToAction("Index");
        }

        public ActionResult ToggleArchive(int id)
        {
            // query for the EmploymentListing record
            var employmentListingInDb = _context.EmploymentListings
                .Include(el => el.ClientCompany)
                .Include(el => el.EmploymentListingSkills
                    .Select(els => els.Skill))
                .Include(el => el.EmploymentListingAccesses
                    .Select(ela => ela.ApplicationUser))
                .SingleOrDefault(el => el.Id == id);

            // is user authorized to archive record?
            bool isAdmin = User.IsInRole(RoleName.CanManageAll);
            string userId = User.Identity.GetUserId();
            bool isAuthorized = _context.EmploymentListingAccesses.Any(ela => (ela.EmploymentListingId == employmentListingInDb.Id) && (ela.ApplicationUserId == userId) && ela.CanArchive);
            if (!isAdmin && !isAuthorized)
            {
                return RedirectToAction("Account", "Login");
            }

            // toggle archive

            if (employmentListingInDb.DateArchived == null)
            {
                employmentListingInDb.DateArchived = DateTime.Now;
            }
            else
            {
                employmentListingInDb.DateArchived = null;
            }

            // save changes
            _context.SaveChanges();

            // return view
            return RedirectToAction("Index");
        }

        public ActionResult ReceiveOffer(int id)
        {
            // query for the EmploymentListing record
            var employmentListingInDb = _context.EmploymentListings
                .Include(el => el.ClientCompany)
                .Include(el => el.EmploymentListingSkills
                    .Select(els => els.Skill))
                .Include(el => el.EmploymentListingAccesses
                    .Select(ela => ela.ApplicationUser))
                .SingleOrDefault(el => el.Id == id);
            // does offer already exist?
            string userId = User.Identity.GetUserId();
            var eoInDb = _context.EmploymentOffers
                .Where(eo => eo.RecipientId == userId && eo.EmploymentListingId == employmentListingInDb.Id)
                .FirstOrDefault();
            if (eoInDb != null)
            {
                return RedirectToAction("Index", "EmploymentListings");
            }
            // create first offer
            var newEmploymentOffer = new EmploymentOffer
            {
                EmploymentListingId = employmentListingInDb.Id,
                RecipientId = User.Identity.GetUserId(),
                RecruiterId = employmentListingInDb.RecruiterId,
            };
            // add to db
            _context.EmploymentOffers.Add(newEmploymentOffer);
            // save changes
            _context.SaveChanges();
            // return view
            return RedirectToAction("Index", "EmploymentListings");
        }

        public ActionResult Delete(int id)
        {
            // does record exist?
            var ELInDb = _context.EmploymentListings.SingleOrDefault(el => el.Id == id);
            if (ELInDb == null)
            {
                return RedirectToAction("Index");
            }

            // is user authorized to delete record?
            bool isAdmin = User.IsInRole(RoleName.CanManageAll);
            string userId = User.Identity.GetUserId();
            bool isAuthorized = _context.EmploymentListingAccesses.Any(ela => (ela.EmploymentListingId == ELInDb.Id) && (ela.ApplicationUserId == userId) && ela.CanDelete);
            if (!isAdmin && !isAuthorized)
            {
                return RedirectToAction("Account", "Login");
            }

            // remove 
            _context.EmploymentListings.Remove(ELInDb);
            // save
            _context.SaveChanges();
            // redirect
            return RedirectToAction("Index");
        }
    }
}