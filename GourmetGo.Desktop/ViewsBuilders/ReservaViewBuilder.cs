using GourmetGo.Desktop.Helpers;
using System.Windows.Forms;

namespace GourmetGo.Desktop.ViewsBuilders
{
    public class ReservaViewBuilder
    {
        private readonly Panel _container;
        private readonly Func<Task> _onRefrescar;

        public DataGridView GridReservas { get; private set; } = new DataGridView();

        public ReservaViewBuilder(Panel container, Func<Task> onRefrescar)
        {
            _container = container;
            _onRefrescar = onRefrescar;
        }

        public void Build()
        {
            _container.SuspendLayout();
            _container.Controls.Clear();

            GridReservas = new DataGridView
            {
                Location = new Point(18, 18),
                Size = new Size(_container.ClientSize.Width - 36, _container.ClientSize.Height - 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoGenerateColumns = false,
                BackgroundColor = ThemeHelpers.Palette.Card,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                EnableHeadersVisualStyles = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            GridReservas.ColumnHeadersDefaultCellStyle.BackColor = ThemeHelpers.Palette.Navy;
            GridReservas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            GridReservas.ColumnHeadersDefaultCellStyle.Font = ThemeHelpers.Ui(10f, FontStyle.Bold);
            GridReservas.DefaultCellStyle.BackColor = ThemeHelpers.Palette.CardWarm;
            GridReservas.DefaultCellStyle.ForeColor = ThemeHelpers.Palette.TextMain;
            GridReservas.DefaultCellStyle.Font = ThemeHelpers.Ui(10f);
            GridReservas.DefaultCellStyle.SelectionBackColor = ThemeHelpers.Palette.Gold;
            GridReservas.DefaultCellStyle.SelectionForeColor = Color.White;

            // Columnas
            GridReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50
            });

            GridReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NombreCliente",
                HeaderText = "Cliente",
                DataPropertyName = "UsuarioId",
                Width = 200
            });

            GridReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha",
                Width = 180
            });

            GridReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hora",
                HeaderText = "Hora",
                DataPropertyName = "Hora",
                Width = 180
            });

            GridReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Personas",
                HeaderText = "Personas",
                DataPropertyName = "CantidadPersonas",
                Width = 80
            });

            GridReservas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 100
            });

            _container.Controls.Add(GridReservas);
            _container.ResumeLayout(true);
        }
    }
}