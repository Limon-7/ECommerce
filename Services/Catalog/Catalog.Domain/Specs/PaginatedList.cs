namespace Catalog.Domain.Specs;

public class PaginatedList<T> where T: class
{
    public PaginatedList(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public PaginatedList()
        {
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
}