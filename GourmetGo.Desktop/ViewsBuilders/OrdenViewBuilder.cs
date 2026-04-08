using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Domain.Enums;
using GourmetGo.Desktop.Services;
using System;
using System.Windows.Forms;

namespace GourmetGo.Desktop.ViewsBuilders
{
    public class OrdenViewBuilder
    {
        private readonly Panel _container;
        private readonly Func<Task> _onLoadOrdenes;
        private readonly Func<int, EstadoOrden, Task> _onCambiarEstado;

        // Controles públicos
        public ComboBox CmbFiltroEstado { get; private set; } = null!;
        public DataGridView GridOrdenes { get; private set; } = null!;
        public Label LblTotalOrdenes { get; private set; } = null!;

        public OrdenViewBuilder(
            Panel container,
            Func<Task> onLoadOrdenes,
            Func<int, EstadoOrden, Task> onCambiarEstado)
        {
            _container = container;
            _onLoadOrdenes = onLoadOrdenes;
            _onCambiarEstado = onCambiarEstado;
        }

        public void Build()
        {
            _container.SuspendLayout();
            _container.Controls.Clear();

            // Panel filtro
            var filterPanel = BuildFilterPanel();
            _container.Controls.Add(filterPanel);

            // Label título grid
            var lblGridTitle = new Label
            {
                AutoSize = true,
                Text = "Órdenes del restaurante:",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Location = new Point(18, 88)
            };
            _container.Controls.Add(lblGridTitle);

            // DataGrid
            GridOrdenes = BuildGridOrdenes();
            _container.Controls.Add(GridOrdenes);

            // Resize
            _container.Resize += (_, __) =>
            {
                filterPanel.Width = _container.ClientSize.Width - 36;
                GridOrdenes.Width = _container.ClientSize.Width - 36;
            };

            _container.ResumeLayout(true);
        }

        private ShadowCardPanel BuildFilterPanel()
        {
            var panel = new ShadowCardPanel
            {
                BackColor = ThemeHelpers.Palette.Card,
                Radius = 18,
                BorderColor = ThemeHelpers.Palette.Border,
                ShadowColor = ThemeHelpers.Palette.Shadow,
                ShadowOffset = 10,
                ShadowBlur = 22,
                Location = new Point(18, 18),
                Size = new Size(_container.ClientSize.Width - 36, 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            panel.SetDoubleBuffered(true);

            var lblFiltro = new Label
            {
                AutoSize = true,
                Text = "Filtrar por estado:",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Location = new Point(18, 16)
            };

            CmbFiltroEstado = new ComboBox
            {
                Location = new Point(180, 12),
                Size = new Size(200, 32),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = ThemeHelpers.Palette.CardWarm,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f)
            };

            // Llenar combo con estados
            CmbFiltroEstado.Items.Add("Todos");
            CmbFiltroEstado.Items.Add(EstadoOrden.Pendiente);
            CmbFiltroEstado.Items.Add(EstadoOrden.Aceptada);
            CmbFiltroEstado.Items.Add(EstadoOrden.EnPreparacion);
            CmbFiltroEstado.Items.Add(EstadoOrden.Lista);
            CmbFiltroEstado.Items.Add(EstadoOrden.Entregada);
            CmbFiltroEstado.Items.Add(EstadoOrden.Cancelada);
            CmbFiltroEstado.SelectedIndex = 0;
            CmbFiltroEstado.SelectedIndexChanged += async (_, __) => await _onLoadOrdenes();

            LblTotalOrdenes = new Label
            {
                AutoSize = true,
                Text = "Total: 0",
                ForeColor = ThemeHelpers.Palette.TextMuted,
                Font = ThemeHelpers.Ui(10f),
                Location = new Point(420, 18)
            };

            panel.Controls.Add(lblFiltro);
            panel.Controls.Add(CmbFiltroEstado);
            panel.Controls.Add(LblTotalOrdenes);

            return panel;
        }

        private DataGridView BuildGridOrdenes()
        {
            var grid = new DataGridView
            {
                Location = new Point(18, 114),
                Size = new Size(_container.ClientSize.Width - 36, 500),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoGenerateColumns = false,
                BackgroundColor = ThemeHelpers.Palette.Card,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                EnableHeadersVisualStyles = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            
            // Esto evita que el error de "Paint" llegue al Program.cs
            grid.DataError += (s, e) => {
                e.ThrowException = false;
            };
           
            // Hace que el cambio se registre apenas eliges una opción en el combo
            grid.CurrentCellDirtyStateChanged += (s, e) => {
                if (grid.IsCurrentCellDirty)
                {
                    grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };
            grid.ColumnHeadersDefaultCellStyle.BackColor = ThemeHelpers.Palette.Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = ThemeHelpers.Ui(10f, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = ThemeHelpers.Palette.CardWarm;
            grid.DefaultCellStyle.ForeColor = ThemeHelpers.Palette.TextMain;
            grid.DefaultCellStyle.Font = ThemeHelpers.Ui(10f);
            grid.DefaultCellStyle.SelectionBackColor = ThemeHelpers.Palette.Gold;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            // Columna ID (oculta)
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 0,
                Visible = false,
                ReadOnly = true
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha",
                Width = 120,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 120,
                ReadOnly = true
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Usuario",
                HeaderText = "Usuario",
                DataPropertyName = "UsuarioId",
                Width = 120,
                ReadOnly = true
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoOrden",
                HeaderText = "Tipo",
                DataPropertyName = "TipoOrden",
                Width = 100,
                ReadOnly = true
            });

            var cmbEstadoCol = new DataGridViewComboBoxColumn
            {
                Name = "CambiarEstado",
                HeaderText = "Cambiar Estado",
                Width = 150,
                ReadOnly = false,
                DataPropertyName = "Estado", // Vincula con la propiedad del DTO
                DataSource = Enum.GetValues(typeof(EstadoOrden)), // Usa la lista real de Enums
                ValueType = typeof(EstadoOrden), // Define el tipo estrictamente
                FlatStyle = FlatStyle.Flat
            };
            grid.Columns.Add(cmbEstadoCol);

            grid.CellValueChanged += async (s, e) =>
            {
                try
                {
                    if (e.ColumnIndex == grid.Columns["CambiarEstado"].Index && e.RowIndex >= 0)
                    {
                        var cell = grid.Rows[e.RowIndex].Cells["CambiarEstado"];
                        if (cell.Value == null) return;

                        var ordenId = (int)(grid.Rows[e.RowIndex].Cells["Id"].Value ?? 0);

                        if (Enum.TryParse(cell.Value.ToString(), out EstadoOrden nuevoEstado))
                        {
                            if (ordenId > 0)
                            {
                                //LIMPIEZA ANTES DE RECARGAR ---
                                // Evita que el foco cause un error de referencia nula al refrescar
                                grid.EndEdit();

                                await _onCambiarEstado(ordenId, nuevoEstado);

                                // Si el mensaje de éxito sale aquí, limpia la selección
                                grid.ClearSelection();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageHelper.Error($"Error: {ex.Message}");
                }
            };

            return grid;
        }
    }
}