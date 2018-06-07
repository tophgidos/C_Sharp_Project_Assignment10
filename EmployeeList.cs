using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;

// Gain access to the BinaryFormatter in mscorlib.dll.
using System.Runtime.Serialization.Formatters.Binary;

namespace Employees
{
    [Serializable]
    public class EmployeeList : List<Employee>
    {
        #region Data members
        private const string DataFile = "Employees.dat";
        private static BinaryFormatter binFormat = new BinaryFormatter();
        #endregion

        #region Class Methods
        // Either load EmployeeList from file or create an initial EmployeeList
        static public EmployeeList GetEmployees()
        {
            EmployeeList emps = null;

            // Load data if file found, otherwise use initial dummy set of Employees
            if (File.Exists(DataFile)) return LoadEmployees();
            else
            {
                // Get initial employees and save to file
                emps = InitialEmployees();
                emps.SaveEmployees();
            }

            return emps;
        }

        // Load EmployeeList from a binary file
        private static EmployeeList LoadEmployees(string dataFile = DataFile)
        {
            EmployeeList emps = null;

            try
            {
                using (Stream fStream = File.OpenRead(dataFile))
                {
                    emps = (EmployeeList)binFormat.Deserialize(fStream);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return emps;
        }

        // Save this instance of EmployeeList to a binary file
        public void SaveEmployees(string dataFile = DataFile)
        {
            try
            {
                using (Stream fStream = new FileStream(dataFile,
                  FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    binFormat.Serialize(fStream, this);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // Populate an initial set of Employees
        private static EmployeeList InitialEmployees()
        {
            // Create Employees
            Executive dan = new Executive("Dan", "Brown", DateTime.Parse("3/20/1963"), 200000, "121-12-1211", 50000, ExecTitle.CEO);
            Executive connie = new Executive("Connie", "Chung", DateTime.Parse("2/5/1972"), 150000, "229-67-7898", 40000, ExecTitle.CFO);
            Manager chucky = new Manager("Chucky", "Jones", DateTime.Parse("4/23/1967"), 100000, "333-23-2322", 9000);
            Manager mary = new Manager("Mary", "Williams", DateTime.Parse("5/9/1963"), 200000, "121-12-1211", 9500);
            Engineer bob = new Engineer("Bob", "Roe", DateTime.Parse("6/30/1986"), 120000, "334-24-2422", DegreeName.MS);
            SalesPerson fran = new SalesPerson("Fran", "Smith", DateTime.Parse("7/5/1975"), 80000, "932-32-3232", 31);
            PTSalesPerson sam = new PTSalesPerson("Sam", "Abbott", DateTime.Parse("8/11/1984"), 20000, "525-76-5030", 20);
            PTSalesPerson sally = new PTSalesPerson("Sally", "McRae", DateTime.Parse("9/12/1979"), 30000, "913-43-4343", 10);
            SupportPerson mike = new SupportPerson("Mike", "Roberts", DateTime.Parse("10/31/1975"), 15000, "229-67-7898", ShiftName.One);
            SupportPerson steve = new SupportPerson("Steve", "Kinny", DateTime.Parse("11/21/1982"), 80000, "913-43-4343", ShiftName.Two);

            // Bonuses and promotions
            dan.GiveBonus(1000);
            bob.GiveBonus(500);
            sally.GiveBonus(400);
            dan.GivePromotion();
            chucky.GivePromotion();
            fran.GivePromotion();

            // Add reports - just report error and continue on exception
            try
            {
                dan.AddReport(chucky);
                dan.AddReport(mary);
                connie.AddReport(fran);
                connie.AddReport(sally);
                mary.AddReport(sam);
                mary.AddReport(mike);
                chucky.AddReport(bob);
                chucky.AddReport(steve);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error adding reports: {e.Message}");
            }

            // Add Expenses
            dan.Expenses.Add(new Expense(DateTime.Parse("1/25/2017"), ExpenseCategory.Travel, "Airline Tickets - San Diego", 300.55));
            dan.Expenses.Add(new Expense(DateTime.Parse("1/27/2017"), ExpenseCategory.Meals, "Breakfast at Hotel", 27.61));
            dan.Expenses.Add(new Expense(DateTime.Parse("1/29/2017"), ExpenseCategory.Lodging, "Conference Hotel - 5 nights", 423.15));

            // Add Employees to list
            return new EmployeeList() { dan, connie, chucky, mary, bob, fran, sam, sally, mike, steve };
        }
        #endregion
    }
}
