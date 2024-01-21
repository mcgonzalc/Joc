
namespace Cliente
{
    partial class Juego
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Juego));
            this.JugadorDerecha = new System.Windows.Forms.PictureBox();
            this.JugadorIzquierda = new System.Windows.Forms.PictureBox();
            this.pelota = new System.Windows.Forms.PictureBox();
            this.TimerSalto = new System.Windows.Forms.Timer(this.components);
            this.TimerPelota = new System.Windows.Forms.Timer(this.components);
            this.porteria2 = new System.Windows.Forms.PictureBox();
            this.porteria = new System.Windows.Forms.PictureBox();
            this.Local = new System.Windows.Forms.Label();
            this.Visitante = new System.Windows.Forms.Label();
            this.TimerPartida = new System.Windows.Forms.Timer(this.components);
            this.Duracion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.JugadorDerecha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JugadorIzquierda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.porteria2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.porteria)).BeginInit();
            this.SuspendLayout();
            // 
            // JugadorDerecha
            // 
            this.JugadorDerecha.BackColor = System.Drawing.Color.Transparent;
            this.JugadorDerecha.Image = global::Cliente.Properties.Resources.jugador_3;
            this.JugadorDerecha.Location = new System.Drawing.Point(924, 369);
            this.JugadorDerecha.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.JugadorDerecha.Name = "JugadorDerecha";
            this.JugadorDerecha.Size = new System.Drawing.Size(76, 72);
            this.JugadorDerecha.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.JugadorDerecha.TabIndex = 0;
            this.JugadorDerecha.TabStop = false;
            // 
            // JugadorIzquierda
            // 
            this.JugadorIzquierda.BackColor = System.Drawing.Color.Transparent;
            this.JugadorIzquierda.Image = global::Cliente.Properties.Resources.Jugador_2_sin_fondo;
            this.JugadorIzquierda.Location = new System.Drawing.Point(150, 369);
            this.JugadorIzquierda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.JugadorIzquierda.Name = "JugadorIzquierda";
            this.JugadorIzquierda.Size = new System.Drawing.Size(76, 72);
            this.JugadorIzquierda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.JugadorIzquierda.TabIndex = 1;
            this.JugadorIzquierda.TabStop = false;
            // 
            // pelota
            // 
            this.pelota.BackColor = System.Drawing.Color.Transparent;
            this.pelota.Image = global::Cliente.Properties.Resources.pelota;
            this.pelota.Location = new System.Drawing.Point(522, 380);
            this.pelota.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pelota.Name = "pelota";
            this.pelota.Size = new System.Drawing.Size(60, 62);
            this.pelota.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pelota.TabIndex = 2;
            this.pelota.TabStop = false;
            // 
            // TimerSalto
            // 
            this.TimerSalto.Tick += new System.EventHandler(this.TimerSalto_Tick);
            // 
            // TimerPelota
            // 
            this.TimerPelota.Tick += new System.EventHandler(this.TimerPelota_Tick);
            // 
            // porteria2
            // 
            this.porteria2.BackColor = System.Drawing.Color.Transparent;
            this.porteria2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.porteria2.Location = new System.Drawing.Point(1060, 252);
            this.porteria2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.porteria2.Name = "porteria2";
            this.porteria2.Size = new System.Drawing.Size(46, 189);
            this.porteria2.TabIndex = 4;
            this.porteria2.TabStop = false;
            // 
            // porteria
            // 
            this.porteria.BackColor = System.Drawing.Color.Transparent;
            this.porteria.Location = new System.Drawing.Point(0, 252);
            this.porteria.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.porteria.Name = "porteria";
            this.porteria.Size = new System.Drawing.Size(44, 189);
            this.porteria.TabIndex = 5;
            this.porteria.TabStop = false;
            // 
            // Local
            // 
            this.Local.AutoSize = true;
            this.Local.BackColor = System.Drawing.Color.Transparent;
            this.Local.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Local.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Local.Location = new System.Drawing.Point(386, 69);
            this.Local.MinimumSize = new System.Drawing.Size(50, 49);
            this.Local.Name = "Local";
            this.Local.Size = new System.Drawing.Size(134, 49);
            this.Local.TabIndex = 6;
            this.Local.Text = "marcador L";
            // 
            // Visitante
            // 
            this.Visitante.AutoSize = true;
            this.Visitante.BackColor = System.Drawing.Color.Transparent;
            this.Visitante.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Visitante.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Visitante.Location = new System.Drawing.Point(645, 69);
            this.Visitante.MinimumSize = new System.Drawing.Size(50, 49);
            this.Visitante.Name = "Visitante";
            this.Visitante.Size = new System.Drawing.Size(136, 49);
            this.Visitante.TabIndex = 7;
            this.Visitante.Text = "marcador V";
            // 
            // TimerPartida
            // 
            this.TimerPartida.Enabled = true;
            this.TimerPartida.Interval = 1000;
            this.TimerPartida.Tick += new System.EventHandler(this.TimerPartida_Tick);
            // 
            // Duracion
            // 
            this.Duracion.AutoSize = true;
            this.Duracion.BackColor = System.Drawing.Color.Transparent;
            this.Duracion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Duracion.ForeColor = System.Drawing.Color.Transparent;
            this.Duracion.Location = new System.Drawing.Point(518, 40);
            this.Duracion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Duracion.Name = "Duracion";
            this.Duracion.Size = new System.Drawing.Size(87, 29);
            this.Duracion.TabIndex = 8;
            this.Duracion.Text = "tiempo";
            // 
            // Juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1104, 586);
            this.Controls.Add(this.Duracion);
            this.Controls.Add(this.Visitante);
            this.Controls.Add(this.Local);
            this.Controls.Add(this.porteria);
            this.Controls.Add(this.porteria2);
            this.Controls.Add(this.pelota);
            this.Controls.Add(this.JugadorIzquierda);
            this.Controls.Add(this.JugadorDerecha);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Juego";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Juego";
            this.Load += new System.EventHandler(this.Juego_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Juego_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Juego_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.JugadorDerecha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JugadorIzquierda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.porteria2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.porteria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox JugadorDerecha;
        private System.Windows.Forms.PictureBox JugadorIzquierda;
        private System.Windows.Forms.PictureBox pelota;
        private System.Windows.Forms.Timer TimerSalto;
        private System.Windows.Forms.Timer TimerPelota;
        private System.Windows.Forms.PictureBox porteria2;
        private System.Windows.Forms.PictureBox porteria;
        private System.Windows.Forms.Label Local;
        private System.Windows.Forms.Label Visitante;
        private System.Windows.Forms.Timer TimerPartida;
        private System.Windows.Forms.Label Duracion;
    }
}