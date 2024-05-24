using CommonLibrary;
using SchemaGenerator.BaseFactory;
using SchemaGenerator.BaseFactory.Models;
using System.Text.Json;
using TableSchemaGenerator.Model;

namespace TableSchemaGenerator
{
    public partial class frmMain : Form
    {
        #region Form fields & attributes    
        private List<string> _sourceReaderDLLs;
        private List<string> _schemaGeneratorDLLs;
        private SourceReaderBase _sourceReader;
        private SchemaGeneratorBase _schemaGenerator;
        private AppData _appData;
        private AppConfiguration _appConfig;
        private List<TableSummary> _tableSummaries;
        private List<TableSchema> _tableSchemas;
        private string srcFile
        {
            get
            {
                return this.txtFilePath.Text;
            }
            set
            {
                this.txtFilePath.Text = value;
            }
        }
        private string desFilePath
        {
            get
            {
                return this.txtDesPath.Text;
            }
            set
            {
                this.txtDesPath.Text = value;
            }
        }
        private readonly string _basePath = System.AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            this._sourceReaderDLLs = new List<string>();
            this._schemaGeneratorDLLs = new List<string>();
            this._appData = new AppData();
            this._appConfig = new AppConfiguration();
            this._tableSummaries = new List<TableSummary>();
            this._tableSchemas = new List<TableSchema>();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            this.initialAppData();
            this.bindingControl();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (!this.valSourceControl())
                return;
            try
            {
                if (this._sourceReader == null)
                    this._sourceReader ??= new SourceReaderFactory(this.cmbSourceType.SelectedValue!.ToString()!, this.cmbSourceConfig.SelectedValue!.ToString()).CreateDocumentReaderInstance(this.srcFile);
                if (this._tableSummaries.Count == 0)
                {
                    this._tableSummaries = this._sourceReader.GetTableSummaries();
                    this.setComboboxData(this.cmbArea, this._tableSummaries.DistinctBy(x => x.Area).Select(x => new DataItem { Type = x.Area, Name = x.Area }).ToList());
                    this.setComboboxData(this.cmbDataBase, this._tableSummaries.DistinctBy(x => x.DataBase).Select(x => new DataItem { Type = x.DataBase, Name = x.DataBase }).ToList());
                    this.setComboboxData(this.cmbSchema, this._tableSummaries.DistinctBy(x => x.Schema).Select(x => new DataItem { Type = x.Schema, Name = x.Schema }).ToList());
                }
                this.bindDataToDataGridView(this.filterTableSummary());

            }
            catch (NullReferenceException ex)
            {
                this._sourceReader = null;
                this.errorProvider.SetError(this.cmbSourceType, "請確認『資料來源類型』是否正確！");
                this.errorProvider.SetError(this.cmbSourceConfig, "請確認『來源設定檔』是否正確！");
                this.errorProvider.SetError(this.btnOpenSrcFile, "請確認『資料來源路徑』是否正確！");
                this.alertWarningMsg($"請確認以下項目：\n\t-『資料來源類型』\n\t-『來源設定檔』\n\t-『資料來源路徑』\n是否正確！\n\n {ex.Message}");
            }
            catch (Exception ex)
            {
                this._sourceReader = null;
                this.alertWarningMsg(ex.Message);
            }
        }

        private void btnGenerateDDL_Click(object sender, EventArgs e)
        {
            if (!this.valGeneratorControl())
                return;

            List<string> list = this._tableSummaries.FindAll(x => x.IsChecked).Select(x => x.TableName).ToList();
            if (!this.valCheckCnt(list.Count))
                return;

            //產生 Generator instance
            if (this._schemaGenerator == null)
                this._schemaGenerator = new SchemaGeneratorFactory(this.cmbDBType.SelectedValue!.ToString()!).CreateSchemaGeneratorInstance();

            this._schemaGenerator.SetData(this._tableSummaries.FindAll(x => x.IsChecked), this._sourceReader.GetTableSchema(list), this.chkMergeFile.Checked, this.chkIsAddDbOrOwner.Checked);

            //產生DDL
            try
            {
                FileInfo fo = new FileInfo(this.saveDesFileDialog.FileName);
                this._schemaGenerator.GeneratorCreateTableDDL(this.desFilePath, fo.Name);
                this.alertMsg("DDL SQL 檔案已產生完成！");
            }
            catch (Exception ex)
            {
                this.alertErrorMsg($"產生DDL失敗！\r\n{ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }


        private void btnOpenSrcFile_Click(object sender, EventArgs e)
        {
            this.openFileDialogSrc.InitialDirectory = new FileInfo(this.txtFilePath.Text).DirectoryName;
            if (this.openFileDialogSrc.ShowDialog(this) == DialogResult.OK)
            {
                this.srcFile = this.openFileDialogSrc.FileName;
                this.txtFilePath.Text = this.srcFile;
                this._sourceReader = null;
                this.dataGV.DataSource = null;
                this._tableSummaries.Clear();
                this._tableSchemas.Clear();
            }
        }

        private void btnOpenDesFile_Click(object sender, EventArgs e)
        {
            this.saveDesFileDialog.InitialDirectory = this.desFilePath;
            if (this.saveDesFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo fo = new FileInfo(this.saveDesFileDialog.FileName);
                this.desFilePath = $"{fo.DirectoryName}\\";
            }
        }

        private void cmbSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._sourceReader is not null && ( DataTrans.CString(this.cmbSourceType.SelectedValue) != DataTrans.CString(this._sourceReader._appConfiguration.DataSourceType) ))
                this._sourceReader = null;

            if (DataTrans.CString(this.cmbSourceType.SelectedValue) != "")
                this.setComboboxData(this.cmbSourceConfig, this.mapData(this.getFile($"{_basePath}App_Data", $"SourceReader.{this.cmbSourceType.SelectedValue}*.json"), null));
        }

        private void cmbDBType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._schemaGenerator is not null && ( DataTrans.CString(this.cmbDBType.SelectedValue) != this._schemaGenerator.DBType ))
            {
                this._schemaGenerator = null;
            }
        }

        #region  funcitons

        private void initialAppData()
        {
            string jsonStr = File.ReadAllText($@"{_basePath}App_Data\AppData.json");
            this._appData = JsonSerializer.Deserialize<AppData>(jsonStr);
            this.srcFile = $@"{_basePath}Template\SDM_MIS.xlsx";
            this.desFilePath = $@"{_basePath}{this._appData.DefaultDestinatinalPath}\";
            if (!Directory.Exists(this.desFilePath))
                Directory.CreateDirectory(this.desFilePath);
            this._sourceReaderDLLs = this.getFile($"{_basePath}Extension", "SourceReader.*.dll", 1);
            this._schemaGeneratorDLLs = this.getFile($"{_basePath}Extension", "SchemaGenerator.*.dll", 1);
        }

        private void bindingControl()
        {
            List<DataItem> lstDocumentTypes = this.mapData(this._sourceReaderDLLs, this._appData.SourceTypes);
            List<DataItem> lstDBTypes = this.mapData(this._schemaGeneratorDLLs, this._appData.DatabaseTypes);
            this.setComboboxData(this.cmbSourceType, lstDocumentTypes);
            this.setComboboxData(this.cmbDBType, lstDBTypes);
            this.setComboboxData(this.cmbSourceConfig);
            this.setComboboxData(this.cmbArea);
            this.setComboboxData(this.cmbDataBase);
            this.setComboboxData(this.cmbSchema);
        }

        private void setComboboxData(ComboBox comboBox, List<DataItem> datas = null)
        {
            comboBox.ValueMember = "type";
            comboBox.DisplayMember = "name";
            if (datas == null)
                comboBox.DataSource = new List<DataItem>() { new DataItem { Type = "", Name = "請選擇.." } };
            else
            {
                datas.Insert(0, new DataItem("", "請選擇.."));
                comboBox.DataSource = datas;
            }
        }






        private Boolean valSourceControl()
        {
            Boolean flag = true;
            string msg = string.Empty;
            this.errorProvider.Clear();
            if (string.IsNullOrEmpty(DataTrans.CString(this.cmbSourceType.SelectedValue)))
            {
                flag = false;
                this.errorProvider.SetError(this.cmbSourceType, "請選擇一項目『資料來源類型』！");
                msg = $"{msg}\n-請選擇一項目『資料來源類型』！";
            }

            if (string.IsNullOrEmpty(DataTrans.CString(this.cmbSourceConfig.SelectedValue)))
            {
                flag = false;
                this.errorProvider.SetError(this.cmbSourceConfig, "請選擇一項目『來源設定檔』！");
                msg = $"{msg}\n-請選擇一項目『來源設定檔』！";
            }

            if (string.IsNullOrEmpty(DataTrans.CString(this.txtFilePath.Text)))
            {
                flag = false;
                this.errorProvider.SetError(this.txtFilePath, "請選擇 Table Schema 來源檔案『資料來源路徑』！");
                msg = $"{msg}\n-請選擇 Table Schema 來源檔案『資料來源路徑』！";
            }

            if (!File.Exists(this.srcFile))
            {
                flag = false;
                this.srcFile = "";
                this.errorProvider.SetError(this.txtFilePath, "Table Schema 來源檔『資料來源路徑』不存在，請重新選擇！");
                msg = $"{msg}\n-Table Schema 來源檔『資料來源路徑』不存在，請重新選擇！";
            }
            if (!flag)
                this.alertWarningMsg(msg);
            return flag;
        }

        private Boolean valGeneratorControl()
        {
            Boolean flag = true;
            string msg = string.Empty;
            this.errorProvider.Clear();

            if (string.IsNullOrEmpty(DataTrans.CString(this.cmbDBType.SelectedValue)))
            {
                flag = false;
                this.errorProvider.SetError(this.cmbDBType, "請選擇一項目『資料庫類型』！");
                msg = $"{msg}\n-請選擇一項目『資料庫類型』！";
            }

            if (this._tableSummaries.Count == 0)
            {
                flag = false;
                this.errorProvider.SetError(this.btnQuery, "目前無資料！請先確認來源資料有資料");
                msg = $"{msg}\n-目前無資料！請先確認來源資料有資料";
            }
            if (!flag)
                this.alertWarningMsg(msg);
            return flag;
        }

        private Boolean valCheckCnt(int cnt)
        {
            Boolean flag = true;
            this.errorProvider.Clear();

            if (cnt == 0)
            {
                flag = false;
                this.errorProvider.SetError(this.btnGenerateDDL, "請勾選至少一個資料表！");
                this.alertWarningMsg("請勾選至少一個資料表！");
            }
            return flag;
        }

        private List<string> getFile(string basePath, string strPattern, int index = -1)
        {
            List<string> list = new List<string>();
            foreach (string fname in Directory.GetFiles(basePath, strPattern, SearchOption.TopDirectoryOnly))
            {
                if (index >= 0)
                    list.Add(Path.GetFileName(fname).Split(".", StringSplitOptions.RemoveEmptyEntries)[index]);
                else
                    list.Add(Path.GetFileName(fname));
            }
            return list;
        }

        /// <summary>
        /// 取得實際要顯示的資料
        /// </summary>
        /// <param name="srcDLLs">Extension資料夾下的dll file</param>
        /// <param name="lstAppDataItem">App_Data json設定檔資料</param>
        /// <returns>回傳以實際dll檔並且設，配對是否有額外說明</returns>
        private List<DataItem> mapData(List<string> srcDLLs, List<DataItem> lstData)
        {
            List<DataItem> dataItems = new List<DataItem>();
            foreach (string ddl in srcDLLs)
            {
                if (lstData is not null && lstData.Exists(x => x.Type.Equals(ddl)))
                    dataItems.Add(new DataItem(ddl, lstData.Find(x => x.Type.Equals(ddl)).Name));
                else
                    dataItems.Add(new DataItem(ddl, ddl));
            }
            return dataItems;
        }

        private void bindDataToDataGridView(List<TableSummary> tableSummaries)
        {
            this.dataGV.AutoGenerateColumns = false;
            this.dataGV.DataSource = tableSummaries;
            if (tableSummaries.Count == 0)
                this.alertWarningMsg("目前查詢沒有資料！");
        }

        private List<TableSummary> filterTableSummary()
        {
            List<TableSummary> tableSummaries = this._tableSummaries.Where(x =>
            DataTrans.CString(x.Area!).Contains(this.cmbArea.SelectedValue.ToString())
          && DataTrans.CString(x.DataBase!).Contains(this.cmbDataBase.SelectedValue.ToString())
          && DataTrans.CString(x.Schema!).Contains(this.cmbSchema.SelectedValue.ToString())
          && ( DataTrans.CString(x.TableName).Contains(this.txtTblKeyword.Text)
                   || DataTrans.CString(x.TableComment).Contains(this.txtTblKeyword.Text)
                   || String.IsNullOrEmpty(this.txtTblKeyword.Text)
                 )
          ).ToList();

            return tableSummaries;
        }
        #endregion



        private void alertWarningMsg(string msg)
        {
            MessageBox.Show(this, msg, "Warning！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void alertErrorMsg(string msg)
        {
            MessageBox.Show(this, msg, "Warning！", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void alertMsg(string msg)
        {
            MessageBox.Show(this, msg, "Informat！", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region 處理DataGridView checkbox全選功能
        //UNDONE 處理DataGridView checkbox全選功能，需優化        
        private void dataGV_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);
                Point pt = e.CellBounds.Location;

                int nChkBoxWidth = 15;
                int nChkBoxHeight = 15;
                int offsetx = ( e.CellBounds.Width - nChkBoxWidth ) / 2;
                int offsety = ( e.CellBounds.Height - nChkBoxHeight ) / 2;

                pt.X += offsetx;
                pt.Y += offsety;

                CheckBox chkBox = new CheckBox();

                chkBox.Size = new Size(nChkBoxWidth, nChkBoxHeight);
                chkBox.Location = pt;
                chkBox.Checked = false;
                chkBox.CheckedChanged += new EventHandler(dgvCheckBox_CheckedChanged);

                ( (DataGridView)sender ).Controls.Add(chkBox);

                e.Handled = true;

            }
        }

        private void dgvCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in this.dataGV.Rows)
                r.Cells[0].Value = ( (CheckBox)sender ).Checked;
        }
        #endregion

    }
}
