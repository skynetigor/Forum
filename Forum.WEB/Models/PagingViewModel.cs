using Forum.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    public class PagingViewModel<T>
    {
        public PagingViewModel(int pageSize, IEnumerable<T> items): this(GetTotalPages(items, pageSize), pageSize ,items)
        {

        }

        public PagingViewModel(int pageNumber,int pageSize,IEnumerable<T> items)
        {
            this.Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);


            int totalPages = GetTotalPages(items, pageSize);
            
            PageInfo = new PageInfo
            {
                PageNumber = pageNumber, PageSize = pageSize, TotalItems = totalPages
            };
        }
        public int Id { get; set; }
        public IEnumerable<T> Items { get; }
        public PageInfo PageInfo { get; }
        public string Name { get; set; }

        private static int GetTotalPages(IEnumerable<T> items, int pageSize)
        {
            if (items.Any())
            {
                double d1 = items.Count();
                double d2 = pageSize;
                var c = Math.Ceiling(d1 / d2);
                return (int)c;
            }

            return 1;
        }
    }
}