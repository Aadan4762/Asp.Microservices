using System.ComponentModel.DataAnnotations;

namespace User_Service.Dto;

public class UpdatePermissionDto
{
    [Required(ErrorMessage = "UserName is required")]
    public string UserName { get; set; } 
}