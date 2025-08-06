﻿using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Stock
{
    public interface IStockService
    {
        Task<StockSummaryDto> GetStockSummaryAsync();
    }
}
