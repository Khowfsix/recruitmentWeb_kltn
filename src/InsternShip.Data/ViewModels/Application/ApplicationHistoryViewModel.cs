using System.Text.Json.Serialization;

namespace InsternShip.Data.ViewModels.Application;

public class ApplicationHistoryViewModel
{
    public Guid ApplicationId { get; set; }
    public string? PositionName { get; set; }
    public Guid Cvid { get; set; }
    public Guid PositionId { get; set; }
    public DateTime DateTime { get; set; }

    [JsonPropertyName("Status")]
    public string? Candidate_status { get; set; }
    public string? Priority { get; set; }
}
