using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace GradCalc
{
    public partial class Form1 : Form
    {
        #region variablen

        public Reihenfolge reihenfolge = new Reihenfolge();
        public Noten noten;
        public string[] fächer = new string[] { "", "De", "En", "Ma", "Bio", "Ch", "Ph", "WP1", "WP2", "LER", "Sp", "Mu", "Ku", "WAT", "Ge", "Pb", "Geo", "---" };
        string[] fg1 = { "De", "Ma", "En", "WP1" }; //fächergruppe I
        string[] fg2 = { "Bio", "Ch", "Ph", "WP2", "LER", "Sp", "Mu", "Ku", "WAT", "Ge", "Pb", "Geo" }; //fächergruppe II
        string option = "";
        string haupt = "";
        SaveFileDialog sfdOptions = new SaveFileDialog();
        SaveFileDialog sfdFiles = new SaveFileDialog();
        bool SpeichernAvailable = false;
        bool saved = false;
        bool testForCells = false;
        PrintDocument printDocument = new PrintDocument();

        #endregion

        #region load/close

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.GridErstellen();
            noten = new Noten(this);
            this.dgvAnsicht.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MyDataGridView_MouseDown);
            this.InitializeMethods();
            this.testForCells = true;
            this.saved = true;
            this.option = AppDomain.CurrentDomain.BaseDirectory + @"Dateien\Optionen\";
            this.haupt = AppDomain.CurrentDomain.BaseDirectory + @"Dateien\Mappen\";
            this.Text = "neues Textdokument...";
        }

        public void GridErstellen()
        {
            for (int i = 1; i < fächer.Length - 1; i++)
            {
                this.dgvAnsicht.Columns.Add("AB" + fächer[i], "A/B");
                this.dgvAnsicht.Columns.Add(fächer[i], fächer[i]);
                this.dgvAnsicht.Columns[fächer[i]].Visible = false;
                this.dgvAnsicht.Columns["AB" + fächer[i]].Visible = false;
                this.dgvAnsicht.Columns[fächer[i]].Width = 40;
                this.dgvAnsicht.Columns["AB" + fächer[i]].Width = 30;
            }
            this.dgvColumnAbschluss.DisplayIndex = dgvAnsicht.Columns.Count - 1;
            this.ResizeGrid();
            this.Size = new Size(this.Size.Width, Screen.FromControl(this).Bounds.Height - 50);
        }

        public void GridÄndern()
        {
            int count = 2;
            this.dgvAnsicht.Rows.Clear();
            for (int i = 1; i < reihenfolge.Length(); i++)
            {
                this.dgvAnsicht.Columns[fächer[i]].Visible = false;
                this.dgvAnsicht.Columns[fächer[i]].DefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet;
                this.dgvAnsicht.Columns["AB" + fächer[i]].Visible = false;
                this.dgvAnsicht.Columns["AB" + fächer[i]].DefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet;
            }
            for (int i = 0; i < reihenfolge.Length(); i++)
                if ((reihenfolge.FächerExp(i) != fächer[0]) && (reihenfolge.FächerExp(i) != fächer[fächer.Length - 1]))
                {
                    this.dgvAnsicht.Columns[reihenfolge.FächerExp(i)].Visible = true;
                    this.dgvAnsicht.Columns[reihenfolge.FächerExp(i)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dgvAnsicht.Columns[reihenfolge.FächerExp(i)].DisplayIndex = count;
                    count++;
                    if (reihenfolge.KurseExp(i))
                    {
                        this.dgvAnsicht.Columns[reihenfolge.FächerExp(i)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet;
                        this.dgvAnsicht.Columns["AB" + reihenfolge.FächerExp(i)].Visible = true;
                        this.dgvAnsicht.Columns["AB" + reihenfolge.FächerExp(i)].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        this.dgvAnsicht.Columns["AB" + reihenfolge.FächerExp(i)].DisplayIndex = count - 1;
                        count++;
                    }
                }
            this.ResizeGrid();
        }

        public void ResizeGrid()
        {
            if ((this.WindowState == FormWindowState.Normal) && (dgvAnsicht.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 40 > 1024)
                && (dgvAnsicht.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 40 <= Screen.FromControl(this).Bounds.Width))
            {
                this.Size = new Size(dgvAnsicht.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 40, this.Size.Height);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((!saved) && (DialogResult.Yes == MessageBox.Show("Wollen Sie die Änderungen speichern?", "Ungespeicherte Änderungen!", MessageBoxButtons.YesNo)))
                this.speichernToolStripMenuItem_Click(sender, e);
        }

        #endregion

        #region Datagridview

        private void InitializeMethods()
        {
            this.AddRow.Click += new System.EventHandler(this.AddRow_Click);
            this.DelRow.Click += new System.EventHandler(this.DeleteRow_Click);
        }

        private void MyDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                try
                {
                    var hit = dgvAnsicht.HitTest(e.X, e.Y);
                    this.dgvAnsicht.ClearSelection();
                    this.dgvAnsicht.Rows[hit.RowIndex].Selected = true;
                }
                catch { }
        }

        private void AddRow_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgvAnsicht.Rows.Insert(dgvAnsicht.Rows.GetFirstRow(DataGridViewElementStates.Selected), new DataGridViewRow());
                this.dgvAnsicht.ClearSelection();
            }
            catch { }
        }

        private void DeleteRow_Click(object sender, EventArgs e)
        {
            try
            {
                dgvAnsicht.Rows.RemoveAt(dgvAnsicht.Rows.GetFirstRow(DataGridViewElementStates.Selected));
                dgvAnsicht.ClearSelection();
            }
            catch { }
        }

        private void CellChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.saved = false;
            if (testForCells)
            {
                //try
                //{
                if (Convert.ToString(this.dgvAnsicht.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == "a")
                    this.dgvAnsicht.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "A";
                if (Convert.ToString(this.dgvAnsicht.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == "b")
                    this.dgvAnsicht.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "B";
                //}
                //catch { }
                this.dgvAnsicht.Rows[e.RowIndex].Cells[2].Value = null;
                for (int i = 1; i < this.fächer.Length - 1; i++)
                    if (!this.dgvAnsicht.Columns[fächer[i]].Visible)
                        continue;
                    else if (Convert.ToString(this.dgvAnsicht.Rows[e.RowIndex].Cells[fächer[i]].Value) == "")
                        return;
                    else if (!this.dgvAnsicht.Columns["AB" + fächer[i]].Visible)
                        continue;
                    else if (Convert.ToString(this.dgvAnsicht.Rows[e.RowIndex].Cells["AB" + fächer[i]].Value) == "")
                        return;
                this.noten.Clear();
                for (int k = 1; k < this.fächer.Length - 1; k++)
                {
                    if (this.dgvAnsicht.Columns[fächer[k]].Visible == true)
                        this.noten.ZensurImp(Convert.ToInt32(this.dgvAnsicht.Rows[e.RowIndex].Cells[fächer[k]].Value),
                            Convert.ToString(this.dgvAnsicht.Rows[e.RowIndex].Cells["AB" + fächer[k]].Value), fächer[k]);
                }
                testForCells = false;
                this.dgvAnsicht.Rows[e.RowIndex].Cells[2].Value = AbschlussBerechnen(noten);
                testForCells = true;
            }
        }

        private string AbschlussBerechnen(Noten noten)
        {
            int eins = 0, zwei = 0, drei = 0, vier = 0, fünf = 0;
            List<int> b_coursesInd = new List<int>();
            List<int> a_coursesInd = new List<int>();
            List<int> rest_coursesInd = new List<int>();
            List<int> fg1Ind = new List<int>();
            List<int> fg2Ind = new List<int>();
            bool ausgleichRemaining = true;
            bool ausgleich2Remaining = true;
            bool ausgleich = false;
            string abschluss = "FORQ";

            for (int i = 0; i < noten.Length; i++)//B-Kurse ermitteln
                if (noten.Kurs(i) == "B")
                {
                    b_coursesInd.Add(i);
                    if (noten.Zensur(i) == 6) //bei note 6 in B-Kursen nur noch EBR möglich
                        abschluss = "EBR";
                }
            for (int i = 0; i < noten.Length; i++)//A-Kurse ermitteln
                if (noten.Kurs(i) == "A")
                {
                    a_coursesInd.Add(i);
                    if (noten.Zensur(i) == 6) //bei note 6 in A-Kursen nur BBR möglich
                        abschluss = "BBR";
                }
            for (int i = 0; i < noten.Length; i++)//restkurse ermitteln
                if (noten.Kurs(i) == "")
                {
                    rest_coursesInd.Add(i);
                    if (noten.Zensur(i) == 6) //bei note 6 in restlichen Fächern nur BBR möglich
                        abschluss = "BBR";
                }
            for (int i = 0; i < fg1.Length; i++)//fg1 ermitteln
                if (noten.Fach(fg1[i]) != -1)
                    fg1Ind.Add(noten.Fach(fg1[i]));
            for (int i = 0; i < fg2.Length; i++)//fg2 ermitteln
                if (noten.Fach(fg2[i]) != -1)
                    fg2Ind.Add(noten.Fach(fg2[i]));

            eins = 0; zwei = 0; drei = 0; vier = 0; fünf = 0;
            for (int i = 0; i < rest_coursesInd.Count; i++)//restliche Fächer noten
                switch (noten.Zensur(rest_coursesInd[i])) //zählen der gleichen Noten
                {
                    case 1:
                        eins++;
                        break;
                    case 2:
                        zwei++;
                        break;
                    case 3:
                        drei++;
                        break;
                    case 4:
                        vier++;
                        break;
                    case 5:
                        fünf++;
                        break;
                }
            //ABSCHLUSS ERMITTELN
            switch (abschluss)
            {
                case "FORQ": ////////////////////FORQ\\\\\\\\\\\\\\\\\\\\<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    if (b_coursesInd.Count < 3) //mindestens 3 B-Kurse
                        goto case "FOR";
                    //AUSGLEICH DER B-KURSE
                    for (int i = 0; i < b_coursesInd.Count; i++)
                        if (noten.Zensur(b_coursesInd[i]) > 3) //B-Kurse mit höchstens einer note schlechter als 3
                            if ((ausgleichRemaining == false) || (noten.Zensur(b_coursesInd[i]) > 4)) //schon einen ausgleich benutzt oder note schlechter als 4
                                goto case "FOR";
                            else
                            {
                                ausgleichRemaining = false;
                                ausgleich = false;
                                for (int k = 0; k < a_coursesInd.Count; k++)
                                    if (noten.Zensur(a_coursesInd[k]) == 1) //A-Kurs noten werden zum ausgleichen herangezogen
                                        ausgleich = true;
                                if (ausgleich == false)
                                    for (int k = 0; k < b_coursesInd.Count; k++)
                                        if (noten.Zensur(b_coursesInd[k]) < 3) //B-Kurs noten werden zum ausgleichen herangezogen
                                            ausgleich = true;
                                if ((ausgleich == false) && (noten.Zensur(noten.Fach("WP1")) > 2)) //WP_I wird zum ausgleichen herangezogen
                                    goto case "FOR";
                            }
                    //AUSGLEICH DER A-KURSE
                    for (int i = 0; i < a_coursesInd.Count; i++)
                        if (noten.Zensur(a_coursesInd[i]) > 2) //A-Kurse mit höchstens einer note schlechter als 2
                            if ((ausgleichRemaining == false) || (noten.Zensur(a_coursesInd[i]) > 3)) //ausgleich benutzt oder note schlechter als 3
                                goto case "FOR";
                            else
                            {
                                ausgleichRemaining = false;
                                ausgleich = false;
                                for (int k = 0; k < a_coursesInd.Count; k++)
                                    if (noten.Zensur(a_coursesInd[k]) == 1) //A-Kurs noten werden zum ausgleichen herangezogen
                                        ausgleich = true;
                                if (ausgleich == false)
                                    for (int k = 0; k < b_coursesInd.Count; k++)
                                        if (noten.Zensur(b_coursesInd[k]) < 3) //B-Kurs noten werden zum ausgleichen herangezogen
                                            ausgleich = true;
                                if ((ausgleich == false) && (noten.Zensur(noten.Fach("WP1")) > 2)) //WP_I wird zum ausgleich herangezogen
                                    goto case "FOR";
                            }
                    //RESTLICHE FÄCHER
                    if (zwei + eins < 2) //mindestens 2 zweien benötigt
                        goto case "FOR";
                    if (fünf > 1) //höchstens 1 fünf
                        goto case "FOR";
                    if (Math.Floor(((eins + zwei * 2 + drei * 3 + vier * 4 + fünf * 5) / (double)rest_coursesInd.Count) * 10) / 10 > 3) //mindestens durchschnitt von 3.0 benötigt
                        goto case "FOR";
                    break;
                case "FOR": ////////////////////FOR\\\\\\\\\\\\\\\\\\\\<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    ausgleichRemaining = true;
                    if (b_coursesInd.Count < 2) //mindestens 3 B-Kurse
                        goto case "EBR";
                    //AUSGLEICH DER B-KURSE
                    for (int i = 0; i < b_coursesInd.Count; i++)
                        if (noten.Zensur(b_coursesInd[i]) > 4) //B-Kurse mit höchstens einer note schlechter als 4
                            if ((ausgleichRemaining == false) || (noten.Zensur(b_coursesInd[i]) > 5)) //schon einen ausgleich benutzt oder note schlechter als 5
                                goto case "EBR";
                            else
                            {
                                ausgleichRemaining = false;
                                ausgleich = false;
                                for (int k = 0; k < a_coursesInd.Count; k++)
                                    if (noten.Zensur(a_coursesInd[k]) < 3) //A-Kurs noten werden zum ausgleichen herangezogen
                                        ausgleich = true;
                                if (ausgleich == false)
                                    for (int k = 0; k < b_coursesInd.Count; k++)
                                        if (noten.Zensur(b_coursesInd[k]) < 4) //B-Kurs noten werden zum ausgleichen herangezogen
                                            ausgleich = true;
                                if ((ausgleich == false) && (noten.Zensur(noten.Fach("WP1")) > 3)) //WP_I wird zum ausgleichen herangezogen
                                    goto case "EBR";
                            }
                    //AUSGLEICH DER A-KURSE
                    for (int i = 0; i < a_coursesInd.Count; i++)
                        if (noten.Zensur(a_coursesInd[i]) > 3) //A-Kurse mit höchstens einer note schlechter als 3
                            if ((ausgleichRemaining == false) || (noten.Zensur(a_coursesInd[i]) > 4)) //ausgleich benutzt oder note schlechter als 4
                                goto case "EBR";
                            else
                            {
                                ausgleichRemaining = false;
                                ausgleich = false;
                                for (int k = 0; k < a_coursesInd.Count; k++)
                                    if (noten.Zensur(a_coursesInd[k]) < 3) //A-Kurs noten werden zum ausgleichen herangezogen
                                        ausgleich = true;
                                if (ausgleich == false)
                                    for (int k = 0; k < b_coursesInd.Count; k++)
                                        if (noten.Zensur(b_coursesInd[k]) < 4) //B-Kurs noten werden zum ausgleichen herangezogen
                                            ausgleich = true;
                                if ((ausgleich == false) && (noten.Zensur(noten.Fach("WP1")) > 3)) //WP_I wird zum ausgleich herangezogen
                                    goto case "EBR";
                            }
                    //RESTLICHE FÄCHER
                    if (drei + zwei + eins < 3) //mindestens 2 zweien benötigt
                        goto case "EBR";
                    if (fünf > 2) //höchstens 2 fünfen
                        goto case "EBR";
                    if (Math.Floor(((eins + zwei * 2 + drei * 3 + vier * 4 + fünf * 5) / (double)rest_coursesInd.Count) * 10) / 10 > 4) //mindestens durchschnitt von 4.0 benötigt
                        goto case "EBR";
                    abschluss = "FOR";
                    break;
                case "EBR": ////////////////////EBR\\\\\\\\\\\\\\\\\\\\<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                    bool deMa5 = false;
                    int usedAusgleichsNoteInd = -1;
                    ausgleichRemaining = true;
                    for (int i = 0; i < b_coursesInd.Count; i++) //B-Kurs noten werden in A-Kurse umgewandelt
                        if (noten.Zensur(b_coursesInd[i]) > 1)
                            noten.ZensurLower(b_coursesInd[i]);
                    for (int i = 0; i < noten.Length; i++)
                        if ((noten.Zensur(i) == 5) && (ausgleichRemaining == false) && (ausgleich2Remaining == false)) //note 5 und keine ausgleiche mehr
                            goto case "BBR";
                        else if (noten.Zensur(i) == 5) //note 5 und mindestens ein ausgleich
                        {
                            if (ausgleichRemaining == true)
                                ausgleichRemaining = false;
                            else
                                ausgleich2Remaining = false;
                            ausgleich = false;
                            if ((i == noten.Fach("De")) || (i == noten.Fach("Ma"))) //deutsch/mathe 5?
                                if (deMa5 == true) //schon eine 5 in deutsch/mathe?
                                    goto case "BBR";
                                else
                                    deMa5 = true;
                            for (int k = 0; k < fg1Ind.Count; k++) //fg I wird nur mit fg I ausgeglichen
                            {
                                if (usedAusgleichsNoteInd >= fg1Ind[k]) //nur mit größeren notenindexen als den evtl bereits genutzten fortfahren
                                    continue;
                                if (i == fg1Ind[k]) //ist das fach teil von fg I?
                                {
                                    if (ausgleich == true)
                                        break;
                                    for (int l = 0; l < fg1Ind.Count; l++)
                                        if (noten.Zensur(fg1Ind[l]) < 4) //ist eine fg I note zum ausgleichen vorhanden?
                                        {
                                            usedAusgleichsNoteInd = fg1Ind[l];
                                            ausgleich = true;
                                            break;
                                        }
                                    if (ausgleich == false)
                                        goto case "BBR";
                                }
                            }
                            if (ausgleich == true) //wenn ausgleich erreicht wurde, muss nicht auf fg II untersucht werden
                                continue;
                            for (int k = 0; k < fg2Ind.Count; k++)
                            {
                                if (usedAusgleichsNoteInd >= fg2Ind[k]) //nur mit größeren notenindexen als den evtl bereits genutzten fortfahren
                                    continue;
                                if (noten.Zensur(fg2Ind[k]) < 4) //ist eine fg II note zum ausgleichen vorhanden?
                                {
                                    usedAusgleichsNoteInd = fg2Ind[k];
                                    ausgleich = true;
                                    break;
                                }
                            }
                            if (ausgleich == false) //konnte kein ausgleich erreicht werden, ist der abschluss nich erreicht
                                goto case "BBR";
                        }
                    abschluss = "EBR";
                    break;
                case "BBR":
                    abschluss = "BBR";
                    break;
            }
            return abschluss;
        }

        #endregion

        #region Datei Toolstrip

        private void neuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((!saved) && (DialogResult.Yes == MessageBox.Show("Wollen Sie die Änderungen speichern?", "Ungespeicherte Änderungen!", MessageBoxButtons.YesNo)))
                this.speichernToolStripMenuItem_Click(sender, e);
            for (int i = 1; i < reihenfolge.Length(); i++)
            {
                this.dgvAnsicht.Columns[fächer[i]].Visible = false;
                this.dgvAnsicht.Columns[fächer[i]].DefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet;
                this.dgvAnsicht.Columns["AB" + fächer[i]].Visible = false;
                this.dgvAnsicht.Columns["AB" + fächer[i]].DefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet;
            }
            dgvAnsicht.Rows.Clear();
            this.saved = true;
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((!saved) && (DialogResult.Yes == MessageBox.Show("Wollen Sie die Änderungen speichern?", "Ungespeicherte Änderungen!", MessageBoxButtons.YesNo)))
                this.speichernToolStripMenuItem_Click(sender, e);
            this.testForCells = false;
            ArrayList import = new ArrayList();
            ArrayList stringArrays = new ArrayList();
            import = this.Laden(false);
            if (import == null)
                return;
            SpeichernAvailable = true;

            foreach (string element in import)
                stringArrays.Add(Convert.ToString(element).Split(';'));

            reihenfolge.Clear();
            foreach (string element in (string[])stringArrays[0])
                this.reihenfolge.FächerImp(element);
            foreach (string element in (string[])stringArrays[1])
                this.reihenfolge.FächerIndImp(element);
            foreach (string element in (string[])stringArrays[2])
                this.reihenfolge.KurseImp(element);
            this.GridÄndern();

            this.dgvAnsicht.Rows.Insert(0, stringArrays.Count - 4);
            for (int i = 0; i < this.dgvAnsicht.ColumnCount; i++)
                for (int k = 3; k < stringArrays.Count; k++)
                    dgvAnsicht.Rows[k - 3].Cells[i].Value = ((string[])stringArrays[k])[i];
            saved = true;
            this.testForCells = true;
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SpeichernAvailable == false)
                speichernUnterToolStripMenuItem_Click(sender, e);
            else
                this.Speichern(false, this.InArraylistSchreiben());
        }

        private void speichernUnterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.SpeicherplatzWählen(false))
            {
                SpeichernAvailable = true;
                this.Speichern(false, this.InArraylistSchreiben());
            }
        }

        private void druckenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(this.dgvAnsicht);
        }

        public bool SpeicherplatzWählen(bool istOption)
        {
            ArrayList Dialogs = new ArrayList();
            Dialogs.Add(this.sfdFiles);
            Dialogs.Add(this.sfdOptions);

            this.sfdOptions.InitialDirectory = this.option;
            this.sfdFiles.InitialDirectory = this.haupt;
            foreach (SaveFileDialog element in Dialogs)
            {
                element.FileName = "Neue Fächerfolge";
                element.DefaultExt = "txt";
                element.Filter = "textfiles (*.txt)|*.txt";
            }
            if (istOption)
            {
                if (DialogResult.Cancel == this.sfdOptions.ShowDialog())
                    return false;
                else
                    return true;
            }
            else
            {
                if (DialogResult.Cancel == this.sfdFiles.ShowDialog())
                    return false;
                else
                    return true;
            }
        }

        public void Speichern(bool istOption, ArrayList save)
        {
            FileStream fs;
            if (istOption)
            {
                try
                {
                    FileStream fileStream = new FileStream(sfdOptions.FileName, FileMode.Create);
                    fs = fileStream;
                }
                catch
                {
                    MessageBox.Show("Datei kann nicht Überschrieben werden");
                    return;
                }
            }
            else
            {
                try
                {
                    FileStream fileStream = new FileStream(sfdFiles.FileName, FileMode.Create);
                    this.Text = System.IO.Path.GetFileName(sfdFiles.FileName);
                    fs = fileStream;
                }
                catch
                {
                    MessageBox.Show("datei kann nicht überschrieben werden");
                    return;
                }
            }

            StreamWriter streamWriter = new StreamWriter(fs);

            try
            {
                foreach (string text in save)
                    streamWriter.WriteLine(text.Remove(text.Length - 1));
                streamWriter.Close();
                fs.Close();
                saved = true;
            }
            catch
            {
                streamWriter.Close();
                fs.Close();
                MessageBox.Show("Error");
            }
        }

        public ArrayList InArraylistSchreiben()
        {
            ArrayList save = new ArrayList();
            string temp = "";

            for (int i = 0; i < this.reihenfolge.Length(); i++)
            {
                temp += this.reihenfolge.FächerExp(i) + ";";
            }
            save.Add(temp);
            temp = "";

            for (int i = 0; i < this.reihenfolge.Length(); i++)
            {
                temp += this.reihenfolge.FächerIndExp(i) + ";";
            }
            save.Add(temp);
            temp = "";

            for (int i = 0; i < this.reihenfolge.Length(); i++)
            {
                temp += this.reihenfolge.KurseExp(i) + ";";
            }
            save.Add(temp);
            temp = "";

            for (int i = 0; i < this.dgvAnsicht.RowCount; i++)
            {
                for (int k = 0; k < this.dgvAnsicht.ColumnCount; k++)
                {
                    temp += Convert.ToString(this.dgvAnsicht.Rows[i].Cells[k].Value) + ";";
                }
                save.Add(temp);
                temp = "";
            }
            return save;
        }

        public ArrayList Laden(bool istOption)
        {
            ArrayList list = new ArrayList();
            OpenFileDialog ofp = new OpenFileDialog();
            FileStream fs;
            if (istOption)
                ofp.InitialDirectory = this.option;
            else
                ofp.InitialDirectory = this.haupt;
            ofp.DefaultExt = "txt";
            ofp.Filter = "textfiles (*.txt)|*.txt";
            if (DialogResult.Cancel == ofp.ShowDialog())
                return null;

            try
            {
                FileStream fileStream = new FileStream(ofp.FileName, FileMode.Open);
                fs = fileStream;
            }
            catch
            {
                MessageBox.Show("Datei nicht gefunden");
                return null;
            }

            if (istOption)
                this.sfdOptions.FileName = ofp.FileName;
            else
            { 
                this.sfdFiles.FileName = ofp.FileName;
                this.Text = ofp.SafeFileName.ToString();
            }

            StreamReader streamReader = new StreamReader(fs);
            while (streamReader.Peek() != -1)
                list.Add(streamReader.ReadLine());
            streamReader.Close();
            fs.Close();
            return list;
        }

        #endregion

        #region Optionen Toolstrip

        private void fächerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options formularOptionen = new Options(this);
            formularOptionen.ShowDialog();
        }

        #endregion

        #region Hilfe Toolstrip

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Convert.ToString(Screen.FromControl(this).Bounds.Height));
            //MessageBox.Show(Convert.ToString(dgvAnsicht.ClientSize) + " " + Convert.ToString(dgvAnsicht.Columns.GetColumnsWidth(DataGridViewElementStates.Displayed)));
            //MessageBox.Show(this.dgvAnsicht.Rows[4].Cells[1].Style.Alignment.ToString());
            MessageBox.Show(this.dgvAnsicht.Columns.GetColumnCount(DataGridViewElementStates.Visible).ToString() + " " + this.dgvAnsicht.Columns[2].DisplayIndex.ToString() + " " + this.dgvAnsicht.Columns.Count.ToString());
        }

        #endregion



    }
}