using System;

namespace bs.order.domain.Models
{
    public record CustomerDto(
        string FirstName
        , string LastName
        , DateTime DateOfBirth
        , int PhoneNumber
        , string EmailAddress
        , AddressDto BillingAddress
        , ConsentDto CustomerConsent
        , CardDetailsDto? CardDetails = null);
}
