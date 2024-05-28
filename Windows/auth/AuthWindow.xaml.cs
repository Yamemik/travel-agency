using System.Text;
using System.Windows;

namespace TravelAgency.Windows.auth
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            using (TravelDBContext db = new())
            {
                IQueryable<User>? users = db.Users
                    .Where(c => c.Login == log.Text && c.Password == pass.Text);

                StringBuilder errors = new StringBuilder();

                if (users.ToList<User>().Count < 1)
                {
                    errors.AppendLine("Неверный логин или пароль");
                }
                else
                {
                    errors.AppendLine("Авторизация успешна");
                }

                MessageBox.Show(errors.ToString());
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
