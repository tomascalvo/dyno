﻿using DevPath.Models;
using DevPath.ViewModels.Projects;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DevPath.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            // the context is a disposable object that must be properly disposed of
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var viewModel = new ProjectFormViewModel()
            {
                SkillOptions = _context.Skills.ToList().Select(skillOption => new SelectListItem
                {
                    Text = skillOption.Title,
                    Value = skillOption.Id.ToString()
                })
            };
            return View("ProjectForm", viewModel);
        }

        // POST: Projects

        [HttpPost]

        public ActionResult Save(ProjectFormViewModel formData)
        {
            if (!ModelState.IsValid)
            {
                return View("ProjectForm", formData);
            }

            if (formData.Id == 0) // CREATE NEW PROJECT
            {
                formData.DateAdded = DateTime.Now;
                var newProject = new Project
                {
                    Title = formData.Title,
                    Description = formData.Description,
                    Icon = formData.Icon,
                    RepositoryUrl = formData.RepositoryUrl,
                    DeploymentUrl = formData.DeploymentUrl,
                };
                _context.Projects.Add(newProject);
                _context.SaveChanges();

                // ADDING NEW ProjectSkills TO REPRESENT THE MANY TO MANY RELATIONSHIP BETWEEN PROJECTS AND SKILLS
                if (formData.SelectedSkillIds != null)
                {
                    foreach (int skillId in formData.SelectedSkillIds)
                    {
                        ProjectSkill newProjectSkill = new ProjectSkill
                        {
                            ProjectId = newProject.Id,
                            SkillId = skillId
                        };
                        _context.ProjectSkills.Add(newProjectSkill);
                    }
                }
            }
            else // UPDATE EXISTING PROJECT
            {
                var projectInDb = _context.Projects.Include(p => p.ProjectSkills.Select(ps => ps.Skill)).Single(p => p.Id == formData.Id);
                projectInDb.Title = formData.Title;
                projectInDb.Description = formData.Description;
                projectInDb.Icon = formData.Icon;
                projectInDb.RepositoryUrl = formData.RepositoryUrl;
                projectInDb.DeploymentUrl = formData.DeploymentUrl;
                projectInDb.DateAdded = formData.DateAdded;

                if (formData.SelectedSkillIds != null)
                {
                    // ADDING NEW ProjectSkills TO REPRESENT THE MANY TO MANY RELATIONSHIP BETWEEN PROJECTS AND SKILLS
                    foreach (int skillId in formData.SelectedSkillIds)
                    {
                        var projectSkillExists = _context.ProjectSkills.Any(ps => ps.ProjectId == projectInDb.Id && ps.SkillId == skillId);
                        if (projectSkillExists == false)
                        {
                            ProjectSkill newProjectSkill = new ProjectSkill
                            {
                                ProjectId = projectInDb.Id,
                                SkillId = skillId
                            };
                            _context.ProjectSkills.Add(newProjectSkill);
                        }
                    }

                    // REMOVING ProjectSkills FOR SKILLS THAT WERE DESELECTED
                    foreach (ProjectSkill priorProjectSkill in projectInDb.ProjectSkills.ToList())
                    {
                        if (formData.SelectedSkillIds.Contains(priorProjectSkill.SkillId) == false)
                        {
                            _context.ProjectSkills.Remove(priorProjectSkill);
                        }
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Projects");
        }

        // GET: Projects

        public ActionResult Index()
        {
            // LAZY LOADING
            // By default, Entity Framework only loads the Project objects, not their related objects. Referencing related objects will cause a null reference exception.
            //var projects = _context.Projects;

            // EAGER LOADING
            // Eager Loading will load the Project object and related objects.
            var projects = _context.Projects.Include(p => p.ProjectSkills.Select(ps => ps.Skill)).ToList();
            return View("List", projects);
        }

        public ActionResult Details(int id)
        {
            // LAZY LOADING
            //var project = _context.Projects.SingleOrDefault(p => p.Id == id);

            // EAGER LOADING
            var project = _context.Projects.Include(p => p.ProjectSkills.Select(ps => ps.Skill)).FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return HttpNotFound();
            }

            return View(project);
        }

        // PUT: Projects

        public ActionResult Edit(int id)
        {
            var projectInDb = _context.Projects
                .Include(p => p.ProjectSkills
                .Select(ps => ps.Skill))
                .SingleOrDefault(p => p.Id == id);

            if (projectInDb == null)
            {
                return HttpNotFound();
            }

            var skillOptions = _context.Skills.ToList();

            var viewModel = new ProjectFormViewModel(projectInDb)
            {
                SkillOptions = skillOptions.Select(skill => new SelectListItem
                {
                    Text = skill.Title,
                    Value = skill.Id.ToString()
                }),
                SelectedSkillIds = projectInDb.ProjectSkills.Select(projectSkill => projectSkill.SkillId).ToList()
            };

            return View("ProjectForm", viewModel);
        }

        // DELETE: Projects

        public ActionResult Delete(int id)
        {
            var projectInDb = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (projectInDb == null)
            {
                return RedirectToAction("Index");
            }
            _context.Projects.Remove(projectInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}