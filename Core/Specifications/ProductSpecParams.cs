﻿namespace Core.Specifications
{
    public class ProductSpecParams
    {
		private readonly int MAX_PAGE_SIZE = 80;

		public int PageIndex { get; set; } = 1;

		private int _pageSize = 10;

		public int PageSize
        {
			get => _pageSize;
			set => _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
		}

		private List<string> _brands = [];

		public List<string> Brands
		{
			get => _brands;
			set {
				_brands = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
			}
		}

		private List<string> _types = [];

		public List<string> Types
		{
			get { return _types; }
			set { 
				_types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList(); 
			}
		}

        public string? Sort { get; set; }
		public string? SortColumn { get; set; }
		public string? SortOrder { get; set; }


    }
}
