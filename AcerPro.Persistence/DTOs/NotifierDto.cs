namespace AcerPro.Persistence.DTOs;

public class NotifierDto
{
    public int Id { get; set; }
    public string Address { get; set; }
    public NotifierTypeDto NotifierType { get; set; }
}
