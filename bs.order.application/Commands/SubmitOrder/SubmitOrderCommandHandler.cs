using System;
using bs.order.domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using bs.order.domain.Enums;

namespace bs.order.application.Commands.SubmitOrder
{
    public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, SubmitOrderResultDto>
    {
        public async Task<SubmitOrderResultDto> Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
        {



            await Task.Delay(200, cancellationToken);

            return new SubmitOrderResultDto
            {
                OrderRef = request.Request.Order.OrderRef,
                OrderStatus = OrderStatus.Paid,
                OrderSubmittedOn = DateTime.Now.Date
            };
        }
    }
}
