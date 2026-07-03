public class Product
{
    public int Id {get;set;}
    public string Name {get; set;} = string.Empty;

    public string Description {get;set;} = string.Empty;

    public decimal Price {get;set;}

    public int stock {get;set;}
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

}