
namespace DECH.Enterprise.Services.Customers.Contracts.Models
{
    public class PaginationFilter
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public PaginationFilter()
        {
            this.Page = 1;
            this.Size = 20;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.Page = pageNumber < 1 ? 1 : pageNumber;
            //this.Size = (pageSize > 20 || pageSize < 1) ? 20 : pageSize;
            this.Size = pageSize < 1  ? 20 : pageSize;
        }
    }
}
