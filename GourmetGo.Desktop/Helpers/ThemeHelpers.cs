using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace GourmetGo.Desktop.Helpers
{
    public static class ThemeHelpers
    {
        // =========================================================
        //  Palette (OBLIGATORIA)
        // =========================================================
        public static class Palette
        {
            public static readonly Color Navy = ColorTranslator.FromHtml("#1E2E4A");
            public static readonly Color NavyDark = ColorTranslator.FromHtml("#16233A");
            public static readonly Color Gold = ColorTranslator.FromHtml("#C9A35B");
            public static readonly Color GoldDark = ColorTranslator.FromHtml("#B08B45");
            public static readonly Color BeigeApp = ColorTranslator.FromHtml("#F3EADF");
            public static readonly Color Card = ColorTranslator.FromHtml("#FFFFFF");
            public static readonly Color CardWarm = ColorTranslator.FromHtml("#FAF4EA");
            public static readonly Color Border = ColorTranslator.FromHtml("#E6D7C3");
            public static readonly Color TextMain = ColorTranslator.FromHtml("#1B1B1B");
            public static readonly Color TextMuted = ColorTranslator.FromHtml("#6E6255");

            // Sombra: la simulamos con un panel que dibuja borde+shadow suave
            public static readonly Color Shadow = Color.FromArgb(18, 0, 0, 0); // ~0.07
        }

        // =========================================================
        //  Fonts
        // =========================================================
        public static Font Ui(float size = 12.5f, FontStyle style = FontStyle.Regular)
            => new Font("Segoe UI", size, style);

        public static Font Title(float size = 22f, FontStyle style = FontStyle.Bold)
        {
            // "Fraunces" si estuviera instalado; fallback Georgia
            var fam = FontFamily.Families.FirstOrDefault(f => string.Equals(f.Name, "Fraunces", StringComparison.OrdinalIgnoreCase));
            return fam != null ? new Font(fam, size, style) : new Font("Georgia", size, style);
        }

        // =========================================================
        //  Public entry points
        // =========================================================
        public static void ApplyFormBase(Form form)
        {
            form.BackColor = Palette.BeigeApp;
            form.Font = Ui(12.5f);
            form.ForeColor = Palette.TextMain;

            form.SetDoubleBuffered(true);

            // Suaviza repaints en contenedores comunes
            foreach (var c in form.Controls.OfType<Control>())
                c.SetDoubleBuffered(true);
        }

        /// <summary>
        /// Aplica un layout split premium (navy izquierda + card derecha) sin romper tus controles.
        /// NO renombra txtCorreo/txtContrasena/btnLogin, solo los reubica/estiliza.
        /// </summary>
       
           public static void ApplyFancyLogin(Form form, Control txtCorreo, Control txtContrasena, Button btnLogin)
           {
            ApplyFormBase(form);

            form.SuspendLayout();

            form.BackColor = Palette.BeigeApp;
            form.MinimumSize = new Size(980, 560);

            // Evitar aplicar 2 veces
            if (form.Controls.Find("panelLeftNavy", true).Length > 0)
            {
                form.ResumeLayout(true);
                return;
            }

            var panelLeftNavy = new Panel
            {
                Name = "panelLeftNavy",
                Dock = DockStyle.Left,
                Width = 420,
                BackColor = Palette.Navy
            };
            panelLeftNavy.SetDoubleBuffered(true);

            var panelRightBeige = new Panel
            {
                Name = "panelRightBeige",
                Dock = DockStyle.Fill,
                BackColor = Palette.BeigeApp,
                Padding = new Padding(36)
            };
            panelRightBeige.SetDoubleBuffered(true);

            var loginLblBrand = new Label
            {
                AutoSize = true,
                Text = "GourmetGo",
                ForeColor = Color.White,
                Font = Title(34f, FontStyle.Bold),
                Location = new Point(34, 70)
            };

            var loginLblTag = new Label
            {
                AutoSize = false,
                Text = "\nAccede con tu cuenta de\npropietario.",
                ForeColor = Color.FromArgb(220, 255, 255, 255),
                Font = Ui(12.5f),
                Location = new Point(36, 150),
                Size = new Size(340, 120)
            };

            panelLeftNavy.Controls.Add(loginLblBrand);
            panelLeftNavy.Controls.Add(loginLblTag);

            var loginCenterHost = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Palette.BeigeApp
            };

            var loginCard = new ShadowCardPanel
            {
                Name = "cardLogin",
                BackColor = Palette.Card,
                Radius = 18,
                BorderColor = Palette.Border,
                ShadowColor = Palette.Shadow,
                ShadowOffset = 10,
                ShadowBlur = 22,
                Size = new Size(460, 360),
                Anchor = AnchorStyles.None
            };

            var loginLblTitle = new Label
            {
                AutoSize = true,
                Text = "Iniciar sesión",
                Font = Title(22f, FontStyle.Bold),
                ForeColor = Palette.TextMain,
                Location = new Point(30, 28)
            };

            var loginLblSub = new Label
            {
                AutoSize = true,
                Text = "Bienvenido de vuelta.",
                Font = Ui(11.5f),
                ForeColor = Palette.TextMuted,
                Location = new Point(32, 70)
            };

            var loginCorreoHost = CreateRoundedInputHost(txtCorreo, "Correo");
            var loginPassHost = CreateRoundedInputHost(txtContrasena, "Contraseña");

            loginCorreoHost.Location = new Point(30, 110);
            loginPassHost.Location = new Point(30, 190);

            btnLogin.Text = (btnLogin.Text == "button1" || string.IsNullOrWhiteSpace(btnLogin.Text)) ? "Login" : btnLogin.Text;
            btnLogin.Size = new Size(400, 48);
            btnLogin.Location = new Point(30, 275);
            StyleGoldButton(btnLogin);

            StyleTextInput(txtCorreo);
            StyleTextInput(txtContrasena);
            if (txtContrasena is TextBox loginTbPass) loginTbPass.UseSystemPasswordChar = true;

            // Meter contenido al card
            loginCard.Controls.Add(loginLblTitle);
            loginCard.Controls.Add(loginLblSub);
            loginCard.Controls.Add(loginCorreoHost);
            loginCard.Controls.Add(loginPassHost);

            // Mover botón al card (no pierde el click)
            MoveToContainer(btnLogin, loginCard);

            loginCenterHost.Resize += (_, __) => CenterControl(loginCard, loginCenterHost);
            loginCenterHost.Controls.Add(loginCard);

            panelRightBeige.Controls.Add(loginCenterHost);

            form.Controls.Add(panelRightBeige);
            form.Controls.Add(panelLeftNavy);

            CenterControl(loginCard, loginCenterHost);

            form.ResumeLayout(true);
        
           }

        /// <summary>
        /// Aplica look base + header si tienes un panelHeader. Si no, crea uno arriba.
        /// </summary>
        public static Panel EnsureAndStyleHeader(Form form, Panel? panelHeader = null)
        {
            ApplyFormBase(form);

            Panel header = panelHeader ?? new Panel
            {
                Name = "panelHeader",
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = Palette.Navy
            };

            header.SetDoubleBuffered(true);

            if (panelHeader == null)
                form.Controls.Add(header);

            StyleHeader(header);
            return header;
        }

        public static void StyleHeader(Panel header)
        {
            header.BackColor = Palette.Navy;

            // Si ya tiene un label/title, no duplicar
            var existing = header.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblHeaderBrand");
            if (existing != null) return;

            var lbl = new Label
            {
                Name = "lblHeaderBrand",
                AutoSize = true,
                Text = "GourmetGo",
                ForeColor = Color.White,
                Font = Title(20f, FontStyle.Bold),
                Location = new Point(18, 18)
            };

            header.Controls.Add(lbl);
        }

        public static ShadowCardPanel CreateKpiCard(string title, string value)
        {
            var card = new ShadowCardPanel
            {
                BackColor = Palette.Card,
                Radius = 18,
                BorderColor = Palette.Border,
                ShadowColor = Palette.Shadow,
                ShadowOffset = 8,
                ShadowBlur = 18,
                Size = new Size(260, 120)
            };

            var lblT = new Label
            {
                AutoSize = true,
                Text = title,
                ForeColor = Palette.TextMuted,
                Font = Ui(11.5f, FontStyle.Regular),
                Location = new Point(18, 18)
            };

            var lblV = new Label
            {
                AutoSize = true,
                Text = value,
                ForeColor = Palette.TextMain,
                Font = Ui(22f, FontStyle.Bold),
                Location = new Point(18, 48)
            };

            card.Controls.Add(lblT);
            card.Controls.Add(lblV);
            return card;
        }

        // =========================================================
        //  Styles
        // =========================================================
        public static void StyleGoldButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Palette.Gold;
            btn.ForeColor = Palette.NavyDark;
            btn.Font = Ui(12.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = Math.Max(btn.Height, 42);

            RoundControl(btn, 12);

            btn.MouseEnter -= GoldEnter;
            btn.MouseLeave -= GoldLeave;
            btn.MouseEnter += GoldEnter;
            btn.MouseLeave += GoldLeave;

            static void GoldEnter(object? s, EventArgs e)
            {
                if (s is Button b) b.BackColor = Palette.GoldDark;
            }
            static void GoldLeave(object? s, EventArgs e)
            {
                if (s is Button b) b.BackColor = Palette.Gold;
            }
        }

        public static void StyleNavyGhostButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(70, 255, 255, 255);
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Color.White;
            btn.Font = Ui(11.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            RoundControl(btn, 12);
        }

        public static void StyleBeigeGhostButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Palette.Border;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = Palette.Navy;
            btn.Font = Ui(11.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            RoundControl(btn, 12);
        }

        public static void StyleTextInput(Control txt)
        {
            txt.Font = Ui(12.5f, FontStyle.Regular);
            txt.ForeColor = Palette.TextMain;
            txt.BackColor = Color.White;

            if (txt is TextBox tb)
            {
                tb.BorderStyle = BorderStyle.None; // porque el host dibuja el borde
                tb.Margin = new Padding(0);
            }
        }

        // =========================================================
        //  Rounded utilities
        // =========================================================
        public static void RoundControl(Control control, int radius)
        {
            control.Resize += (_, __) =>
            {
                var rect = new Rectangle(0, 0, control.Width, control.Height);
                using var path = RoundedRect(rect, radius);
                control.Region = new Region(path);
            };

            // aplicar una vez
            var rectNow = new Rectangle(0, 0, control.Width, control.Height);
            using var pathNow = RoundedRect(rectNow, radius);
            control.Region = new Region(pathNow);
        }

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int r = Math.Max(1, radius);
            int d = r * 2;

            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(bounds.Left, bounds.Top, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Top, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void CenterControl(Control child, Control parent)
        {
            child.Left = (parent.ClientSize.Width - child.Width) / 2;
            child.Top = (parent.ClientSize.Height - child.Height) / 2;
        }

        private static void MoveToContainer(Control c, Control newParent)
        {
            if (c.Parent != null)
                c.Parent.Controls.Remove(c);

            newParent.Controls.Add(c);
        }

        /// <summary>
        /// Crea un panel redondeado con borde cálido y mete dentro el TextBox existente.
        /// Esto evita el "problema del host" y no requiere rehacer el diseñador.
        /// </summary>
        private static Control CreateRoundedInputHost(Control txt, string placeholderLabel)
        {
            var wrapper = new ShadowCardPanel
            {
                BackColor = Color.White,
                Radius = 14,
                BorderColor = Palette.Border,
                ShadowColor = Color.Transparent, // inputs sin sombra
                ShadowOffset = 0,
                ShadowBlur = 0,
                Size = new Size(360, 58)
            };

            var lbl = new Label
            {
                AutoSize = true,
                Text = placeholderLabel,
                ForeColor = Palette.TextMuted,
                Font = Ui(10.5f, FontStyle.Regular),
                Location = new Point(14, 8)
            };

            txt.Location = new Point(14, 28);
            txt.Width = wrapper.Width - 28;
            txt.Height = 22;
            txt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            wrapper.Controls.Add(lbl);
            wrapper.Controls.Add(txt);

            return wrapper;
        }

        // =========================================================
        //  Double buffering extension
        // =========================================================
        public static void SetDoubleBuffered(this Control control, bool enabled)
        {
            // Control.DoubleBuffered es protected, lo activamos via reflection.
            var prop = control.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            prop?.SetValue(control, enabled, null);
        }
    }

    /// <summary>
    /// Panel tipo "card" con borde redondeado y sombra MUY sutil.
    /// </summary>
    public class ShadowCardPanel : Panel
    {
        public int Radius { get; set; } = 18;
        public Color BorderColor { get; set; } = ThemeHelpers.Palette.Border;

        public Color ShadowColor { get; set; } = ThemeHelpers.Palette.Shadow;
        public int ShadowOffset { get; set; } = 8;
        public int ShadowBlur { get; set; } = 18;

        public ShadowCardPanel()
        {
            DoubleBuffered = true;
            Padding = new Padding(16);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var g = e.Graphics;

            var shadowRect = new Rectangle(ShadowOffset, ShadowOffset, Width - ShadowOffset - 1, Height - ShadowOffset - 1);
            var cardRect = new Rectangle(0, 0, Width - ShadowOffset - 1, Height - ShadowOffset - 1);

            // sombra sutil (simulada con fill semitransparente)
            if (ShadowColor.A > 0)
            {
                using var shadowPath = ThemeHelpers.RoundedRect(shadowRect, Radius);
                using var shadowBrush = new SolidBrush(ShadowColor);
                g.FillPath(shadowBrush, shadowPath);
            }

            // card
            using (var path = ThemeHelpers.RoundedRect(cardRect, Radius))
            using (var brush = new SolidBrush(BackColor))
            using (var pen = new Pen(BorderColor, 1))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            // recorta el panel (region) para que los controles internos no se salgan visualmente
            var rect = new Rectangle(0, 0, Width - ShadowOffset, Height - ShadowOffset);
            using var path = ThemeHelpers.RoundedRect(rect, Radius);
            Region = new Region(path);
        }
    }
}