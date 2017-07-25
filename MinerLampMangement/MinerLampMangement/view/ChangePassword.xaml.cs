using System.Collections.Generic;
using System.Windows;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view
{
    /// <summary>
    /// ChangePassword.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePassword : Window
    {
        public string Username;
        public string Oldpassword;
        public string Password1;
        public string Password2;
        //数据库中提取的初始数据
        public List<SettingInfo> OldsettingInfo;

        public ChangePassword()
        {
            InitializeComponent();
        }

        //退出按钮
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //修改按钮
        private void ChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            OldsettingInfo =
                MongoDbHelper<SettingInfo>.FindAllDocuments(CollectionNames.SettingInfoTest);
            Username = Usernametxb.Text;
            Oldpassword = Oldpasswordtxb.Text;
            Password1 = PasswordBox1.Password;
            Password2 = PasswordBox2.Password;
            if (Password1 == Password2)
            {
                if (Password1 != "" && Password2 != "" && Username != "" && Oldpassword != "")
                {
                    if (Oldpassword != Password2)
                    {
                        if (!CheckUserInfo())
                        {
                            MongoDbHelper<SettingInfo>.UpdateUserInfo(OldsettingInfo[0].UserInfos);
                            MessageBox.Show("密码修改成功！");
                            //清空文本框
                            Usernametxb.Text = "";
                            Oldpasswordtxb.Text = "";
                            PasswordBox1.Password = "";
                            PasswordBox2.Password = "";
                        }
                        else
                        {
                            MessageBox.Show("用户名或原始密码错误！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("修改后的密码不能与原始密码相同！");
                    }
                }
                else
                {
                    MessageBox.Show("请检查信息是否输入完整！");
                }
            }
            else
            {
                MessageBox.Show("两次输入的新密码不一样！");
            }
        }

        //检查用户名和密码是否存在
        public bool CheckUserInfo()
        {
            var result = true;
            if (OldsettingInfo.Count != 0)
            {
                //判断用户名是否被使用
                foreach (var olduserinfo in OldsettingInfo[0].UserInfos)
                {
                    if (Username == olduserinfo.Username && Oldpassword == olduserinfo.Password)
                    {
                        olduserinfo.Password = Password2;
                        result = false;
                    }
                }
            }
            return result;
        }
    }
}