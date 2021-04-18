﻿using System;

namespace bs.component.integrations.Orders
{
    public interface IOrderCreatedEvent
    {   
        public Guid OrderRef { get; }
        public string CustomerName { get; }
        public string CustomerEmail { get; set; }
    }
}
