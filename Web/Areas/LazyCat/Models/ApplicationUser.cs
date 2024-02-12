using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminLab.Models.Enums;

namespace AdminLab.Models;
public class ApplicationUser
{
    public String Id { get; set; }
    public Int32 AccessFailedCount { get; set; }
    public String? ConcurrencyStamp { get; set; }
    public String? Email { get; set; }
    public Boolean EmailConfirmed { get; set; }
    public String FirstName { get; set; }
    public String? LastName { get; set; }
    public Boolean LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public String? NormalizedEmail { get; set; }
    public String? NormalizedUserName { get; set; }
    public String? PasswordHash { get; set; }
    public String? PhoneNumber { get; set; }
    public Boolean PhoneNumberConfirmed { get; set; }
    public String? SecurityStamp { get; set; }
    public Boolean TwoFactorEnabled { get; set; }
    public String? UserName { get; set; }
}
