namespace CarRepair.MVC.Models
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager()
        {
            
        }

        public Pager(int totalItems, int page, int pageSize = 6)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage  = page;
            
            if(currentPage < 1)
            {
                currentPage = 1;
            }

            int startPage = currentPage - 5;
            int endPage = currentPage + 4;

            if(startPage <= 0)
            {
                endPage -= (startPage -1);
                startPage = 1;
            }

            if(endPage > totalPages)
            {
                endPage = totalPages;
                if(endPage>10)
                {
                    startPage = endPage -9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

        }
    }
}