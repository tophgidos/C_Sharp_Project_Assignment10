﻿// CSD 228 - Assignment 9 Solution - Nat Ballou
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Employees
{
    public partial class CompHome : Page
    {
        #region Data members
        private EmployeeList empList;
        #endregion

        #region Constructors
        public CompHome()
        {
            InitializeComponent();
        }

        public CompHome(EmployeeList emps) : this()
        {
            empList = emps;

            // Set event handler for Employee type radio button
            this.EmployeeTypeRadioButtons.SelectionChanged += new SelectionChangedEventHandler(EmployeeTypeRadioButtons_SelectionChanged);

            // Select the first employee type radio button
            this.EmployeeTypeRadioButtons.SelectedIndex = 0;
            RefreshEmployeeList();
        }
        #endregion

        #region Class methods / Event handlers
        // Handle enable/disable of Details and Expenses buttons
        private void Button_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // Check if an Employee is selected to enable Review button
            e.CanExecute = dgEmps.SelectedIndex >= 0;
        }

        // Handle Details button click
        private void Details_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Create Details page and navigate to page
            this.NavigationService.Navigate(new CompDetails(this.dgEmps.SelectedItem));
        }

        // Handle Double click event on data grid row 
        private void Details_DoubleClick(object sender, RoutedEventArgs e)
        {
            // Create Details page and navigate to page
            this.NavigationService.Navigate(new CompDetails(this.dgEmps.SelectedItem));
        }

        // Handle Expenses button click
        private void Expenses_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Create Expenses page and navigate to page
            CompExpenses expensesPage = new CompExpenses(this.dgEmps.SelectedItem);
            this.NavigationService.Navigate(expensesPage);
        }

        // Handle Add employee button click
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CompAddEmployee(this, empList));
        }

        // Handle Rmove employee button click
        private void RemoveEmp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RemoveEmp();
        }

        // Handle changes to Employee type radio buttons
        private void EmployeeTypeRadioButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshEmployeeList();
        }

        public void RemoveEmp()
        {
            empList.Remove((Employee)this.dgEmps.SelectedItem);
        }

        // Filter Employee list according to radio button setting
        public void RefreshEmployeeList()
        {
            List<Employee> empList1;

            // Apply the selection
            switch (this.EmployeeTypeRadioButtons.SelectedIndex)
            {
                // Managers
                case 1:
                    empList1 = (List<Employee>)empList.FindAll(obj => obj is Manager);
                    break;

                // Sales
                case 2:
                    empList1 = (List<Employee>)empList.FindAll(obj => obj is SalesPerson);
                    break;

                // Other
                case 3:
                    empList1 = (List<Employee>)empList.FindAll(obj => !(obj is Manager || obj is SalesPerson));
                    break;

                // All 
                default: empList1 = empList;
                    break;
            }

            dgEmps.ItemsSource = new ObservableCollection<Employee>(empList1);

            dgEmps.Items.Refresh();
        }
        #endregion
    }
}
