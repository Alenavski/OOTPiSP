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
    public partial class OutwearForm : Form
    {
        Form1.UpdateMethod Add_Display;
        int ObjectIndex;

        public OutwearForm(Form1.UpdateMethod method, object obj, int index)
        {
            InitializeComponent();
            foreach (string i in Enum.GetNames(typeof(CatalogItem.TColor)))
            {
                cmbColor.Items.Add(i);
            }
            foreach (string i in Enum.GetNames(typeof(Outwear.TNecklineType)))
            {
                cmbNecklineType.Items.Add(i);
            }
            foreach (string i in Enum.GetNames(typeof(Outwear.TCollarType)))
            {
                cmbCollarType.Items.Add(i);
            }
            foreach (string i in Enum.GetNames(typeof(Outwear.TFastenerType)))
            {
                cmbFastenerType.Items.Add(i);
            }
            foreach (CatalogItem sleeve in Form1.Catalog)
            {
                if (sleeve.Category == CatalogItem.TCategorys.Sleeves)
                    cmbSleeves.Items.Add(sleeve.Name);
            }
            if (obj != null)
            {
                txtName.Text = (obj as Outwear).Name;
                txtCloth.Text = (obj as Outwear).Cloth;
                txtLength.Text = (obj as Outwear).Length.ToString();
                txtPrice.Text = (obj as Outwear).Price.ToString();
                cmbColor.SelectedItem = (obj as Outwear).Color.ToString();
                cmbSleeves.SelectedItem = (obj as Outwear).sleeves.Name;
                cmbNecklineType.SelectedItem = (obj as Outwear).NecklineType.ToString();
                cmbCollarType.SelectedItem= (obj as Outwear).CollarType.ToString();
                cmbFastenerType.SelectedItem = (obj as Outwear).FastenerType.ToString();
                chbBelt.Checked= (obj as Outwear).isBelt;
            }
            Add_Display = method;
            ObjectIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool belt;
            Outwear.TCollarType collar;
            Outwear.TFastenerType fastener;
            Outwear.TNecklineType neck;
            Sleeves sleeve;
            if (cmbNecklineType.SelectedItem != null && cmbCollarType.SelectedItem != null && cmbFastenerType.SelectedItem != null && cmbSleeves.SelectedItem != null)
            {
                neck = (Outwear.TNecklineType)Enum.Parse(typeof(Outwear.TNecklineType), cmbNecklineType.SelectedItem.ToString());
                collar = (Outwear.TCollarType)Enum.Parse(typeof(Outwear.TCollarType), cmbCollarType.SelectedItem.ToString());
                fastener = (Outwear.TFastenerType)Enum.Parse(typeof(Outwear.TFastenerType), cmbFastenerType.SelectedItem.ToString());
                sleeve = (Sleeves)Form1.Catalog.Find(x => x.Name == cmbSleeves.SelectedItem.ToString()&&x.Category==CatalogItem.TCategorys.Sleeves);
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
                Outwear outwear = new Outwear(belt,fastener,collar,sleeve, neck, (string)value[0], (string)value[1], (int)value[2], (double)value[3], (CatalogItem.TColor)value[4]);
                Add_Display(outwear, ObjectIndex);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object obj = null;
            Form form = Form1.Creators[6].Create(Form1.window.AddObject, obj, -1);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            object obj = Form1.Catalog.Find(x => x.Name == cmbSleeves.SelectedItem.ToString() && x.Category == CatalogItem.TCategorys.Sleeves);
            int ind = Form1.Catalog.IndexOf((Sleeves)obj);
            Form form = Form1.Creators[6].Create(Form1.window.AddObject, obj, ind);
            form.Show();
        }

        private void OutwearForm_Activated(object sender, EventArgs e)
        {
            object obj = cmbSleeves.SelectedItem;
            cmbSleeves.Items.Clear();
            foreach (CatalogItem sleeve in Form1.Catalog)
            {
                if (sleeve.Category == CatalogItem.TCategorys.Sleeves)
                    cmbSleeves.Items.Add(sleeve.Name);
            }
            cmbSleeves.SelectedItem = obj;
        }
    }
}
