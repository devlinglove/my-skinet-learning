namespace Webapi.RequestHelpers
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; } // Items count
        public int TotalPageCount { get; private set; }
        public bool HasPrevious => (PageIndex > 1);
        public bool HasNext => (PageIndex < TotalPageCount);


        public IReadOnlyList<T> Data { get; set; }

        //public Pagination()
        //{
            
        //}
        public Pagination(int count, IReadOnlyList<T> data, int pageSize, int pageIndex)
        {
            Count = count;
            Data = data;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPageCount = (int)Math.Ceiling(Count / (double)PageSize);
        }
    }
}
