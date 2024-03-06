namespace Web.ViewModels.Product
{
    public class ProductLoadMoreVM
    {
        public List<Core.Entities.Product> BestSellingProducts { get; set; }
        public bool IsLast { get; set; }

    }
}
