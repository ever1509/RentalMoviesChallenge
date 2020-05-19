using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentalMovies.Application.Common.Models
{
    public class Pagination<T>
    {
        public int Page { get; set; }
        public int Count => Items?.Count() ?? 0;
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
