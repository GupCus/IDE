using API.Clients;

namespace WindowsForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    loginButton.Enabled = false;
                    loginButton.Text = "Iniciando sesión...";

                    var authService = AuthServiceProvider.Instance;
                    bool success = await authService.LoginAsync(usernameTextBox.Text, passwordTextBox.Text);

                    if (success)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.", "Error de autenticación", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        passwordTextBox.Clear();
                        passwordTextBox.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al iniciar sesión: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    loginButton.Enabled = true;
                    loginButton.Text = "Iniciar Sesión";
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            errorProvider.SetError(usernameTextBox, string.Empty);
            errorProvider.SetError(passwordTextBox, string.Empty);

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                errorProvider.SetError(usernameTextBox, "El nombre de usuario es requerido");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                errorProvider.SetError(passwordTextBox, "La contraseña es requerida");
                isValid = false;
            }

            return isValid;
        }

        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                loginButton_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }
    }
}