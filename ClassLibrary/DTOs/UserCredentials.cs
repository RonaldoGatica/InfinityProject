using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryInfinity.DTOs
{
    public class UserCredentials
    {
        [Required, StringLength(100, MinimumLength = 5)]
        public string Nombre { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 3)]
        public string Usuario { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 5)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 3)]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
