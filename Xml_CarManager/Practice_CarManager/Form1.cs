using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Form1()
        {
            InitializeComponent();
            Text = "주차 관리_Xml";
            dataGridView1.DataSource = DataManager.Cars;
            dataGridView1.CurrentCellChanged += DataGridView1_CurrentCellChanged;
        }

        //DataGridView로 데이터 불러오기
        private void DataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Car car = dataGridView1.CurrentRow.DataBoundItem as Car;
                textBox_parkingSpot.Text = car.parkingSpot;
                textBox_carNumber.Text = car.carNumber;
                textBox_driverName.Text = car.driverName;
                textBox_phoneNumber.Text = car.phoneNumber;
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

        //주차 버튼 클릭
        private void button_in_Click(object sender, EventArgs e)
        {
            string showMessage = "";
            
            if (textBox_parkingSpot.Text.Trim() == "" || textBox_carNumber.Text.Trim() == ""
                 || textBox_driverName.Text.Trim() == "" || textBox_phoneNumber.Text.Trim() == "")
            {     //모든 정보를 입력해야 함
                showMessage = "모든 정보 입력 필요.";
                ShowMessage(button_in.Text, showMessage);
            }
            else
            {
                try
                {
                    Car car = DataManager.Cars.Single((x) => x.parkingSpot == textBox_parkingSpot.Text);
                    if (car.carNumber.Trim() == "")    //주차 여부 확인
                    {
                        car.carNumber = textBox_carNumber.Text;
                        car.driverName = textBox_driverName.Text;        
                        car.phoneNumber = textBox_phoneNumber.Text;   
                        car.parkingTime = DateTime.Now;

                        dataGridView1.DataSource = null; 
                        dataGridView1.DataSource = DataManager.Cars;  
                        DataManager.Save(); //저장

                        showMessage = "\"" + car.parkingSpot + "\"에 \"" + car.carNumber + "\"를 주차 완료.";
                        ShowMessage(button_in.Text, showMessage);
                    }
                    else
                    {
                        showMessage = "주차 중인 곳으로 주차 불가.";
                        ShowMessage(button_in.Text, showMessage);
                    }
                }
                catch (Exception)
                {
                    showMessage = "존재하지 않는 주차 공간.";
                    ShowMessage(button_in.Text, showMessage);
                }
            }

        }

        //출차 버튼 클릭
        private void button_out_Click(object sender, EventArgs e)
        {
            string showMessage = "";
            if (textBox_parkingSpot.Text.Trim() == "")  //출차시에는 차량번호 확인하지 않아도 되도록 함
            {
                showMessage = "주차 공간 입력 필요.";
                ShowMessage(button_out.Text, showMessage);
            }
            else
            {
                try
                {
                    Car car = DataManager.Cars.Single((x) => x.parkingSpot == textBox_parkingSpot.Text);
                    if (car.carNumber.Trim() == "")    //주차 여부 확인
                    {
                        showMessage = "빈 주차 공간으로 출차 불가.";
                        ShowMessage(button_out.Text, showMessage);
                    }
                    else
                    {
                        showMessage = "\"" + car.parkingSpot + "\"에 주차된\"" + car.carNumber + "\"출차 완료.";
                        car.carNumber = "";
                        car.driverName = "";             
                        car.phoneNumber = ""; 
                        car.parkingTime = new DateTime();

                        dataGridView1.DataSource = null;    //기존 정보를 지움
                        dataGridView1.DataSource = DataManager.Cars;   //새로 생성한 정보를 소스에 넣어줌
                        DataManager.Save(); //저장

                        ShowMessage(button_out.Text, showMessage);
                    }
                }
                catch (Exception)
                {
                    showMessage = "존재하지 않는 주차 공간.";
                    ShowMessage(button_out.Text, showMessage);
                }
            }
        }

        //확인 버튼 클릭
        private void button_spotCheck_Click(object sender, EventArgs e)
        {
            string showMessage = "";
            if (textBox_spotCheck.Text.Trim() == "")
            {
                showMessage = "주차 공간 입력 필요.";
                ShowMessage(button_spotCheck.Text, showMessage);
            }
            else
            {
                try
                {
                    Car car = DataManager.Cars.Single((x) => x.parkingSpot == textBox_spotCheck.Text);
                    if (car.carNumber.Trim() == "")    //주차 여부 확인
                    {
                        showMessage = "빈 주차 공간.";
                        ShowMessage(button_spotCheck.Text, showMessage);
                    }
                    else
                    {
                        showMessage = "\"" + car.parkingSpot + "\"에 \"" + car.carNumber + "\"가 주차되어 있음.";
                        ShowMessage(button_spotCheck.Text, showMessage);
                    }
                }
                catch (Exception)
                {
                    showMessage = "존재하지 않는 주차 공간.";
                    ShowMessage(button_spotCheck.Text, showMessage);
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
