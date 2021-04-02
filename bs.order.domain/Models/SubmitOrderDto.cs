﻿namespace bs.order.domain.Models
{
    public record SubmitOrderDto(
        CustomerDto Customer
        , PaymentDto PaymentRequest
        , OrderDto Order);
}