using System;

namespace AcerPro.Persistence.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime ExpireIn { get; set; }
}
