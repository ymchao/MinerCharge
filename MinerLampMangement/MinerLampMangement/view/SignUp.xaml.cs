using System;
using System.Collections.Generic;
using System.Windows;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view
{
    /// <summary>
    /// SignUp.xaml 的交互逻辑
    /// </summary>
    public partial class SignUp : Window
    {
        public string Username;
        public string Password1;
        public string Password2;
        //数据库中提取的初始数据
        public List<SettingInfo> OldsettingInfo;
           
        public SignUp()
        {
            InitializeComponent();
        }

        //取消按钮
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //注册按钮
        private void SignUpButton_OnClick(object sender, RoutedEventArgs e)
        {
            OldsettingInfo =
            MongoDbHelper<SettingInfo>.FindAllDocuments(CollectionNames.SettingInfoTest);
            Username = Usernametxb.Text;
            Password1 = PasswordBox1.Password;
            Password2 = PasswordBox2.Password;
            if (Password1 == Password2)
            {
                if (Password1 != "" && Password2 != "" && Username != "")
                {
                    if (CheckUserInfo())
                    {
                        if (OldsettingInfo.Count != 0)
                        {
                            //将新的员工通过AddUserInfo添加到list并更新到数据库中
                            MongoDbHelper<SettingInfo>.UpdateUserInfo(AddUserInfo(Username, Password2).UserInfos);
                        }
                        //如果获取的数据为零，则直接先建新集合，直接插入数据即可
                        else
                        {
                            var newuserinfo = new UserInfo()
                            {
                                Username = Username,
                                Password = Password2
                            };
                            var settinginfo = new SettingInfo()
                            {
                                UserInfos = new List<UserInfo>()
                            };
                            settinginfo.UserInfos.Add(newuserinfo);
                            MongoDbHelper<SettingInfo>.Insert(CollectionNames.SettingInfoTest, settinginfo);
                        }
                        MessageBox.Show("注册用户成功！");
                        //清空文本框
                        Usernametxb.Text = "";
                        PasswordBox1.Password = "";
                        PasswordBox2.Password = "";
                    }
                    else
                    {
                        MessageBox.Show("用户名已被注册，请重新设置用户名！");
                    }
                }
                else
                {
                    MessageBox.Show("请检查信息是否输入完整！");
                }
            }
            else
            {
                MessageBox.Show("两次输入的密码不一样！");
            }
        }

        //检查用户名是否存在
        public bool CheckUserInfo()
        {
            var result = true;
            if (OldsettingInfo.Count != 0)
            {
                //判断用户名是否被使用
                foreach (var olduserinfo in OldsettingInfo[0].UserInfos)
                {
                    if (Username == olduserinfo.Username)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        //将新用户信息添加到数据库中
        public SettingInfo AddUserInfo(string username, string password)
        {
            var newuserinfo = new UserInfo()
            {
                Username = username,
                Password = password
            };
            OldsettingInfo[0].UserInfos.Add(newuserinfo);
            return OldsettingInfo[0];
        }
    }
}