using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace WinADONET2
{
    public partial class Form1 : Form
    {
        string strConn = ConfigurationManager.ConnectionStrings["gudi"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(strConn);

            StringBuilder sb = new StringBuilder();

            sb.Append("select e.emp_no, concat(first_name, ' ', last_name) emp_name, ");
            sb.Append(" E.dept_no, d.dept_name ");
            sb.Append(" from current_dept_emp E join departments D ");
            sb.Append(" on e.dept_no = D.dept_no");
            sb.Append(" where E.dept_no = 'd009';");

            MySqlDataAdapter da = new MySqlDataAdapter(sb.ToString(), conn);
            DataSet ds = new DataSet();

            conn.Open();
            da.Fill(ds);
            conn.Close();

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = strConn;

            StringBuilder sb = new StringBuilder();

            sb.Append("select e.emp_no , concat(first_name, ' ', last_name) emp_name , ");
            sb.Append(" E.dept_no , d.dept_name ");
            sb.Append(" from current_dept_emp E join departments D ");
            sb.Append(" on e.dept_no = D.dept_no");
            sb.Append(" where E.dept_no = 'd009';");

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sb.ToString();
            cmd.Connection = conn;

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);
            conn.Close();

            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersHeight = 30;

            //DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            //col1.HeaderText = "사원번호";
            //col1.DataPropertyName = "emp_no";
            //col1.Width = 100;
            //col1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns.Add(col1);

            CommonUtil1.AddGridTextColumn(dataGridView1, "사번", "emp_no", 100, true, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil1.AddGridTextColumn(dataGridView1, "사원명", "emp_name", 250);
            CommonUtil1.AddGridTextColumn(dataGridView1, "부서", "dept_no", 100, false, DataGridViewContentAlignment.MiddleCenter);
            CommonUtil1.AddGridTextColumn(dataGridView1, "부서명", "dept_name", 200);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
