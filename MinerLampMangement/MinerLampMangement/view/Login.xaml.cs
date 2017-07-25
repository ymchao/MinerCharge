using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        readonly WaitingBox _waitingBox = new WaitingBox();

        public Login()
        {
            InitializeComponent();
            //打开窗口，默认焦点在用户名的输入窗口上
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                (Action) (() => { Keyboard.Focus(Usernamebox); }));
        }

        //关闭按钮
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //注册账号
        private void SignUpAndChangePassword_OnClick(object sender, MouseButtonEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.ShowDialog();
        }

        //修改密码
        private void ChangePassword_OnClick(object sender, MouseButtonEventArgs e)
        {
            var changepassword = new ChangePassword();
            changepassword.ShowDialog();
        }

        //登陆按钮
        private void Landing_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //提取所有用户信息
                var oldsettingInfo =
                    MongoDbHelper<SettingInfo>.FindAllDocuments(CollectionNames.SettingInfoTest);
                if (oldsettingInfo.Count != 0)
                {
                    var result = false;
                    foreach (var userinfo in oldsettingInfo[0].UserInfos)
                    {
                        if (userinfo.Username == Usernamebox.Text && userinfo.Password == Passwordbox.Password)
                        {
                            result = true;
                        }
                    }
                    if (result)
                    {
                        object username = Usernamebox.Text;
                        this.Close();
                        //打开等待窗口
                        _waitingBox.Show();
                        Thread thread = new Thread(new ParameterizedThreadStart(Openmainview));
                        thread.Start(username);
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误！");
                    }
                }
                else
                {
                    MessageBox.Show("未找到任何用户信息，请先注册账号！");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("数据库连接异常！");
            }
        }

        public void Openmainview(object username)
        {
           Dispatcher.Invoke(() =>
            {
                MainWindow mainWindow = new MainWindow(username.ToString());
                mainWindow.Show();
                _waitingBox.Close();
            });
        }
    }
}