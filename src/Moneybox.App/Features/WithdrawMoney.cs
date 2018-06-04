using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var user = this.accountRepository.GetAccountById(fromAccountId);
            user.Withdraw(amount);
            this.accountRepository.Update(user);

            if (user.Balance < Account.FundsLowValue)
            {
                this.notificationService.NotifyFundsLow(user.User.Email);
            }
        }
    }
}
