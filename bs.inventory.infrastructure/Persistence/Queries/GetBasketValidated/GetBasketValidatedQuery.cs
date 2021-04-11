using System;
using bs.inventory.domain.Models;
using MediatR;

namespace bs.inventory.infrastructure.Persistence.Queries.GetBasketValidated
{
    public class GetBasketValidatedQuery : IRequest<BasketValidateResultDto>
    {
        public Guid BasketRef { get; }

        public GetBasketValidatedQuery(Guid basketRef)
        {
            BasketRef = basketRef;
        }
    }
}
