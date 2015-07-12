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
    public partial class ConfigurationForm : Form
    {

        public ConfigurationForm()
        {
            InitializeComponent();

        }

        private void saveForm()
        {
            foreach (var component in this.Controls)
            {
                try
                {
                    if (component.GetType() == typeof(TextBox))
                    {
                        TextBox x = (TextBox)component;
                        SecureLocalStore.storeItem(x.Name, x.Text);

                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

            }

 
            



        }


        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            if (this.Controls != null)
            {
                foreach (var component in this.Controls)
                {
                    if (component.GetType() == typeof(TextBox))
                    {
                        TextBox x = (TextBox)component;
                        x.Text = SecureLocalStore.getItem(x.Name);
                    }
                }
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.saveForm();

            Task.Factory.StartNew(() =>
          {

              Synchronizer s = new Synchronizer();
              s.SyncData();
          });

            this.Close();

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            this.saveForm();

            this.lblTestResult.Text = "";

            MWSWebRequest req = new MWSWebRequest();
            string msg = "";

            if(req.isServiceUp(out msg))
            {
                this.lblTestResult.Text = msg;

            }
            else
            {
                this.lblTestResult.Text = msg;
            }
        }

    


 
    }
}
