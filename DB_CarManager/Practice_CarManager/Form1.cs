using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practice_CarManager
{
    public partial class Form1 : Form
    {
        public SqlConnection conn = new SqlConnection();

        public Form1()
        {
            InitializeComponent();
            Text = "주차 관리_DB";
            Query_ViewAll();
        }

        //DB 연결
        private void ConnectDB()
        {
            try
            {
                conn.ConnectionString = string.Format("Data Source=({0}); " +
                        "Initial Catalog = {1};" +
                        "Integrated Security = {2};" +
                        "Timeout = 3"
                        , "local", "MYDB1", "SSPI");
                conn = new SqlConnection(conn.ConnectionString);
                conn.Open();
            }
            catch (Exception)
            {
                //MessageBox.Show("데이터베이스 연결 실패.");
            }
        }

        //DataGridView로 데이터 불러오기
        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                textBox_parkingSpot.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox_spotCheck.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox_carNumber.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox_driverName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox_phoneNumber.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception)
            {

            }
        }

        //로그저장 메소드
        private void WriteLog(string contents)
        {
            using (StreamWriter writer = new StreamWriter(@"./CarManager_Log.txt", true))
            {
                writer.WriteLine(contents);
            }
        }

        //메세지박스 팝업 + 로그저장 메소드 호출
        private void ShowMessage (string clickedBtn, string showMessage)
        {
            string activeMessage = "[" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "]  <" + clickedBtn + ">  ";
            MessageBox.Show(showMessage);
            listBox1.Items.Insert(0, activeMessage + showMessage);
            WriteLog(activeMessage + showMessage);
        }

        //DataGridView 출력 쿼리
        private void Query_ViewAll()
        {
            ConnectDB();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * From Cars";
                //"Select * From Cars";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Cars");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "Cars";

            conn.Close();
        }

        //주차 쿼리
        private void Query_In()
        {
            try
            {
                if (Query_SpotCheck(textBox_parkingSpot.Text).Trim() == "")    //주차 가능
                {
                    ConnectDB();
                    string sqlcommand = "Update Cars set carNumber = @p1, driverName = @p2, phoneNumber = @p3, parkingTime = @p4 " +
                                        "where parkingSpot = @p5";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@p1", textBox_carNumber.Text);
                    cmd.Parameters.AddWithValue("@p2", textBox_driverName.Text);
                    cmd.Parameters.AddWithValue("@p3", textBox_phoneNumber.Text);
                    cmd.Parameters.AddWithValue("@p4", DateTime.Now.ToString(""));
                    cmd.Parameters.AddWithValue("@p5", textBox_parkingSpot.Text);
                    cmd.CommandText = sqlcommand;
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    ShowMessage(button_in.Text, 
                        "\"" + textBox_parkingSpot.Text + "\"에 \"" + textBox_carNumber.Text + "\" 주차 완료.");
                }
                else    //주차 불가
                {
                    ShowMessage(button_in.Text, "주차 중인 곳으로 주차 불가.");
                }
            }
            catch (Exception)   //그 외 오류
            {
                ShowMessage(button_in.Text, "존재하지 않는 주차 공간.");
            }
            
        }

        //주차 버튼 클릭
        private void button_in_Click(object sender, EventArgs e)
        {
            if (textBox_parkingSpot.Text.Trim() == "" || textBox_carNumber.Text.Trim() == ""
                 || textBox_driverName.Text.Trim() == "" || textBox_phoneNumber.Text.Trim() == "")
            {     //모든 정보를 입력해야 함
                ShowMessage(button_in.Text, "모든 정보 입력 필요.");
            }
            else
            {
                Query_In();
                Query_ViewAll();
            }
        }

        //출차 쿼리
        private void Query_Out()
        {
            try
            {
                if (Query_SpotCheck(textBox_parkingSpot.Text).Trim() == "")    //주차 여부 확인
                {
                    ShowMessage(button_out.Text, "빈 주차 공간으로 출차 불가.");
                }
                else
                {
                    ConnectDB();
                    string sqlcommand = "Update Cars set carNumber = @p1, driverName = @p1, phoneNumber = @p1, parkingTime = @p1 " +
                                        "where parkingSpot = @p2";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@p1", "");
                    cmd.Parameters.AddWithValue("@p2", textBox_parkingSpot.Text);
                    cmd.CommandText = sqlcommand;
                    cmd.ExecuteNonQuery();

                    conn.Close();

                    ShowMessage(button_out.Text,
                        "\"" + textBox_parkingSpot.Text + "\"에 \"" + textBox_carNumber.Text + "\" 출차 완료.");
                }
            }
            catch (Exception)
            {
                ShowMessage(button_out.Text, "존재하지 않는 주차 공간.");
            }
        }

        //출차 버튼 클릭
        private void button_out_Click(object sender, EventArgs e)
        {
            if (textBox_parkingSpot.Text.Trim() == "")  //출차시에는 차량번호 확인하지 않아도 되도록 함
            {
                ShowMessage(button_out.Text, "주차 공간 입력 필요.");
            }
            else
            {
                Query_Out();
                Query_ViewAll();
            }
        }

        //확인 쿼리
        private string Query_SpotCheck(string parkingSpot)
        {
            ConnectDB();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select * From Cars Where parkingSpot = @p1";
            cmd.Parameters.AddWithValue("@p1", parkingSpot);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Cars");

            //int rowLength = ds.Tables[0].Rows.Count;
            string carNumber = ds.Tables[0].Rows[0]["carNumber"].ToString();

            conn.Close();

            return carNumber;
        }

        //확인 버튼 클릭
        private void button_spotCheck_Click(object sender, EventArgs e)
        {
            if (textBox_spotCheck.Text.Trim() == "")
            {
                ShowMessage(button_spotCheck.Text, "주차 공간 입력 필요.");
            }
            else
            {
                try
                {
                    if (Query_SpotCheck(textBox_spotCheck.Text).Trim() == "")
                    {
                        ShowMessage(button_spotCheck.Text, "빈 주차 공간.");
                    }
                    else
                    {
                        ShowMessage(button_spotCheck.Text, 
                            "\"" + textBox_spotCheck.Text + "\"에 \"" + Query_SpotCheck(textBox_spotCheck.Text) + "\"가 주차되어 있음.");
                    }

                }
                catch (Exception)
                {
                    ShowMessage(button_spotCheck.Text, "존재하지 않는 주차 공간.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_parkingSpot.Text = "";
            textBox_carNumber.Text = "";
            textBox_driverName.Text = "";
            textBox_phoneNumber.Text = "";
        }
    }
}
