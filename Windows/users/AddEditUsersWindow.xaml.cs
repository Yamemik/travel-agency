using System.Text;
using System.Windows;

namespace TravelAgency.Windows.users
{
    public partial class AddEditUsersWindow : Window
    {
        private User _currentUser = new User();

        public AddEditUsersWindow(User selectedUser)
        {
            InitializeComponent();

            if (selectedUser != null)
                _currentUser = selectedUser;
            DataContext = _currentUser;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentUser.Name))
                errors.AppendLine("Укажите название отеля");


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
                        // Random rnd = new Random();
                        db.Users.Add(_currentUser);
                    }
                    else if (_currentUser.Id == 0)
                    {

                    }

                    // сохранение отслеживаемых изменений в базе данных
                    db.SaveChanges();
                    MessageBox.Show("Информация сохранена!");
                    HotelWindow hotelWindow = new HotelWindow();
                    hotelWindow.Show();
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
            this.Close();
        }
    }
}
