using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;

namespace ZonBarcode
{
    public partial class MainForm : Form
    {
    
        public MainForm()
        {
            InitializeComponent();

        }


        private void MainForm_Load(object sender, EventArgs e)
        {

            Task.Factory.StartNew(() =>
            {

                DataStore.loadFiles();
                this.Invoke((MethodInvoker)(() => listBox1.DataSource = DataStore.Labels));
                this.Invoke((MethodInvoker)(() =>  listBox1.DisplayMember = "displayVal"));

            });


            Task.Factory.StartNew(() =>
            {

                Synchronizer s = new Synchronizer();
                s.SyncLoop();

            });





            if (String.IsNullOrEmpty(SecureLocalStore.getItem("SellerId")))
            {
                Form configForm = new ConfigurationForm();

                configForm.ShowDialog();

            }


            string val = SecureLocalStore.getItem("NumCopies");
            if(!string.IsNullOrEmpty(val))
            {
                this.Copies.Text = val;
            }

            val = SecureLocalStore.getItem("Condition");
            if (!string.IsNullOrEmpty(val))
            {
                this.Condition.Text = val;
            }


            val = SecureLocalStore.getItem("sizeChosen");
            if (!string.IsNullOrEmpty(val))
            {
                this.sizeChosen.Text = val;
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form configForm = new ConfigurationForm();

            configForm.Show();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex>=0)
                this.pictureBox1.Image = ((FNSkuLabel)listBox1.Items[listBox1.SelectedIndex]).getImage();
            

        }
        
        private void loadListBox()
        {

            this.Invoke((MethodInvoker)(() => listBox1.Items.Clear()));

            foreach(InventoryMember invMemb in DataStore.Inventory.Values)
            {
                Product p = DataStore.getProductByAsin(invMemb.ASIN);

                FNSkuLabel f = new FNSkuLabel(p.ASIN, invMemb.FNSKU, p.Title, "New", 3.0, 1.0);
                
                this.Invoke((MethodInvoker)(() => listBox1.Items.Add(f)));

                
            }


        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex >= 0)
            {

                int copies = 100;
                int.TryParse(this.Copies.Text, out copies);

                var label = (FNSkuLabel)listBox1.Items[listBox1.SelectedIndex];
                Pdf pdf = new Pdf();
                Double width = 3.0;
                Double height = 1.0;

                string[] sizeArr = sizeChosen.Text.Split('x');
                Double.TryParse(sizeArr[0], out height);
                Double.TryParse(sizeArr[1], out width);

                pdf.create(label, this.Condition.Text, copies, width, height);

                //var label = (FNSkuLabel)listBox1.Items[listBox1.SelectedIndex];

                //Pdf pdf = new Pdf();
                //pdf.create(pictureBox1.Image, label.Title, 3.0, 1.0);

             

            }
        }

        private void Copies_TextChanged(object sender, EventArgs e)
        {
            SecureLocalStore.storeItem("NumCopies",this.Copies.Text.ToString());
        }

        private void Condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            SecureLocalStore.storeItem("Condition", this.Condition.Text.ToString());

            
        }

        private void sizeChosen_SelectedIndexChanged(object sender, EventArgs e)
        {
            SecureLocalStore.storeItem("sizeChosen", this.sizeChosen.Text.ToString());

        }

      

        

    }
}
