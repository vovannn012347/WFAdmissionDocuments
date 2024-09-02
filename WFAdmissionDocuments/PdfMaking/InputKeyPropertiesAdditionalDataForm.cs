using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;

using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments.DocumentSets
{
    public partial class InputKeyPropertiesAdditionalDataForm : Form
    {
        public List<string> KeyAdditionalDatas = new List<string>();
        public InputKeyPropertiesAdditionalDataForm()
        {
            InitializeComponent();
            //dataGridView1.AutoGenerateColumns = false;
        }

        private void KeyedVariableAdditionalDataForm_Load(object sender, EventArgs e)
        {
            SetStringListToDataGridView(KeyAdditionalDatas);
            LocalizeForm();
        }

        private void LocalizeForm()
        {
            Thread.CurrentThread.CurrentCulture = Constants.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = Constants.CurrentCulture;

            this.Text = Resources.InputKeyPropertiesAdditionalDataForm_Title;// = "Additional key field data"
            
            this.KeyDataColumn.HeaderText = Resources.InputKeyPropertiesAdditionalDataForm_KeyDatas_Title; //"Key Datas"

            this.buttonOk.Text = Resources.Ok_Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KeyAdditionalDatas = HarvestRowsFromDataGridView();

            this.DialogResult = DialogResult.OK;
        }

        private void SetStringListToDataGridView(List<string> stringList)
        {
            DataTable dataTable = new DataTable();
            DataColumn column = new DataColumn()
            {
                ColumnName = "KeyDataColumn",
                DataType = typeof(string)
            };
            dataTable.Columns.Add(column);

            foreach (var str in stringList)
            {
                dataTable.Rows.Add(str);
            }

            dataGridView1.DataSource = dataTable;
        }

        private List<string> HarvestRowsFromDataGridView()
        {
            List<string> stringList = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    stringList.Add(row.Cells[0].Value.ToString());
                }
            }

            return stringList;
        }



    }
}
