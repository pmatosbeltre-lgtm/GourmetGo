using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;
using GourmetGo.Desktop.Services;
using System;
using System.Windows.Forms;

namespace GourmetGo.Desktop.ViewsBuilders
{
    /// <summary>
    /// Builder para construir la UI de la vista de Menú
    /// Separa la lógica de construcción de UI del Main form
    /// </summary>
    public class MenuViewBuilder
    {
        private readonly Panel _container;
        private readonly Action<int> _onEditarMenu;
        private readonly Action<int, string> _onEliminarMenu;
        private readonly Func<Task> _onRefrescarMenus;
        private readonly Func<Task> _onCrearMenu;
        private readonly Func<Task> _onAgregarPlato;
        private readonly Func<Task> _onLoadPlatos;

        // Controles públicos para acceso desde Main
        // Inicializar con 'new' para que nunca sean null, 
        // aunque Build() no se haya ejecutado aún.
        public ComboBox CmbMenus { get; private set; } = new ComboBox();
        public DataGridView GridMenus { get; private set; } = new DataGridView();
        public DataGridView GridPlatos { get; private set; } = new DataGridView();
        public TextBox TxtNuevoMenu { get; private set; } = new TextBox();
        public Button BtnCrearMenu { get; private set; } = new Button();
        public Button BtnRefrescarMenu { get; private set; } = new Button();
        public TextBox TxtPlatoNombre { get; private set; } = new TextBox();
        public NumericUpDown NumPlatoPrecio { get; private set; } = new NumericUpDown();
        public CheckBox ChkDisponible { get; private set; } = new CheckBox();
        public Button BtnAgregarPlato { get; private set; } = new Button();

        public MenuViewBuilder(
            Panel container,
            Action<int> onEditarMenu,
            Action<int, string> onEliminarMenu,
            Func<Task> onRefrescarMenus,
            Func<Task> onCrearMenu,
            Func<Task> onAgregarPlato,
            Func<Task> onLoadPlatos)
        {
            _container = container;
            _onEditarMenu = onEditarMenu;
            _onEliminarMenu = onEliminarMenu;
            _onRefrescarMenus = onRefrescarMenus;
            _onCrearMenu = onCrearMenu;
            _onAgregarPlato = onAgregarPlato;
            _onLoadPlatos = onLoadPlatos;
        }

        public void Build()
        {
            _container.SuspendLayout();
            _container.Controls.Clear();

            // Panel superior: Crear menú
            var topPanel = BuildTopPanel();
            _container.Controls.Add(topPanel);

            // Label + DataGrid de menús
            var lblMenusGrid = new Label
            {
                AutoSize = true,
                Text = "Menús del restaurante:",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Location = new Point(18, 178)
            };
            _container.Controls.Add(lblMenusGrid);

            GridMenus = BuildGridMenus();  
            _container.Controls.Add(GridMenus);

            // Label informativo
            var lblSelectCombo = new Label
            {
                AutoSize = true,
                Text = "O selecciona un menú:",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Location = new Point(18, 398)
            };
            _container.Controls.Add(lblSelectCombo);

            CmbMenus = new ComboBox
            {
                Location = new Point(180, 394),
                Size = new Size(250, 32),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = ThemeHelpers.Palette.CardWarm,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f)
            };
            CmbMenus.SelectedIndexChanged += async (_, __) => await _onLoadPlatos();
            _container.Controls.Add(CmbMenus);

            // Panel central: Agregar platos
            var middlePanel = BuildMiddlePanel();
            _container.Controls.Add(middlePanel);

            // Label + DataGrid de platos
            var lblGridTitle = new Label
            {
                AutoSize = true,
                Text = "Platos disponibles:",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Location = new Point(18, 572)
            };
            _container.Controls.Add(lblGridTitle);

            
            GridPlatos = BuildGridPlatos();
            _container.Controls.Add(GridPlatos);

            // Evento de resize
            _container.Resize += (_, __) =>
            {
                topPanel.Width = _container.ClientSize.Width - 36;
                GridMenus.Width = _container.ClientSize.Width - 36;
                middlePanel.Width = _container.ClientSize.Width - 36;
                GridPlatos.Width = _container.ClientSize.Width - 36;
            };

            _container.ResumeLayout(true);
        }

        private ShadowCardPanel BuildTopPanel()
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
                Size = new Size(_container.ClientSize.Width - 36, 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            panel.SetDoubleBuffered(true);

            var lblTitle = new Label
            {
                AutoSize = true,
                Text = "Crear nuevo menú",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Title(14f, FontStyle.Bold),
                Location = new Point(18, 14)
            };

            TxtNuevoMenu = new TextBox
            {
                Text = "",
                Location = new Point(18, 48),
                Size = new Size(280, 36),
                BorderStyle = BorderStyle.None,
                BackColor = ThemeHelpers.Palette.CardWarm,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Padding = new Padding(8)
            };
            TxtNuevoMenu.SetDoubleBuffered(true);

            BtnCrearMenu = new Button
            {
                Text = "Crear menú",
                Location = new Point(312, 48),
                Size = new Size(140, 36),
                ForeColor = Color.White,
                Font = ThemeHelpers.Ui(11f)
            };
            ThemeHelpers.StyleGoldButton(BtnCrearMenu);
            BtnCrearMenu.Click += async (_, __) => await _onCrearMenu();

            BtnRefrescarMenu = new Button
            {
                Text = "🔄 Refrescar",
                Location = new Point(468, 48),
                Size = new Size(120, 36),
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f)
            };
            ThemeHelpers.StyleBeigeGhostButton(BtnRefrescarMenu);
            BtnRefrescarMenu.Click += async (_, __) => await _onRefrescarMenus();

            TxtNuevoMenu.Clear();

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(TxtNuevoMenu);
            panel.Controls.Add(BtnCrearMenu);
            panel.Controls.Add(BtnRefrescarMenu);

            return panel;
        }

        private DataGridView BuildGridMenus()
        {
            var grid = new DataGridView
            {
                Location = new Point(18, 204),
                Size = new Size(_container.ClientSize.Width - 36, 180),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
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

            // Estilo
            grid.ColumnHeadersDefaultCellStyle.BackColor = ThemeHelpers.Palette.Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = ThemeHelpers.Ui(10f, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = ThemeHelpers.Palette.CardWarm;
            grid.DefaultCellStyle.ForeColor = ThemeHelpers.Palette.TextMain;
            grid.DefaultCellStyle.Font = ThemeHelpers.Ui(10f);
            grid.DefaultCellStyle.SelectionBackColor = ThemeHelpers.Palette.Gold;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            // Columnas
            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "ID",
                DataPropertyName = "Id",
                Width = 50,
                ReadOnly = true
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200,
                ReadOnly = true
            });

            grid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Activo",
                HeaderText = "Activo",
                DataPropertyName = "Activo",
                Width = 80,
                ReadOnly = true
            });

            var btnEditarCol = new DataGridViewButtonColumn
            {
                Name = "Editar",
                HeaderText = "Editar",
                Text = "✏️ Editar",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            grid.Columns.Add(btnEditarCol);

            var btnEliminarCol = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Eliminar",
                Text = "🗑️ Eliminar",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            grid.Columns.Add(btnEliminarCol);

            // Evento click
            grid.CellClick += (s, e) =>
            {
                try
                {
                    // 1. Validar que no sea clic en el encabezado
                    if (e.RowIndex < 0) return;

                    // 2. Obtener el ID de forma segura (aquí está la corrección del error)
                    var cellValue = grid.Rows[e.RowIndex].Cells["Id"].Value;
                    if (cellValue == null || !int.TryParse(cellValue.ToString(), out int menuId))
                    {
                        return; // Si no hay un ID válido, no hacemos nada
                    }

                    // 3. Actualizar el estado global del Builder
                    _lastSelectedMenuId = menuId;

                    // 4. Lógica de los botones
                    if (e.ColumnIndex == grid.Columns["Editar"].Index)
                    {
                        _onEditarMenu?.Invoke(menuId);
                    }
                    else if (e.ColumnIndex == grid.Columns["Eliminar"].Index)
                    {
                        var nombre = grid.Rows[e.RowIndex].Cells["Nombre"].Value?.ToString() ?? "";
                        _onEliminarMenu?.Invoke(menuId, nombre);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al seleccionar menú: {ex.Message}");
                }

            };

            return grid;
        }
        private ShadowCardPanel BuildMiddlePanel()
        {
            var panel = new ShadowCardPanel
            {
                BackColor = ThemeHelpers.Palette.Card,
                Radius = 18,
                BorderColor = ThemeHelpers.Palette.Border,
                ShadowColor = ThemeHelpers.Palette.Shadow,
                ShadowOffset = 10,
                ShadowBlur = 22,
                Location = new Point(18, 428),
                Size = new Size(_container.ClientSize.Width - 36, 130),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            panel.SetDoubleBuffered(true);

            var lblTitle = new Label
            {
                AutoSize = true,
                Text = "Agregar nuevo plato",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Title(14f, FontStyle.Bold),
                Location = new Point(18, 14)
            };

            TxtPlatoNombre = new TextBox
            {
                Text = "",
                Location = new Point(18, 50),
                Size = new Size(210, 36),
                BorderStyle = BorderStyle.None,
                BackColor = ThemeHelpers.Palette.CardWarm,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Padding = new Padding(8),
                PlaceholderText = "Nombre del plato"
            };
            TxtPlatoNombre.SetDoubleBuffered(true);

            NumPlatoPrecio = new NumericUpDown
            {
                Location = new Point(238, 50),
                Size = new Size(140, 36),
                BackColor = ThemeHelpers.Palette.CardWarm,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                DecimalPlaces = 2,
                Increment = 0.5m,
                Minimum = 0,
                Maximum = 1000
            };

            ChkDisponible = new CheckBox
            {
                Text = "Disponible",
                Location = new Point(388, 58),
                Size = new Size(140, 20),
                Checked = true,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f)
            };

            BtnAgregarPlato = new Button
            {
                Text = "Agregar plato",
                Location = new Point(538, 50),
                Size = new Size(140, 36),
                ForeColor = Color.White,
                Font = ThemeHelpers.Ui(11f)
            };
            ThemeHelpers.StyleGoldButton(BtnAgregarPlato);
            BtnAgregarPlato.Click += async (_, __) => await _onAgregarPlato();

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(TxtPlatoNombre);
            panel.Controls.Add(NumPlatoPrecio);
            panel.Controls.Add(ChkDisponible);
            panel.Controls.Add(BtnAgregarPlato);

            return panel;
        }

        private DataGridView BuildGridPlatos()
        {
            var grid = new DataGridView
            {
                Location = new Point(18, 598),
                Size = new Size(_container.ClientSize.Width - 36, 250),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoGenerateColumns = true,
                BackgroundColor = ThemeHelpers.Palette.Card,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                EnableHeadersVisualStyles = false
            };

            grid.ColumnHeadersDefaultCellStyle.BackColor = ThemeHelpers.Palette.Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = ThemeHelpers.Ui(10f, FontStyle.Bold);
            grid.DefaultCellStyle.BackColor = ThemeHelpers.Palette.CardWarm;
            grid.DefaultCellStyle.ForeColor = ThemeHelpers.Palette.TextMain;
            grid.DefaultCellStyle.Font = ThemeHelpers.Ui(10f);
            grid.DefaultCellStyle.SelectionBackColor = ThemeHelpers.Palette.Gold;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            return grid;
        }

        public async Task MostrarDialogoEditarMenuAsync(
            MenuService menuService,
            Func<Task> onRefrescar)
        {
            try
            {
                var result = await menuService.ObtenerPorIdAsync(_lastSelectedMenuId);
                if (!result.Success || result.Data == null)
                {
                    MessageHelper.Error("No se pudo cargar el menú.");
                    return;
                }

                var menu = result.Data;

                var dialog = new Form
                {
                    Text = "Editar Menú",
                    Width = 400,
                    Height = 250,
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    BackColor = ThemeHelpers.Palette.BeigeApp
                };

                ThemeHelpers.ApplyFormBase(dialog);

                var lblNombre = new Label
                {
                    Text = "Nombre del menú:",
                    Location = new Point(20, 20),
                    AutoSize = true,
                    ForeColor = ThemeHelpers.Palette.TextMain,
                    Font = ThemeHelpers.Ui(11f)
                };

                var txtNombre = new TextBox
                {
                    Text = menu.Nombre,
                    Location = new Point(20, 50),
                    Size = new Size(340, 36),
                    BackColor = ThemeHelpers.Palette.Card,
                    ForeColor = ThemeHelpers.Palette.TextMain,
                    Font = ThemeHelpers.Ui(11f),
                    Padding = new Padding(8)
                };

                var chkActivo = new CheckBox
                {
                    Text = "Menú activo",
                    Checked = menu.Activo,
                    Location = new Point(20, 100),
                    ForeColor = ThemeHelpers.Palette.TextMain,
                    Font = ThemeHelpers.Ui(11f)
                };

                var btnGuardar = new Button
                {
                    Text = "Guardar",
                    Location = new Point(200, 150),
                    Size = new Size(160, 40),
                    ForeColor = Color.White,
                    Font = ThemeHelpers.Ui(11f)
                };
                ThemeHelpers.StyleGoldButton(btnGuardar);

                btnGuardar.Click += async (_, __) =>
                {
                    if (string.IsNullOrWhiteSpace(txtNombre.Text))
                    {
                        MessageHelper.Error("El nombre no puede estar vacío.");
                        return;
                    }

                    btnGuardar.Enabled = false;

                    try
                    {
                        var updateResult = await menuService.ActualizarAsync(_lastSelectedMenuId, new UpdateMenuDTO
                        {
                            Nombre = txtNombre.Text.Trim(),
                            Activo = chkActivo.Checked
                        });

                        if (!updateResult.Success)
                        {
                            MessageHelper.Error(updateResult.Message);
                            return;
                        }

                        MessageHelper.Info("Menú actualizado correctamente.");
                        await onRefrescar();
                        dialog.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageHelper.Error($"Error: {ex.Message}");
                    }
                    finally
                    {
                        btnGuardar.Enabled = true;
                    }
                };

                dialog.Controls.Add(lblNombre);
                dialog.Controls.Add(txtNombre);
                dialog.Controls.Add(chkActivo);
                dialog.Controls.Add(btnGuardar);

                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error abriendo diálogo: {ex.Message}");
            }
        }

        public async Task EliminarMenuAsync(
            MenuService menuService,
            int menuId,
            string nombreMenu,
            Func<Task> onRefrescar)
        {
            try
            {
                var result = MessageBox.Show(
                    $"¿Eliminar el menú '{nombreMenu}'?\n\n⚠️ ADVERTENCIA: Se eliminarán también todos los platos asociados a este menú.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;

                var deleteResult = await menuService.EliminarAsync(menuId);

                if (!deleteResult.Success)
                {
                    MessageHelper.Error(deleteResult.Message);
                    return;
                }

                MessageHelper.Info("Menú eliminado correctamente.");
                await onRefrescar();
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error eliminando menú: {ex.Message}");
            }
        }

        // Variable privada para guardar el menú seleccionado
        private int _lastSelectedMenuId;
    }
}