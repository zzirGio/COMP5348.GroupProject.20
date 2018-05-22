using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Model;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Components.Interfaces
{
    public interface IDeliveryNotificationProvider
    {
        void NotifyDeliveryCompletion(Guid pDeliveryId, DeliveryStatus status);
    }
}
