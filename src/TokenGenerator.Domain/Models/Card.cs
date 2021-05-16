using System;

namespace TokenGenerator.Domain.Models
{
    public class Card
    {
        public Guid CardId { get; set; }
        public int CustomerId { get; set; }
        public long CardNumber { get; set; }
        public DateTime TokenCreationDate { get; set; }

        public Card()
        {

        }

        public Card(int customerId, long cardNumber)
        {
            CardId = Guid.NewGuid();
            TokenCreationDate = DateTime.Now;
            CustomerId = customerId;
            CardNumber = cardNumber;
        }
    }
}
