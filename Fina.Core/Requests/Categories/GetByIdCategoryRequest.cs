using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Categories
{
    public class GetByIdCategoryRequest: Request
    {
        public long Id { get; set; }
    }
}