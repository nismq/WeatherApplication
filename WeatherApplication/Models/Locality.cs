using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace WeatherApplication.Models;

[Table("locality")]
[Index("LocalityName", Name = "UQ__locality__71FF7E86D93DE688", IsUnique = true)]
public partial class Locality
{
    [Key]
    [Column("localityId")]
    public int LocalityId { get; set; }

    [Column("localityName")]
    [StringLength(100)]
    [Unicode(false)]
    public string LocalityName { get; set; } = null!;
    [InverseProperty("Locality")]
    public virtual ICollection<Weatherdatum> Weatherdata { get; } = new List<Weatherdatum>();
}
