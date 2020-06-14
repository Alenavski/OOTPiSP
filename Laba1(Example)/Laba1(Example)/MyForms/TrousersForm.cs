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
    public partial class TrousersForm : Form
    {
        Form1.UpdateMethod Add_Display;
        int ObjectIndex;

        public TrousersForm(Form1.UpdateMethod method, object obj, int index)
        {
            InitializeComponent();
            foreach (string i in Enum.GetNames(typeof(CatalogItem.TColor)))
            {
                cmbColor.Items.Add(i);
            }
            if (obj != null)
            {
                txtName.Text = (obj as Trousers).Name;
                txtCloth.Text = (obj as Trousers).Cloth;
                txtLength.Text = (obj as Trousers).Length.ToString();
                txtPrice.Text = (obj as Trousers).Price.ToString();
                cmbColor.SelectedItem = (obj as Trousers).Color.ToString();
                chbPockets.Checked = (obj as Trousers).Pockets;
                chbTwists.Checked = (obj as Trousers).Twists;
                chbArrows.Checked = (obj as Trousers).Arrows;
                chbHoles.Checked = (obj as Trousers).Holes;
            }
            Add_Display = method;
            ObjectIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool pockets, twists, arrows, holes;
            pockets = chbPockets.Checked;
            twists = chbTwists.Checked;
            arrows = chbArrows.Checked;
            holes = chbHoles.Checked;
            object[] value = Form1.GetCommonData(txtName, txtCloth, txtPrice, txtLength, cmbColor);
            if (value != null)
            {
                Trousers top = new Trousers(pockets, twists, arrows, holes,(string)value[0], (string)value[1], (int)value[2], (double)value[3], (CatalogItem.TColor)value[4]);
                Add_Display(top, ObjectIndex);
                this.Close();
            }
        }
    }
}
