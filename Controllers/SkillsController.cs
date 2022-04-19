using DevPath.Models;
using DevPath.ViewModels.Skills;
using System;
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
            var viewModel = new SkillFormViewModel();
            return View("SkillForm", viewModel);
        }

        public ActionResult Save(Skill skill)
        {


            if (!ModelState.IsValid)
            {
                var viewModel = new SkillFormViewModel(skill);
                return View("SkillForm", viewModel);
            }

            if (skill.Id == 0)
            {
                skill.DateAdded = DateTime.Now;
                _context.Skills.Add(skill);
            }
            else
            {
                var skillInDb = _context.Skills.Single(s => s.Id == skill.Id);
                skillInDb.Icon = skill.Icon;
                skillInDb.Title = skill.Title;
                skillInDb.Description = skill.Description;
                skillInDb.ReleaseDate = skill.ReleaseDate;
                skillInDb.DateAdded = skill.DateAdded;
                skillInDb.Repository = skill.Repository;
                skillInDb.Documentation = skill.Documentation;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Skills");
        }

        public ActionResult Index()
        {
            var skills = _context.Skills;
            return View("List", skills);
        }

        public ActionResult Details(int id)
        {
            var skillInDb = _context.Skills.FirstOrDefault(s => s.Id == id);
            if (skillInDb == null)
            {
                return HttpNotFound();
            }
            return View(skillInDb);
        }

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