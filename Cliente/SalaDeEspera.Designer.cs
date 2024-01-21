namespace Cliente
{
    partial class SalaDeEspera
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaDeEspera));
            this.LabelUsuarioConsultado = new System.Windows.Forms.TextBox();
            this.UsuarioaConsultar = new System.Windows.Forms.TextBox();
            this.LabelConsulta = new System.Windows.Forms.TextBox();
            this.Consulta = new System.Windows.Forms.ComboBox();
            this.LabelResultado = new System.Windows.Forms.TextBox();
            this.Resultado = new System.Windows.Forms.TextBox();
            this.BotonConsulta = new System.Windows.Forms.Button();
            this.TablaUsuariosConectados = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BotonInvitacion = new System.Windows.Forms.Button();
            this.HistorialChat = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BotonEnviarMensajeChat = new System.Windows.Forms.Button();
            this.MensajeChatAEnviar = new System.Windows.Forms.RichTextBox();
            this.LabelChat = new System.Windows.Forms.TextBox();
            this.BotonInicioPartida = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TablaUsuariosConectados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelUsuarioConsultado
            // 
            this.LabelUsuarioConsultado.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelUsuarioConsultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUsuarioConsultado.Location = new System.Drawing.Point(35, 24);
            this.LabelUsuarioConsultado.Name = "LabelUsuarioConsultado";
            this.LabelUsuarioConsultado.ReadOnly = true;
            this.LabelUsuarioConsultado.Size = new System.Drawing.Size(87, 30);
            this.LabelUsuarioConsultado.TabIndex = 0;
            this.LabelUsuarioConsultado.TabStop = false;
            this.LabelUsuarioConsultado.Text = "Usuario:";
            // 
            // UsuarioaConsultar
            // 
            this.UsuarioaConsultar.Location = new System.Drawing.Point(138, 31);
            this.UsuarioaConsultar.Name = "UsuarioaConsultar";
            this.UsuarioaConsultar.Size = new System.Drawing.Size(198, 22);
            this.UsuarioaConsultar.TabIndex = 1;
            // 
            // LabelConsulta
            // 
            this.LabelConsulta.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelConsulta.Location = new System.Drawing.Point(35, 87);
            this.LabelConsulta.Name = "LabelConsulta";
            this.LabelConsulta.ReadOnly = true;
            this.LabelConsulta.Size = new System.Drawing.Size(99, 30);
            this.LabelConsulta.TabIndex = 2;
            this.LabelConsulta.TabStop = false;
            this.LabelConsulta.Text = "Consulta:";
            // 
            // Consulta
            // 
            this.Consulta.FormattingEnabled = true;
            this.Consulta.Items.AddRange(new object[] {
            "Partidas ganadas",
            "Puntos obtenidos",
            "Partidas jugadas"});
            this.Consulta.Location = new System.Drawing.Point(152, 87);
            this.Consulta.Name = "Consulta";
            this.Consulta.Size = new System.Drawing.Size(184, 24);
            this.Consulta.TabIndex = 4;
            // 
            // LabelResultado
            // 
            this.LabelResultado.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelResultado.Location = new System.Drawing.Point(35, 254);
            this.LabelResultado.Name = "LabelResultado";
            this.LabelResultado.ReadOnly = true;
            this.LabelResultado.Size = new System.Drawing.Size(102, 30);
            this.LabelResultado.TabIndex = 5;
            this.LabelResultado.TabStop = false;
            this.LabelResultado.Text = "Resultado:";
            // 
            // Resultado
            // 
            this.Resultado.Location = new System.Drawing.Point(143, 259);
            this.Resultado.Name = "Resultado";
            this.Resultado.Size = new System.Drawing.Size(198, 22);
            this.Resultado.TabIndex = 6;
            // 
            // BotonConsulta
            // 
            this.BotonConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonConsulta.Location = new System.Drawing.Point(91, 156);
            this.BotonConsulta.Name = "BotonConsulta";
            this.BotonConsulta.Size = new System.Drawing.Size(187, 52);
            this.BotonConsulta.TabIndex = 7;
            this.BotonConsulta.Text = "Consultar";
            this.BotonConsulta.UseVisualStyleBackColor = true;
            this.BotonConsulta.Click += new System.EventHandler(this.BotonConsulta_Click);
            // 
            // TablaUsuariosConectados
            // 
            this.TablaUsuariosConectados.AllowUserToAddRows = false;
            this.TablaUsuariosConectados.AllowUserToDeleteRows = false;
            this.TablaUsuariosConectados.AllowUserToOrderColumns = true;
            this.TablaUsuariosConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaUsuariosConectados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre});
            this.TablaUsuariosConectados.Location = new System.Drawing.Point(418, 31);
            this.TablaUsuariosConectados.Name = "TablaUsuariosConectados";
            this.TablaUsuariosConectados.ReadOnly = true;
            this.TablaUsuariosConectados.RowHeadersWidth = 51;
            this.TablaUsuariosConectados.RowTemplate.Height = 24;
            this.TablaUsuariosConectados.RowTemplate.ReadOnly = true;
            this.TablaUsuariosConectados.Size = new System.Drawing.Size(321, 191);
            this.TablaUsuariosConectados.TabIndex = 8;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 125;
            // 
            // BotonInvitacion
            // 
            this.BotonInvitacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonInvitacion.Location = new System.Drawing.Point(418, 241);
            this.BotonInvitacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BotonInvitacion.Name = "BotonInvitacion";
            this.BotonInvitacion.Size = new System.Drawing.Size(321, 52);
            this.BotonInvitacion.TabIndex = 10;
            this.BotonInvitacion.Text = "Retar jugador";
            this.BotonInvitacion.UseVisualStyleBackColor = true;
            this.BotonInvitacion.Click += new System.EventHandler(this.BotonInvitacion_Click);
            // 
            // HistorialChat
            // 
            this.HistorialChat.BackColor = System.Drawing.SystemColors.Window;
            this.HistorialChat.Enabled = false;
            this.HistorialChat.Location = new System.Drawing.Point(35, 389);
            this.HistorialChat.Name = "HistorialChat";
            this.HistorialChat.ReadOnly = true;
            this.HistorialChat.Size = new System.Drawing.Size(704, 165);
            this.HistorialChat.TabIndex = 11;
            this.HistorialChat.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(35, 318);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(704, 22);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // BotonEnviarMensajeChat
            // 
            this.BotonEnviarMensajeChat.Enabled = false;
            this.BotonEnviarMensajeChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonEnviarMensajeChat.Location = new System.Drawing.Point(601, 565);
            this.BotonEnviarMensajeChat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BotonEnviarMensajeChat.Name = "BotonEnviarMensajeChat";
            this.BotonEnviarMensajeChat.Size = new System.Drawing.Size(138, 37);
            this.BotonEnviarMensajeChat.TabIndex = 16;
            this.BotonEnviarMensajeChat.Text = "Enviar mensaje";
            this.BotonEnviarMensajeChat.UseVisualStyleBackColor = true;
            this.BotonEnviarMensajeChat.Click += new System.EventHandler(this.BotonEnviarMensajeChat_Click);
            // 
            // MensajeChatAEnviar
            // 
            this.MensajeChatAEnviar.Enabled = false;
            this.MensajeChatAEnviar.Location = new System.Drawing.Point(35, 565);
            this.MensajeChatAEnviar.MaxLength = 900;
            this.MensajeChatAEnviar.Name = "MensajeChatAEnviar";
            this.MensajeChatAEnviar.Size = new System.Drawing.Size(560, 37);
            this.MensajeChatAEnviar.TabIndex = 15;
            this.MensajeChatAEnviar.Text = "";
            this.MensajeChatAEnviar.TextChanged += new System.EventHandler(this.MensajeChatAEnviar_TextChanged);
            // 
            // LabelChat
            // 
            this.LabelChat.Cursor = System.Windows.Forms.Cursors.Default;
            this.LabelChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelChat.Location = new System.Drawing.Point(323, 351);
            this.LabelChat.Name = "LabelChat";
            this.LabelChat.ReadOnly = true;
            this.LabelChat.Size = new System.Drawing.Size(151, 30);
            this.LabelChat.TabIndex = 17;
            this.LabelChat.TabStop = false;
            this.LabelChat.Text = "Chat de partida";
            this.LabelChat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // BotonInicioPartida
            // 
            this.BotonInicioPartida.Enabled = false;
            this.BotonInicioPartida.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BotonInicioPartida.Location = new System.Drawing.Point(35, 624);
            this.BotonInicioPartida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BotonInicioPartida.Name = "BotonInicioPartida";
            this.BotonInicioPartida.Size = new System.Drawing.Size(704, 52);
            this.BotonInicioPartida.TabIndex = 18;
            this.BotonInicioPartida.Text = "Empezar partida";
            this.BotonInicioPartida.UseVisualStyleBackColor = true;
            this.BotonInicioPartida.Click += new System.EventHandler(this.BotonInicioPartida_Click);
            // 
            // SalaDeEspera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 687);
            this.Controls.Add(this.BotonInicioPartida);
            this.Controls.Add(this.LabelChat);
            this.Controls.Add(this.MensajeChatAEnviar);
            this.Controls.Add(this.BotonEnviarMensajeChat);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.HistorialChat);
            this.Controls.Add(this.BotonInvitacion);
            this.Controls.Add(this.TablaUsuariosConectados);
            this.Controls.Add(this.BotonConsulta);
            this.Controls.Add(this.Resultado);
            this.Controls.Add(this.LabelResultado);
            this.Controls.Add(this.Consulta);
            this.Controls.Add(this.LabelConsulta);
            this.Controls.Add(this.UsuarioaConsultar);
            this.Controls.Add(this.LabelUsuarioConsultado);
            this.Name = "SalaDeEspera";
            this.Text = "Sala de espera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SalaDeEspera_FormClosing);
            this.Load += new System.EventHandler(this.SalaDeEspera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaUsuariosConectados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LabelUsuarioConsultado;
        private System.Windows.Forms.TextBox UsuarioaConsultar;
        private System.Windows.Forms.TextBox LabelConsulta;
        private System.Windows.Forms.ComboBox Consulta;
        private System.Windows.Forms.TextBox LabelResultado;
        private System.Windows.Forms.Button BotonConsulta;
        public System.Windows.Forms.TextBox Resultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.Button BotonInvitacion;
        private System.Windows.Forms.DataGridView TablaUsuariosConectados;
        private System.Windows.Forms.RichTextBox HistorialChat;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BotonEnviarMensajeChat;
        private System.Windows.Forms.RichTextBox MensajeChatAEnviar;
        private System.Windows.Forms.TextBox LabelChat;
        private System.Windows.Forms.Button BotonInicioPartida;
    }
}