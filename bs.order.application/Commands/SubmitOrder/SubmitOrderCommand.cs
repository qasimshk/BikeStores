using bs.order.domain.Models;
using MediatR;

namespace bs.order.application.Commands.SubmitOrder
{
    public class SubmitOrderCommand : IRequest<SubmitOrderResultDto>
    {
        public SubmitOrderDto Request { get; }

        public SubmitOrderCommand(SubmitOrderDto request)
        {
            Request = request;
        }
    }
}
