﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Shortener.Service.Model
{
    public class UrlData
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        public string CustomUrl { get; set; }
    }
}