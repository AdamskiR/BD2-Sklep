namespace Sklep
{
    partial class FormMain
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
            this.shopDataSet = new Sklep.ShopDataSet();
            this.Produkty = new System.Windows.Forms.Label();
            this.listProducts = new System.Windows.Forms.ListBox();
            this.lProducent = new System.Windows.Forms.Label();
            this.listVendors1 = new System.Windows.Forms.ListBox();
            this.buttRejestracja = new System.Windows.Forms.Button();
            this.labelzalogowany = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxlogin = new System.Windows.Forms.TextBox();
            this.textBoxhaslo = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.buttonIdzDoSklepu = new System.Windows.Forms.Button();
            this.buttonWyloguj = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.użytkownikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rejsetracjaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.shopDataSet)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shopDataSet
            // 
            this.shopDataSet.DataSetName = "ShopDataSet";
            this.shopDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Produkty
            // 
            this.Produkty.AutoSize = true;
            this.Produkty.Location = new System.Drawing.Point(-3, 76);
            this.Produkty.Name = "Produkty";
            this.Produkty.Size = new System.Drawing.Size(49, 13);
            this.Produkty.TabIndex = 0;
            this.Produkty.Text = "Produkty";
            // 
            // listProducts
            // 
            this.listProducts.FormattingEnabled = true;
            this.listProducts.Location = new System.Drawing.Point(35, 39);
            this.listProducts.Name = "listProducts";
            this.listProducts.Size = new System.Drawing.Size(241, 342);
            this.listProducts.TabIndex = 1;
            this.listProducts.SelectedIndexChanged += new System.EventHandler(this.listProducts_SelectedIndexChanged);
            // 
            // lProducent
            // 
            this.lProducent.AutoSize = true;
            this.lProducent.Location = new System.Drawing.Point(296, 9);
            this.lProducent.Name = "lProducent";
            this.lProducent.Size = new System.Drawing.Size(56, 13);
            this.lProducent.TabIndex = 2;
            this.lProducent.Text = "Producent";
            this.lProducent.Click += new System.EventHandler(this.label1_Click);
            // 
            // listVendors1
            // 
            this.listVendors1.FormattingEnabled = true;
            this.listVendors1.Location = new System.Drawing.Point(299, 39);
            this.listVendors1.Name = "listVendors1";
            this.listVendors1.Size = new System.Drawing.Size(241, 342);
            this.listVendors1.TabIndex = 1;
            this.listVendors1.SelectedIndexChanged += new System.EventHandler(this.listProducts_SelectedIndexChanged);
            // 
            // buttRejestracja
            // 
            this.buttRejestracja.Location = new System.Drawing.Point(581, 29);
            this.buttRejestracja.Name = "buttRejestracja";
            this.buttRejestracja.Size = new System.Drawing.Size(191, 35);
            this.buttRejestracja.TabIndex = 3;
            this.buttRejestracja.Text = "Rejestracja";
            this.buttRejestracja.UseVisualStyleBackColor = true;
            this.buttRejestracja.Click += new System.EventHandler(this.buttRejestracja_Click);
            // 
            // labelzalogowany
            // 
            this.labelzalogowany.AutoSize = true;
            this.labelzalogowany.Location = new System.Drawing.Point(582, 102);
            this.labelzalogowany.Name = "labelzalogowany";
            this.labelzalogowany.Size = new System.Drawing.Size(94, 13);
            this.labelzalogowany.TabIndex = 4;
            this.labelzalogowany.Text = "Zalogowany jako: ";
            this.labelzalogowany.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(581, 179);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(191, 35);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Zaloguj";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttLogin_Click);
            // 
            // textBoxlogin
            // 
            this.textBoxlogin.Location = new System.Drawing.Point(581, 127);
            this.textBoxlogin.Name = "textBoxlogin";
            this.textBoxlogin.Size = new System.Drawing.Size(188, 20);
            this.textBoxlogin.TabIndex = 5;
            // 
            // textBoxhaslo
            // 
            this.textBoxhaslo.Location = new System.Drawing.Point(581, 153);
            this.textBoxhaslo.Name = "textBoxhaslo";
            this.textBoxhaslo.Size = new System.Drawing.Size(188, 20);
            this.textBoxhaslo.TabIndex = 5;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(668, 102);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(0, 13);
            this.labelUsername.TabIndex = 6;
            // 
            // buttonIdzDoSklepu
            // 
            this.buttonIdzDoSklepu.Enabled = false;
            this.buttonIdzDoSklepu.Location = new System.Drawing.Point(581, 261);
            this.buttonIdzDoSklepu.Name = "buttonIdzDoSklepu";
            this.buttonIdzDoSklepu.Size = new System.Drawing.Size(191, 35);
            this.buttonIdzDoSklepu.TabIndex = 3;
            this.buttonIdzDoSklepu.Text = "Przejdź do oferty";
            this.buttonIdzDoSklepu.UseVisualStyleBackColor = true;
            this.buttonIdzDoSklepu.Click += new System.EventHandler(this.buttPrzejdzDoOferty_Click);
            // 
            // buttonWyloguj
            // 
            this.buttonWyloguj.Location = new System.Drawing.Point(581, 179);
            this.buttonWyloguj.Name = "buttonWyloguj";
            this.buttonWyloguj.Size = new System.Drawing.Size(191, 35);
            this.buttonWyloguj.TabIndex = 3;
            this.buttonWyloguj.Text = "Wyloguj";
            this.buttonWyloguj.UseVisualStyleBackColor = true;
            this.buttonWyloguj.Visible = false;
            this.buttonWyloguj.Click += new System.EventHandler(this.buttwyloguj_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.użytkownikToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // użytkownikToolStripMenuItem
            // 
            this.użytkownikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rejsetracjaToolStripMenuItem});
            this.użytkownikToolStripMenuItem.Name = "użytkownikToolStripMenuItem";
            this.użytkownikToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.użytkownikToolStripMenuItem.Text = "Użytkownik";
            // 
            // rejsetracjaToolStripMenuItem
            // 
            this.rejsetracjaToolStripMenuItem.Name = "rejsetracjaToolStripMenuItem";
            this.rejsetracjaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.rejsetracjaToolStripMenuItem.Text = "Rejsetracja";
            // 
            // FormMain
            // 
            this.AccessibleDescription = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.textBoxhaslo);
            this.Controls.Add(this.textBoxlogin);
            this.Controls.Add(this.labelzalogowany);
            this.Controls.Add(this.buttonWyloguj);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.buttonIdzDoSklepu);
            this.Controls.Add(this.buttRejestracja);
            this.Controls.Add(this.lProducent);
            this.Controls.Add(this.listVendors1);
            this.Controls.Add(this.listProducts);
            this.Controls.Add(this.Produkty);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Alledrogo";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.shopDataSet)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ShopDataSet shopDataSet;
        private System.Windows.Forms.Label Produkty;
        private System.Windows.Forms.ListBox listProducts;
        private System.Windows.Forms.Label lProducent;
        private System.Windows.Forms.ListBox listVendors1;
        private System.Windows.Forms.Button buttRejestracja;
        private System.Windows.Forms.Label labelzalogowany;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxlogin;
        private System.Windows.Forms.TextBox textBoxhaslo;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Button buttonIdzDoSklepu;
        private System.Windows.Forms.Button buttonWyloguj;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem użytkownikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rejsetracjaToolStripMenuItem;
    }
}

