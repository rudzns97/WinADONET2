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
    public partial class Form2 : Form
    {
        string strConn = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //데이터 그리드뷰 설정
            CommonUtil1.SetInitGridView(dgvMember);

            CommonUtil1.AddGridTextColumn(dgvMember, "회원ID", "userID") ;
            CommonUtil1.AddGridTextColumn(dgvMember, "회원명", "userName", 150);
            CommonUtil1.AddGridTextColumn(dgvMember, "비밀번호", "userPwd", 120);

            //등록된 데이터 목록 조회
            DataRetrieve();
        }
        private void DataRetrieve()
        {
            MySqlConnection conn = new MySqlConnection(strConn);
            string sql = "select userID, userPwd, userName from members;";

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            conn.Open();
            da.Fill(ds, "member");
            conn.Close();

            dgvMember.DataSource = ds.Tables["member"];

            txtID.Text = txtName.Text = txtPwd.Text = "";
            txtID.Enabled = true;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            //유효성 체크
            if (txtID.Text.Trim().Length < 1)
            {
                MessageBox.Show("회원ID를 입력하여 주십시오");
                return;
            }
            //DB저장
            MySqlConnection conn = new MySqlConnection(strConn);
            string sql = $@"insert into members (userID, userPwd, userName)
                            values('{txtID.Text }', '{ txtPwd.Text}', '{ txtName.Text}' )";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            //저장완료메세지
            //MessageBox.Show("회원이 등록되었습니다.");
            DataRetrieve();
            
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DataRetrieve();
        }

        private void dgvMember_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Text = dgvMember.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvMember.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPwd.Text = dgvMember.Rows[e.RowIndex].Cells[2].Value.ToString();

            txtID.Enabled = false;

            /*
            string userid = dgvMember.Rows[e.RowIndex].Cells[0].Value.ToString();
            MySqlConnection conn = new MySqlConnection(strConn);
            MySqlCommand cmd = new MySqlCommand("select * from members where userID='" + userid + "'", conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtID.Text = reader["userID"].ToString();
                txtName.Text = reader["userName"].ToString();
                txtPwd.Text = reader["userPwd"].ToString();
            }
            conn.Close();
            */
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtID.Enabled)
            {
                MessageBox.Show("수정할 회원을 선택하여 주십시오.");
                return;
            }
            //DB수정
            MySqlConnection conn = new MySqlConnection(strConn);
            string sql = $@"update members 
                                            set userPwd = '{ txtPwd.Text}' ,
                                                userName ='{ txtName.Text}'
                                            where userID = '{txtID.Text }'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            //저장완료메세지
            //MessageBox.Show("회원이 등록되었습니다.");
            DataRetrieve();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //삭제
            if (MessageBox.Show($"{txtName.Text}님의 데이터를 정말로 삭제하시겠습니까?", "삭제확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (txtID.Enabled)
                {
                    MessageBox.Show("삭제할 회원을 선택하여 주십시오.");
                    return;
                }
                //DB삭제
                MySqlConnection conn = new MySqlConnection(strConn);
                string sql = $@"delete from members 
                            where userID = '{txtID.Text }'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                //저장완료메세지
                //MessageBox.Show("회원이 등록되었습니다.");
                DataRetrieve();
            }
        }
    }
}
