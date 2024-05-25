using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Coffee.Presentation.Blazor.Common.Interfaces;

namespace Coffee.Presentation.Blazor.Pages.Forecasts.Add;

public class AddForecastForm
{

    [Inject] private ITemperatureCalculationsExternalService WeatherCalculationsService { get; set; }

    public Guid Key { get; set; } = Guid.NewGuid();

    [Required] public DateTime Date { get; set; } = DateTime.Now;

    [Required] public int TemperatureF { get; set; } 

    [Required] public int TemperatureC { get; set; } = (int)((0 - 32) * 5) / 9;

    public List<int> Zipcodes { get; set; } = new List<int>();

    [Required]
    public string ZipcodesToString { get; set; }
    
}