using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.view;
using MinerLampMangement.view.view2;
using MinerLampMangement.view.viewbean;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        private Presenter presenter;

        public List<MinerDoorControl> Controls = new List<MinerDoorControl>();

        //记录柜号
        private int _cabinetId;

        private int _pollingtimes;

        //起一个计时器
        public DispatcherTimer PollingCabinetTimer = new DispatcherTimer();

        public MainWindow(string username)
        {
            InitializeComponent();
            presenter = new Presenter(this);
            InitMinerDoorShow(); //初始化界面的100个Grid
            InitleftNavigation(); //初始化左侧导航栏的柜门标签
            InitDepartmentTree(); //初始化左侧部门树
            InitleftTotalInfo(); //初始化左侧导航栏的统计汇总信息
            //登陆时间
            TimeTextBlock.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //登陆用户名
            UsernameTextBlock.Text += username;
            //加入定时器事件
            PollingCabinetTimer.Tick += new EventHandler(PollingCabinetTimer_Tick);
            //设置刷新时间：TimeSpan（时, 分， 秒）
            PollingCabinetTimer.Interval = new TimeSpan(0, 0, 1);
        }

        #region 界面初始化

        /// <summary>
        /// 初始化及更新主界面的显示
        /// </summary>
        public void InitMinerDoorShow()
        {
            GridStatusShow.Children.Clear();
            var id = 1;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    var minerDoorControl = new MinerDoorControl(presenter)
                    {
                        //为每个grid加一个名字属性，方便后面识别柜门号
                        Name = "grid" + id,
                    };
                    //在grid中动态添加控件
                    GridStatusShow.Children.Add(minerDoorControl);
                    //设定控件在Grid中的位置
                    Grid.SetRow(minerDoorControl, i);
                    Grid.SetColumn(minerDoorControl, j);
                    //将控件添加到集合中方便下一步使用
                    Controls.Add(minerDoorControl);
                    id++;
                    if (id == 101)
                    {
                        //显示满100个grid小格子，则跳出
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 初始化左侧导航栏
        /// </summary>
        /// <param name="window"></param>
        public void InitleftNavigation()
        {
            var list = MongoDbHelper<DoorInfo>.DistinctInfoInt(CollectionNames.MinerLampTest, "GroupId"); //long  返回文档条数
            if (list.Count > 0)
            {
                for (int i = 1; i <= list.Count; i++)
                {
                    var btn = new Button()
                    {
                        Name = "btn" + i, //为button命名
                        Content = i + "号充电柜"
                    };
                    btn.SetValue(Button.StyleProperty, Application.Current.Resources["LeftButton"]);
                    //btn.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xB5, 0xC7, 0xF4));
                    btn.Click += new RoutedEventHandler(btn_click); //  button的路由方法
                    LeftNavigation.Children.Add(btn); //LeftNavigation为左侧的一个StackPanel
                }
            }
        }

        /// <summary>
        /// 初始化左侧部门树
        /// </summary>
        public void InitDepartmentTree()
        {
            List<TreeviewDepartment> itemList = InitXaml.InitDepartmentTree(); //初始化左侧部门树
            TreeView.ItemsSource = itemList;
        }

        /// <summary>
        /// 初始化统计汇总信息
        /// </summary>
        public void InitleftTotalInfo()
        {
            var list = MongoDbTool.CountTotalInfoByStatus();
            Totalcabinet.Text += list[0] / 100;
            Totaldoor.Text += list[0];
            Totalminer.Text += list[1];
            Downminer.Text += list[3];
            Upminer.Text += list[1] - list[3];
            Charginglamp.Text += list[4];
            Fulllamp.Text += list[5];
            Usinglamp.Text += list[3];
            Problemlamp.Text += list[6];
            Sparelamp.Text += list[7];
        }

        #endregion

        #region 点击左侧导航栏，更新右侧grid

        /// <summary>
        /// 左侧的导航栏的button的路由事件为更新右侧的100个grid的信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btn_click(object sender, RoutedEventArgs e)
        {
            //刷新次数置零
            _pollingtimes = 1;

            //sender是传入的object对象转换为button对象
            Button btn = sender as Button;
            foreach (var button in LeftNavigation.Children.OfType<Button>())
            {
                //将背景和字体全还原为初始状态
                button.Background = Brushes.White;
                button.Foreground = Brushes.Black;
            }
            btn.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x37, 0x7F, 0xED));
            btn.Foreground = Brushes.White;
            //获取点击时，button的名字
            var cabinetid = btn.Name;
            //将button名字的前三个字母btn砍掉就是柜号了
            _cabinetId = int.Parse(cabinetid.Substring(3));
            //  InitializationInfo.UpdateDoorShow(cabinetId);
            presenter.UpdateDoorShow(_cabinetId);
        }

        #endregion

        #region 测试功能

        /// <summary>
        /// 插入20个充电柜和2000名员工（测试用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        //        {
        //            MongoDbTool.InsertAttendanceTest();
        //            MongoDbTool.InsertDeviceInfo();
        //            MongoDbTool.InsertPeopleTest();
        //        }
        /// <summary>
        /// 刷新expander2,更新员工OBJECTID到设备集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        //        {
        //            MongoDbTool.UpdateObjectId(CollectionNames.MinerLampTest);
        //        }
        /// <summary>
        /// 清空集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //        private void Button2_OnClick(object sender, RoutedEventArgs e)
        //        {
        //            MongoDbTool.ClearCollection("Employee", "Device", "LampStatusHistory");
        //        }

        #endregion

        /// <summary>
        /// 系统设置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemSet_OnClick(object sender, RoutedEventArgs e)
        {
            //模式窗口，子窗口不关闭，父窗口无法操作
            SystemSet systemSet = new SystemSet {Owner = this};
            systemSet.ShowDialog();
        }

        /// <summary>
        /// 矿工信息查询按钮 跳转界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinerInfoQuery_OnClick(object sender, RoutedEventArgs e)
        {
            //员工信息查询及导出页面
            MinerInfoSet minerInfoSet = new MinerInfoSet
            {
                Owner = this,
                ChangeBtn = {Visibility = Visibility.Hidden},
                DeleteBtn = {Visibility = Visibility.Hidden},
                AddBtn = {Visibility = Visibility.Hidden}
            };
            //隐藏修改按钮
            Grid.SetColumn(minerInfoSet.ExportResultsBtn, 0);
            Grid.SetColumnSpan(minerInfoSet.ExportResultsBtn, 4);
            minerInfoSet.ShowDialog();
        }

        /// <summary>
        /// 员工信息修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinerInfoChange_OnClick(object sender, RoutedEventArgs e)
        {
            //员工信息查询及导出页面
            MinerInfoSet minerInfoSet = new MinerInfoSet
            {
                Owner = this,
                DeleteBtn = {Visibility = Visibility.Hidden},
                AddBtn = {Visibility = Visibility.Hidden}
            };
            //隐藏修改按钮
            Grid.SetColumn(minerInfoSet.ChangeBtn, 0);
            Grid.SetColumnSpan(minerInfoSet.ChangeBtn, 2);
            Grid.SetColumn(minerInfoSet.ExportResultsBtn, 2);
            Grid.SetColumnSpan(minerInfoSet.ExportResultsBtn, 2);
            minerInfoSet.ShowDialog();
        }

        /// <summary>
        /// 员工信息删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinerInfoDelete_OnClick(object sender, RoutedEventArgs e)
        {
            //员工信息查询及导出页面
            MinerInfoSet minerInfoSet = new MinerInfoSet
            {
                Owner = this,
                ChangeBtn = {Visibility = Visibility.Hidden},
                AddBtn = {Visibility = Visibility.Hidden}
            };
            //隐藏修改按钮
            Grid.SetColumn(minerInfoSet.DeleteBtn, 0);
            Grid.SetColumnSpan(minerInfoSet.DeleteBtn, 2);
            Grid.SetColumn(minerInfoSet.ExportResultsBtn, 2);
            Grid.SetColumnSpan(minerInfoSet.ExportResultsBtn, 2);
            minerInfoSet.ShowDialog();
        }

        /// <summary>
        /// 员工信息添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinerInfoAdd_OnClick(object sender, RoutedEventArgs e)
        {
            //员工信息查询及导出页面
            MinerInfoSet minerInfoSet = new MinerInfoSet
            {
                Owner = this,
                ChangeBtn = {Visibility = Visibility.Hidden},
                DeleteBtn = {Visibility = Visibility.Hidden}
            };
            //隐藏修改按钮
            Grid.SetColumn(minerInfoSet.AddBtn, 0);
            Grid.SetColumnSpan(minerInfoSet.AddBtn, 2);
            Grid.SetColumn(minerInfoSet.ExportResultsBtn, 2);
            Grid.SetColumnSpan(minerInfoSet.ExportResultsBtn, 2);
            minerInfoSet.ShowDialog();
        }

        /// <summary>
        /// 矿工考勤信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkTimeQuery_OnClick(object sender, RoutedEventArgs e)
        {
            WorkTimeQuery workTimeQuery = new WorkTimeQuery {Owner = this};
            workTimeQuery.ShowDialog();
        }

        /// <summary>
        /// 矿灯信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LampQuery_OnClick(object sender, RoutedEventArgs e)
        {
            LampQuery lampQuery = new LampQuery {Owner = this};
            lampQuery.ShowDialog();
        }

        /// <summary>
        /// 参数设置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Preferences_OnClick(object sender, RoutedEventArgs e)
        {
            PreferencesSet preferencesSet = new PreferencesSet {Owner = this};
            preferencesSet.ShowDialog();
        }

        /// <summary>
        /// 超时下井统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryTimeout_OnClick(object sender, RoutedEventArgs e)
        {
            QueryTimeout queryTimeout = new QueryTimeout {Owner = this};
            queryTimeout.ShowDialog();
        }

        /// <summary>
        /// 导入导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InOutData_OnClick(object sender, RoutedEventArgs e)
        {
            InOutData inOutData = new InOutData {Owner = this};
            inOutData.ShowDialog();
        }

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatebaseBackUp_OnClick(object sender, RoutedEventArgs e)
        {
            DatebaseBackUp datebaseBackUp = new DatebaseBackUp {Owner = this};
            datebaseBackUp.ShowDialog();
        }

        /// <summary>
        /// 充电柜管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CabinetManage_OnClick(object sender, RoutedEventArgs e)
        {
            CabinetManage cabinetManage = new CabinetManage {Owner = this};
            cabinetManage.ShowDialog();
        }

        /// <summary>
        /// 系统初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeviceInit_OnClick(object sender, RoutedEventArgs e)
        {
            DeviceInit deviceInit = new DeviceInit {Owner = this};
            deviceInit.ShowDialog();
        }

        /// <summary>
        /// 轮寻功能按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PollingCabinet_OnChecked(object sender, RoutedEventArgs e)
        {
            //清空轮寻计数
            _pollingtimes = 1;
            //如果checkbox选中，则开去定时器
            PollingCabinetTimer.Start();
        }

        private void PollingCabinetcheckbox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            //如果checkbox没选中，则关闭定时器
            PollingCabinetTimer.Stop();
            aaabox.Text = "轮询关闭 " ;

        }

        //添加到计时器中的事件
        public void PollingCabinetTimer_Tick(object sender, EventArgs e)
        {
            //执行更新左边100个柜门信息事件
            presenter.UpdateDoorShow(_cabinetId);
            //更新刷新次数
            aaabox.Text = "轮询次数：" + _pollingtimes;
            _pollingtimes=_pollingtimes+1;
        }
    }
}