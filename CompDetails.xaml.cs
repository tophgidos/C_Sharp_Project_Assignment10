using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        List<Employee> ValidEmps = new List<Employee>();
        List<string> empNames = new List<string>();
        

        // Custom constructor to pass Employee object
        public CompDetails(object data) : this()
        {
            // Bind context to Employee
            this.DataContext = data;

            if (data is Employee)
            {
                emp = (Employee)data;

                string name1 = "";
                string value1 = "";
                string name2 = "";
                string value2 = "";

                emp.SpareDetailProp1(ref name1, ref value1);
                if (!(emp is Manager))
                {
                    emp.SpareDetailProp2(ref name2, ref value2);
                    reports.Visibility = Visibility.Collapsed;
                    RemoveReportsButton.Visibility = Visibility.Collapsed;
                    AddReportsButton.Visibility = Visibility.Collapsed;
                    EmpsComboBox.Visibility = Visibility.Collapsed;
                }
                else
                {
                    
                    name2 = "Reports:";
                    RemoveReportsButton.Visibility = Visibility.Visible;
                    reports.Visibility = Visibility.Visible;
                    AddReportsButton.Visibility = Visibility.Visible;
                    EmpsComboBox.Visibility = Visibility.Visible;

                    reports.ItemsSource = (Manager)emp;

                    //set equal to employee list
                    /*EmployeeList empList1 = ;

                    foreach (Employee tempEmp in empList1)
                    {
                        if(((Manager)emp).CanAddReport(tempEmp) == true)
                        { 
                            ValidEmps.Add(tempEmp);
                            empNames.Add(tempEmp.Name);
                        }
                    }*/

                    EmpsComboBox.ItemsSource = empNames;
                }

                SpareProp1Name.Content = name1;
                SpareProp1Value.Content = value1;
                SpareProp2Name.Content = name2;
                SpareProp2Value.Content = value2;
            }
        }
        #endregion

        private void RemoveReport_Executed(object sender, RoutedEventArgs e)
        {
            ((Manager)emp).RemoveReport((Employee)reports.SelectedItem);
            reports.Items.Refresh();
        }

        private void RemoveReport_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = reports.SelectedIndex >= 0;
        }

        private void AddReport_Executed(object sender, RoutedEventArgs e)
        {
            ((Manager)emp).AddReport(ValidEmps[EmpsComboBox.SelectedIndex]);
            reports.Items.Refresh();
        }

        private void AddReport_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = EmpsComboBox.SelectedIndex >= 0;
        }

        private void Bonus_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            float b = float.Parse(BonusInput.Text);
            this.emp.GiveBonus(b);
        }

        private void Promotion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.emp.GivePromotion();
            
        }
    }
}
