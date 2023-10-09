namespace Cliente
{
    partial class PantallaSesionUsuario
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Usuario = new System.Windows.Forms.TextBox();
            this.Contrasena = new System.Windows.Forms.MaskedTextBox();
            this.LabelUsuario = new System.Windows.Forms.TextBox();
            this.LabelContrasena = new System.Windows.Forms.TextBox();
            this.OpcionInicioSesion = new System.Windows.Forms.RadioButton();
            this.BotonInicioSesion = new System.Windows.Forms.Button();
            this.BotonRegistroCuenta = new System.Windows.Forms.Button();
            this.OpcionCuentaNueva = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // Usuario
            // 
            this.Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Usuario.Location = new System.Drawing.Point(253, 82);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(237, 30);
            this.Usuario.TabIndex = 0;
            // 
            // Contrasena
            // 
            this.Contrasena.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Contrasena.Location = new System.Drawing.Point(253, 191);
            this.Contrasena.Name = "Contrasena";
            this.Contrasena.Size = new System.Drawing.Size(237, 30);
            this.Contrasena.TabIndex = 1;
            this.Contrasena.UseSystemPasswordChar = true;
            // 
            // LabelUsuario
            // 
            this.LabelUsuario.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUsuario.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelUsuario.Location = new System.Drawing.Point(91, 82);
            this.LabelUsuario.Name = "LabelUsuario";
            this.LabelUsuario.ReadOnly = true;
            this.LabelUsuario.Size = new System.Drawing.Size(90, 30);
            this.LabelUsuario.TabIndex = 2;
            this.LabelUsuario.Text = "Usuario:";
            // 
            // LabelContrasena
            // 
            this.LabelContrasena.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelContrasena.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelContrasena.ForeColor = System.Drawing.SystemColors.ControlText;
            this.LabelContrasena.Location = new System.Drawing.Point(91, 191);
            this.LabelContrasena.Name = "LabelContrasena";
            this.LabelContrasena.ReadOnly = true;
            this.LabelContrasena.Size = new System.Drawing.Size(125, 30);
            this.LabelContrasena.TabIndex = 3;
            this.LabelContrasena.Text = "Contraseña:";
            // 
            // OpcionInicioSesion
            // 
            this.OpcionInicioSesion.AutoSize = true;
            this.OpcionInicioSesion.Location = new System.Drawing.Point(102, 128);
            this.OpcionInicioSesion.Name = "OpcionInicioSesion";
            this.OpcionInicioSesion.Size = new System.Drawing.Size(218, 20);
            this.OpcionInicioSesion.TabIndex = 4;
            this.OpcionInicioSesion.TabStop = true;
            this.OpcionInicioSesion.Text = "Tengo ya una cuenta registrada";
            this.OpcionInicioSesion.UseVisualStyleBackColor = true;
            this.OpcionInicioSesion.CheckedChanged += new System.EventHandler(this.OpcionInicioSesion_CheckedChanged);
            // 
            // BotonInicioSesion
            // 
            this.BotonInicioSesion.Enabled = false;
            this.BotonInicioSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonInicioSesion.Location = new System.Drawing.Point(102, 261);
            this.BotonInicioSesion.Name = "BotonInicioSesion";
            this.BotonInicioSesion.Size = new System.Drawing.Size(179, 63);
            this.BotonInicioSesion.TabIndex = 5;
            this.BotonInicioSesion.Text = "Iniciar sesión";
            this.BotonInicioSesion.UseVisualStyleBackColor = true;
            this.BotonInicioSesion.Click += new System.EventHandler(this.BotonInicioSesion_Click);
            // 
            // BotonRegistroCuenta
            // 
            this.BotonRegistroCuenta.Enabled = false;
            this.BotonRegistroCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonRegistroCuenta.Location = new System.Drawing.Point(297, 261);
            this.BotonRegistroCuenta.Name = "BotonRegistroCuenta";
            this.BotonRegistroCuenta.Size = new System.Drawing.Size(179, 63);
            this.BotonRegistroCuenta.TabIndex = 6;
            this.BotonRegistroCuenta.Text = "Registrar la cuenta";
            this.BotonRegistroCuenta.UseVisualStyleBackColor = true;
            this.BotonRegistroCuenta.Click += new System.EventHandler(this.BotonRegistroCuenta_Click);
            // 
            // OpcionCuentaNueva
            // 
            this.OpcionCuentaNueva.AutoSize = true;
            this.OpcionCuentaNueva.Location = new System.Drawing.Point(102, 155);
            this.OpcionCuentaNueva.Name = "OpcionCuentaNueva";
            this.OpcionCuentaNueva.Size = new System.Drawing.Size(215, 20);
            this.OpcionCuentaNueva.TabIndex = 7;
            this.OpcionCuentaNueva.TabStop = true;
            this.OpcionCuentaNueva.Text = "No tengo una cuenta registrada";
            this.OpcionCuentaNueva.UseVisualStyleBackColor = true;
            this.OpcionCuentaNueva.CheckedChanged += new System.EventHandler(this.OpcionCuentaNueva_CheckedChanged);
            // 
            // PantallaSesionUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 410);
            this.Controls.Add(this.OpcionCuentaNueva);
            this.Controls.Add(this.BotonRegistroCuenta);
            this.Controls.Add(this.BotonInicioSesion);
            this.Controls.Add(this.OpcionInicioSesion);
            this.Controls.Add(this.LabelContrasena);
            this.Controls.Add(this.LabelUsuario);
            this.Controls.Add(this.Contrasena);
            this.Controls.Add(this.Usuario);
            this.Name = "PantallaSesionUsuario";
            this.Text = "Head Soccer - Inicio de sesión/Registro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PantallaSesionUsuario_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Usuario;
        private System.Windows.Forms.MaskedTextBox Contrasena;
        private System.Windows.Forms.TextBox LabelUsuario;
        private System.Windows.Forms.TextBox LabelContrasena;
        private System.Windows.Forms.RadioButton OpcionInicioSesion;
        private System.Windows.Forms.Button BotonInicioSesion;
        private System.Windows.Forms.Button BotonRegistroCuenta;
        private System.Windows.Forms.RadioButton OpcionCuentaNueva;
    }
}

