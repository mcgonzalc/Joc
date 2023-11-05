
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Juego));
            this.Jugador1 = new System.Windows.Forms.PictureBox();
            this.Jugador2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Jugador1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jugador2)).BeginInit();
            this.SuspendLayout();
            // 
            // Jugador1
            // 
            this.Jugador1.BackColor = System.Drawing.Color.Transparent;
            this.Jugador1.Image = global::Cliente.Properties.Resources.jugador_3;
            this.Jugador1.Location = new System.Drawing.Point(588, 236);
            this.Jugador1.Name = "Jugador1";
            this.Jugador1.Size = new System.Drawing.Size(76, 73);
            this.Jugador1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Jugador1.TabIndex = 0;
            this.Jugador1.TabStop = false;
            // 
            // Jugador2
            // 
            this.Jugador2.BackColor = System.Drawing.Color.Transparent;
            this.Jugador2.Image = global::Cliente.Properties.Resources.jugador_2;
            this.Jugador2.Location = new System.Drawing.Point(27, 220);
            this.Jugador2.Name = "Jugador2";
            this.Jugador2.Size = new System.Drawing.Size(74, 89);
            this.Jugador2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Jugador2.TabIndex = 1;
            this.Jugador2.TabStop = false;
            // 
            // Juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(738, 414);
            this.Controls.Add(this.Jugador2);
            this.Controls.Add(this.Jugador1);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Juego";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Juego";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Juego_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Juego_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.Jugador1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Jugador2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Jugador1;
        private System.Windows.Forms.PictureBox Jugador2;
    }
}