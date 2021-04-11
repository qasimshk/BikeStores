using System;
using System.Collections.Generic;

namespace bs.inventory.domain.Models
{
    public class BasketValidateResultDto
    {
        public BasketValidateResultDto()
        {
            BasketItemsResults = new List<BasketItemsDto>();
        }

        public Guid BasketRef { get; set; }
        public double Total { get; set; }
        public List<BasketItemsDto> BasketItemsResults { get; set; }
    }
    
    public class BasketItemsDto
    {
        public Guid ProductRef { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
