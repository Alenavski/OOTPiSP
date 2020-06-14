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
    public partial class SleeveForm : Form
    {
        Form1.UpdateMethod Add_Display;
        int ObjectIndex;
        public SleeveForm(Form1.UpdateMethod method, object obj, int index)
        {
            InitializeComponent();
            foreach (string i in Enum.GetNames(typeof(CatalogItem.TColor)))
            {
                cmbColor.Items.Add(i);
            }
            if (obj != null)
            {
                txtName.Text = (obj as Sleeves).Name;
                txtCloth.Text = (obj as Sleeves).Cloth;
                txtLength.Text = (obj as Sleeves).Length.ToString();
                txtPrice.Text = (obj as Sleeves).Price.ToString();
                cmbColor.SelectedItem = (obj as Sleeves).Color.ToString();
                chbTransparancy.Checked = (obj as Sleeves).Transparancy;
                chbNeeded.Checked = (obj as Sleeves).isNeedAdding;
            }
            Add_Display = method;
            ObjectIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool transparancy, needed;
            transparancy = chbTransparancy.Checked;
            needed = chbNeeded.Checked;
            object[] value = Form1.GetCommonData(txtName, txtCloth, txtPrice, txtLength, cmbColor);
            if (value != null)
            {
               Sleeves dress = new Sleeves(transparancy,needed, (string)value[0], (string)value[1], (int)value[2], (double)value[3], (CatalogItem.TColor)value[4]);
                Add_Display(dress, ObjectIndex);
                this.Close();
            }
        }
    }
}
