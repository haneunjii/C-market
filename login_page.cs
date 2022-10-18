using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;


namespace supermarket.Display
{
    public partial class login_page : UserControl
    {
        Display.flatform_main fl = new Display.flatform_main();
        public login_page()
        {
            InitializeComponent();
        }
        private void login_page_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(fbc);
            }
            catch
            {
                MessageBox.Show("문제발생");
            }
        }
        FirebaseConfig fbc = new FirebaseConfig()
        {
            AuthSecret = "AIzaSyC8FDbyONe9rEpZ88g_MDfZ9jp5HjfORXU",
            BasePath = "https://userdata-64f98-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        private void btn_register_Click(object sender, EventArgs e)
        {
            var result = client.Get("가입자 명단/" + tx_id.Text);
            upload upd1 = result.ResultAs<upload>();
            upload upd2 = new upload()
            {
                Name = tx_name.Text,
                id = tx_id.Text,
                pw = tx_pw.Text
            };

            var send = client.Set("가입자 명단/" + tx_id.Text, upd2);
            MessageBox.Show("회원가입 완료");
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            var result = client.Get("가입자 명단/" + tx_id.Text);
            dawnload upd1 = result.ResultAs<dawnload>(); //데이버베이스레서 가져온 정보 다운로드 클래스에 저장        
            upload upd2 = new upload()  //폼에서 입력한 정보 업로드 틀레스에 저장
            {
                Name = tx_name.Text,
                id = tx_id.Text,
                pw = tx_pw.Text
            };

            if (tx_id.Text != "" | tx_name.Text != "" | tx_pw.Text !="") //이력란에 아무것도 입력이 안되어 있는 경우
            {
                MessageBox.Show("정보를 입력해 주세요");
            }
            else if(tx_id.Text != "" && tx_name.Text != "" && tx_pw.Text != "") //입력란에 입력이 되있는 경우
            {
                
                if(tx_id.Text != upd1.id && tx_name.Text != upd1.Name && tx_pw.Text != upd1.pw) //가입이 안된 직원인 경우
                {
                    MessageBox.Show("가입이 안된 직원입니다");
                }
                else      //가입이 되어있는 경우
                {
                    MessageBox.Show("로그인 되었습니다");
                    panel1.Controls.Clear();
                    //가입된 직원일경우
                }
            }
        }
    }
}
