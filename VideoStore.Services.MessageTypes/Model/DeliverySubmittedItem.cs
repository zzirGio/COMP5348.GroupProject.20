using System;

namespace VideoStore.Services.MessageTypes.Model
{
    public class DeliverySubmittedItem
    {
        public string OrderNumber { get; set; }
        public Guid DeliveryId { get; set; }
    }
}