using bs.order.domain.Enums;
using System;

namespace bs.order.domain.Models
{
    public record PaymentDto(
        int CustomerId
        , int CardDetailsId
        , Guid PaymentRef
        , double Amount
        , DateTime TransactionDate
        , PaymentType PaymentType
        , TransactionStatus TransactionStatus);
}
