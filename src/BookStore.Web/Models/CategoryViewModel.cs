using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        public bool IsSelected { get; set; }

        [Required]
        [HiddenInput]
        public string Name { get; set; }
    }
}