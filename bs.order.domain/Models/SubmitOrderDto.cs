using System;

namespace bs.order.domain.Models
{
    public record SubmitOrderDto(
        Guid CorrelationId
        , CustomerDto Customer
        , PaymentDto PaymentRequest
        , OrderDto Order);
}
