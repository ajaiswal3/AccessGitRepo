using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NRI.Models
{
    public class QueryParameters
    {
        [Required]
        public string user { get; set; }
        [Required]
        public string token  { get; set; }
    }
}
