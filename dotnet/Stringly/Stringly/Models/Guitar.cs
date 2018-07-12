using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stringly.Models
{
	public class Guitar
	{
		public int Id { get; set; }

		[Required]
		[StringLength(400)]
		[Display(Name = "Guitar Model")]
		public string Name { get; set; }

		[Display(Name = "Description of the Guitar")]
		public string Description { get; set; }

		[Display(Name = "Price")]
		[RegularExpression(@"\d+(\.\d{0,2})*$")] // maximum two decimal digits
		//[Required(ErrorMessage = "The price field needs to be a number with at maximum 2 decimal numbers.")]
		public float Price { get; set; }

		public string ImageURL { get; set; }

		public DateTime InclusionDate { get;set; }

		public string SKU { get; set; }
	}
}