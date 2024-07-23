using System.ComponentModel.DataAnnotations;

public class TechnologiesDTO
{
    public string Id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The DepartmentId field is required.")]
    public string DepartmentId { get; set; }

    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}
