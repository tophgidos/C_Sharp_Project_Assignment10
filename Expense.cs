using System;

namespace Employees
{
    public enum ExpenseCategory { Travel, Meals, Lodging, Conference, Misc }

    [Serializable]
    public class Expense
    {
        #region Data members / properties
        public DateTime Date { get; set; } = DateTime.Today;
        public ExpenseCategory Category { get; set; } = ExpenseCategory.Travel;
        public string Description { get; set; } = "";
        public double Amount { get; set; } = 0.0;
        #endregion

        #region Constructors
        public Expense() { }

        public Expense(DateTime expDate, ExpenseCategory category, string description, double amount)
        {
            Date = expDate;
            Category = category;
            Description = description;
            Amount = amount;
        }
        #endregion
    }
}
