using GourmetGo.Application.DTOs.Seguridad;
using GourmetGo.Desktop.Helpers;
using GourmetGo.Desktop.Services;
using System;
using System.Windows.Forms;

namespace GourmetGo.Desktop.Forms
{
    public partial class frmLOGIN : Form
    {
        public frmLOGIN()
        {
            InitializeComponent();
        }

        private void frmLOGIN_Load(object sender, EventArgs e)
        {
            // Decoración premium (NO toca tu btnLogin_Click)
            ThemeHelpers.ApplyFancyLogin(this, txtCorreo, txtContrasena, btnLogin);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                btnLogin.Enabled = false;

                var api = new ApiClient();
                var auth = new AuthService(api);

                var dto = new LoginRequestDTO
                {
                    Correo = txtCorreo.Text.Trim(),
                    Contrasena = txtContrasena.Text
                };

                var resp = await auth.LoginAsync(dto);

                if (resp == null || string.IsNullOrWhiteSpace(resp.Token))
                {
                    MessageHelper.Error("Login falló: token vacío.");
                    return;
                }

                TokenStore.SetToken(resp.Token);

                var main = new Main();
                main.FormClosed += (_, __) => this.Close();
                main.Show();

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error login: {ex.Message}");
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }
    }
}