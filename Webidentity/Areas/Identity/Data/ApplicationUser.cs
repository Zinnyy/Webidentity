using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Webidentity.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    [PersonalData]

    [Column(TypeName = "nvarchar(100)")]
    [Display(Name = "First Name")]
    public string FistName { get; set; }



    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    [Display(Name = "Last Name")]
    public string Lastname { get; set; }


    [PersonalData]
    [Column(TypeName = "nvarchar(15)")]
    public string Phone { get; set; }


    [PersonalData]
    [Column(TypeName = "nvarchar(255)")]
    public string Location { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(255)")]
    public string Bio { get; set; }

  public bool IsActivated { get; set; }

    public bool IsDeactivated { get; set; }
   
}

