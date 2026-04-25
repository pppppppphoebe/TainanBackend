using NpgsqlTypes;

namespace TainanBackend.Models;

public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string? OpeningHours { get; set; }
    
    public NpgsqlPoint? Location { get; set; }

    public string? GoogleMapsUrl { get; set; }
    public decimal? Rating { get; set; }
    public int ReviewCount { get; set; }
}