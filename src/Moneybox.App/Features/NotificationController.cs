using Moneybox.App.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Features
{
    class NotificationController
    {
        private INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public void NotifyFundsLow(string email)
        {
            notificationService.NotifyFundsLow(email);
        }

        public void NotifyApproachingPayInLimit(string email)
        {
            notificationService.NotifyApproachingPayInLimit(email);
        }
    }
}
