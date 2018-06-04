using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;
        public const decimal FundsLowValue = 500m;
        public const decimal NearLimitValue = 500m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public void Withdraw(decimal amount)
        {
            var newBalance = this.Balance - amount;

            if (newBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            this.Balance -= amount;
            this.Withdrawn -= amount;
        }

        public void Deposit(decimal amount)
        {
            var newBalance = this.PaidIn + amount;

            if (newBalance > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            this.Balance += amount;
            this.PaidIn += amount;
        }

        public decimal PaidIn { get; set; }
    }
}
