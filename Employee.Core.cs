using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Employees
{
    [Serializable]
    public partial class Employee : INotifyPropertyChanged
    {
        #region Data members and Properties
        private string empFirstName;
        private string empLastName;
        private int empID = nextId++;
        private float currPay;
        private DateTime empDOB;
        private string empSSN;
        private static int nextId = 1;   // Used to generate unique Ids

        public static int NamespaceLength = 10;
        public List<Expense> Expenses = new List<Expense>();

     

        public string Name { get { return string.Format($"{empFirstName} {empLastName}"); } }
        public string FirstName { get { return empFirstName; } }
        public string LastName { get { return empLastName; } }
        public int Id { get { return empID; } }
        public float Pay { get { return currPay; } set
            {
                if (value == currPay) return;
                currPay = value;
                OnPropertyChanged();
            } }
        public int Age { get { return (DateTime.Now.Year - empDOB.Year); } }
        public DateTime DateOfBirth { get { return empDOB; } }
        public string SocialSecurityNumber { get { return empSSN; } }
        public virtual string Role { get { return GetType().ToString().Substring(NamespaceLength); } }
        public float BonusInput { get; set; }
        public object SelectedItem { get; internal set; }
        #endregion

        #region Constructors
        public Employee() { }

        public Employee(string firstName, string lastName, DateTime birthday, float pay, string ssn)
        {
            empFirstName = firstName;
            empLastName = lastName;
            empDOB = birthday;
            currPay = pay;
            empSSN = ssn;
        }
        #endregion

        #region Serialization customization for MaxId
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // Called when the deserialization process is complete.
            if (empID >= nextId) nextId = empID + 1;
        }
        //test comment
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        internal void OnPropertyChanged ([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}