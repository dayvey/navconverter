using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace navconverter
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnBetoltAdo;
        private Button btnBetoltFaber;
        private Button btnParosit;
        private Button btnExport;
        private ListBox listBoxAdo;
        private ListBox listBoxFaber;
        private TextBox txtCikkszam;
        private Label lblCikkszam;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnBetoltAdo = new Button();
            btnBetoltFaber = new Button();
            btnParosit = new Button();
            btnExport = new Button();
            listBoxAdo = new ListBox();
            listBoxFaber = new ListBox();
            txtCikkszam = new TextBox();
            lblCikkszam = new Label();
            SuspendLayout();
            // 
            // btnBetoltAdo
            // 
            btnBetoltAdo.Location = new Point(12, 12);
            btnBetoltAdo.Name = "btnBetoltAdo";
            btnBetoltAdo.Size = new Size(200, 30);
            btnBetoltAdo.TabIndex = 0;
            btnBetoltAdo.Text = "Adóhivatali fájl betöltése";
            btnBetoltAdo.Click += btnBetoltAdo_Click;
            // 
            // btnBetoltFaber
            // 
            btnBetoltFaber.Location = new Point(220, 12);
            btnBetoltFaber.Name = "btnBetoltFaber";
            btnBetoltFaber.Size = new Size(200, 30);
            btnBetoltFaber.TabIndex = 1;
            btnBetoltFaber.Text = "Faber fájl betöltése";
            btnBetoltFaber.Click += btnBetoltFaber_Click;
            // 
            // btnParosit
            // 
            btnParosit.Location = new Point(320, 365);
            btnParosit.Name = "btnParosit";
            btnParosit.Size = new Size(150, 30);
            btnParosit.TabIndex = 6;
            btnParosit.Text = "Összepárosítás";
            btnParosit.Click += btnParosit_Click;
            // 
            // btnExport
            // 
            btnExport.Location = new Point(480, 365);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(150, 30);
            btnExport.TabIndex = 7;
            btnExport.Text = "Exportálás CSV-be";
            btnExport.Click += btnExport_Click;
            // 
            // listBoxAdo
            // 
            listBoxAdo.ItemHeight = 15;
            listBoxAdo.Location = new Point(12, 60);
            listBoxAdo.Name = "listBoxAdo";
            listBoxAdo.Size = new Size(877, 289);
            listBoxAdo.TabIndex = 2;
            listBoxAdo.SelectedIndexChanged += listBoxAdo_SelectedIndexChanged;
            // 
            // listBoxFaber
            // 
            listBoxFaber.ItemHeight = 15;
            listBoxFaber.Location = new Point(895, 60);
            listBoxFaber.Name = "listBoxFaber";
            listBoxFaber.Size = new Size(494, 289);
            listBoxFaber.TabIndex = 3;
            // 
            // txtCikkszam
            // 
            txtCikkszam.Location = new Point(150, 368);
            txtCikkszam.Name = "txtCikkszam";
            txtCikkszam.Size = new Size(150, 23);
            txtCikkszam.TabIndex = 5;
            // 
            // lblCikkszam
            // 
            lblCikkszam.AutoSize = true;
            lblCikkszam.Location = new Point(12, 370);
            lblCikkszam.Name = "lblCikkszam";
            lblCikkszam.Size = new Size(121, 15);
            lblCikkszam.TabIndex = 4;
            lblCikkszam.Text = "Kiválasztott cikkszám:";
            // 
            // Form1
            // 
            ClientSize = new Size(1394, 420);
            Controls.Add(btnBetoltAdo);
            Controls.Add(btnBetoltFaber);
            Controls.Add(listBoxAdo);
            Controls.Add(listBoxFaber);
            Controls.Add(lblCikkszam);
            Controls.Add(txtCikkszam);
            Controls.Add(btnParosit);
            Controls.Add(btnExport);
            Name = "Form1";
            Text = "Adat Párosító (Adóhivatal ↔ Faber)";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
