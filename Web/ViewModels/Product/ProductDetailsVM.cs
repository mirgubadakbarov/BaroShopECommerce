namespace Web.ViewModels.Product
{
    public class ProductDetailsVM
    {
        public Core.Entities.Product Product { get; set; }
        public List<Core.Entities.Product> RelatedProducts { get; set; }


    }
}
