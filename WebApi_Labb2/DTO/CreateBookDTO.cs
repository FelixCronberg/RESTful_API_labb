﻿using System.ComponentModel.DataAnnotations;
using WebApi_Labb2.Models;

namespace WebApi_Labb2.DTO
{
	public class CreateBookDTO
	{
		public required string Title { get; set; }
		public string? ISBN { get; set; }
		public decimal? Rating { get; set; }
		public int? ReleaseYear { get; set; }

		public ICollection<Author> Authors { get; set; } = [];

	}
}
