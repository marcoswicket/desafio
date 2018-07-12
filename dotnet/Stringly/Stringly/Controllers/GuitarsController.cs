using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Text.RegularExpressions;
using Stringly.Models;
using Stringly.ViewModels;

namespace Stringly.Controllers
{
	public class GuitarsController : Controller
	{
		private ApplicationDbContext _context;

		// Database access
		public GuitarsController()
		{
			_context = new ApplicationDbContext();
		}

		// Dispose of the var
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		// GET: Guitars/New
		public ActionResult New()
		{
			var viewModel = new GuitarFormViewModel { };

			return View("GuitarForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Guitar guitar)
		{
			//if (!ModelState.IsValid)
			//{
			//	var viewModel = new GuitarFormViewModel
			//	{
			//		Guitar = guitar,
			//		Error = true
			//	};

			//	return View("GuitarForm", viewModel);
			//}
			if (guitar.SKU != null && guitar.Name != string.Empty)
			{
				// Remove special characters
				string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
				string replacement = "";
				Regex rgx = new Regex(pattern);
				string cleanName = rgx.Replace(guitar.Name, replacement);

				guitar.SKU = guitar.Id + "_" + cleanName.Replace(" ", "_").ToLower();
			}

			if (guitar.Id == 0)
			{
				guitar.InclusionDate = DateTime.Now;
				_context.Guitars.Add(guitar);
			} else
			{
				var guitarInDb = _context.Guitars.Single(c => c.Id == guitar.Id);

				guitarInDb.Name = guitar.Name;
				guitarInDb.Description = guitar.Description;
				guitarInDb.Price = guitar.Price;
				guitarInDb.ImageURL = guitar.ImageURL;

				guitarInDb.SKU = guitar.SKU;
			}

			try
			{
				_context.SaveChanges();
			}
			catch (DbEntityValidationException e) 
			{
				Console.WriteLine(e);
			}
			return RedirectToAction("Index", "Guitars");
		}

		// GET: Guitars
		public ViewResult Index()
		{
			var guitars = _context.Guitars.ToList();

			return View(guitars);
		}

		// GET: Guitars/Id
		public ActionResult Details(int id)
		{
			var guitar = _context.Guitars.SingleOrDefault(c => c.Id == id);

			if (guitar == null) return HttpNotFound();

			return View(guitar);
		}

		public ActionResult Edit(int id)
		{
			var guitar = _context.Guitars.SingleOrDefault(c => c.Id == id);

			if (guitar == null) return HttpNotFound();

			var viewModel = new GuitarFormViewModel {
				Guitar = guitar
			};

			return View("GuitarForm", viewModel);
		}

		//GET: Guitars/Random
		//public ActionResult Random()
		//{
		//	var guitar = new Guitar() { Name = "Les Paul" };

		//	return View(guitar);
		//}
	}
}