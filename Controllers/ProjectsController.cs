using DevPath.Models;
using DevPath.ViewModels.Projects;
using System;
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

            };
            return View("ProjectForm", viewModel);
        }

        // POST: Projects

        [HttpPost]

        public ActionResult Save(Project project)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProjectFormViewModel(project);
                return View("ProjectForm", viewModel);
            }

            if (project.Id == 0)
            {
                project.DateAdded = DateTime.Now;
                _context.Projects.Add(project);
            }
            else
            {
                var projectInDb = _context.Projects.Single(p => p.Id == project.Id);
                projectInDb.Title = project.Title;
                projectInDb.Description = project.Description;
                projectInDb.Icon = project.Icon;
                projectInDb.RepositoryUrl = project.RepositoryUrl;
                projectInDb.DeploymentUrl = project.DeploymentUrl;
                projectInDb.DateAdded = project.DateAdded;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Projects");
        }

        // GET: Projects

        public ActionResult Index()
        {
            //int projectsCount = _context.Projects.Count();
            var projects = _context.Projects;
            return View("List", projects);
        }

        public ActionResult Details(int id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // PUT: Projects

        public ActionResult Edit(int id)
        {
            var projectInDb = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (projectInDb == null)
            {
                return HttpNotFound();
            }
            var viewModel = new ProjectFormViewModel(projectInDb);
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