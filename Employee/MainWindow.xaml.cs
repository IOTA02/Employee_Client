using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Employee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EmployeeFrame.Navigate(new EmployeesPage());
            Manager.EmployeeFrame = EmployeeFrame;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeFrame.Navigate(new AddEmployee(null));
            Manager.EmployeeFrame = EmployeeFrame;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeFrameContent = EmployeeFrame.Content;

            if (employeeFrameContent is EmployeesPage employeesPage)
            {
                var selectedItem = employeesPage.EmployeesListView.SelectedItem;

                if (selectedItem != null)
                {
                    Manager.EmployeeFrame.Navigate(new AddEmployee(selectedItem as GetEmployees));
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeFrameContent = EmployeeFrame.Content;

            if (employeeFrameContent is EmployeesPage employeesPage)
            {
                var selectedItem = (GetEmployees)employeesPage.EmployeesListView.SelectedItem;

                if (selectedItem != null)
                {
                    var client = new HttpClient();

                    client.BaseAddress = new Uri("https://localhost:7053/");

                    int selectedId = selectedItem.Id;
                    HttpResponseMessage response = await client.DeleteAsync($"/api/DeleteEmployee/{selectedId}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Успешно!");

                        EmployeesPage pageUpdate = new EmployeesPage();
                        pageUpdate.UpdateContent();
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка при выполнении DELETE: {response.StatusCode} Send ID: {selectedId}");

                        EmployeesPage pageUpdate = new EmployeesPage();
                        pageUpdate.UpdateContent();
                    }
                }
            }
        }
    }
}