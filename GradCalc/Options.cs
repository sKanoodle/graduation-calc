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

namespace GradCalc
{
    public partial class Options : Form
    {
        ComboBox[] comboBoxArray = new ComboBox[17];
        CheckBox[] checkBoxArray = new CheckBox[17];
        Form1 hauptfenster;
        bool saved = true;

        public Options(Form1 caller)
        {
            hauptfenster = caller;
            InitializeComponent();
        }

        private void konfigurationLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList list;
            string[] fächer;
            string[] kurse;
            list = hauptfenster.Laden(true);
            if (list == null)
                return;
            fächer = Convert.ToString(list[0]).Split(';');
            kurse = Convert.ToString(list[1]).Split(';');
            for (int i = 0; i < fächer.Length; i++)
            {
                this.comboBoxArray[i].SelectedIndex = Convert.ToInt32(fächer[i]);
                this.checkBoxArray[i].Checked = Convert.ToBoolean(kurse[i]);
            }
            this.saved = true;
        }

        private void konfigurationSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.KonfigurationSpeichern();
        }

        private void nurABKurseZurücksetzenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (CheckBox element in checkBoxArray)
                element.Checked = false;
            this.saved = false;
        }

        private void alleZurücksetzenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (CheckBox element in checkBoxArray)
                element.Checked = false;
            foreach (ComboBox element in comboBoxArray)
                element.SelectedIndex = 0;
            this.saved = false;
        }

        private void Options_Load(object sender, EventArgs e)
        {
            this.checkBoxArray[0] = cbKurs0;
            this.checkBoxArray[1] = cbKurs1;
            this.checkBoxArray[2] = cbKurs2;
            this.checkBoxArray[3] = cbKurs3;
            this.checkBoxArray[4] = cbKurs4;
            this.checkBoxArray[5] = cbKurs5;
            this.checkBoxArray[6] = cbKurs6;
            this.checkBoxArray[7] = cbKurs7;
            this.checkBoxArray[8] = cbKurs8;
            this.checkBoxArray[9] = cbKurs9;
            this.checkBoxArray[10] = cbKurs10;
            this.checkBoxArray[11] = cbKurs11;
            this.checkBoxArray[12] = cbKurs12;
            this.checkBoxArray[13] = cbKurs13;
            this.checkBoxArray[14] = cbKurs14;
            this.checkBoxArray[15] = cbKurs15;
            this.checkBoxArray[16] = cbKurs16;

            this.comboBoxArray[0] = cbFach0;
            this.comboBoxArray[1] = cbFach1;
            this.comboBoxArray[2] = cbFach2;
            this.comboBoxArray[3] = cbFach3;
            this.comboBoxArray[4] = cbFach4;
            this.comboBoxArray[5] = cbFach5;
            this.comboBoxArray[6] = cbFach6;
            this.comboBoxArray[7] = cbFach7;
            this.comboBoxArray[8] = cbFach8;
            this.comboBoxArray[9] = cbFach9;
            this.comboBoxArray[10] = cbFach10;
            this.comboBoxArray[11] = cbFach11;
            this.comboBoxArray[12] = cbFach12;
            this.comboBoxArray[13] = cbFach13;
            this.comboBoxArray[14] = cbFach14;
            this.comboBoxArray[15] = cbFach15;
            this.comboBoxArray[16] = cbFach16;

            this.ControlBox = false;

            foreach (ComboBox element in this.comboBoxArray)
            {
                element.BindingContext = new BindingContext();
                element.DataSource = hauptfenster.fächer;
                element.SelectedIndexChanged += new EventHandler(SelectedItemChanged);
            }

            foreach (CheckBox element in this.checkBoxArray)
                element.Enabled = false;

            if (hauptfenster.reihenfolge.Length() == 0)
                return;
            for (int i = 0; i < this.comboBoxArray.Length; i++)
            {
                this.comboBoxArray[i].SelectedIndex = hauptfenster.reihenfolge.FächerIndExp(i);
                this.checkBoxArray[i].Checked = hauptfenster.reihenfolge.KurseExp(i);
            }
            this.saved = true;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            foreach (ComboBox element in comboBoxArray)
                for (int i = 0; i < comboBoxArray.Length; i++)
                {
                    if (ReferenceEquals(element, comboBoxArray[i]))
                        continue;
                    if ((element.SelectedIndex != 0) && (element.SelectedIndex != comboBoxArray.Length) && (element.SelectedIndex == comboBoxArray[i].SelectedIndex))
                    {
                        MessageBox.Show("bitte keine Fächer doppelt eingeben");
                        return;
                    }
                }
            hauptfenster.reihenfolge.Clear();
            foreach (ComboBox element in comboBoxArray)
            {
                hauptfenster.reihenfolge.FächerImp(element.SelectedItem);
                hauptfenster.reihenfolge.FächerIndImp(element.SelectedIndex);
            }
            foreach (CheckBox element in checkBoxArray)
                hauptfenster.reihenfolge.KurseImp(element.Checked);
            hauptfenster.GridÄndern();
            if ((saved == false) && (DialogResult.Yes == MessageBox.Show("Wollen Sie die Konfiguration vor dem Fortfahren speichern?", "Achtung!", MessageBoxButtons.YesNo)))
                this.KonfigurationSpeichern();
            this.Close();
        }

        private void SelectedItemChanged(object sender, EventArgs e)
        {
            int position = -1;
            this.saved = false;
            for (int i = 0; i < this.comboBoxArray.Length; i++)
                if ((object.ReferenceEquals(sender, this.comboBoxArray[i])) && (this.comboBoxArray[i].SelectedIndex == this.comboBoxArray.Length))
                {
                    position = i + 1;
                    break;
                }
            if (position >= 0)
                for (int i = position; i < this.comboBoxArray.Length; i++)
                    this.comboBoxArray[i].SelectedIndex = this.comboBoxArray.Length;
            for (int i = 0; i < this.comboBoxArray.Length; i++)
                if ((this.comboBoxArray[i].SelectedIndex == this.comboBoxArray.Length) || (this.comboBoxArray[i].SelectedIndex == 0))
                {
                    this.checkBoxArray[i].Enabled = false;
                    this.checkBoxArray[i].Checked = false;
                }
                else
                    this.checkBoxArray[i].Enabled = true;
        }

        private void KonfigurationSpeichern()
        {
            if (hauptfenster.SpeicherplatzWählen(true))
            {
                ArrayList save = new ArrayList();
                string temp = "";
                foreach (ComboBox element in this.comboBoxArray)
                    temp += element.SelectedIndex + ";";
                save.Add(temp);
                temp = "";
                foreach (CheckBox element in this.checkBoxArray)
                    temp += element.Checked + ";";
                save.Add(temp);
                hauptfenster.Speichern(true, save);
                this.saved = true;
            }
        }

    }
}
