namespace ProductApi.Exeptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(string message) : base($"Product - {message} not found ")
    {
    }
}