namespace TableSchemaGenerator
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && ( components != null ))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            dataGV = new DataGridView();
            chkbox = new DataGridViewCheckBoxColumn();
            Area = new DataGridViewTextBoxColumn();
            Database = new DataGridViewTextBoxColumn();
            Schema = new DataGridViewTextBoxColumn();
            TableName = new DataGridViewTextBoxColumn();
            TableComment = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            chkMergeFile = new CheckBox();
            cmbSourceType = new ComboBox();
            cmbDBType = new ComboBox();
            label3 = new Label();
            txtTblKeyword = new TextBox();
            btnQuery = new Button();
            btnGenerateDDL = new Button();
            menuStrip1 = new MenuStrip();
            設定ToolStripMenuItem = new ToolStripMenuItem();
            設定文件ToolStripMenuItem = new ToolStripMenuItem();
            說明HToolStripMenuItem = new ToolStripMenuItem();
            errorProvider = new ErrorProvider(components);
            label4 = new Label();
            openFileDialogSrc = new OpenFileDialog();
            txtFilePath = new TextBox();
            btnOpenSrcFile = new Button();
            label5 = new Label();
            txtDesPath = new TextBox();
            saveDesFileDialog = new SaveFileDialog();
            btnOpenDesFile = new Button();
            cmbSourceConfig = new ComboBox();
            label6 = new Label();
            chkIsAddDbOrOwner = new CheckBox();
            cmbArea = new ComboBox();
            label7 = new Label();
            cmbDataBase = new ComboBox();
            label8 = new Label();
            label9 = new Label();
            cmbSchema = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGV).BeginInit();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // dataGV
            // 
            dataGV.AllowUserToAddRows = false;
            dataGV.AllowUserToDeleteRows = false;
            dataGV.AllowUserToOrderColumns = true;
            dataGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGV.Columns.AddRange(new DataGridViewColumn[] { chkbox, Area, Database, Schema, TableName, TableComment });
            dataGV.Location = new Point(19, 178);
            dataGV.Name = "dataGV";
            dataGV.RowHeadersWidth = 51;
            dataGV.Size = new Size(1087, 400);
            dataGV.TabIndex = 6;
            dataGV.CellPainting += dataGV_CellPainting;
            // 
            // chkbox
            // 
            chkbox.DataPropertyName = "IsChecked";
            chkbox.Frozen = true;
            chkbox.HeaderText = "";
            chkbox.MinimumWidth = 6;
            chkbox.Name = "chkbox";
            chkbox.Width = 35;
            // 
            // Area
            // 
            Area.DataPropertyName = "Area";
            Area.Frozen = true;
            Area.HeaderText = "Area(類別)";
            Area.MinimumWidth = 6;
            Area.Name = "Area";
            Area.Width = 125;
            // 
            // Database
            // 
            Database.DataPropertyName = "DataBase";
            Database.Frozen = true;
            Database.HeaderText = "DataBase";
            Database.MinimumWidth = 6;
            Database.Name = "Database";
            Database.ReadOnly = true;
            Database.Width = 125;
            // 
            // Schema
            // 
            Schema.DataPropertyName = "Schema";
            Schema.Frozen = true;
            Schema.HeaderText = "Schema";
            Schema.MinimumWidth = 6;
            Schema.Name = "Schema";
            Schema.ReadOnly = true;
            Schema.Width = 125;
            // 
            // TableName
            // 
            TableName.DataPropertyName = "TableName";
            TableName.Frozen = true;
            TableName.HeaderText = "Table Name";
            TableName.MinimumWidth = 6;
            TableName.Name = "TableName";
            TableName.ReadOnly = true;
            TableName.Width = 200;
            // 
            // TableComment
            // 
            TableComment.DataPropertyName = "TableComment";
            TableComment.Frozen = true;
            TableComment.HeaderText = "Table Comment";
            TableComment.MinimumWidth = 6;
            TableComment.Name = "TableComment";
            TableComment.ReadOnly = true;
            TableComment.Width = 260;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 29);
            label1.Name = "label1";
            label1.Size = new Size(91, 15);
            label1.TabIndex = 0;
            label1.Text = "資料來源類型：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(759, 32);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 1;
            label2.Text = "資料庫類型：";
            // 
            // chkMergeFile
            // 
            chkMergeFile.AutoSize = true;
            chkMergeFile.Location = new Point(825, 84);
            chkMergeFile.Name = "chkMergeFile";
            chkMergeFile.Size = new Size(122, 19);
            chkMergeFile.TabIndex = 2;
            chkMergeFile.Text = "是否合併單一檔案";
            chkMergeFile.UseVisualStyleBackColor = true;
            // 
            // cmbSourceType
            // 
            cmbSourceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSourceType.DropDownWidth = 220;
            cmbSourceType.FlatStyle = FlatStyle.Popup;
            cmbSourceType.FormattingEnabled = true;
            cmbSourceType.Location = new Point(118, 26);
            cmbSourceType.Name = "cmbSourceType";
            cmbSourceType.Size = new Size(280, 23);
            cmbSourceType.TabIndex = 3;
            cmbSourceType.SelectedIndexChanged += cmbSourceType_SelectedIndexChanged;
            // 
            // cmbDBType
            // 
            cmbDBType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDBType.DropDownWidth = 200;
            cmbDBType.FlatStyle = FlatStyle.Flat;
            cmbDBType.FormattingEnabled = true;
            cmbDBType.Location = new Point(836, 29);
            cmbDBType.Name = "cmbDBType";
            cmbDBType.Size = new Size(280, 23);
            cmbDBType.TabIndex = 4;
            cmbDBType.SelectedIndexChanged += cmbDBType_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(5, 114);
            label3.Name = "label3";
            label3.Size = new Size(115, 15);
            label3.TabIndex = 5;
            label3.Text = "資料表關鍵字查詢：";
            // 
            // txtTblKeyword
            // 
            txtTblKeyword.Location = new Point(118, 111);
            txtTblKeyword.Name = "txtTblKeyword";
            txtTblKeyword.Size = new Size(614, 23);
            txtTblKeyword.TabIndex = 7;
            // 
            // btnQuery
            // 
            btnQuery.Location = new Point(825, 110);
            btnQuery.Name = "btnQuery";
            btnQuery.Size = new Size(75, 23);
            btnQuery.TabIndex = 8;
            btnQuery.Text = "查詢";
            btnQuery.UseVisualStyleBackColor = true;
            btnQuery.Click += btnQuery_Click;
            // 
            // btnGenerateDDL
            // 
            btnGenerateDDL.Location = new Point(906, 110);
            btnGenerateDDL.Name = "btnGenerateDDL";
            btnGenerateDDL.Size = new Size(75, 23);
            btnGenerateDDL.TabIndex = 9;
            btnGenerateDDL.Text = "產生DDL";
            btnGenerateDDL.UseVisualStyleBackColor = true;
            btnGenerateDDL.Click += btnGenerateDDL_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 設定ToolStripMenuItem, 說明HToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1118, 24);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // 設定ToolStripMenuItem
            // 
            設定ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 設定文件ToolStripMenuItem });
            設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            設定ToolStripMenuItem.Size = new Size(58, 20);
            設定ToolStripMenuItem.Text = "設定(&S)";
            // 
            // 設定文件ToolStripMenuItem
            // 
            設定文件ToolStripMenuItem.Name = "設定文件ToolStripMenuItem";
            設定文件ToolStripMenuItem.Size = new Size(122, 22);
            設定文件ToolStripMenuItem.Text = "設定文件";
            // 
            // 說明HToolStripMenuItem
            // 
            說明HToolStripMenuItem.Name = "說明HToolStripMenuItem";
            說明HToolStripMenuItem.Size = new Size(60, 20);
            說明HToolStripMenuItem.Text = "說明(&H)";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 58);
            label4.Name = "label4";
            label4.Size = new Size(91, 15);
            label4.TabIndex = 11;
            label4.Text = "資料來源路徑：";
            // 
            // openFileDialogSrc
            // 
            openFileDialogSrc.FileName = "openFileDialog";
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(118, 55);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.ReadOnly = true;
            txtFilePath.Size = new Size(614, 23);
            txtFilePath.TabIndex = 12;
            // 
            // btnOpenSrcFile
            // 
            btnOpenSrcFile.Location = new Point(738, 55);
            btnOpenSrcFile.Name = "btnOpenSrcFile";
            btnOpenSrcFile.Size = new Size(29, 23);
            btnOpenSrcFile.TabIndex = 13;
            btnOpenSrcFile.Text = "...";
            btnOpenSrcFile.UseVisualStyleBackColor = true;
            btnOpenSrcFile.Click += btnOpenSrcFile_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(29, 87);
            label5.Name = "label5";
            label5.Size = new Size(91, 15);
            label5.TabIndex = 5;
            label5.Text = "預設儲存路徑：";
            // 
            // txtDesPath
            // 
            txtDesPath.Location = new Point(118, 84);
            txtDesPath.Name = "txtDesPath";
            txtDesPath.ReadOnly = true;
            txtDesPath.Size = new Size(614, 23);
            txtDesPath.TabIndex = 7;
            // 
            // saveDesFileDialog
            // 
            saveDesFileDialog.FileName = "crtTableDDL.sql";
            saveDesFileDialog.Filter = "SQL檔|*.sql";
            // 
            // btnOpenDesFile
            // 
            btnOpenDesFile.Location = new Point(738, 84);
            btnOpenDesFile.Name = "btnOpenDesFile";
            btnOpenDesFile.Size = new Size(29, 23);
            btnOpenDesFile.TabIndex = 14;
            btnOpenDesFile.Text = "...";
            btnOpenDesFile.UseVisualStyleBackColor = true;
            btnOpenDesFile.Click += btnOpenDesFile_Click;
            // 
            // cmbSourceConfig
            // 
            cmbSourceConfig.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSourceConfig.DropDownWidth = 200;
            cmbSourceConfig.FlatStyle = FlatStyle.Flat;
            cmbSourceConfig.FormattingEnabled = true;
            cmbSourceConfig.Location = new Point(474, 26);
            cmbSourceConfig.Name = "cmbSourceConfig";
            cmbSourceConfig.Size = new Size(280, 23);
            cmbSourceConfig.TabIndex = 16;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(400, 29);
            label6.Name = "label6";
            label6.Size = new Size(79, 15);
            label6.TabIndex = 15;
            label6.Text = "來源設定檔：";
            // 
            // chkIsAddDbOrOwner
            // 
            chkIsAddDbOrOwner.AutoSize = true;
            chkIsAddDbOrOwner.Location = new Point(825, 60);
            chkIsAddDbOrOwner.Name = "chkIsAddDbOrOwner";
            chkIsAddDbOrOwner.Size = new Size(231, 19);
            chkIsAddDbOrOwner.TabIndex = 17;
            chkIsAddDbOrOwner.Text = "是否在資料表名前加Db/Owner Name";
            chkIsAddDbOrOwner.UseVisualStyleBackColor = true;
            // 
            // cmbArea
            // 
            cmbArea.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbArea.DropDownWidth = 220;
            cmbArea.FlatStyle = FlatStyle.Popup;
            cmbArea.FormattingEnabled = true;
            cmbArea.Location = new Point(118, 140);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(200, 23);
            cmbArea.TabIndex = 19;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(35, 143);
            label7.Name = "label7";
            label7.Size = new Size(77, 15);
            label7.TabIndex = 18;
            label7.Text = "Area(類別)：";
            // 
            // cmbDataBase
            // 
            cmbDataBase.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDataBase.DropDownWidth = 220;
            cmbDataBase.FlatStyle = FlatStyle.Popup;
            cmbDataBase.FormattingEnabled = true;
            cmbDataBase.Location = new Point(400, 140);
            cmbDataBase.Name = "cmbDataBase";
            cmbDataBase.Size = new Size(200, 23);
            cmbDataBase.TabIndex = 21;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(324, 143);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 20;
            label8.Text = "DataBase：";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(612, 146);
            label9.Name = "label9";
            label9.Size = new Size(64, 15);
            label9.TabIndex = 22;
            label9.Text = "Schema：";
            // 
            // cmbSchema
            // 
            cmbSchema.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSchema.DropDownWidth = 220;
            cmbSchema.FlatStyle = FlatStyle.Popup;
            cmbSchema.FormattingEnabled = true;
            cmbSchema.Location = new Point(682, 140);
            cmbSchema.Name = "cmbSchema";
            cmbSchema.Size = new Size(200, 23);
            cmbSchema.TabIndex = 23;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1118, 607);
            Controls.Add(cmbSchema);
            Controls.Add(label9);
            Controls.Add(cmbDataBase);
            Controls.Add(label8);
            Controls.Add(cmbArea);
            Controls.Add(label7);
            Controls.Add(chkIsAddDbOrOwner);
            Controls.Add(cmbSourceConfig);
            Controls.Add(label6);
            Controls.Add(btnOpenDesFile);
            Controls.Add(btnOpenSrcFile);
            Controls.Add(txtFilePath);
            Controls.Add(label4);
            Controls.Add(btnGenerateDDL);
            Controls.Add(btnQuery);
            Controls.Add(txtDesPath);
            Controls.Add(txtTblKeyword);
            Controls.Add(dataGV);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(cmbDBType);
            Controls.Add(cmbSourceType);
            Controls.Add(chkMergeFile);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "frmMain";
            Text = "Table Schema Generator";
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)dataGV).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private CheckBox chkMergeFile;
        private ComboBox cmbSourceType;
        private ComboBox cmbDBType;
        private Label label3;
        private DataGridView dataGV;
        private TextBox txtTblKeyword;
        private Button btnQuery;
        private Button btnGenerateDDL;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem 設定ToolStripMenuItem;
        private ToolStripMenuItem 說明HToolStripMenuItem;
        private ErrorProvider errorProvider;
        private ToolStripMenuItem 設定文件ToolStripMenuItem;
        private TextBox txtFilePath;
        private Label label4;
        private OpenFileDialog openFileDialogSrc;
        private Button btnOpenSrcFile;
        private TextBox txtDesPath;
        private Label label5;
        private Button btnOpenDesFile;
        private SaveFileDialog saveDesFileDialog;
        private DataGridViewCheckBoxColumn chkbox;
        private DataGridViewTextBoxColumn Area;
        private DataGridViewTextBoxColumn Database;
        private DataGridViewTextBoxColumn Schema;
        private DataGridViewTextBoxColumn TableName;
        private DataGridViewTextBoxColumn TableComment;
        private CheckBox chkIsAddDbOrOwner;
        private ComboBox cmbSourceConfig;
        private Label label6;
        private Label label9;
        private ComboBox cmbDataBase;
        private Label label8;
        private ComboBox cmbArea;
        private Label label7;
        private ComboBox cmbSchema;
    }
}
