using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private static AccountController accountController;
        private static NotificationController notificationController;

        public WithdrawMoney() { }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            // Get account byu id
            var fromAccount = accountController.GetAccountById(fromAccountId);

            var fromWithdraw = fromAccount.Withdrawn - amount;

            // Checks
            if(fromWithdraw < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make withdrawel");
            }

            if (fromWithdraw < 500m)
            {
                notificationController.NotifyFundsLow(fromAccount.User.Email);
            }

            // Update balances
            fromAccount.Withdrawn = fromAccount.Withdrawn - amount;
            fromAccount.Balance = fromAccount.Balance - amount;

            accountController.Update(fromAccount);
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
    }
}
