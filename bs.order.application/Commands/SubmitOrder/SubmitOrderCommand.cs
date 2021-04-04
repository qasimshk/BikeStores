using bs.order.domain.Models;
using MediatR;

namespace bs.order.application.Commands.SubmitOrder
{
    public class SubmitOrderCommand : IRequest<SubmitOrderResultDto>
    {
        public SubmitOrderDto SubmitOrder { get; }

        public SubmitOrderCommand(SubmitOrderDto submitOrder)
        {
            SubmitOrder = submitOrder;
        }
    }
}
