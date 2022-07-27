namespace DemoApp.WebClient.Models;

using System.ComponentModel.DataAnnotations;

public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(15)]
    public string Name { get; set; }
}

