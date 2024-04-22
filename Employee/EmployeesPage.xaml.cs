using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows.Controls;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Data;

namespace Employee
{
    /// <summary>
    /// Interaction logic for EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();

            UpdateContent();
        }
        internal async Task FormatHTTPEmployeeResponse(HttpResponseMessage responseEmployee)
        {
            if (responseEmployee.IsSuccessStatusCode)
            {
                var responseContent = await responseEmployee.Content.ReadAsStringAsync();
                var employees = JsonSerializer.Deserialize<List<GetEmployees>>(responseContent);

                EmployeesListView.ItemsSource = employees;
            }
            else
            {
                MessageBox.Show("Error" + responseEmployee.StatusCode);
            }
        }
        internal async Task FormatHTTPEPositionsResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var positions = JsonSerializer.Deserialize<List<GetPositions>>(responseContent);
                MessageBox.Show(Convert.ToString(positions));
            }
            else
            {
                MessageBox.Show("Error" + response.StatusCode);
            }
        }
        public GetEmployees SelectedEmployee
        {
            get { return (GetEmployees)EmployeesListView.SelectedItem; }
        }

        internal void UpdateContent ()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:7053/");

            var responseEmployee = client.GetAsync("/api/GetAllEmployees").Result;
            FormatHTTPEmployeeResponse(responseEmployee);
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                UpdateContent();
            }
        }
    }
}
