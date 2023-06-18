using System;
using System.Collections.Generic;

namespace AcerPro.Persistence.DTOs;

public class TargetAppDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UrlAddress { get; set; }
    public int UserId { get; set; }
    public int MonitoringIntervalInSeconds { get; set; }
    public bool? IsHealthy { get; set; }
    public DateTime? LastDownDateTime { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public List<NotifierDto> Notifiers { get; set; }
}
