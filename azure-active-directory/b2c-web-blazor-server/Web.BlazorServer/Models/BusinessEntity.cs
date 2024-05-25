namespace Goodtocode.Samples.Domain;

public class BusinessEntity
{
    public string PartitionKey { get; set; } = "Default";
    public Guid RowKey { get { return BusinessKey; } set { BusinessKey = value; } }
    public Guid BusinessKey { get; set; }
    public string BusinessName { get; set; } = String.Empty;
    public string TaxNumber { get; set; } = String.Empty;
}
