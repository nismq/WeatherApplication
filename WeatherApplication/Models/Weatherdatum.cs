using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace WeatherApplication.Models;

[Table("weatherdata")]
public partial class Weatherdatum
{
    [Key]
    [Column("weatherdataId")]
    public int WeatherdataId { get; set; }

    [Column("temperature", TypeName = "decimal(4, 1)")]
    public decimal Temperature { get; set; }

    [Column("rainfall", TypeName = "decimal(3, 1)")]
    public decimal? Rainfall { get; set; }

    [Column("wind_speed")]
    public int? WindSpeed { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("localityId")]
    public int? LocalityId { get; set; }

    [Column("day", TypeName = "date")]
    public DateTime Day { get; set; }

    [JsonIgnore]
    [ForeignKey("LocalityId")]
    [InverseProperty("Weatherdata")]
    public virtual Locality? Locality { get; set; }
}
