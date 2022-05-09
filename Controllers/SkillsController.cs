using DevPath.Models;
using DevPath.ViewModels.Skills;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.Controllers
{
    public class SkillsController : Controller
    {
        private ApplicationDbContext _context;
        public SkillsController()
        {
            // the context is a disposable object
            // the controller constructor instantiates the context
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            // this method disposes the context
            _context.Dispose();
        }

        public ActionResult New()
        {
            string userId = User.Identity.GetUserId();
            var viewModel = new SkillFormViewModel()
            {
                AddedById = userId,
                ProjectOptions = _context.Projects
                .Include(p => p.ApplicationUserProjects)
                .Where(p => p.ApplicationUserProjects
                    .Any(aup => aup.ApplicationUserId == userId) || p.AddedById == userId
                )
                .ToList()
                    .Select(project => new SelectListItem
                    {
                        Text = project.Title,
                        Value = project.Id.ToString()
                    }
                    ),
            };
            return View("SkillForm", viewModel);
        }

        public static void AddProjectSkills(int skillId, List<int> selectedProjectSkillIds, ApplicationDbContext context)
        {
            foreach (int projectId in selectedProjectSkillIds)
            {
                // Validate that related entity does not already exist.
                var psExists = context.ProjectSkills
                    .Any(ps => ps.SkillId == skillId && ps.ProjectId == projectId);
                if (psExists) continue;

                // Loop through list of projectIds, instantiating a new ProjectSkill and adding it to the db context.
                ProjectSkill newProjectSkill = new ProjectSkill
                {
                    SkillId = skillId,
                    ProjectId = projectId
                };
                context.ProjectSkills.Add(newProjectSkill);
            }
        }

        public ActionResult Save(SkillFormViewModel formData)
        {
            string userId = User.Identity.GetUserId();
            if (!ModelState.IsValid) // DATA VALIDATION
            {
                formData.ProjectOptions = new SelectList(
                    _context.Projects
                        .Include(p => p.ApplicationUserProjects.Select(aup => aup.ApplicationUser))
                        .Where(p => p.AddedById == userId || p.ApplicationUserProjects
                            .Any(aup => aup.ApplicationUserId == userId)
                    ),
                    "Id",
                    "Title");
                return View("SkillForm", formData);
            }

            if (formData.Id == 0) // Save New Skill
            {
                var newSkill = new Skill
                {
                    Title = formData.Title,
                    Description = formData.Description,
                    Icon = formData.Icon,
                    Developer = formData.Developer,

                    // DateTime Properties
                    ReleaseDate = formData.ReleaseDate,
                    DateAdded = DateTime.Now,

                    // Url Properties
                    RepositoryUrl = formData.RepositoryUrl,
                    DocumentationUrl = formData.DocumentationUrl,

                    // Entity Relationships
                    AddedById = formData.AddedById
                };

                _context.Skills.Add(newSkill);
                _context.SaveChanges();

                // Add new ProjectSkills to represent the many to many relationship between Skills and Projects.

                if (formData.SelectedProjectIds != null)
                {
                    AddProjectSkills(newSkill.Id, formData.SelectedProjectIds, _context);
                }

                //return RedirectToAction("Details", "Skills", new { id = newSkill.Id });
                return RedirectToAction("Index", "Skills");
            }

            // Save Changes to Existing Skill

            // Authorization
            if (
                    !(
                        User.IsInRole(RoleName.CanManageAll)
                        || User.IsInRole(RoleName.CanManageSkills)
                    || User.Identity.GetUserId() == formData.AddedById
                    )
                ) return RedirectToAction("Account", "Login");

            // Query db for record to update.
            var skillInDb = _context.Skills
                .Include(s => s.AddedBy)
                .Include(s => s.ProjectSkills
                    .Select(ps => ps.Project))
                .Single(s => s.Id == formData.Id);

            // Update property values
            skillInDb.Icon = formData.Icon;
            skillInDb.Title = formData.Title;
            skillInDb.Description = formData.Description;
            skillInDb.Developer = formData.Developer;

            // DateTime Properties
            skillInDb.ReleaseDate = formData.ReleaseDate;
            skillInDb.DateAdded = formData.DateAdded;

            // Url Properties
            skillInDb.RepositoryUrl = formData.RepositoryUrl;
            skillInDb.DocumentationUrl = formData.DocumentationUrl;

            // Update related entities.

            // Add new ProjectSkills to represent the many to many relationship between skills and projects.

            // Add new ProjectSkills to represent the many to many relationship between Skills and Projects.

            if (formData.SelectedProjectIds != null)
            {
                AddProjectSkills(skillInDb.Id, formData.SelectedProjectIds, _context);
            }

            // Remove ProjectSkills that were deselected.
            foreach (ProjectSkill existingPS in skillInDb.ProjectSkills.ToList())
            {
                // Validate that the existing related entity was chosen for removal.
                if (formData.SelectedProjectIds.Contains(existingPS.ProjectId)) continue;
                // Remove related entity.
                _context.ProjectSkills.Remove(existingPS);
            }

            _context.SaveChanges();
            return RedirectToAction("Details", "Skills", new { id = skillInDb.Id });
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var skills = _context.Skills
                .Include(s => s.ProjectSkills
                    .Select(ps => ps.Project)
                ).OrderBy(s => s.DateAdded)
                .ToList();
            return View("List", skills);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var skillInDb = _context.Skills
                .Include(s => s.AddedBy)
                                .Include(s => s.ProjectSkills
                    .Select(ps => ps.Project))
                .FirstOrDefault(s => s.Id == id);
            if (skillInDb == null)
            {
                return HttpNotFound();
            }
            return View(skillInDb);
        }

        [Authorize(Roles = RoleName.CanManageSkills + "," + RoleName.CanManageAll)]
        public ActionResult Edit(int id)
        {
            string userId = User.Identity.GetUserId();

            // Query db for record to edit.
            var skillInDb = _context.Skills
                .Include(s => s.AddedBy)
                .Include(s => s.ProjectSkills
                    .Select(ps => ps.Project))
                .SingleOrDefault(s => s.Id == id);

            if (skillInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new SkillFormViewModel(skillInDb)
            {
                ProjectOptions = _context.Projects
                .Include(p => p.ApplicationUserProjects)
                .Where(p => p.ApplicationUserProjects
                    .Any(aup => aup.ApplicationUserId == userId) || p.AddedById == userId
                )
                .ToList()
                    .Select(project => new SelectListItem
                    {
                        Text = project.Title,
                        Value = project.Id.ToString()
                    }),
            };
            return View("SkillForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageSkills + "," + RoleName.CanManageAll)]
        public ActionResult Delete(int id)
        {
            var skillInDb = _context.Skills.SingleOrDefault(s => s.Id == id);
            if (skillInDb == null)
            {
                return RedirectToAction("Index");
            }
            _context.Skills.Remove(skillInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}