using System.Text;
using System.Windows;

namespace TravelAgency.Windows.users
{
    public partial class AddEditUsersWindow : Window
    {
        private User _currentUser = new User();

        public AddEditUsersWindow(User? selectedUser)
        {
            InitializeComponent();

            if (selectedUser != null)
                _currentUser = selectedUser;
            DataContext = _currentUser;

            role.SelectedItem = _currentUser.Role;

            string[] arr_role = { "admin", "user" };

            role.ItemsSource = arr_role;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.Surname))
                errors.AppendLine("Укажите фамилию");


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            using (TravelDBContext db = new())
            {
                try
                {
                    if (_currentUser.Id == 0)
                    {
                        IQueryable<User>? user = db.Users;
                        _currentUser.Login = login.Text;
                        db.Users.Add(_currentUser);
                    }
                    else
                    {
                        User updateUser = db.Users
                            .First(c => c.Id == _currentUser.Id);
                        _currentUser.Login = login.Text;

                        updateUser.Login = _currentUser.Surname;
                        updateUser.Name = _currentUser.Name;
                        updateUser.Patronymic = _currentUser.Patronymic;
                        updateUser.Role = _currentUser.Role;
                        updateUser.Login = _currentUser.Login;
                        updateUser.Password = _currentUser.Password;
                    }

                    // сохранение отслеживаемых изменений в базе данных
                    db.SaveChanges();
                    MessageBox.Show("Информация сохранена!");

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
                this.Close(); 
            }
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            login.Text = surname.Text + name.Text.FirstOrDefault() + patr.Text.FirstOrDefault();
        }
    }
}
