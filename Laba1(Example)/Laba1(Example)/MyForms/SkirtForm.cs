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
    public partial class SkirtForm : Form
    {
        Form1.UpdateMethod Add_Display;
        int ObjectIndex;

        public SkirtForm(Form1.UpdateMethod method, object obj, int index)
        {
            InitializeComponent();
            foreach (string i in Enum.GetNames(typeof(CatalogItem.TColor)))
            {
                cmbColor.Items.Add(i);
            }
            foreach (string i in Enum.GetNames(typeof(Skirt.TSilhouetteType)))
            {
                cmbSilhouetteType.Items.Add(i);
            }
            foreach (string i in Enum.GetNames(typeof(Skirt.TCutType)))
            {
                cmbCutType.Items.Add(i);
            }
            if (obj != null)
            {
                txtName.Text = (obj as Skirt).Name;
                txtCloth.Text = (obj as Skirt).Cloth;
                txtLength.Text = (obj as Skirt).Length.ToString();
                txtPrice.Text = (obj as Skirt).Price.ToString();
                cmbColor.SelectedItem = (obj as Skirt).Color.ToString();
                chbPockets.Checked = (obj as Skirt).Pockets;
                txtWidth.Text = (obj as Skirt).Width.ToString();
                cmbSilhouetteType.SelectedItem= (obj as Skirt).SilhouetteType.ToString();
                cmbCutType.SelectedItem = (obj as Skirt).CutType.ToString();
            }
            Add_Display = method;
            ObjectIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool pockets;
            int width;
            Skirt.TCutType cuttype;
            Skirt.TSilhouetteType silhouettetype;
            if (cmbSilhouetteType.SelectedItem != null && cmbCutType.SelectedItem != null)
            {
                silhouettetype = (Skirt.TSilhouetteType)Enum.Parse(typeof(Skirt.TSilhouetteType), cmbSilhouetteType.SelectedItem.ToString());
                cuttype = (Skirt.TCutType)Enum.Parse(typeof(Skirt.TCutType), cmbCutType.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("В одном из комбо полей нет данных!!");
                return;
            }
            try
            {
                width = Convert.ToInt32(txtWidth.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Не верно введена длина");
                return;
            }
            pockets = chbPockets.Checked;
            object[] value = Form1.GetCommonData(txtName, txtCloth, txtPrice, txtLength, cmbColor);
            if (value != null)
            {
                Skirt top = new Skirt(silhouettetype,cuttype,pockets,width, (string)value[0], (string)value[1], (int)value[2], (double)value[3], (CatalogItem.TColor)value[4]);
                Add_Display(top, ObjectIndex);
                this.Close();
            }
        }
    }
}
