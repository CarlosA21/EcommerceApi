namespace EcommerceAPI.Specification
{
    public class ProductSpecificationParams
    {
        private const int MaxPageSize = 50;

        private int _pageSize = 3;
        public int? Brand { get; set; }
        public int? Category { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string ? Search { get; set; }
    }
}
