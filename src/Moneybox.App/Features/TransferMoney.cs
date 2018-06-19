using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {

        private static AccountController accountController;
        private static NotificationController notificationController;

        public TransferMoney() { }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = GetAccountById(fromAccountId);
            var to = GetAccountById(toAccountId);

            var fromBalance = from.Balance - amount;
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (fromBalance < 500m)
            {
                NotifyFundsLow(from.User.Email);
            }

            var paidIn = to.PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                NotifyApproachingPayInLimit(to.User.Email);
            }

            from.Balance = from.Balance - amount;
            from.Withdrawn = from.Withdrawn - amount;

            to.Balance = to.Balance + amount;
            to.PaidIn = to.PaidIn + amount;

            Update(from);
            Update(to);
        }

        public Account GetAccountById(Guid id)
        {
            return accountController.GetAccountById(id);
        }

        public void Update(Account account)
        {
            accountController.Update(account);
        }

        public void NotifyFundsLow(string email)
        {
            notificationController.NotifyFundsLow(email);
        }
        
        public void NotifyApproachingPayInLimit(string email)
        {
            notificationController.NotifyApproachingPayInLimit(email);
        }
    }
}
