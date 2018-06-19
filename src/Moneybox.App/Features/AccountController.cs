using Moneybox.App.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Features
{
    class AccountController
    {
        private IAccountRepository accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public Account GetAccountById(Guid id)
        {
            return accountRepository.GetAccountById(id);
        }

        public void Update(Account account)
        {
            accountRepository.Update(account);
        }


    }
}
