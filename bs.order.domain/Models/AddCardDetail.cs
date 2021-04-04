using System;
using bs.component.integrations.Customers;

namespace bs.order.domain.Models
{
    public class AddCardDetail : ICardDetail
    {
        private readonly ICardDetail _cardDetail;

        public AddCardDetail(ICardDetail cardDetail)
        {
            _cardDetail = cardDetail;
        }

        public long CardNumber => _cardDetail.CardNumber;
        public int CardType => _cardDetail.CardType;
        public DateTime Expiry => _cardDetail.Expiry;
        public string CardHolderName => _cardDetail.CardHolderName;
        public int SecurityNumber => _cardDetail.SecurityNumber;
    }
}