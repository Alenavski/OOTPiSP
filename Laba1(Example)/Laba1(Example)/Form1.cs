using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba1_Example_
{
    public partial class Form1 : Form
    {
        public static Form1 window = null;
        public static Plugin _curr_Plugin;
        public static List<CatalogItem> Catalog = new List<CatalogItem>();
        public delegate void UpdateMethod(object obj, int index);

        private static string[] Names = {"Название", "Категория", "Цвет", "Ткань", "Длина", "Цена"};
        private static int[] Length = { 170, 120, 120, 150, 100, 100 };
        public static Creator[] Creators = { new TopCreator(), new SkirtCreator(), new TrousersCreator(), new OutwearCreator(), new DressCreator(), new JumpSuitCreator(), new SleevesCreator() };
        public static FileCreator[] FileCreators = { new BinFileCreator(), new JsonFileCreator(), new TextFileCreator() };

        public Form1()
        {
            InitializeComponent();
            window = this;
            ColumnHeader header;
            for (int i=0; i<6; i++)
            {
                header = new ColumnHeader();
                header.Text = Names[i];
                header.Width =Length[i];
                listView1.Columns.Add(header);
            }
            listView1.View = View.Details;
            foreach (string i in Enum.GetNames(typeof(CatalogItem.TCategorys)))
            {
                comboBox1.Items.Add(i);
            }
            saveFileDialog1.Filter = "Binary file|*.bin|Json file|*.json|Text file|*.txt";
            openFileDialog1.Filter = "Binary file|*.bin|Json file|*.json|Text file|*.txt";
        }

        public void AddObject(object Obj, int ind)
        {
            if (ind == -1)
            {
                Catalog.Add((CatalogItem)Obj);
            }
            else
            {
                Catalog.RemoveAt(ind);
                Catalog.Insert(ind, (CatalogItem)Obj);
            }  
            ShowListView();
        }

        private void ShowListView()
        {
            int i = 0;
            listView1.Items.Clear();
            foreach (CatalogItem item in Catalog)
            {
                if (item.Category != CatalogItem.TCategorys.Sleeves)
                {
                    AddLinetoListView();
                    listView1.Items[i].SubItems[0].Text = item.Name;
                    listView1.Items[i].SubItems[1].Text = item.Category.ToString();
                    listView1.Items[i].SubItems[2].Text = item.Color.ToString();
                    listView1.Items[i].SubItems[3].Text = item.Cloth;
                    listView1.Items[i].SubItems[4].Text = item.Length.ToString();
                    listView1.Items[i].SubItems[5].Text = item.Price.ToString();
                    i++;
                }
            }
        }

        public static void AddLinetoListView()
        {
            ListViewItem lvi = new ListViewItem();
            for (int i = 0; i < 6; i++)
            {
                ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
                lvi.SubItems.Add(lvsi);
            }
            Form1.window.listView1.Items.Add(lvi);
        }

        public static void Browse(object obj, int ind)
        {

        }

        public static object[] GetCommonData(TextBox Name,TextBox Cloth,TextBox Price,TextBox Length,ComboBox Color)
        {
            object[] values = new object[5];
            if (Name.Text != "" && Cloth.Text != "")
            {
                values[0] = (string)Name.Text;
                values[1] = (string)Cloth.Text;
            }
            else
            {
                MessageBox.Show("В одном из текстовых полей нет данных!!");
                return null;
            }
            if (Color.SelectedItem != null)
            {
                values[4] = (CatalogItem.TColor)Enum.Parse(typeof(CatalogItem.TColor), Color.SelectedItem.ToString());
            }
            else
            {
                MessageBox.Show("В одном из комбо полей нет данных!!");
                return null;
            }
            try
            {
                values[3] = Convert.ToDouble(Price.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Не верно введена стоимость");
                return null;
            }
            try
            {
                values[2] = Convert.ToInt32(Length.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Не верно введена длина");
                return null;
            }
            return values;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                object obj = null;
                Form form = Creators[comboBox1.SelectedIndex].Create(AddObject, obj, -1);
                form.Show();
            }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int j = 0;
            for (int i = 0; i <= Catalog.Count - 1; i++)
            {
                if (Catalog[i].Category != CatalogItem.TCategorys.Sleeves)
                {
                    if (listView1.Items[j].Selected)
                    {
                        int ind = comboBox1.Items.IndexOf(listView1.Items[j].SubItems[1].Text);
                        Form form = Creators[ind].Create(AddObject, Catalog[i], i);
                        form.Show();
                    }
                    j++;
                }
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int j = 0;
            for (int i = 0; i <= Catalog.Count - 1; i++)
            { 
                if (Catalog[i].Category != CatalogItem.TCategorys.Sleeves)
                {
                    if (listView1.Items[j].Selected)
                    {
                        Catalog.RemoveAt(i);
                        listView1.Items.RemoveAt(j);
                    }
                    j++;
                }
            }
        }

        private void просмотретьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int j = 0;
            for (int i = 0; i <= Catalog.Count - 1; i++)
            {
                if (Catalog[i].Category != CatalogItem.TCategorys.Sleeves)
                {
                    if (listView1.Items[j].Selected)
                    {
                        int ind = comboBox1.Items.IndexOf(listView1.Items[j].SubItems[1].Text);
                        Form form = Creators[ind].Create(Browse, Catalog[i], i);
                        form.Show();
                    }
                    j++;
                }
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            byte[] data=FileCreators[saveFileDialog1.FilterIndex - 1].FileSave(Catalog);
            ChoosePluginForm pluginForm = new ChoosePluginForm(data, filename);
            pluginForm.Show();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            byte[] serialized = null;
            byte[] data = null;
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                serialized = new byte[(int)fs.Length];
                fs.Read(serialized, 0, serialized.Length);
            }
            int res = Plugin.FindPlugin(filename);

            switch (res)
            {
                case -1:
                    MessageBox.Show("Соответствующий плагин отсутствует!!!");
                    return;
                case 1:
                    data = Plugin.ActivatePlugin(Form1._curr_Plugin, serialized, false);
                    break;
                case 0:
                    data = serialized;
                    break;
            }
            Catalog = FileCreators[openFileDialog1.FilterIndex - 1].FileOpen(data);
            ShowListView();
        }
    }
}
