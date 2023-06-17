namespace AcerPro.Persistence.DTOs;

public class TargetAppDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlAddress { get; set; }
    public int MonitoringIntervalInSeconds { get; set; }
}