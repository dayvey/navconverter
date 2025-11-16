using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using navconverter.Models;
using Newtonsoft.Json;

namespace navconverter
{
    public partial class Form1 : Form
    {
        
        private List<AdoHivataliTetel> adoLista = new();
        private List<FaberTetel> faberLista = new();
        private const string ParositasFajl = "parositasok.json";
        string[] exportOszlopok = new string[]
        {
            "Cikkszam",
            "Tetel",
            "Mennyiseg",
            "Egysegar",
            "Afa",
            "Netto",
            "AfaOsszeg",
            "Brutto"
        };


        public Form1()
        {
            InitializeComponent();
            listBoxAdo.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxAdo.DrawItem += ListBoxAdo_DrawItem;
            listBoxFaber.DrawMode = DrawMode.OwnerDrawFixed;
            listBoxFaber.DrawItem += ListBoxFaber_DrawItem;
            BetoltParositasok();

        }

        private void btnBetoltAdo_Click(object sender, EventArgs e)
        {
            string corepath = Application.StartupPath;
            string filepath = Path.Combine(corepath, "hello.csv");
            var lista = new List<AdoHivataliTetel>();
            if (File.Exists(filepath))
            {
                var lines = File.ReadAllLines(filepath).Skip(1);
                foreach (var line in lines)
                {
                    var p = line.Split(";   ");
                    lista.Add(new AdoHivataliTetel
                    {
                        Cikkszam = p[0],
                        Tetel = p[1],
                        Mennyiseg = p[2],
                        Egysegar = p[3],
                        Afa = p[4],
                        AfaOsszeg = p[6],
                        Netto = p[5],
                        Brutto = p[7]
                    });
                }
            }
            string path = FajltValaszt();
            if (string.IsNullOrEmpty(path)) return;

            adoLista = BeolvasAdoHivatal(path);
            for (int i = 0; i < lista.Count; i++)
            {
                for (int j = 0; j < adoLista.Count; j++) 
                {
                    string name1 = lista[i].Tetel.ToString();
                    string name2 = adoLista[j].Tetel.ToString();
                    if (name1.Equals(name2))
                    {
                        adoLista[j].Cikkszam = lista[i].Cikkszam;
                    }
                }
            }

            listBoxAdo.DataSource = null;
            listBoxAdo.DataSource = adoLista;
            listBoxAdo.DisplayMember = "Tetel";

        }

        private void btnBetoltFaber_Click(object sender, EventArgs e)
        {
            string path = FajltValaszt();
            if (string.IsNullOrEmpty(path)) return;

            faberLista = BeolvasFaber(path);
            listBoxFaber.DataSource = null;
            listBoxFaber.DataSource = faberLista;
            listBoxFaber.DisplayMember = "DisplayText";
        }

        private void btnParosit_Click(object sender, EventArgs e)
        {
            if (listBoxAdo.SelectedItem is not AdoHivataliTetel ado ||
                listBoxFaber.SelectedItem is not FaberTetel faber)
            {
                MessageBox.Show("Válassz ki mindkét listából egy-egy elemet!");
                return;
            }

            // Cikkszám hozzáadása az adóhivatali tételhez
            ado.Cikkszam = faber.Cikkszam;

            // opcionálisan a faber megnevezést is elmentheted, ha szükséges
            ado.FaberMegnevezes = faber.Megnevezes;

            faber.Chosen = 1;

            txtCikkszam.Text = faber.Cikkszam;
            MentesParositasok();
            listBoxAdo.Refresh();
            listBoxFaber.Refresh();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var parositott = adoLista.Where(a => !string.IsNullOrEmpty(a.Cikkszam)).ToList();
            if (parositott.Count == 0)
            {
                MessageBox.Show("Nincs párosított tétel!");
                return;
            }

            var saveDialog = new SaveFileDialog
            {
                Filter = "CSV fájl|*.csv",
                FileName = "export.csv"
            };
            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            var lines = new List<string>
            {
            string.Join(";   ", exportOszlopok) // fejléc
            };

            foreach (var t in parositott)
            {
                var row = new List<string>
                {
                    t.Cikkszam,
                    t.Tetel,
                    t.Mennyiseg,
                    t.Egysegar,
                    t.Afa,
                    t.Netto,
                    t.AfaOsszeg,
                    t.Brutto
                };
                lines.Add(string.Join(";   ", row));
            }

            File.WriteAllLines(saveDialog.FileName, lines);

            string corepath = Application.StartupPath;
            string filepath = Path.Combine(corepath, "hello.csv");

            var lista = new List<AdoHivataliTetel>();
            if (File.Exists(filepath))
            {
                var lines2 = File.ReadAllLines(filepath).Skip(1);
                foreach (var line in lines2)
                {
                    var p = line.Split(";   ");
                    lista.Add(new AdoHivataliTetel
                    {
                        Cikkszam = p[0].Trim(),
                        Tetel = p[1].Trim(),
                        Mennyiseg = p[2].Trim(),
                        Egysegar = p[3].Trim(),
                        Afa = p[4].Trim(),
                        AfaOsszeg = p[6].Trim(),
                        Netto = p[5].Trim(),
                        Brutto = p[7].Trim()
                    });
                }

            }
            if (File.Exists(filepath))
            {
                var addition = new List<AdoHivataliTetel>();
                foreach (var t in lista)
                {
                    addition.Add(t);
                }

                for (int i = 0; i < parositott.Count; i++)
                {
                    string trimmed = (parositott[i].Tetel).Trim();
                    bool c = true;
                    for (int j = 0; j < lista.Count; j++)
                    {
                        string trimmed2 = (lista[j].Tetel).Trim();
                        if (trimmed == trimmed2)
                            c = false;
                    }
                    if (c)
                    {
                        addition.Add(parositott[i]);
                    }
                }

                var addedLines = new List<string>
                {
                string.Join(";   ", exportOszlopok) // fejléc
                };


                foreach (var t in addition)
                {
                    var row = new List<string>
                    {
                        t.Cikkszam,
                        t.Tetel,
                        t.Mennyiseg,
                        t.Egysegar,
                        t.Afa,
                        t.Netto,
                        t.AfaOsszeg,
                        t.Brutto
                    };
                    addedLines.Add(string.Join(";   ", row));
                }

                File.WriteAllLines(filepath, addedLines);
            }
            else
                File.WriteAllLines(filepath, lines);
            MessageBox.Show("Export kész!");
        }

        private void ListBoxAdo_DrawItem(object sender, DrawItemEventArgs e)
        {
            bool kivalasztott = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            if (e.Index < 0) return;
            var item = (AdoHivataliTetel)listBoxAdo.Items[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(
                string.IsNullOrEmpty(item.Cikkszam) ? System.Drawing.Color.White : System.Drawing.Color.LightGreen), e.Bounds);
            if (kivalasztott)
                e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.LightBlue), e.Bounds);
            e.Graphics.DrawString(item.DisplayText, e.Font, System.Drawing.Brushes.Black, e.Bounds);
        }

        private void ListBoxFaber_DrawItem(object sender, DrawItemEventArgs e)
        {
            bool kivalasztott = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            if (e.Index < 0) return;
            var item = (FaberTetel)listBoxFaber.Items[e.Index];
            if (item.Chosen == 0)
            {
                e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.White), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.LightGreen), e.Bounds);
            }
            if (kivalasztott)
                e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.LightBlue), e.Bounds);
            e.Graphics.DrawString(item.DisplayText, e.Font, System.Drawing.Brushes.Black, e.Bounds);
        }

        private static string FajltValaszt()
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Excel vagy CSV|*.xlsx;*.xls;*.csv"
            };
            return ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : "";
        }

        // --- Excel és CSV olvasás ---
        private static List<AdoHivataliTetel> BeolvasAdoHivatal(string path)
        {
            var lista = new List<AdoHivataliTetel>();

            if (path.EndsWith(".csv"))
            {
                var lines = File.ReadAllLines(path).Skip(1);
                foreach (var line in lines)
                {
                    var p = line.Split(';');
                    if (p.Length < 8) continue;
                    lista.Add(new AdoHivataliTetel
                    {
                        Tetel = p[1],
                        Mennyiseg = p[2],
                        Egysegar = p[4],
                        Afa = p[6],
                        AfaOsszeg = p[27],
                        Netto = p[29],
                        Brutto = p[31]
                    });
                }
            }
            else
            {
                using var wb = new XLWorkbook(path);
                var ws = wb.Worksheet(1);
                int lastRow = ws.LastRowUsed().RowNumber();
                for (int row = 1; row <= lastRow; row++)
                {
                    if (ws.Cell(row, 2).GetString() == "Megnevezés")
                    {
                        row += 1;
                        lista.Add(new AdoHivataliTetel
                        {
                            Tetel = ws.Cell(row, 2).GetString(),
                            Mennyiseg = ws.Cell(row, 3).GetString(),
                            Egysegar = ws.Cell(row, 5).GetString(),
                            Afa = ws.Cell(row, 7).GetString(),
                            AfaOsszeg = ws.Cell(row + 3, 2).GetString(),
                            Netto = ws.Cell(row + 3, 1).GetString(),
                            Brutto = ws.Cell(row + 3, 5).GetString()

                        });
                        row += 17;
                    }
                }
            }

            return lista;
        }

        private static List<FaberTetel> BeolvasFaber(string path)
        {
            var lista = new List<FaberTetel>();

            if (path.EndsWith(".csv"))
            {
                var lines = File.ReadAllLines(path).Skip(1);
                foreach (var line in lines)
                {
                    var p = line.Split(';');
                    if (p.Length < 2) continue;
                    lista.Add(new FaberTetel
                    {
                        Cikkszam = p[3],
                        Megnevezes = p[0]
                    });
                }
            }
            else
            {
                using var wb = new XLWorkbook(path);
                var ws = wb.Worksheet(1);
                foreach (var row in ws.RowsUsed().Skip(1))
                {
                    lista.Add(new FaberTetel
                    {
                        Cikkszam = row.Cell(4).GetString(),
                        Megnevezes = row.Cell(1).GetString()
                    });
                }
            }

            return lista;
        }

        // --- JSON mentés/betöltés ---
        private void MentesParositasok()
        {
            File.WriteAllText(ParositasFajl, JsonConvert.SerializeObject(adoLista, Formatting.Indented));
        }

        private void BetoltParositasok()
        {
            if (File.Exists(ParositasFajl))
            {
                var json = File.ReadAllText(ParositasFajl);
                adoLista = JsonConvert.DeserializeObject<List<AdoHivataliTetel>>(json) ?? new();
            }
        }

        // --- Exportálás CSV-be --- //
        private static void ExportCsv(string filePath, List<AdoHivataliTetel> lista)
        {
            var lines = new List<string>
            {
                "Cikkszam;  Tetel;  Mennyiseg;  Egysegar;   Afa;    Netto;  Brutto"
            };

            foreach (var i in lista)
            {
                lines.Add($"{i.Cikkszam};   {i.Tetel};   {i.Mennyiseg};   {i.Egysegar};   {i.Afa};    {i.Netto};   {i.Brutto}");
            }

            File.WriteAllLines(filePath, lines);
        }

        private void listBoxAdo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAdo.SelectedItem is AdoHivataliTetel ado)
            {
                listBoxAdo.Text = ado.Tetel;  // CSAK a megnevezés
            }
        }
    }
}
