using System.ComponentModel.DataAnnotations;

namespace PixelForge.Dtos;

public record class GameSummaryDto(
    int Id, 
    string Name, 
    string Genre, 
    decimal Price,
    DateOnly ReleaseDate);
    
