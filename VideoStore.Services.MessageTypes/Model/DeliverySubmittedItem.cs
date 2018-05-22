using System;

namespace VideoStore.Services.MessageTypes.Model
{
    public class DeliverySubmittedItem
    {
        public Guid OrderNumber { get; set; }
        public Guid DeliveryId { get; set; }
    }
}