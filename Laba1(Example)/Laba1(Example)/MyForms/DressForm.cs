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
    public partial class DressForm : Form
    {
        Form1.UpdateMethod Add_Display;
        int ObjectIndex;

        public DressForm(Form1.UpdateMethod method, object obj, int index)
        {
            InitializeComponent();
            foreach (string i in Enum.GetNames(typeof(CatalogItem.TColor)))
            {
                cmbColor.Items.Add(i);
            }
            foreach (string i in Enum.GetNames(typeof(Dress.TDecorShoulders)))
            {
                cmbDecoreShoulders.Items.Add(i);
            }
            foreach (CatalogItem item in Form1.Catalog)
            {
                if (item.Category==CatalogItem.TCategorys.Top)
                    cmbTop.Items.Add(item.Name);
                if (item.Category == CatalogItem.TCategorys.Skirt)
                    cmbSkirt.Items.Add(item.Name);
            }
            if (obj != null)
            {
                txtName.Text = (obj as Dress).Name;
                txtCloth.Text = (obj as Dress).Cloth;
                txtLength.Text = (obj as Dress).Length.ToString();
                txtPrice.Text = (obj as Dress).Price.ToString();
                cmbColor.SelectedItem = (obj as Dress).Color.ToString();
                cmbTop.SelectedItem = (obj as Dress).top.Name;
                cmbSkirt.SelectedItem = (obj as Dress).skirt.Name;
                cmbDecoreShoulders.SelectedItem = (obj as Dress).DecorShoulders.ToString();
                chbBelt.Checked = (obj as Dress).isBelt;
            }
            Add_Display = method;
            ObjectIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dress.TDecorShoulders decore;
            bool belt;
            Top top;
            Skirt skirt;
            if (cmbTop.SelectedItem != null && cmbSkirt.SelectedItem != null && cmbDecoreShoulders.SelectedItem != null)
            {
                decore = (Dress.TDecorShoulders)Enum.Parse(typeof(Dress.TDecorShoulders), cmbDecoreShoulders.SelectedItem.ToString());
                top = (Top)Form1.Catalog.Find(x => x.Name == cmbTop.SelectedItem.ToString()&& x.Category == CatalogItem.TCategorys.Top);
                skirt = (Skirt)Form1.Catalog.Find(x => x.Name == cmbSkirt.SelectedItem.ToString() && x.Category == CatalogItem.TCategorys.Skirt);
            }
            else
            {
                MessageBox.Show("В одном из комбо полей нет данных!!");
                return;
            }
            belt = chbBelt.Checked;
            object[] value = Form1.GetCommonData(txtName, txtCloth, txtPrice, txtLength, cmbColor);
            if (value != null)
            {
                Dress dress = new Dress(top,skirt,belt,decore,(string)value[0], (string)value[1], (int)value[2], (double)value[3], (CatalogItem.TColor)value[4]);
                Add_Display(dress, ObjectIndex);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object obj = null;
            Form form = Form1.Creators[0].Create(Form1.window.AddObject, obj,-1);
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            object obj = null;
            Form form = Form1.Creators[1].Create(Form1.window.AddObject, obj,-1);
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
            object obj = Form1.Catalog.Find(x => x.Name == cmbSkirt.SelectedItem.ToString()&&x.Category == CatalogItem.TCategorys.Skirt);
            int ind = (int)(obj as Skirt).Category;
            int i = Form1.Catalog.IndexOf((Skirt)obj);
            Form form = Form1.Creators[ind].Create(Form1.window.AddObject, obj, i);
            form.Show();
        }

        private void DressForm_Activated(object sender, EventArgs e)
        {
            object obj1 = cmbTop.SelectedItem;
            object obj2 = cmbSkirt.SelectedItem;
            cmbTop.Items.Clear();
            cmbSkirt.Items.Clear();
            foreach (CatalogItem item in Form1.Catalog)
            {
                if (item.Category == CatalogItem.TCategorys.Top)
                    cmbTop.Items.Add(item.Name);
                if (item.Category == CatalogItem.TCategorys.Skirt)
                    cmbSkirt.Items.Add(item.Name);
            }
            cmbTop.SelectedItem = obj1;
            cmbSkirt.SelectedItem = obj2;
        }

    }
}
