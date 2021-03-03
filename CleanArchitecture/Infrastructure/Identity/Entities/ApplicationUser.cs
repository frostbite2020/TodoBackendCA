using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150) ")]
        public string FullName { get; set; }
    }
}
