using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba1_Example_
{
    public partial class TopForm : Form
    {
        Form1.UpdateMethod Add_Display;
        int ObjectIndex;
        public TopForm(Form1.UpdateMethod method, object obj, int index)
        {
            InitializeComponent();
            foreach (string i in Enum.GetNames(typeof(CatalogItem.TColor)))
            {
               cmbColor.Items.Add(i);
            }
            foreach (string i in Enum.GetNames(typeof(Top.TNecklineType)))
            {
                cmbNecklineType.Items.Add(i);
            }
            foreach (Sleeves sleeve in Form1.CatalogSleeves)
            {
                cmbSleeves.Items.Add(sleeve.Name);
            }
            if (obj != null)
            {
                txtName.Text = (obj as Top).Name;
                txtCloth.Text = (obj as Top).Cloth;
                txtLength.Text = (obj as Top).Length.ToString();
                txtPrice.Text= (obj as Top).Price.ToString();
                cmbColor.SelectedItem = (obj as Top).Color.ToString();
                cmbSleeves.SelectedItem= (obj as Top).sleeves.Name;
                cmbNecklineType.SelectedItem = (obj as Top).NecklineType.ToString();
            }
            Add_Display = method;
            ObjectIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Top.TNecklineType neck;
            Sleeves sleeve;
            if (cmbNecklineType.SelectedItem != null&& cmbSleeves.SelectedItem != null)
            {
               neck= (Top.TNecklineType)Enum.Parse(typeof(Top.TNecklineType), cmbNecklineType.SelectedItem.ToString());
               sleeve=Form1.CatalogSleeves.Find(x => x.Name == cmbSleeves.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("В одном из комбо полей нет данных!!");
                return;
            }
            object[] value=Form1.GetCommonData(txtName, txtCloth, txtPrice, txtLength, cmbColor);
            if (value != null)
            {
                Top top = new Top(sleeve, neck, (string)value[0], (string)value[1], (int)value[2], (double)value[3], (CatalogItem.TColor)value[4]);
                Add_Display(top, ObjectIndex);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object obj = null;
            Form form=Form1.Creators[6].Create(Form1.AddSleeve, obj, Form1.CatalogSleeves.Count);
            form.Show();
        }

        private void TopForm_Activated(object sender, EventArgs e)
        {
            object obj = cmbSleeves.SelectedItem;
            cmbSleeves.Items.Clear();
            foreach (Sleeves sleeve in Form1.CatalogSleeves)
            {
                cmbSleeves.Items.Add(sleeve.Name);
            }
            cmbSleeves.SelectedItem = obj;       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Form1.CatalogSleeves.Exists(x => x.Name == cmbSleeves.SelectedItem.ToString()))
            {
                object obj = Form1.CatalogSleeves.Find(x => x.Name == cmbSleeves.SelectedItem.ToString());
                int ind = Form1.CatalogSleeves.IndexOf((Sleeves)obj);
                Form form = Form1.Creators[6].Create(Form1.AddSleeve, obj, Form1.CatalogSleeves.Count);
                form.Show();
            }
        }
    }
}
