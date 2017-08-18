using Forum.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    public class PagingViewModel<T>
    {
        private IEnumerable<T> items;
        private PageInfo pageInfo;
        public PagingViewModel(int pageNumber,int pageSize,IEnumerable<T> items)
        {
            this.items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            int totalPages = items.Count() == 0 ? 1 : items.Count();
            pageInfo = new PageInfo
            {
                PageNumber = pageNumber, PageSize = pageSize, TotalItems = totalPages
            };
        }
        public int Id { get; set; }
        public IEnumerable<T> Items { get { return items; } }
        public PageInfo PageInfo { get { return pageInfo; } }
        public string Name { get; set; }
    }
}