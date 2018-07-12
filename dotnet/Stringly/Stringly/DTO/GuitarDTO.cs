using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stringly.DTO
{
	public class GuitarDTO
	{
		public int Id { get; set; }

		[Required]
		[StringLength(400)]
		public string Name { get; set; }
		
		public string Description { get; set; }
		
		[RegularExpression(@"\d+(\.\d{0,2})*$")] // maximum two decimal digits
		public float Price { get; set; }

		public string ImageURL { get; set; }

		public DateTime InclusionDate { get; set; }

		public string SKU { get; set; }
	}
}