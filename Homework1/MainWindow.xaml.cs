using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDataStorage
{
    public partial class MainWindow : Window
    {
        struct PersonRecords
        {
            public int id;
            public string name;
            public int age;
            public string address;
        }

        static int nextId = 1;  //autoincrementing ID
        const int N = 100;    //the maximum number of records
        PersonRecords[] records = new PersonRecords[N]; //an array of records
        int count = 0;  //current number of records

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            if (count >= N)
            {
                MessageBox.Show("Storage is full!");
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Invalid age!");
                return;
            }

            records[count].id = nextId++;
            records[count].name = txtName.Text;
            records[count].age = age;
            records[count].address = txtAddress.Text;
            count++;

            MessageBox.Show("Record added!");
        }

        private void ShowRecords_Click(object sender, RoutedEventArgs e)
        {
            lstRecords.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                lstRecords.Items.Add($"{records[i].id}: {records[i].name}, {records[i].age}, {records[i].address}");
            }
        }

        private void SortByAge_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(records, 0, count, Comparer<PersonRecords>.Create((a, b) => a.age.CompareTo(b.age)));
            ShowRecords_Click(sender, e);
        }

        private void SortByName_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(records, 0, count, Comparer<PersonRecords>.Create((a, b) => a.name.CompareTo(b.name)));
            ShowRecords_Click(sender, e);
        }

        private void SearchByAge_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Invalid age!");
                return;
            }

            lstRecords.Items.Clear();
            for (int i = 0; i < count; i++)
            {
                if (records[i].age == age)
                {
                    lstRecords.Items.Add($"{records[i].id}: {records[i].name}, {records[i].age}, {records[i].address}");
                }
            }
        }
        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            lstRecords.Items.Clear();

            for (int i = 0; i < count; i++)
            {
                if (records[i].name == name)
                {
                    lstRecords.Items.Add($"{records[i].id}: {records[i].name}, {records[i].age}, {records[i].address}");
                }
            }
        }

        private void DeleteByAge_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Invalid age!");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                if (records[i].age == age)
                {
                    count--;
                    for (int j = 0; j < count; j++)
                    {
                        records[j] = records[j + 1];
                    }
                    i--;    // Check same index again
                }
            }

            MessageBox.Show("Records deleted!");
            ShowRecords_Click(sender, e);
        }

        private void DeleteByName_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();

            for (int i = 0; i < count; i++)
            {
                if (records[i].name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    count--;
                    for (int j = i; j < count; j++)
                        records[j] = records[j + 1];
                    i--; // Check same index again
                }
            }

            MessageBox.Show("Records deleted!");
            ShowRecords_Click(sender, e);
        }
    }
}
