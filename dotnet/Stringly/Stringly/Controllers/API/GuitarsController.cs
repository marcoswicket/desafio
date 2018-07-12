using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;
using Stringly.Models;
using AutoMapper;
using Stringly.DTO;

namespace Stringly.Controllers.API
{
	public class GuitarsController : ApiController
	{
		private ApplicationDbContext _context;

		public GuitarsController()
		{
			_context = new ApplicationDbContext();
		}

		// GET /api/guitars
		public IEnumerable<GuitarDTO> GetGuitars()
		{
			return _context.Guitars.ToList().Select(Mapper.Map<Guitar, GuitarDTO>);
		}

		// GET /api/guitars/{id}
		public IHttpActionResult GetGuitar(int id)
		{
			var guitar = _context.Guitars.SingleOrDefault(c => c.Id == id);
			if (guitar == null) return NotFound();

			return Ok(Mapper.Map<Guitar, GuitarDTO>(guitar));
		}

		// POST /api/guitars
		[HttpPost]
		public IHttpActionResult CreateGuitar(GuitarDTO guitarDTO)
		{
			if (!ModelState.IsValid) return BadRequest();

			// Remove special characters
			string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
			string replacement = "";
			Regex rgx = new Regex(pattern);
			string cleanName = rgx.Replace(guitarDTO.Name, replacement);

			guitarDTO.SKU = guitarDTO.Id + "_" + cleanName.Replace(" ", "_").ToLower();

			guitarDTO.InclusionDate = DateTime.Now;

			var guitar = Mapper.Map<GuitarDTO, Guitar>(guitarDTO);

			_context.Guitars.Add(guitar);
			_context.SaveChanges();

			guitarDTO.Id = guitar.Id;

			return Created(new Uri(Request.RequestUri.ToString() + '/' + guitar.Id), guitarDTO);
		}

		// PUT /api/guitars/{id}
		[HttpPut]
		public void UpdateGuitar(int id, GuitarDTO guitarDTO)
		{
			if (!ModelState.IsValid) throw new HttpResponseException(HttpStatusCode.BadRequest);

			var guitarInDb = _context.Guitars.SingleOrDefault(c => c.Id == id);

			if (guitarInDb == null) throw new HttpResponseException(HttpStatusCode.NotFound);

			Mapper.Map<GuitarDTO, Guitar>(guitarDTO, guitarInDb);

			string pattern = @"(?i)[^0-9a-záéíóúàèìòùâêîôûãõç\s]";
			string replacement = "";
			Regex rgx = new Regex(pattern);
			string cleanName = rgx.Replace(guitarInDb.Name, replacement);

			guitarInDb.SKU = guitarInDb.Id + "_" + cleanName.Replace(" ", "_").ToLower();

			_context.SaveChanges();
		}

		// DELETE /api/guitars/{id}
		public void DeleteGuitar(int id)
		{
			var guitarInDb = _context.Guitars.SingleOrDefault(c => c.Id == id);

			if (guitarInDb == null) throw new HttpResponseException(HttpStatusCode.NotFound);

			_context.Guitars.Remove(guitarInDb);
			_context.SaveChanges();
		}
	}
}
