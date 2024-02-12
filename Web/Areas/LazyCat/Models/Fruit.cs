using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminLab.Models.Enums;

namespace AdminLab.Models;
public class Fruit
{
    public Int32 Id { get; set; }
    public String Color { get; set; }
    public String Name { get; set; }
    public Taste Taste { get; set; }
}
