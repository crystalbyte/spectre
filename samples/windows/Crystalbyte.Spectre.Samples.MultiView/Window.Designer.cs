namespace Crystalbyte.Spectre.Samples {
    partial class Window {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this._layout = new System.Windows.Forms.TableLayoutPanel();
            this.frame1 = new Crystalbyte.Spectre.Samples.Frame();
            this.frame2 = new Crystalbyte.Spectre.Samples.Frame();
            this.frame3 = new Crystalbyte.Spectre.Samples.Frame();
            this.frame4 = new Crystalbyte.Spectre.Samples.Frame();
            this._layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // _layout
            // 
            this._layout.ColumnCount = 2;
            this._layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._layout.Controls.Add(this.frame1, 0, 0);
            this._layout.Controls.Add(this.frame2, 1, 0);
            this._layout.Controls.Add(this.frame3, 0, 1);
            this._layout.Controls.Add(this.frame4, 1, 1);
            this._layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this._layout.Location = new System.Drawing.Point(0, 0);
            this._layout.Name = "_layout";
            this._layout.RowCount = 2;
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._layout.Size = new System.Drawing.Size(884, 552);
            this._layout.TabIndex = 3;
            // 
            // frame1
            // 
            this.frame1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.frame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frame1.Location = new System.Drawing.Point(3, 3);
            this.frame1.Name = "frame1";
            this.frame1.Size = new System.Drawing.Size(436, 270);
            this.frame1.TabIndex = 0;
            // 
            // frame2
            // 
            this.frame2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.frame2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frame2.Location = new System.Drawing.Point(445, 3);
            this.frame2.Name = "frame2";
            this.frame2.Size = new System.Drawing.Size(436, 270);
            this.frame2.TabIndex = 1;
            // 
            // frame3
            // 
            this.frame3.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.frame3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frame3.Location = new System.Drawing.Point(3, 279);
            this.frame3.Name = "frame3";
            this.frame3.Size = new System.Drawing.Size(436, 270);
            this.frame3.TabIndex = 2;
            // 
            // frame4
            // 
            this.frame4.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.frame4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frame4.Location = new System.Drawing.Point(445, 279);
            this.frame4.Name = "frame4";
            this.frame4.Size = new System.Drawing.Size(436, 270);
            this.frame4.TabIndex = 3;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(884, 552);
            this.Controls.Add(this._layout);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 590);
            this.Name = "Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spectre -Multi View Sample";
            this._layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _layout;
        private Frame frame1;
        private Frame frame2;
        private Frame frame3;
        private Frame frame4;

    }
}

