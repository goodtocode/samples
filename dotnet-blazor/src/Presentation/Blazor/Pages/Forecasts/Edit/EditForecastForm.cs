using System.ComponentModel.DataAnnotations;

namespace Coffee.Presentation.Blazor.Pages.Forecasts.Edit;

public class EditForecastForm
{
    public Guid Key { get; set; } = Guid.NewGuid();

    [Required] public DateTime Date { get; set; } = DateTime.Now;

    [Required] public int TemperatureF { get; set; }

    [Required] public int TemperatureC { get; set; }

    public List<int> Zipcodes { get; set; } = new();

    [Required] public string ZipcodesToString { get; set; }

    public bool Deleted { get; set; }
}