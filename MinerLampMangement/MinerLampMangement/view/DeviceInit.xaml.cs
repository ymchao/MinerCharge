using System.Windows;
using System.Windows.Input;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view
{
    /// <summary>
    /// DeviceInit.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceInit : Window
    {
        public DeviceInit()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 限制控件只能输入数字，屏蔽空格等符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmidbox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back ||
                e.Key == Key.Left || e.Key == Key.Right)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }



        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitDatabase_OnClick(object sender, RoutedEventArgs e)
        {
            if (numbox.Text != "")
            {
                //弹出确认框
                MessageBoxResult result = MessageBox.Show("确认初始化【" + numbox.Text + "】台充电柜组吗？", "提示",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                //如果点击确定
                if (result == MessageBoxResult.Yes)
                {
                    //写入相应个数的设备数据到数据库中
                    MongoDbTool.InsertDeviceInfo2(int.Parse(numbox.Text));
                    MessageBox.Show("初始化【" + numbox.Text + "】组充电柜成功！");
                }
            }
            else
            {
                MessageBox.Show("柜子数量不能设定为空！");
            }
        }

        /// <summary>
        ///导入员工数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportEmployee_OnClickc(object sender, RoutedEventArgs e)
        {
            if (numbox.Text != "")
            {
                //调用员工信息导入方法,告知需要服务器更新的柜子数量
                InOutData.ImportEmployee();
                //导入员工信息后，调用objectid方法，将员工objectid和设备绑定起来
                MongoDbTool.UpdateObjectId();
                MessageBox.Show("员工、设备Objectid绑定成功！");

                //告知服务器，员工信息更新了，需要下发到柜子
//                var url = "/operate/devices/update/255/255?name=true&department=true&cardnumber=true&maxgroupid=" +
//                          int.Parse(numbox.Text);
//                var receive = HttpClient.HttpClient.HttpGet(url, "text/plain");
//                MessageBox.Show(receive == "success" ? "服务器收到下发指令！" : "服务器未收到下发指令！");
            }
            else
            {
                MessageBox.Show("柜子数量不能设定为空！");
            }
        }

        /// <summary>
        /// 导入TM卡信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportTm_OnClick(object sender, RoutedEventArgs e)
        {
            if (numbox.Text != "")
            {
                //调用TM信息导入方法
                InOutData.ImportTmInfo();

//                //告知服务器，TM信息更新了，需要下发到柜子
//                var url = "/operate/devices/update/255/255?name=true&department=true&cardnumber=true&maxgroupid=" +
//                          int.Parse(numbox.Text);
//                var receive = HttpClient.HttpClient.HttpGet(url, "text/plain");
//                MessageBox.Show(receive == "success" ? "服务器收到下发指令！" : "服务器未收到下发指令！");
            }
            else
            {
                MessageBox.Show("柜子数量不能设定为空！");
            }
        }

        /// <summary>
        /// 下发消息给服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendToServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (numbox.Text != "")
            {
                //弹出确认框
                //  MessageBoxResult result = MessageBox.Show("确认下发【" + numbox.Text + "】台充电柜组的更新信息到服务器？", "提示",
                MessageBoxResult result = MessageBox.Show("确认下发全部充电柜组的更新信息到服务器？", "提示",
                     MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                //如果点击确定
                if (result == MessageBoxResult.Yes)
                {
                    //统计柜子总数
                    var list = MongoDbHelper<DoorInfo>.DistinctInfoInt(CollectionNames.MinerLampTest, "GroupId"); //long  返回文档条数
                    int a = list.Count;

                    //告知服务器，柜子信息更新了，需要下发到柜子
                    var url = "/operate/devices/update/255/255?name=true&department=true&cardnumber=true&maxgroupid=" +
                              a;
                    var receive = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(receive == "success" ? "服务器收到下发指令！" : "服务器未收到下发指令！");
                }
            }
            else
            {
                MessageBox.Show("柜子数量不能设定为空！");
            }
        }

        //单柜下发信息
        private void SendoneToServer_OnClick(object sender, RoutedEventArgs e)
        {
            if (cabinetnumbox.Text != "")
            {
                //弹出确认框
                MessageBoxResult result = MessageBox.Show("确认下发【" + cabinetnumbox.Text + "】号充电柜组的更新信息到服务器？", "提示",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                //如果点击确定
                if (result == MessageBoxResult.Yes)
                {
                    //告知服务器，柜子信息更新了，需要下发到柜子
                    var url = "/operate/devices/update/"+int.Parse(cabinetnumbox.Text)+"/255?name=true&department=true&cardnumber=true";
                    var receive = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(receive == "success" ? "服务器收到下发指令！" : "服务器未收到下发指令！");
                }
            }
            else
            {
                MessageBox.Show("柜号不能设定为空！");
            }
        }
    }
}