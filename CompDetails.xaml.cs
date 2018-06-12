using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Collections;
using System.ComponentModel;


namespace Employees
{
    public partial class CompDetails : Page
    {
        #region Constructors
        public CompDetails()
        {
            InitializeComponent();
        }

        Employee emp;

        public float BonusAmount { get; set; } = 500;

        // Custom constructor to pass Employee object
        public CompDetails(object data) : this()
        {
            // Bind context to Employee
            this.DataContext = data;
            BonusInput.DataContext = this;

            if (data is Employee)
            {
                emp = (Employee)data;

                string name1 = "";
                string value1 = "";
                string name2 = "";
                string value2 = "";

                emp.SpareDetailProp1(ref name1, ref value1);
                emp.SpareDetailProp2(ref name2, ref value2);

                SpareProp1Name.Content = name1;
                SpareProp1Value.Content = value1;
                SpareProp2Name.Content = name2;
                SpareProp2Value.Content = value2;
            }
        }
        #endregion


        #region Methods

        private void Bonus_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           this.emp.GiveBonus(BonusAmount);
           //refresh page
           Refresh_Page();
        }

        
        private void Bonus_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = BonusAmount <= 10000 && BonusAmount >= 500;
            
        }

        private void Promotion_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            this.emp.GivePromotion();
            //refresh page
            Refresh_Page();
            
        }


        private void Refresh_Page()
        {
            object obj = this.DataContext;
            this.DataContext = null;
            this.DataContext = obj;

            string name1 = "";
            string value1 = "";
            string name2 = "";
            string value2 = "";

            emp.SpareDetailProp1(ref name1, ref value1);
            emp.SpareDetailProp2(ref name2, ref value2);

            SpareProp1Name.Content = name1;
            SpareProp1Value.Content = value1;
            SpareProp2Name.Content = name2;
            SpareProp2Value.Content = value2;
        }
        #endregion

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }
    }
}
