using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Models
{
    public interface InterfaceProductRepo
    {
        IQueryable<Product> Products { get; }
    }
}
