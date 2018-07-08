using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebApp.ViewModels
{
    public class QueryViewModel<T>
    {
        public int Id { get; set; }
        public bool IsExistQueryResult { get; set; } = true;
        public T QueryResult { get; set; }
    }
}
