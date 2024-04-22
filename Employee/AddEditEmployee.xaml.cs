using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace Employee
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        private GetEmployees _currentEmployee = new GetEmployees();
        public AddEmployee(GetEmployees SelectedEmployee)
        {
            InitializeComponent();
            if (SelectedEmployee != null)
            {
                _currentEmployee = SelectedEmployee;
            }

            DataContext = _currentEmployee;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? SelectedDate = Birthday.SelectedDate;
            DateTime ActualDate = (DateTime.Now);
            if (SelectedDate.HasValue)
            {
                // Преобразуем nullable DateTime в DateTime
                ActualDate = SelectedDate.Value;

                // Теперь actualDate содержит значение выбранной даты
            }

            bool CheckBoxActivate;
            if (IsActive.IsChecked == true)
            {
                CheckBoxActivate = true;
            }
            else
            {
                CheckBoxActivate = false;
            }
            string PositionConvert = Position.Text;
            string SalaryConvert = Salary.Text;
            
            var postEmployee = new PostEmployee
            {
                Firstname = Firstname.Text,
                Surname = Surname.Text,
                Lastname = Lastname.Text,
                Birthday = ActualDate,
                PositionId = Convert.ToInt16(PositionConvert),
                Salary = Convert.ToInt32(SalaryConvert),
                IsActive = CheckBoxActivate
            };

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7053/");

            var Json = JsonSerializer.Serialize(postEmployee);
            var content = new StringContent(Json, Encoding.UTF8, "application/json");

            var response = client.PostAsync("/api/PostEmployee", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var postResponse = JsonSerializer.Deserialize<PostEmployee>(responseContent, options);
                MessageBox.Show(Convert.ToString(postResponse.Id));
            }
            else
            {
                MessageBox.Show("Error" + response.StatusCode);
            }

            EmployeesPage pageUpdate = new EmployeesPage();
            pageUpdate.UpdateContent();

            Manager.EmployeeFrame.GoBack();

            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.EmployeeFrame.GoBack();
        }
    }
}
