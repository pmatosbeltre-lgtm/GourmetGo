namespace GourmetGo.Desktop.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TapControl = new TabControl();
            tabMain = new TabPage();
            lblTitulo = new Label();
            tabMenus = new TabPage();
            tabPlatos = new TabPage();
            tabReservas = new TabPage();
            tabOrdenes = new TabPage();
            tabPagos = new TabPage();
            tabResenas = new TabPage();
            lblRestauranteNombre = new Label();
            lblUsuario = new Label();
            lblRol = new Label();
            lblRestauranteDireccion = new Label();
            lblCapacidad = new Label();
            TapControl.SuspendLayout();
            tabMain.SuspendLayout();
            SuspendLayout();
            // 
            // TapControl
            // 
            TapControl.Controls.Add(tabMain);
            TapControl.Controls.Add(tabMenus);
            TapControl.Controls.Add(tabPlatos);
            TapControl.Controls.Add(tabReservas);
            TapControl.Controls.Add(tabOrdenes);
            TapControl.Controls.Add(tabPagos);
            TapControl.Controls.Add(tabResenas);
            TapControl.Dock = DockStyle.Fill;
            TapControl.Location = new Point(0, 0);
            TapControl.Name = "TapControl";
            TapControl.SelectedIndex = 0;
            TapControl.Size = new Size(1013, 516);
            TapControl.TabIndex = 0;
            TapControl.Tag = "Main";
            // 
            // tabMain
            // 
            tabMain.Controls.Add(lblCapacidad);
            tabMain.Controls.Add(lblRestauranteDireccion);
            tabMain.Controls.Add(lblRol);
            tabMain.Controls.Add(lblUsuario);
            tabMain.Controls.Add(lblRestauranteNombre);
            tabMain.Controls.Add(lblTitulo);
            tabMain.Location = new Point(4, 24);
            tabMain.Name = "tabMain";
            tabMain.Padding = new Padding(3);
            tabMain.Size = new Size(1005, 488);
            tabMain.TabIndex = 0;
            tabMain.Text = "Home";
            tabMain.UseVisualStyleBackColor = true;
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.LightGray;
            lblTitulo.FlatStyle = FlatStyle.Popup;
            lblTitulo.ForeColor = SystemColors.ActiveCaptionText;
            lblTitulo.Location = new Point(271, 96);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(375, 40);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Bienvenido!";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabMenus
            // 
            tabMenus.Location = new Point(4, 24);
            tabMenus.Name = "tabMenus";
            tabMenus.Padding = new Padding(3);
            tabMenus.Size = new Size(1005, 488);
            tabMenus.TabIndex = 1;
            tabMenus.Text = "Menús";
            tabMenus.UseVisualStyleBackColor = true;
            // 
            // tabPlatos
            // 
            tabPlatos.Location = new Point(4, 24);
            tabPlatos.Name = "tabPlatos";
            tabPlatos.Size = new Size(1005, 488);
            tabPlatos.TabIndex = 2;
            tabPlatos.Text = "Platos";
            tabPlatos.UseVisualStyleBackColor = true;
            // 
            // tabReservas
            // 
            tabReservas.Location = new Point(4, 24);
            tabReservas.Name = "tabReservas";
            tabReservas.Padding = new Padding(3);
            tabReservas.Size = new Size(1005, 488);
            tabReservas.TabIndex = 3;
            tabReservas.Text = "Reservas";
            tabReservas.UseVisualStyleBackColor = true;
            // 
            // tabOrdenes
            // 
            tabOrdenes.Location = new Point(4, 24);
            tabOrdenes.Name = "tabOrdenes";
            tabOrdenes.Size = new Size(1005, 488);
            tabOrdenes.TabIndex = 4;
            tabOrdenes.Text = "Ordenes";
            tabOrdenes.UseVisualStyleBackColor = true;
            // 
            // tabPagos
            // 
            tabPagos.Location = new Point(4, 24);
            tabPagos.Name = "tabPagos";
            tabPagos.Size = new Size(1005, 488);
            tabPagos.TabIndex = 5;
            tabPagos.Text = "Pagos";
            tabPagos.UseVisualStyleBackColor = true;
            // 
            // tabResenas
            // 
            tabResenas.Location = new Point(4, 24);
            tabResenas.Name = "tabResenas";
            tabResenas.Size = new Size(1005, 488);
            tabResenas.TabIndex = 6;
            tabResenas.Text = "Resenas";
            tabResenas.UseVisualStyleBackColor = true;
            // 
            // lblRestauranteNombre
            // 
            lblRestauranteNombre.BackColor = Color.LightGray;
            lblRestauranteNombre.FlatStyle = FlatStyle.Popup;
            lblRestauranteNombre.ForeColor = SystemColors.ActiveCaptionText;
            lblRestauranteNombre.Location = new Point(271, 149);
            lblRestauranteNombre.Name = "lblRestauranteNombre";
            lblRestauranteNombre.Size = new Size(375, 40);
            lblRestauranteNombre.TabIndex = 1;
            lblRestauranteNombre.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsuario
            // 
            lblUsuario.BackColor = Color.LightGray;
            lblUsuario.FlatStyle = FlatStyle.Popup;
            lblUsuario.ForeColor = SystemColors.ActiveCaptionText;
            lblUsuario.Location = new Point(167, 260);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(269, 40);
            lblUsuario.TabIndex = 2;
            lblUsuario.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRol
            // 
            lblRol.BackColor = Color.LightGray;
            lblRol.FlatStyle = FlatStyle.Popup;
            lblRol.ForeColor = SystemColors.ActiveCaptionText;
            lblRol.Location = new Point(167, 327);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(269, 40);
            lblRol.TabIndex = 3;
            lblRol.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRestauranteDireccion
            // 
            lblRestauranteDireccion.BackColor = Color.LightGray;
            lblRestauranteDireccion.FlatStyle = FlatStyle.Popup;
            lblRestauranteDireccion.ForeColor = SystemColors.ActiveCaptionText;
            lblRestauranteDireccion.Location = new Point(481, 260);
            lblRestauranteDireccion.Name = "lblRestauranteDireccion";
            lblRestauranteDireccion.Size = new Size(269, 40);
            lblRestauranteDireccion.TabIndex = 4;
            lblRestauranteDireccion.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblCapacidad
            // 
            lblCapacidad.BackColor = Color.LightGray;
            lblCapacidad.FlatStyle = FlatStyle.Popup;
            lblCapacidad.ForeColor = SystemColors.ActiveCaptionText;
            lblCapacidad.Location = new Point(481, 327);
            lblCapacidad.Name = "lblCapacidad";
            lblCapacidad.Size = new Size(269, 40);
            lblCapacidad.TabIndex = 5;
            lblCapacidad.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1013, 516);
            Controls.Add(TapControl);
            Name = "Main";
            Text = "Main";
            Load += Main_Load;
            TapControl.ResumeLayout(false);
            tabMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl TapControl;
        private TabPage tabMain;
        private TabPage tabMenus;
        private TabPage tabPlatos;
        private TabPage tabReservas;
        private TabPage tabOrdenes;
        private TabPage tabPagos;
        private TabPage tabResenas;
        private Label lblTitulo;
        private Label lblRol;
        private Label lblUsuario;
        private Label lblRestauranteNombre;
        private Label lblCapacidad;
        private Label lblRestauranteDireccion;
    }
}