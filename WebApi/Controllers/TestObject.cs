using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers;

public class TestObject
{
    public TestObject(string name)
    {
        Name = name;
    }

    [Required]
    public string Name { get; set; }
}