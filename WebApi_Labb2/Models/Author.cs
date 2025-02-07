﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi_Labb2.Models
{
	public class Author
	{
		public int AuthorId { get; set; }

		public required string FirstName { get; set; }
		public required string LastName { get; set; }

		//Nav
		[JsonIgnore]
		public ICollection<Book> Books { get; set; } = [];

	}
}
