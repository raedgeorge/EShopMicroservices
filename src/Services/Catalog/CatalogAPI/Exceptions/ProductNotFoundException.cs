namespace CatalogAPI.Exceptions
{
    public class ProductNotFoundException(string message) : Exception(message)
    {
    }
}
