using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;
using GourmetGo.Application.DTOs.Catalogo.Plato;
using GourmetGo.Desktop.Services;
using GourmetGo.Desktop.Helpers;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GourmetGo.Desktop.Forms
{
    public partial class Main : Form
    {
        private void Main_Load(object sender, EventArgs e)
        {
            ThemeHelpers.ApplyFormBase(this);

            ThemeHelpers.EnsureAndStyleHeader(this);
        }
        // Servicios
        private readonly RestauranteService _restauranteService;
        private readonly MenuService _menuService;
        private readonly PlatoService _platoService;
        private bool _menuUiBuilt = false;

        private int _restauranteId;

        // Controles UI del tab Menú
        private ComboBox _cmbMenus = null!;
        private DataGridView _gridPlatos = null!;
        private TextBox _txtNuevoMenu = null!;
        private Button _btnCrearMenu = null!;
        private Button _btnRefrescarMenu = null!;
        private TextBox _txtPlatoNombre = null!;
        private NumericUpDown _numPlatoPrecio = null!;
        private CheckBox _chkDisponible = null!;
        private Button _btnAgregarPlato = null!;

        // Layout base
        private Panel _sidebar = null!;
        private Panel _header = null!;
        private Panel _content = null!;

        // “Tabs” (vistas)
        private Panel _viewHome = null!;
        private Panel _viewMenu = null!;
        private Panel _viewOrdenes = null!;
        private Panel _viewReservas = null!;
        private Panel _viewAjustes = null!;

        // Header labels dinámicos
        private Label _lblHeaderTitle = null!;
        private Label _lblHeaderSub = null!;

        public Main()
        {
            InitializeComponent();

            // Servicios HTTP
            var api = new ApiClient();
            _restauranteService = new RestauranteService(api);
            _menuService = new MenuService(api);
            _platoService = new PlatoService(api);

            // Estilo base del form
            ThemeHelpers.ApplyFormBase(this);
            this.Text = "GourmetGo";
            this.MinimumSize = new Size(1100, 680);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Construir UI
            BuildShell();

            // Cargar datos después de mostrar
            this.Shown += async (_, __) => await LoadHomeDataAsync();
        }
        #region
        // =========================
        // UI Shell: Sidebar + Header + Content
        // =========================
        private void BuildShell()
        {
            this.SuspendLayout();
            this.Controls.Clear();

            // CREAR _content
            _content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelpers.Palette.BeigeApp
            };
            _content.SetDoubleBuffered(true);

            // CREAR _header Y _sidebar
            _header = BuildHeader();
            _sidebar = BuildSidebar();

            _viewHome = BuildHomeView();

            _viewMenu = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelpers.Palette.BeigeApp
            };
            _viewMenu.SetDoubleBuffered(true);

            _viewOrdenes = BuildPlaceholderView("Órdenes", "Aquí irá el tablero de órdenes, estados y detalles.");
            _viewReservas = BuildPlaceholderView("Reservas", "Aquí irá la agenda/calendario de reservas.");
            _viewAjustes = BuildPlaceholderView("Ajustes", "Aquí irá configuración, usuario, restaurante, etc.");

            _content.Controls.Add(_viewHome);
            _content.Controls.Add(_viewMenu);
            _content.Controls.Add(_viewOrdenes);
            _content.Controls.Add(_viewReservas);
            _content.Controls.Add(_viewAjustes);

            // Orden de docking
            this.Controls.Add(_content);
            this.Controls.Add(_header);
            this.Controls.Add(_sidebar);

            // Arranca en Home
            ShowView(_viewHome, "🏠 Dashboard", "Bienvenido a GourmetGo");

            this.ResumeLayout(true);
        }

        private Panel BuildSidebar()
        {
            var sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = ThemeHelpers.Palette.Navy,
                Padding = new Padding(14)
            };
            sidebar.SetDoubleBuffered(true);

            var lblBrand = new Label
            {
                AutoSize = true,
                Text = "GourmetGo",
                ForeColor = Color.White,
                Font = ThemeHelpers.Title(22f, FontStyle.Bold),
                Location = new Point(12, 18)
            };

            var lblTag = new Label
            {
                AutoSize = false,
                Text = "Premium Dashboard",
                ForeColor = Color.FromArgb(220, 255, 255, 255),
                Font = ThemeHelpers.Ui(11.5f),
                Location = new Point(14, 54),
                Size = new Size(210, 20)
            };

            var navHost = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Location = new Point(0, 90),
                Padding = new Padding(0, 18, 0, 0),
                Size = new Size(sidebar.Width, 420),
                BackColor = ThemeHelpers.Palette.Navy
            };

            // Botones navegación
            var btnHome = MakeNavButton("🏠  Home");
            btnHome.Click += (_, __) => ShowView(_viewHome, "🏠 Dashboard", "Vista general y métricas");

            var btnMenu = MakeNavButton("🍴  Menú");
            btnMenu.Click += async (_, __) =>
            {
                ShowView(_viewMenu, "🍴 Menú", "Gestión de menús y platos");

                if (!_menuUiBuilt)
                {
                    BuildMenuViewUi(_viewMenu);
                    _menuUiBuilt = true;
                }

                await LoadMenusAsync();
            };

            var btnOrdenes = MakeNavButton("📋  Órdenes");
            btnOrdenes.Click += (_, __) => ShowView(_viewOrdenes, "📋 Órdenes", "Seguimiento y estados");

            var btnReservas = MakeNavButton("🗓️  Reservas");
            btnReservas.Click += (_, __) => ShowView(_viewReservas, "🗓️ Reservas", "Agenda de reservas");

            var btnAjustes = MakeNavButton("⚙️  Ajustes");
            btnAjustes.Click += (_, __) => ShowView(_viewAjustes, "⚙️ Ajustes", "Preferencias y cuenta");

            navHost.Controls.Add(btnHome);
            navHost.Controls.Add(btnMenu);
            navHost.Controls.Add(btnOrdenes);
            navHost.Controls.Add(btnReservas);
            navHost.Controls.Add(btnAjustes);

            // Separador y logout abajo
            var spacer = new Panel { Dock = DockStyle.Fill, BackColor = ThemeHelpers.Palette.Navy };

            var btnLogout = MakeNavButton("⎋  Cerrar sesión");
            btnLogout.Margin = new Padding(0, 10, 0, 0);
            btnLogout.Click += (_, __) =>
            {
                TokenStore.Clear();
                this.Close();
            };

            var bottomHost = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = ThemeHelpers.Palette.Navy
            };
            bottomHost.Controls.Add(btnLogout);
            btnLogout.Location = new Point(0, 18);

            sidebar.Controls.Add(bottomHost);
            sidebar.Controls.Add(spacer);
            sidebar.Controls.Add(navHost);
            sidebar.Controls.Add(lblTag);
            sidebar.Controls.Add(lblBrand);

            return sidebar;
        }

        private Button MakeNavButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Width = 220,
                Height = 44,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 6, 0, 0)
            };

            // Ghost navy style (white text)
            ThemeHelpers.StyleNavyGhostButton(btn);

            // Hover elegante
            btn.FlatAppearance.BorderColor = Color.FromArgb(70, 255, 255, 255);
            btn.MouseEnter += (_, __) => btn.BackColor = Color.FromArgb(28, 255, 255, 255);
            btn.MouseLeave += (_, __) => btn.BackColor = Color.Transparent;

            return btn;
        }

        private Panel BuildHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = ThemeHelpers.Palette.NavyDark,
                Padding = new Padding(18, 12, 18, 12)
            };
            header.SetDoubleBuffered(true);

            _lblHeaderTitle = new Label
            {
                AutoSize = true,
                Text = "Dashboard",
                ForeColor = Color.White,
                Font = ThemeHelpers.Title(18f, FontStyle.Bold),
                Location = new Point(18, 12)
            };

            _lblHeaderSub = new Label
            {
                AutoSize = true,
                Text = "Cargando...",
                ForeColor = Color.FromArgb(220, 255, 255, 255),
                Font = ThemeHelpers.Ui(11.5f),
                Location = new Point(20, 42)
            };

            // Acciones a la derecha (placeholders)
            var btnBell = new Button { Text = "🔔", Width = 46, Height = 38 };
            var btnFav = new Button { Text = "★", Width = 46, Height = 38 };
            var btnProfile = new Button { Text = "👤", Width = 46, Height = 38 };

            ThemeHelpers.StyleNavyGhostButton(btnBell);
            ThemeHelpers.StyleNavyGhostButton(btnFav);
            ThemeHelpers.StyleNavyGhostButton(btnProfile);

            btnBell.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFav.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnProfile.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // Posicionamiento manual (simple)
            header.Controls.Add(_lblHeaderTitle);
            header.Controls.Add(_lblHeaderSub);
            header.Controls.Add(btnProfile);
            header.Controls.Add(btnFav);
            header.Controls.Add(btnBell);

            header.Resize += (_, __) =>
            {
                var right = header.ClientSize.Width - 18;
                btnProfile.Location = new Point(right - btnProfile.Width, 16);
                btnFav.Location = new Point(btnProfile.Left - 10 - btnFav.Width, 16);
                btnBell.Location = new Point(btnFav.Left - 10 - btnBell.Width, 16);
            };

            return header;
        }

        // =========================
        // Views (Tabs)
        // =========================
        private Panel BuildHomeView()
        {
            var view = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelpers.Palette.BeigeApp
            };
            view.SetDoubleBuffered(true);

            // Título bienvenida
            var lblWelcome = new Label
            {
                Name = "lblWelcome",
                AutoSize = true,
                Text = "Bienvenido",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Title(26f, FontStyle.Bold),
                Location = new Point(8, 8)
            };

            var lblHint = new Label
            {
                Name = "lblHint",
                AutoSize = true,
                Text = "Resumen general de tu restaurante hoy.",
                ForeColor = ThemeHelpers.Palette.TextMuted,
                Font = ThemeHelpers.Ui(12.5f),
                Location = new Point(10, 48)
            };

            // Stats cards (placeholders)
            var cardsHost = new FlowLayoutPanel
            {
                Name = "cardsHost",
                Location = new Point(8, 92),
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = ThemeHelpers.Palette.BeigeApp,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(view.ClientSize.Width - 16, 150),
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            cardsHost.SetDoubleBuffered(true);

            view.Resize += (_, __) =>
            {
                cardsHost.Width = view.ClientSize.Width - 16;
            };

            var c1 = ThemeHelpers.CreateKpiCard("Reservas (hoy)", "—");
            var c2 = ThemeHelpers.CreateKpiCard("Órdenes activas", "—");
            var c3 = ThemeHelpers.CreateKpiCard("Capacidad", "—");

            // Hover sutil
            AddCardHover(c1);
            AddCardHover(c2);
            AddCardHover(c3);

            cardsHost.Controls.Add(c1);
            cardsHost.Controls.Add(c2);
            cardsHost.Controls.Add(c3);

            // Acciones rápidas
            var actionsCard = new ShadowCardPanel
            {
                BackColor = ThemeHelpers.Palette.Card,
                Radius = 18,
                BorderColor = ThemeHelpers.Palette.Border,
                ShadowColor = ThemeHelpers.Palette.Shadow,
                ShadowOffset = 10,
                ShadowBlur = 22,
                Location = new Point(8, 260),
                Size = new Size(820, 160),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblActions = new Label
            {
                AutoSize = true,
                Text = "Acciones rápidas",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Title(16f, FontStyle.Bold),
                Location = new Point(18, 18)
            };

            var btnNuevaOrden = new Button { Text = "Nueva orden", Size = new Size(170, 42), Location = new Point(18, 64) };
            var btnNuevaReserva = new Button { Text = "Nueva reserva", Size = new Size(170, 42), Location = new Point(198, 64) };
            var btnVerMenu = new Button { Text = "Ver menú", Size = new Size(170, 42), Location = new Point(378, 64) };

            ThemeHelpers.StyleGoldButton(btnNuevaOrden);
            ThemeHelpers.StyleBeigeGhostButton(btnNuevaReserva);
            ThemeHelpers.StyleBeigeGhostButton(btnVerMenu);

            actionsCard.Controls.Add(lblActions);
            actionsCard.Controls.Add(btnNuevaOrden);
            actionsCard.Controls.Add(btnNuevaReserva);
            actionsCard.Controls.Add(btnVerMenu);

            // Ajustar ancho en resize
            view.Resize += (_, __) =>
            {
                actionsCard.Width = view.ClientSize.Width - 16;
            };

            view.Controls.Add(lblWelcome);
            view.Controls.Add(lblHint);
            view.Controls.Add(cardsHost);
            view.Controls.Add(actionsCard);

            return view;
        }

        private Panel BuildPlaceholderView(string title, string description)
        {
            var view = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeHelpers.Palette.BeigeApp
            };
            view.SetDoubleBuffered(true);

            var card = new ShadowCardPanel
            {
                BackColor = ThemeHelpers.Palette.Card,
                Radius = 18,
                BorderColor = ThemeHelpers.Palette.Border,
                ShadowColor = ThemeHelpers.Palette.Shadow,
                ShadowOffset = 10,
                ShadowBlur = 22,
                Dock = DockStyle.Fill
            };

            var lblT = new Label
            {
                AutoSize = true,
                Text = title,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Title(22f, FontStyle.Bold),
                Location = new Point(22, 20)
            };

            var lblD = new Label
            {
                AutoSize = false,
                Text = description,
                ForeColor = ThemeHelpers.Palette.TextMuted,
                Font = ThemeHelpers.Ui(12.5f),
                Location = new Point(24, 62),
                Size = new Size(700, 60)
            };

            card.Controls.Add(lblT);
            card.Controls.Add(lblD);
            view.Controls.Add(card);

            return view;
        }
        private void BuildMenuViewUi(Panel container)
        {
            container.SuspendLayout();
            container.Controls.Clear();

            // Panel superior: Gestión de menús
            var topPanel = new ShadowCardPanel
            {
                BackColor = ThemeHelpers.Palette.Card,
                Radius = 18,
                BorderColor = ThemeHelpers.Palette.Border,
                ShadowColor = ThemeHelpers.Palette.Shadow,
                ShadowOffset = 10,
                ShadowBlur = 22,
                Location = new Point(18, 18),
                Size = new Size(container.ClientSize.Width - 36, 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            topPanel.SetDoubleBuffered(true);

            // Título
            var lblMenuTitle = new Label
            {
                AutoSize = true,
                Text = "Crear nuevo menú",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Title(14f, FontStyle.Bold),
                Location = new Point(18, 14)
            };

            // TextBox nuevo menú
            _txtNuevoMenu = new TextBox
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
            _txtNuevoMenu.SetDoubleBuffered(true);

            // Botón crear menú
            _btnCrearMenu = new Button
            {
                Text = "Crear menú",
                Location = new Point(312, 48),
                Size = new Size(140, 36),
                ForeColor = Color.White,
                Font = ThemeHelpers.Ui(11f)
            };
            ThemeHelpers.StyleGoldButton(_btnCrearMenu);
            _btnCrearMenu.Click += async (_, __) => await CreateMenuAsync();

            // Botón refrescar
            _btnRefrescarMenu = new Button
            {
                Text = "🔄 Refrescar",
                Location = new Point(468, 48),
                Size = new Size(120, 36),
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f)
            };
            ThemeHelpers.StyleBeigeGhostButton(_btnRefrescarMenu);
            _btnRefrescarMenu.Click += async (_, __) => await LoadMenusAsync();

            topPanel.Controls.Add(lblMenuTitle);
            topPanel.Controls.Add(_txtNuevoMenu);
            topPanel.Controls.Add(_btnCrearMenu);
            topPanel.Controls.Add(_btnRefrescarMenu);

            // ComboBox seleccionar menú
            var lblSelectMenu = new Label
            {
                AutoSize = true,
                Text = "Selecciona un menú:",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Location = new Point(18, 178)
            };

            _cmbMenus = new ComboBox
            {
                Location = new Point(18, 204),
                Size = new Size(360, 32),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = ThemeHelpers.Palette.Card,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Padding = new Padding(6)
            };
            _cmbMenus.SelectedIndexChanged += async (_, __) => await LoadPlatosAsync();

            // Panel central: Gestión de platos
            var middlePanel = new ShadowCardPanel
            {
                BackColor = ThemeHelpers.Palette.Card,
                Radius = 18,
                BorderColor = ThemeHelpers.Palette.Border,
                ShadowColor = ThemeHelpers.Palette.Shadow,
                ShadowOffset = 10,
                ShadowBlur = 22,
                Location = new Point(18, 244),
                Size = new Size(container.ClientSize.Width - 36, 130),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            middlePanel.SetDoubleBuffered(true);

            var lblPlatoTitle = new Label
            {
                AutoSize = true,
                Text = "Agregar nuevo plato",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Title(14f, FontStyle.Bold),
                Location = new Point(18, 14)
            };

            _txtPlatoNombre = new TextBox
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
            _txtPlatoNombre.SetDoubleBuffered(true);

            _numPlatoPrecio = new NumericUpDown
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

            _chkDisponible = new CheckBox
            {
                Text = "Disponible",
                Location = new Point(388, 58),
                Size = new Size(140, 20),
                Checked = true,
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f)
            };

            _btnAgregarPlato = new Button
            {
                Text = "Agregar plato",
                Location = new Point(538, 50),
                Size = new Size(140, 36),
                ForeColor = Color.White,
                Font = ThemeHelpers.Ui(11f)
            };
            ThemeHelpers.StyleGoldButton(_btnAgregarPlato);
            _btnAgregarPlato.Click += async (_, __) => await CreatePlatoAsync();

            middlePanel.Controls.Add(lblPlatoTitle);
            middlePanel.Controls.Add(_txtPlatoNombre);
            middlePanel.Controls.Add(_numPlatoPrecio);
            middlePanel.Controls.Add(_chkDisponible);
            middlePanel.Controls.Add(_btnAgregarPlato);

            // DataGridView platos
            var lblGridTitle = new Label
            {
                AutoSize = true,
                Text = "Platos disponibles:",
                ForeColor = ThemeHelpers.Palette.TextMain,
                Font = ThemeHelpers.Ui(11f),
                Location = new Point(18, 388)
            };

            _gridPlatos = new DataGridView
            {
                Location = new Point(18, 414),
                Size = new Size(container.ClientSize.Width - 36, 200),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoGenerateColumns = true,
                BackgroundColor = ThemeHelpers.Palette.Card,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                EnableHeadersVisualStyles = false
            };
            _gridPlatos.ColumnHeadersDefaultCellStyle.BackColor = ThemeHelpers.Palette.Navy;
            _gridPlatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _gridPlatos.ColumnHeadersDefaultCellStyle.Font = ThemeHelpers.Ui(10f, FontStyle.Bold);
            _gridPlatos.DefaultCellStyle.BackColor = ThemeHelpers.Palette.CardWarm;
            _gridPlatos.DefaultCellStyle.ForeColor = ThemeHelpers.Palette.TextMain;
            _gridPlatos.DefaultCellStyle.Font = ThemeHelpers.Ui(10f);
            _gridPlatos.DefaultCellStyle.SelectionBackColor = ThemeHelpers.Palette.Gold;
            _gridPlatos.DefaultCellStyle.SelectionForeColor = Color.White;

            // Ajustar ancho en resize
            container.Resize += (_, __) =>
            {
                topPanel.Width = container.ClientSize.Width - 36;
                middlePanel.Width = container.ClientSize.Width - 36;
                _gridPlatos.Width = container.ClientSize.Width - 36;
            };

            container.Controls.Add(topPanel);
            container.Controls.Add(lblSelectMenu);
            container.Controls.Add(_cmbMenus);
            container.Controls.Add(middlePanel);
            container.Controls.Add(lblGridTitle);
            container.Controls.Add(_gridPlatos);

            container.ResumeLayout(true);
        }
        private void ShowView(Panel viewToShow, string headerTitle, string headerSub)
        {
            foreach (var v in new[] { _viewHome, _viewMenu, _viewOrdenes, _viewReservas, _viewAjustes })
                v.Visible = false;

            viewToShow.Visible = true;
            _lblHeaderTitle.Text = headerTitle;
            _lblHeaderSub.Text = headerSub;
        }

        private void AddCardHover(Control card)
        {
            var original = card.BackColor;
            card.MouseEnter += (_, __) => card.BackColor = ThemeHelpers.Palette.CardWarm;
            card.MouseLeave += (_, __) => card.BackColor = original;

            foreach (Control child in card.Controls)
            {
                child.MouseEnter += (_, __) => card.BackColor = ThemeHelpers.Palette.CardWarm;
                child.MouseLeave += (_, __) => card.BackColor = original;
            }
        }
        #endregion
        // =========================
        // Data loading for Home
        // =========================
        private async Task LoadHomeDataAsync()
        {
            try
            {
                if (!TokenStore.HasToken)
                {
                    _lblHeaderSub.Text = "Sesión no válida. Vuelve a iniciar sesión.";
                    return;
                }

                var token = TokenStore.AccessToken!;
                var usuarioId = TokenHelper.GetUsuarioIdFromToken(token);
                var nombre = TokenHelper.GetNombreFromToken(token);

                // Bienvenida
                var lblWelcome = _viewHome.Controls.Find("lblWelcome", true).FirstOrDefault() as Label;
                if (lblWelcome != null)
                    lblWelcome.Text = $"Bienvenido, {nombre}";

                // Cargar restaurante (si el endpoint está listo)
                if (usuarioId > 0)
                {
                    var result = await _restauranteService.ObtenerMioAsync(usuarioId);
                    if (result.Success && result.Data != null)
                    {
                        _lblHeaderSub.Text = $"Restaurante: {result.Data.Nombre}";
                        UpdateKpis(result.Data.Capacidad);
                    }
                    else
                    {
                        _lblHeaderSub.Text = "No se pudo cargar restaurante (verifica API / endpoint).";
                    }
                }
                else
                {
                    _lblHeaderSub.Text = "No se pudo obtener UsuarioId del token.";
                }
            }
            catch (Exception ex)
            {
                _lblHeaderSub.Text = $"Error cargando datos: {ex.Message}";
            }
        }

        private void UpdateKpis(int capacidad)
        {
            var cardsHost = _viewHome.Controls.Find("cardsHost", true).FirstOrDefault() as FlowLayoutPanel;
            if (cardsHost == null) return;

            // Solo actualiza textos (en esta fase son placeholders)
            // Cards: Reservas, Órdenes, Capacidad
            if (cardsHost.Controls.Count >= 3)
            {
                // Capacidad card (3ra)
                var capCard = cardsHost.Controls[2];
                var valueLabel = capCard.Controls.OfType<Label>().LastOrDefault();
                if (valueLabel != null)
                    valueLabel.Text = capacidad.ToString();
            }
        }

        private async Task LoadMenusAsync()
        {
            await EnsureRestauranteIdAsync();

            _cmbMenus.Enabled = false;
            _btnRefrescarMenu.Enabled = false;

            try
            {
                var result = await _menuService.ObtenerPorRestauranteAsync(_restauranteId);

                if (!result.Success)
                {
                    MessageHelper.Error(result.Message);
                    _cmbMenus.DataSource = null;
                    _gridPlatos.DataSource = null;
                    return;
                }

                var menus = result.Data ?? new List<MenuDTO>();

                _cmbMenus.DataSource = menus;
                _cmbMenus.DisplayMember = "Nombre";
                _cmbMenus.ValueMember = "Id";

                // Limpiar textbox del menú después de refrescar
                _txtNuevoMenu.Clear();

                if (menus.Count > 0)
                    await LoadPlatosAsync();
                else
                    _gridPlatos.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error cargando menús: {ex.Message}");
            }
            finally
            {
                _cmbMenus.Enabled = true;
                _btnRefrescarMenu.Enabled = true;
            }
        }

        private async Task LoadPlatosAsync()
        {
            if (_cmbMenus.SelectedItem is not MenuDTO m) return;

            try
            {
                var result = await _platoService.ObtenerPorMenuAsync(m.Id);

                if (!result.Success)
                {
                    MessageHelper.Error(result.Message);
                    _gridPlatos.DataSource = null;
                    return;
                }

                _gridPlatos.DataSource = result.Data ?? new List<PlatoDTO>();
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error cargando platos: {ex.Message}");
            }
        }
        private async Task CreateMenuAsync()
        {
            await EnsureRestauranteIdAsync();

            var nombre = _txtNuevoMenu.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageHelper.Error("Escribe un nombre de menú.");
                return;
            }

            _btnCrearMenu.Enabled = false;

            try
            {
                var result = await _menuService.CrearAsync(new CreateMenuDTO
                {
                    RestauranteId = _restauranteId,
                    Nombre = nombre
                });

                if (!result.Success)
                {
                    MessageHelper.Error(result.Message);
                    return;
                }

                MessageHelper.Info("Menú creado correctamente.");
                _txtNuevoMenu.Clear();
                await LoadMenusAsync();
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"No se pudo crear el menú: {ex.Message}");
            }
            finally
            {
                _btnCrearMenu.Enabled = true;
            }
        }
        private async Task CreatePlatoAsync()
        {
            if (_cmbMenus.SelectedItem is not MenuDTO m)
            {
                MessageHelper.Error("Selecciona un menú primero.");
                return;
            }

            var nombre = _txtPlatoNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageHelper.Error("Escribe el nombre del plato.");
                return;
            }

            _btnAgregarPlato.Enabled = false;

            try
            {
                
                var menuId = m.Id;  

                var result = await _platoService.CrearAsync(new CreatePlatoDTO
                {
                    MenuId = menuId,
                    Nombre = nombre,
                    Precio = _numPlatoPrecio.Value,
                });

                if (!result.Success)
                {
                    MessageHelper.Error(result.Message);
                    return;
                }

                MessageHelper.Info("Plato agregado correctamente.");
                _txtPlatoNombre.Clear();
                _numPlatoPrecio.Value = 0;
                _chkDisponible.Checked = true;

                await LoadPlatosAsync();
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"No se pudo agregar el plato: {ex.Message}");
            }
            finally
            {
                _btnAgregarPlato.Enabled = true;
            }
        }
        private async Task EnsureRestauranteIdAsync()
        {
            if (_restauranteId > 0) return;

            if (!TokenStore.HasToken)
                throw new InvalidOperationException("Sesión no válida (sin token).");

            var token = TokenStore.AccessToken!;
            var usuarioId = TokenHelper.GetUsuarioIdFromToken(token);

            if (usuarioId <= 0)
                throw new InvalidOperationException("No se pudo obtener UsuarioId del token.");

            var res = await _restauranteService.ObtenerMioAsync(usuarioId);

            if (!res.Success || res.Data == null)
                throw new InvalidOperationException(res.Message);

            // Ajusta este nombre si tu RestauranteDTO usa otro:
            _restauranteId = res.Data.Id;
        }
        
        private int GetMenuId(MenuDTO m)
        {
            // Cambia esta línea al nombre real del ID en tu DTO:
            // return m.IdMenu;
            return (int)m.GetType().GetProperty("IdMenu")?.GetValue(m)!;
        }
    }
}