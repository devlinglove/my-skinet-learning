using Core.Entities;


namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        //public ProductSpecification(string? brand, string? type, string? sort) : base(x => 
        //        (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
        //        (string.IsNullOrWhiteSpace(type) || x.Type == type) 
        //    )
        //public ProductSpecification(ProductSpecParams specParams, Dictionary<string, string> qParams) : base(x =>
        //        (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) &&
        //        (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type))
        //    )
        public ProductSpecification(ProductSpecParams specParams, Dictionary<string, string> qParams) : base (null)

        {
            ApplyDynamicFiltering(qParams);
            ApplyOrdering(specParams.SortColumn, specParams.SortOrder);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex -1 ), specParams.PageSize);

          

            switch (specParams.Sort)
            {
                case "priceAsc": 
                    AddOrderBy(x => x.Price); 
                    break;
                case "priceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }


        }


    }
}
