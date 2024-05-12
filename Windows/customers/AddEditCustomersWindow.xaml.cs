using System.Data;
using System.Windows;

namespace TravelAgency.Windows.customers
{
    public partial class AddEditCustomersWindow : Window
    {
        private Customer _currentData = new Customer();

        public AddEditCustomersWindow(Customer? selectedRow)
        {
            InitializeComponent();

            if (selectedRow != null)
            {
                _currentData = selectedRow;
                dpBirthDate.SelectedDate = _currentData.BirthDate.ToDateTime(TimeOnly.MinValue);

            }
            else
            {
                DateTime thisDay = DateTime.Today;

                dpBirthDate.SelectedDate = thisDay.AddYears(-30);
            }

            DataContext = _currentData;

            cbGender.SelectedItem = _currentData.Gender;

            string[] arr_role = { "men", "women" };

            cbGender.ItemsSource = arr_role;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (TravelDBContext db = new())
            {
                try
                {
                    if (dpBirthDate.SelectedDate.HasValue)
                    {
                        DateTime dateTime = dpBirthDate.SelectedDate.Value;
                        _currentData.BirthDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                    }

                    if (_currentData.Id == 0)
                    {
                        db.Customers.Add(_currentData);
                    }
                    else
                    {
                        Customer updateRow = db.Customers
                            .First(c => c.Id == _currentData.Id);

                        updateRow.Surname = _currentData.Surname;
                        updateRow.Name = _currentData.Name;
                        updateRow.Patronymic = _currentData.Patronymic;
                        updateRow.Gender = _currentData.Gender;
                        updateRow.PassportNumber = _currentData.PassportNumber;
                        updateRow.BirthDate = _currentData.BirthDate;
                    }

                    // сохранение отслеживаемых изменений в базе данных
                    db.SaveChanges();
                    MessageBox.Show("Информация сохранена!");

                    CustomersWindow win = new CustomersWindow();
                    win.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Закрыть?", "Закрыть", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (response == MessageBoxResult.Yes)
            {
                CustomersWindow win = new CustomersWindow();
                win.Show();
                this.Close();
            }
        }
    }
}
