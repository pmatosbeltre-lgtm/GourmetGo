using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;
using GourmetGo.Application.DTOs.Catalogo.Plato;
using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Desktop.Helpers;
using GourmetGo.Desktop.Services;
using GourmetGo.Desktop.ViewsBuilders;
using GourmetGo.Domain.Enums;

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

        private OrdenViewBuilder _ordenViewBuilder;
        private OrdenService _ordenService;
        private bool _ordenUiBuilt = false;

        private ReservaViewBuilder _reservaViewBuilder = null!;
        private bool _reservaUiBuilt = false;

        private readonly ReservaService _reservaService; 
        private bool _menuUiBuilt = false;

        private int _restauranteId;

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
            _ordenService = new OrdenService(api);
            _reservaService = new ReservaService(api); 


            // Estilo base del form
            ThemeHelpers.ApplyFormBase(this);
            this.Text = "GourmetGo";
            this.MinimumSize = new Size(1100, 680);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Construir UI
            BuildShell();

            // Cargar datos después de mostrar
            this.Shown += async (_, __) => await LoadHomeDataAsync();

            //Pantalla completa
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;



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
            btnOrdenes.Click += async (_, __) =>
            {
                ShowView(_viewOrdenes, "📋 Órdenes", "Seguimiento y estados");
                if (!_ordenUiBuilt)
                {
                    BuildOrdenViewUi(_viewOrdenes);
                    _ordenUiBuilt = true;
                }
                await LoadOrdenesAsync();
            };

            var btnReservas = MakeNavButton("🗓️  Reservas");
            btnReservas.Click += async (_, __) =>
            {
                ShowView(_viewReservas, "🗓️ Reservas", "Agenda de reservas");
                if (!_reservaUiBuilt)
                {
                    BuildReservaViewUi(_viewReservas); 
                    _reservaUiBuilt = true;
                }
                await LoadReservasAsync();
            };

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
        private MenuViewBuilder _menuViewBuilder = null!;
        //private DataGridView _gridMenus = null!;

        private void BuildMenuViewUi(Panel container)
        {
            _menuViewBuilder = new MenuViewBuilder(
                container,
                async (menuId) => await _menuViewBuilder.MostrarDialogoEditarMenuAsync(_menuService, async () => await LoadMenusAsync()),
                async (menuId, nombre) => await _menuViewBuilder.EliminarMenuAsync(_menuService, menuId, nombre, async () => await LoadMenusAsync()),
                async () => await LoadMenusAsync(),
                async () => await CreateMenuAsync(),
                async () => await CreatePlatoAsync(),
                async () => await LoadPlatosAsync()
            );

            _menuViewBuilder.Build();

            _menuViewBuilder.CmbMenus.SelectedIndexChanged += async (s, e) => {
                await LoadPlatosAsync();
            };

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
                var nombre = TokenHelper.GetNombreFromToken(token);
                var usuarioId = TokenHelper.GetUsuarioIdFromToken(token);

                // 1. Bienvenida
                var lblWelcome = _viewHome.Controls.Find("lblWelcome", true).FirstOrDefault() as Label;
                if (lblWelcome != null) lblWelcome.Text = $"Bienvenido, {nombre}";

                // 2. Cargar Restaurante y KPIs
                if (usuarioId > 0)
                {
                    var result = await _restauranteService.ObtenerMioAsync(usuarioId);
                    if (result.Success && result.Data != null)
                    {
                        _restauranteId = result.Data.Id;
                        _lblHeaderSub.Text = $"Restaurante: {result.Data.Nombre}";

                        // Actualizamos Capacidad
                        UpdateKpis(result.Data.Capacidad);

                        // --- NUEVO: Cargar contadores de Órdenes y Reservas ---
                        await RefreshDashboardStatsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _lblHeaderSub.Text = $"Error cargando datos: {ex.Message}";
            }
        }

        private async Task RefreshDashboardStatsAsync()
        {
            try
            {
                // 1. Buscamos las Cards dentro del cardsHost
                var cardsHost = _viewHome.Controls.Find("cardsHost", true).FirstOrDefault() as FlowLayoutPanel;
                if (cardsHost == null || cardsHost.Controls.Count < 2) return;

                // 2. Obtener datos de Órdenes 
                var resultOrd = await _ordenService.ObtenerOrdenesPorRestauranteAsync(_restauranteId);

                if (resultOrd.Success && resultOrd.Data != null)
                {
                    var activas = resultOrd.Data.Count(o => o.Estado != EstadoOrden.Cancelada);

                    // La Card de Órdenes es la segunda (índice 1)
                    var ordCard = cardsHost.Controls[1];
                    var lblValue = ordCard.Controls.OfType<Label>().LastOrDefault();
                    if (lblValue != null) lblValue.Text = activas.ToString();
                }

                // 3. Reservas (Placeholder por ahora)
                var resCard = cardsHost.Controls[0];
                var lblResValue = resCard.Controls.OfType<Label>().LastOrDefault();
                if (lblResValue != null) lblResValue.Text = "0";

                // 1. Declaramos las variables locales para que el método las reconozca
                int ordenesActivas = 1;
                int reservasHoy = 0; // O el valor que calcules para reservas

                // 2. Ahora sí las pasamos al método
                UpdateDashboardUi(reservasHoy, ordenesActivas);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error actualizando Dashboard: {ex.Message}");
            }
        }

        private void UpdateKpis(int capacidad)
        {
            var cardsHost = _viewHome.Controls.Find("cardsHost", true).FirstOrDefault() as FlowLayoutPanel;
            if (cardsHost == null || cardsHost.Controls.Count < 3) return;

            // Capacidad card es la tercera (índice 2)
            var capCard = cardsHost.Controls[2];
            var valueLabel = capCard.Controls.OfType<Label>().LastOrDefault();
            if (valueLabel != null)
                valueLabel.Text = capacidad.ToString();
        }
        private async Task LoadMenusAsync()
        {
            await EnsureRestauranteIdAsync();

            _menuViewBuilder.CmbMenus.Enabled = false;
            _menuViewBuilder.BtnRefrescarMenu.Enabled = false;

            try
            {
                var result = await _menuService.ObtenerPorRestauranteAsync(_restauranteId);

                if (!result.Success)
                {
                    MessageHelper.Error(result.Message);
                    _menuViewBuilder.CmbMenus.DataSource = null;
                    _menuViewBuilder.GridMenus.DataSource = null;
                    _menuViewBuilder.GridPlatos.DataSource = null;
                    return;
                }

                var menus = result.Data ?? new List<MenuDTO>();

                _menuViewBuilder.GridMenus.DataSource = menus;
                _menuViewBuilder.CmbMenus.DataSource = menus; 
                _menuViewBuilder.CmbMenus.DisplayMember = "Nombre";
                _menuViewBuilder.CmbMenus.ValueMember = "Id";

                _menuViewBuilder.TxtNuevoMenu.Clear();

                if (menus.Count > 0)
                {
                    _menuViewBuilder.CmbMenus.SelectedIndex = 0;  
                    await LoadPlatosAsync();  
                }
                else
                {
                    _menuViewBuilder.GridPlatos.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error cargando menús: {ex.Message}");
            }
            finally
            {
                _menuViewBuilder.CmbMenus.Enabled = true;
                _menuViewBuilder.BtnRefrescarMenu.Enabled = true;
            }
        }

        private async Task LoadPlatosAsync()
        {
            if (_menuViewBuilder?.CmbMenus == null) return;

            var selectedMenu = _menuViewBuilder.CmbMenus.SelectedItem as MenuDTO;

            // Si no hay nada seleccionado, limpiamos el grid y salimos
            if (selectedMenu == null)
            {
                _menuViewBuilder.GridPlatos.DataSource = null;
                return;
            }

            try
            {
                var result = await _platoService.ObtenerPorMenuAsync(selectedMenu.Id);

                if (result.Success && result.Data != null)
                {

                    _menuViewBuilder.GridPlatos.DataSource = null;
                    _menuViewBuilder.GridPlatos.DataSource = result.Data.ToList();

                    _menuViewBuilder.GridPlatos.Refresh();
                }
                else
                {
                    _menuViewBuilder.GridPlatos.DataSource = null;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error cargando platos: {ex.Message}");
            }
        }
        private async Task CreateMenuAsync()
        {
            await EnsureRestauranteIdAsync();

            var nombre = _menuViewBuilder.TxtNuevoMenu.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageHelper.Error("Escribe un nombre de menú.");
                return;
            }

            _menuViewBuilder.BtnCrearMenu.Enabled = false;

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
                _menuViewBuilder.TxtNuevoMenu.Clear();
                await LoadMenusAsync(); // Recarga la lista para que aparezca el nuevo
            }
            catch (System.Text.Json.JsonException)
            {

                MessageHelper.Info("Menú gestionado correctamente.");
                await LoadMenusAsync();
            }
            catch (Exception ex)
            {

                MessageHelper.Error($"Error: {ex.Message}");
            }
            finally
            {
                _menuViewBuilder.BtnCrearMenu.Enabled = true;
            }
        }
        private async Task CreatePlatoAsync()
        {

            if (_menuViewBuilder.CmbMenus.SelectedItem is not MenuDTO m)
            {
                MessageHelper.Error("Selecciona un menú primero.");
                return;
            }

            var nombre = _menuViewBuilder.TxtPlatoNombre.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageHelper.Error("Escribe el nombre del plato.");
                return;
            }

            _menuViewBuilder.BtnAgregarPlato.Enabled = false;

            try
            {
                var result = await _platoService.CrearAsync(new CreatePlatoDTO
                {
                    MenuId = m.Id,
                    Nombre = nombre,
                    Precio = _menuViewBuilder.NumPlatoPrecio.Value
                });

                if (!result.Success)
                {
                    MessageHelper.Error(result.Message);
                    return;
                }

                MessageHelper.Info("Plato agregado correctamente.");

                // Limpiamos los controles del builder
                _menuViewBuilder.TxtPlatoNombre.Clear();
                _menuViewBuilder.NumPlatoPrecio.Value = 0;

                // Recargamos la lista de platos
                await LoadPlatosAsync();
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error al crear plato: {ex.Message}");
            }
            finally
            {
                _menuViewBuilder.BtnAgregarPlato.Enabled = true;
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

        private void BuildOrdenViewUi(Panel container)
        {
            _ordenViewBuilder = new OrdenViewBuilder(
                container,
                async () => await LoadOrdenesAsync(),
                async (ordenId, nuevoEstado) => await CambiarEstadoOrdenAsync(ordenId, nuevoEstado)
            );
            _ordenViewBuilder.Build();
        }

        private async Task LoadOrdenesAsync()
        {
            try
            {
                await EnsureRestauranteIdAsync();
                var result = await _ordenService.ObtenerOrdenesPorRestauranteAsync(_restauranteId);
                var ordenes = result.Data ?? new List<OrdenDTO>();

                if (_ordenViewBuilder?.CmbFiltroEstado?.SelectedItem != null)

                {
                    var estado = _ordenViewBuilder.CmbFiltroEstado.SelectedItem.ToString();
                    if (estado != "Todos" && Enum.TryParse(typeof(EstadoOrden), estado, out var e))
                        ordenes = ordenes.Where(o => o.Estado == (EstadoOrden)e).ToList();
                }
                _ordenViewBuilder.GridOrdenes.DataSource = ordenes;
                _ordenViewBuilder.LblTotalOrdenes.Text = $"Total: {ordenes.Count}";
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error: {ex.Message}");
            }
        }
        private async Task CambiarEstadoOrdenAsync(int ordenId, EstadoOrden nuevoEstado)
        {
            try
            {
                var result = await _ordenService.ActualizarEstadoAsync(ordenId, new UpdateOrdenDTO { NuevoEstado = nuevoEstado });
                if (result.Success)
                {
                    MessageHelper.Info(result.Message);
                    await LoadOrdenesAsync();
                }
                else
                    MessageHelper.Error(result.Message);
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error: {ex.Message}");
            }
        }
        private void UpdateDashboardUi(int reservas, int ordenes)
        {
            //Asegúrate de usar los nombres correctos de tus Labels
            lblReservasHoy.Text = reservas.ToString();
            lblOrdenesActivas.Text = ordenes.ToString();

            // Si quieres darle un toque extra, cambia el color si hay muchas órdenes
            lblOrdenesActivas.ForeColor = ordenes > 5 ? Color.Red : ThemeHelpers.Palette.BeigeApp;

            // Si quieres ver algo, usa el que sí encontraste:
            if (_lblHeaderTitle != null)
            {
                _lblHeaderTitle.Text = $"GourmetGo - {ordenes} órdenes hoy";
            }
        }

        private DataGridView _dgvReservas = null!;

        private void BuildReservaViewUi(Panel container)
        {
            _reservaViewBuilder = new ReservaViewBuilder(
                container,
                async () => await LoadReservasAsync()
            );
            _reservaViewBuilder.Build();
        }
        private async Task LoadReservasAsync()
        {
            try
            {
                await EnsureRestauranteIdAsync();
                var result = await _reservaService.ObtenerPorRestauranteAsync(_restauranteId);
                var reservas = result.Data ?? new List<ReservaDTO>();

                _reservaViewBuilder.GridReservas.DataSource = null;
                _reservaViewBuilder.GridReservas.DataSource = reservas;
            }
            catch (Exception ex)
            {
                MessageHelper.Error($"Error cargando reservas: {ex.Message}");
            }
        }
    }
}