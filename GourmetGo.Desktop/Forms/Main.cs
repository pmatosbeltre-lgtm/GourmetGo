using GourmetGo.Desktop.Helpers;
using GourmetGo.Desktop.Services;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GourmetGo.Desktop.Forms
{
    public partial class Main : Form
    {
        private readonly RestauranteService _restauranteService;

        private Panel header;
        private Panel content;
        public Main()
        {
            InitializeComponent();

            InitializeComponent();

            var api = new ApiClient();
            _restauranteService = new RestauranteService(api);

            lblTitulo.Text = "Bienvenido";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ThemeHelpers.ApplyFormBase(this);

            // Si ya tienes un panelHeader en diseñador, pásalo.
            // Si no, esto crea uno arriba automáticamente:
            ThemeHelpers.EnsureAndStyleHeader(this);
        }
    }
}