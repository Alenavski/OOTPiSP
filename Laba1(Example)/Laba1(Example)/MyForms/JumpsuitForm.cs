using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba1_Example_.MyForms
{
    public partial class JumpsuitForm : Form
    {
        Form1.UpdateMethod Add_Display;
        int ObjectIndex;

        public JumpsuitForm(Form1.UpdateMethod method, object obj, int index)
        {
            InitializeComponent();
            foreach (string i in Enum.GetNames(typeof(CatalogItem.TColor)))
            {
                cmbColor.Items.Add(i);
            }
            foreach (string i in Enum.GetNames(typeof(JumpSuit.TDecorBelt)))
            {
                cmbDecoreBelt.Items.Add(i);
            }
            foreach (CatalogItem item in Form1.Catalog)
            {
                if (item.Category == CatalogItem.TCategorys.Top)
                    cmbTop.Items.Add(item.Name);
                if (item.Category == CatalogItem.TCategorys.Trousers)
                    cmbTrousers.Items.Add(item.Name);
            }
            if (obj != null)
            {
                txtName.Text = (obj as JumpSuit).Name;
                txtCloth.Text = (obj as JumpSuit).Cloth;
                txtLength.Text = (obj as JumpSuit).Length.ToString();
                txtPrice.Text = (obj as JumpSuit).Price.ToString();
                cmbColor.SelectedItem = (obj as JumpSuit).Color.ToString();
                cmbTop.SelectedItem = (obj as JumpSuit).top.Name;
                cmbTrousers.SelectedItem = (obj as JumpSuit).trousers.Name;
                cmbDecoreBelt.SelectedItem = (obj as JumpSuit).DecorBelt.ToString();
            }
            Add_Display = method;
            ObjectIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JumpSuit.TDecorBelt decore;
            Top top;
            Trousers trousers;
            if (cmbTop.SelectedItem != null && cmbTrousers.SelectedItem != null && cmbDecoreBelt.SelectedItem != null)
            {
                decore = (JumpSuit.TDecorBelt)Enum.Parse(typeof(JumpSuit.TDecorBelt), cmbDecoreBelt.SelectedItem.ToString());
                top = (Top)Form1.Catalog.Find(x => x.Name == cmbTop.SelectedItem.ToString() && x.Category == CatalogItem.TCategorys.Top);
                trousers = (Trousers)Form1.Catalog.Find(x => x.Name == cmbTrousers.SelectedItem.ToString() && x.Category == CatalogItem.TCategorys.Trousers);
            }
            else
            {
                MessageBox.Show("В одном из комбо полей нет данных!!");
                return;
            }
            object[] value = Form1.GetCommonData(txtName, txtCloth, txtPrice, txtLength, cmbColor);
            if (value != null)
            {
                JumpSuit jumpSuit = new JumpSuit(top, trousers, decore, (string)value[0], (string)value[1], (int)value[2], (double)value[3], (CatalogItem.TColor)value[4]);
                Add_Display(jumpSuit, ObjectIndex);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object obj = null;
            Form form = Form1.Creators[0].Create(Form1.window.AddObject, obj, -1);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            object obj = null;
            Form form = Form1.Creators[2].Create(Form1.window.AddObject, obj, -1);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            object obj = Form1.Catalog.Find(x => x.Name == cmbTop.SelectedItem.ToString()&&x.Category == CatalogItem.TCategorys.Top);
            int ind = (int)(obj as Top).Category;
            int i = Form1.Catalog.IndexOf((Top)obj);
            Form form = Form1.Creators[ind].Create(Form1.window.AddObject, obj, i);
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            object obj = Form1.Catalog.Find(x => x.Name == cmbTrousers.SelectedItem.ToString()&&x.Category == CatalogItem.TCategorys.Trousers);
            int ind = (int)(obj as Trousers).Category;
            int i = Form1.Catalog.IndexOf((Trousers)obj);
            Form form = Form1.Creators[ind].Create(Form1.window.AddObject, obj, i);
            form.Show();
        }

        private void JumpsuitForm_Activated(object sender, EventArgs e)
        {
            object obj1 = cmbTop.SelectedItem;
            object obj2 = cmbTrousers.SelectedItem;
            cmbTop.Items.Clear();
            cmbTrousers.Items.Clear();
            foreach (CatalogItem item in Form1.Catalog)
            {
                if (item.Category == CatalogItem.TCategorys.Top)
                    cmbTop.Items.Add(item.Name);
                if (item.Category == CatalogItem.TCategorys.Trousers)
                    cmbTrousers.Items.Add(item.Name);
            }
            cmbTop.SelectedItem = obj1;
            cmbTrousers.SelectedItem = obj2;
        }
    }
}
