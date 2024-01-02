
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
            this.Jugador1 = new System.Windows.Forms.PictureBox();
            this.Jugador2 = new System.Windows.Forms.PictureBox();
            this.pelota = new System.Windows.Forms.PictureBox();
            this.TimerSalto = new System.Windows.Forms.Timer(this.components);
            this.TimerPelota = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.porteria2 = new System.Windows.Forms.PictureBox();
            this.porteria = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Jugador1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jugador2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.porteria2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.porteria)).BeginInit();
            this.SuspendLayout();
            // 
            // Jugador1
            // 
            this.Jugador1.BackColor = System.Drawing.Color.Transparent;
            this.Jugador1.Image = global::Cliente.Properties.Resources.jugador_3;
            this.Jugador1.Location = new System.Drawing.Point(632, 240);
            this.Jugador1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Jugador1.Name = "Jugador1";
            this.Jugador1.Size = new System.Drawing.Size(51, 47);
            this.Jugador1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Jugador1.TabIndex = 0;
            this.Jugador1.TabStop = false;
            // 
            // Jugador2
            // 
            this.Jugador2.BackColor = System.Drawing.Color.Transparent;
            this.Jugador2.Image = global::Cliente.Properties.Resources.jugador_2;
            this.Jugador2.Location = new System.Drawing.Point(100, 240);
            this.Jugador2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Jugador2.Name = "Jugador2";
            this.Jugador2.Size = new System.Drawing.Size(51, 47);
            this.Jugador2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Jugador2.TabIndex = 1;
            this.Jugador2.TabStop = false;
            // 
            // pelota
            // 
            this.pelota.BackColor = System.Drawing.Color.Transparent;
            this.pelota.Image = global::Cliente.Properties.Resources.pelota;
            this.pelota.Location = new System.Drawing.Point(348, 247);
            this.pelota.Name = "pelota";
            this.pelota.Size = new System.Drawing.Size(40, 40);
            this.pelota.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pelota.TabIndex = 2;
            this.pelota.TabStop = false;
            // 
            // TimerSalto
            // 
            this.TimerSalto.Tick += new System.EventHandler(this.Temporizador_Tick);
            // 
            // TimerPelota
            // 
            this.TimerPelota.Tick += new System.EventHandler(this.TimerPelota_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 32);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // porteria2
            // 
            this.porteria2.BackColor = System.Drawing.Color.Transparent;
            this.porteria2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.porteria2.Location = new System.Drawing.Point(707, 180);
            this.porteria2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.porteria2.Name = "porteria2";
            this.porteria2.Size = new System.Drawing.Size(31, 107);
            this.porteria2.TabIndex = 4;
            this.porteria2.TabStop = false;
            // 
            // porteria
            // 
            this.porteria.BackColor = System.Drawing.Color.Transparent;
            this.porteria.Location = new System.Drawing.Point(0, 180);
            this.porteria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.porteria.Name = "porteria";
            this.porteria.Size = new System.Drawing.Size(29, 107);
            this.porteria.TabIndex = 5;
            this.porteria.TabStop = false;
            // 
            // Juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(736, 381);
            this.Controls.Add(this.porteria);
            this.Controls.Add(this.porteria2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pelota);
            this.Controls.Add(this.Jugador2);
            this.Controls.Add(this.Jugador1);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Juego";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Juego";
            this.Load += new System.EventHandler(this.Juego_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Juego_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Juego_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.Jugador1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jugador2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pelota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.porteria2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.porteria)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Jugador1;
        private System.Windows.Forms.PictureBox Jugador2;
        private System.Windows.Forms.PictureBox pelota;
        private System.Windows.Forms.Timer TimerSalto;
        private System.Windows.Forms.Timer TimerPelota;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox porteria2;
        private System.Windows.Forms.PictureBox porteria;
    }
}