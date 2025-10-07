namespace ModernUI
{
    partial class btnTopMenu
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn = new System.Windows.Forms.Button();
            this.pictRight = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictRight)).BeginInit();
            this.SuspendLayout();
            // 
            // btn
            // 
            this.btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn.FlatAppearance.BorderSize = 0;
            this.btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn.ForeColor = System.Drawing.Color.White;
            this.btn.Location = new System.Drawing.Point(0, 0);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(129, 32);
            this.btn.TabIndex = 1;
            this.btn.Text = "button1";
            this.btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn.UseVisualStyleBackColor = false;
            // 
            // pictRight
            // 
            this.pictRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.pictRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictRight.Image = global::ModernUI.Properties.Resources.arrowright_white9;
            this.pictRight.InitialImage = global::ModernUI.Properties.Resources.arrowright_white9;
            this.pictRight.Location = new System.Drawing.Point(129, 0);
            this.pictRight.Name = "pictRight";
            this.pictRight.Size = new System.Drawing.Size(15, 32);
            this.pictRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictRight.TabIndex = 2;
            this.pictRight.TabStop = false;
            // 
            // btnTopMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn);
            this.Controls.Add(this.pictRight);
            this.Name = "btnTopMenu";
            this.Size = new System.Drawing.Size(144, 32);
            ((System.ComponentModel.ISupportInitialize)(this.pictRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.PictureBox pictRight;
    }
}
