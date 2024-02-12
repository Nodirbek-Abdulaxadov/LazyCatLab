using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminLab.Models.Enums;

namespace AdminLab.Models;
public class IdentityRole
{
    public String Id { get; set; }
    public String? ConcurrencyStamp { get; set; }
    public String? Name { get; set; }
    public String? NormalizedName { get; set; }
}
