using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Input;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view
{
    /// <summary>
    /// CabinetManage.xaml 的交互逻辑
    /// </summary>
    public partial class CabinetManage : Window
    {
        public List<int> GroupIdList;

        public CabinetManage()
        {
            InitializeComponent();
            InitShow(); //初始化combox下拉菜单操作
        }

        public class ComItem
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        /// <summary>
        /// 初始化界面，主要是下来按钮的信息显示柜门名称
        /// </summary>
        public void InitShow()
        {
            //去重查询柜数
             GroupIdList = MongoDbTool.DistinctInfoInt(CollectionNames.MinerLampTest, "GroupId");
            //long  返回文档条数,也就是柜数
            List<ComItem> comItemList = new List<ComItem>();
            for (int i = 1; i <= GroupIdList.Count; i++)
            {
                var item = new ComItem()
                {
                    Name = i + "号充电柜",
                    Id = i
                };
                comItemList.Add(item);
            }
            ComboBox.ItemsSource = comItemList; //combox绑定数据源
            ComboBox.DisplayMemberPath = "Name";
            ComboBox.SelectedValuePath = "Id"; //绑定选择项

            //根据选中的柜门号，显示100个充电柜信息
        }

        /// <summary>
        /// 单门开锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenOneDoor_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CabinetIdBox.Text != null && DoorIdBox.Text != null)
                {
                    var cabinetid = CabinetIdBox.Text;
                    var doorid = DoorIdBox.Text;
                    var url = "/operate/devices/unlock/" + cabinetid + "/" + doorid;

                    var result = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(result == "success" ? "开锁成功！" : "开锁失败！");
                }
                else
                {
                    MessageBox.Show("柜号，柜门号填写不完整！");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("开锁失败，请稍后重试！");
            }
        }

        /// <summary>
        /// 整柜开锁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCabinet_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CabinetIdBox.Text != null)
                {
                    var cabinetid = CabinetIdBox.Text;
                    var url = "/operate/devices/unlock/" + cabinetid + "/255";

                    var result = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(result == "success" ? "开锁成功！" : "开锁失败！");
                }
                else
                {
                    MessageBox.Show("柜号不能为空！");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("开锁失败，请稍后重试！");
            }
        }

        /// <summary>
        /// 选中充电柜号操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            try
            {
                //根据选中的柜号，查询对应100个柜门的信息，并显示
                var doorinfo = MongoDbHelper<DoorInfo>.FindPartCabinetInfo(int.Parse(CabinetIdBox.Text),
                    CollectionNames.MinerLampTest);
                DataGrid.ItemsSource = doorinfo;
            }
            catch (Exception)
            {
                MessageBox.Show("柜门号选择错误，请重试！");
            }

        }

        /// <summary>
        /// dategrid 双击操作，将柜门号，传入textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var doubleselect = DataGrid.SelectedItem as DoorInfo;
            //将柜门好，填入相应的textbox
            if (doubleselect != null) DoorIdBox.Text = doubleselect.DeviceId.ToString();
            //自动生成Tm卡编号
            if (doubleselect != null)
                TmIdTextBox.Text = (doubleselect.GroupId * 1000 + doubleselect.DeviceId).ToString();
        }

        /// <summary>
        /// Tm卡号单柜门写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OneDoorWrite_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //获取柜号，柜门号
                var cabinetId = int.Parse(CabinetIdBox.Text);
                var doorId = int.Parse(DoorIdBox.Text);
                //获取新的Tm卡号
                var tmNumber = TmNumberTextBox.Text;
                //执行TM卡转换函数
                //var newTmNumber = Presenter.CardParseLong(tmNumber);
                //将新的Tm卡号，根据柜门号和柜号更新到数据库中，单柜门更新
                MongoDbHelper<DoorInfo>.UpdateTmNumberInfo(cabinetId, doorId, tmNumber);
                //写入成功后，要告诉服务器，让服务器下放新卡号到柜子上
                var url = "/operate/devices/update/" + CabinetIdBox.Text +"/"+ DoorIdBox.Text+
                          "?name=false&department=false&cardnumber=true";
                var result = HttpClient.HttpClient.HttpGet(url, "text/plain");
                MessageBox.Show(result == "success" ? "单柜写入Tm卡号成功！" : "单柜写入Tm卡号失败！");
            }
            catch (Exception)
            {
                MessageBox.Show("写入失败，请检查后重试！");
            }
        }

        /// <summary>
        /// Tm卡号整柜写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllDoorWrite_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //获取柜号，柜门号
                var cabinetId = int.Parse(CabinetIdBox.Text);
                var doorId = int.Parse(DoorIdBox.Text);
                //获取新的Tm卡号
                var tmNumber = TmNumberTextBox.Text;
                //执行TM卡转换函数
                //var newTmNumber = Presenter.CardParseLong(tmNumber);
                //将新的Tm卡号，根据柜号更新到数据库中,整柜更新
                MongoDbHelper<DoorInfo>.UpdateTmNumberInfo(cabinetId, tmNumber);
                //写入成功后，要告诉服务器，让服务器下放新卡号到柜子上
                var url = "/operate/devices/update/" + CabinetIdBox.Text +
                          "/255?name=false&department=false&cardnumber=true&maxgroupid=false";
                var result = HttpClient.HttpClient.HttpGet(url, "text/plain");
                MessageBox.Show(result == "success" ? "整柜写入Tm卡号成功！" : "整柜写入Tm卡号失败！");
            }
            catch (Exception)
            {
                MessageBox.Show("写入失败，请检查后重试！");
            }
        }

        //单柜门初始化
        private void OneDeviceInit_OnClick(object sender, RoutedEventArgs e)
        {
            if (CabinetIdBox.Text != ""&&DoorIdBox.Text!="")
            {
                //弹出确认框
                MessageBoxResult result = MessageBox.Show("确认初始化【" + CabinetIdBox.Text + "】号充电柜，【"+ DoorIdBox.Text + "】号柜门吗？", "提示",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                //如果点击确定
                if (result == MessageBoxResult.Yes)
                {
                    //告知服务器，柜子信息更新了，需要下发到柜子
                    var url = "/operate/devices/reset/" + CabinetIdBox.Text + "/"+ DoorIdBox.Text + "?name=true&department=true&cardnumber=true";
                    var receive = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(receive == "success" ? "服务器收到下发指令！" : "服务器未收到下发指令！");
                }
            }
            else
            {
                MessageBox.Show("柜号，柜门号不能设定为空！");
            }
        }
        //整柜初始化
        private void OneGroupInit_OnClick(object sender, RoutedEventArgs e)
        {
            if (CabinetIdBox.Text != "" )
            {
                //弹出确认框
                MessageBoxResult result = MessageBox.Show("确认初始化【" + CabinetIdBox.Text + "】号充电柜组吗？", "提示",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                //如果点击确定
                if (result == MessageBoxResult.Yes)
                {
                    //告知服务器，柜子信息更新了，需要下发到柜子
                    var url = "/operate/devices/reset/" + CabinetIdBox.Text + "/255?name=true&department=true&cardnumber=true";
                    var receive = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(receive == "success" ? "服务器收到下发指令！" : "服务器未收到下发指令！");
                }
            }
            else
            {
                MessageBox.Show("柜组号不能设定为空！");
            }
        }
        //所有柜组初始化
        private void AllGroupInit_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //弹出确认框
                MessageBoxResult result = MessageBox.Show("确认初始化所有充电柜组吗？", "提示",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                //如果点击确定
                if (result == MessageBoxResult.Yes)
                {
                    //告知服务器，柜子信息更新了，需要下发到柜子
                    var url = "/operate/devices/reset/255/255?name=true&department=true&cardnumber=true&maxgroupid=" + GroupIdList.Count;
                    var receive = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(receive == "success" ? "服务器收到下发指令！" : "服务器未收到下发指令！");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("初始化所有柜组失败！");
            }   
        }
    }
}