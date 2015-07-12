namespace ZonBarcode
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SellerId = new System.Windows.Forms.TextBox();
            this.sellerIDLabel = new System.Windows.Forms.Label();
            this.MarketplaceId_label = new System.Windows.Forms.Label();
            this.MarketplaceId = new System.Windows.Forms.TextBox();
            this.AccessKey_Label = new System.Windows.Forms.Label();
            this.AccessKey = new System.Windows.Forms.TextBox();
            this.SecretKey_Label = new System.Windows.Forms.Label();
            this.SecretKey = new System.Windows.Forms.TextBox();
            this.AuthToken_Label = new System.Windows.Forms.Label();
            this.AuthToken = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblTestResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SellerId
            // 
            this.SellerId.Location = new System.Drawing.Point(157, 61);
            this.SellerId.Name = "SellerId";
            this.SellerId.Size = new System.Drawing.Size(345, 20);
            this.SellerId.TabIndex = 0;
            // 
            // sellerIDLabel
            // 
            this.sellerIDLabel.AutoSize = true;
            this.sellerIDLabel.Location = new System.Drawing.Point(94, 67);
            this.sellerIDLabel.Name = "sellerIDLabel";
            this.sellerIDLabel.Size = new System.Drawing.Size(47, 13);
            this.sellerIDLabel.TabIndex = 1;
            this.sellerIDLabel.Text = "Seller ID";
            // 
            // MarketplaceId_label
            // 
            this.MarketplaceId_label.AutoSize = true;
            this.MarketplaceId_label.Location = new System.Drawing.Point(76, 105);
            this.MarketplaceId_label.Name = "MarketplaceId_label";
            this.MarketplaceId_label.Size = new System.Drawing.Size(75, 13);
            this.MarketplaceId_label.TabIndex = 3;
            this.MarketplaceId_label.Text = "MarketplaceId";
            // 
            // MarketplaceId
            // 
            this.MarketplaceId.Location = new System.Drawing.Point(157, 98);
            this.MarketplaceId.Name = "MarketplaceId";
            this.MarketplaceId.Size = new System.Drawing.Size(345, 20);
            this.MarketplaceId.TabIndex = 1;
            // 
            // AccessKey_Label
            // 
            this.AccessKey_Label.AutoSize = true;
            this.AccessKey_Label.Location = new System.Drawing.Point(76, 141);
            this.AccessKey_Label.Name = "AccessKey_Label";
            this.AccessKey_Label.Size = new System.Drawing.Size(77, 13);
            this.AccessKey_Label.TabIndex = 5;
            this.AccessKey_Label.Text = "Access Key ID";
            // 
            // AccessKey
            // 
            this.AccessKey.Location = new System.Drawing.Point(157, 134);
            this.AccessKey.Name = "AccessKey";
            this.AccessKey.Size = new System.Drawing.Size(345, 20);
            this.AccessKey.TabIndex = 2;
            // 
            // SecretKey_Label
            // 
            this.SecretKey_Label.AutoSize = true;
            this.SecretKey_Label.Location = new System.Drawing.Point(76, 176);
            this.SecretKey_Label.Name = "SecretKey_Label";
            this.SecretKey_Label.Size = new System.Drawing.Size(59, 13);
            this.SecretKey_Label.TabIndex = 7;
            this.SecretKey_Label.Text = "Secret Key";
            // 
            // SecretKey
            // 
            this.SecretKey.Location = new System.Drawing.Point(157, 169);
            this.SecretKey.Name = "SecretKey";
            this.SecretKey.Size = new System.Drawing.Size(345, 20);
            this.SecretKey.TabIndex = 3;
            // 
            // AuthToken_Label
            // 
            this.AuthToken_Label.AutoSize = true;
            this.AuthToken_Label.Location = new System.Drawing.Point(76, 214);
            this.AuthToken_Label.Name = "AuthToken_Label";
            this.AuthToken_Label.Size = new System.Drawing.Size(60, 13);
            this.AuthToken_Label.TabIndex = 9;
            this.AuthToken_Label.Text = "AuthToken";
            // 
            // AuthToken
            // 
            this.AuthToken.Location = new System.Drawing.Point(157, 207);
            this.AuthToken.Name = "AuthToken";
            this.AuthToken.Size = new System.Drawing.Size(345, 20);
            this.AuthToken.TabIndex = 4;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(393, 264);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(79, 264);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblTestResult
            // 
            this.lblTestResult.AutoSize = true;
            this.lblTestResult.Location = new System.Drawing.Point(76, 302);
            this.lblTestResult.Name = "lblTestResult";
            this.lblTestResult.Size = new System.Drawing.Size(100, 13);
            this.lblTestResult.TabIndex = 12;
            this.lblTestResult.Text = "                               ";
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 382);
            this.Controls.Add(this.lblTestResult);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.AuthToken_Label);
            this.Controls.Add(this.AuthToken);
            this.Controls.Add(this.SecretKey_Label);
            this.Controls.Add(this.SecretKey);
            this.Controls.Add(this.AccessKey_Label);
            this.Controls.Add(this.AccessKey);
            this.Controls.Add(this.MarketplaceId_label);
            this.Controls.Add(this.MarketplaceId);
            this.Controls.Add(this.sellerIDLabel);
            this.Controls.Add(this.SellerId);
            this.Name = "ConfigurationForm";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.ConfigurationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SellerId;
        private System.Windows.Forms.Label sellerIDLabel;
        private System.Windows.Forms.Label MarketplaceId_label;
        private System.Windows.Forms.TextBox MarketplaceId;
        private System.Windows.Forms.Label AccessKey_Label;
        private System.Windows.Forms.TextBox AccessKey;
        private System.Windows.Forms.Label SecretKey_Label;
        private System.Windows.Forms.TextBox SecretKey;
        private System.Windows.Forms.Label AuthToken_Label;
        private System.Windows.Forms.TextBox AuthToken;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label lblTestResult;

    }
}

