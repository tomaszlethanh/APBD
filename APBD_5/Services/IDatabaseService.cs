using APBD_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_5.Services
{
    public interface IDatabaseService
    {
        Task<int> RegisterProductAsync(ProductWarehouse productWarehouse);
        Task<int> RegisterProductByProcedureAsync(ProductWarehouse productWarehouse);
    }
}
