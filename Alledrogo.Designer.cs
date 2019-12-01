using System.Drawing;

namespace Sklep
{
    partial class Alledrogo
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
            this.shopDataSet = new Sklep.ShopDataSet();
            this.subCategoriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.subCategoriesTableAdapter = new Sklep.ShopDataSetTableAdapters.SubCategoriesTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.listBoxKategorie = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.shopDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subCategoriesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // shopDataSet
            // 
            this.shopDataSet.DataSetName = "ShopDataSet";
            this.shopDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // subCategoriesBindingSource
            // 
            this.subCategoriesBindingSource.DataMember = "SubCategories";
            this.subCategoriesBindingSource.DataSource = this.shopDataSet;
            // 
            // subCategoriesTableAdapter
            // 
            this.subCategoriesTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20F);
            this.label1.Location = new System.Drawing.Point(313, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Oto nasza oferta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Zalogowany jako: ";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(103, 18);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(0, 13);
            this.labelUsername.TabIndex = 3;
            // 
            // listBoxKategorie
            // 
            this.listBoxKategorie.FormattingEnabled = true;
            this.listBoxKategorie.Location = new System.Drawing.Point(15, 78);
            this.listBoxKategorie.Name = "listBoxKategorie";
            this.listBoxKategorie.Size = new System.Drawing.Size(164, 303);
            this.listBoxKategorie.TabIndex = 4;
            // 
            // Alledrogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBoxKategorie);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Alledrogo";
            this.Text = "Alledrogo";
            this.Load += new System.EventHandler(this.Alledrogo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.shopDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subCategoriesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ShopDataSet shopDataSet;
        private System.Windows.Forms.BindingSource subCategoriesBindingSource;
        private ShopDataSetTableAdapters.SubCategoriesTableAdapter subCategoriesTableAdapter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.ListBox listBoxKategorie;
    }
}