using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.view.viewbean;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view
{
    /// <summary>
    /// LampStatusInfoShow.xaml 的交互逻辑
    /// </summary>
    public partial class MinerInfoShow : Window
    {
        //设定一个判断条件，让tabitem只有在第一次切换时才执行getattendance语句
        private int _i = 0;
        //通过上个窗口，将员工信息传进来
        public MinerInfo MinerInfo;

        private List<StatusHistory.LampInfo> _listlampinfo;
        private List<Attendance.Attendanceinfo> _listattendanceinfo;

        public MinerInfoShow()
        {
            InitializeComponent();
            //在datagrid首列加id
            //在datagrid首列加id
            this.DataGrid1.LoadingRow += new EventHandler<DataGridRowEventArgs>(DataGridFirstColum.DataGrid_LoadingRow);
            this.DataGrid2.LoadingRow += new EventHandler<DataGridRowEventArgs>(DataGridFirstColum.DataGrid_LoadingRow);
        }

        /// <summary>
        /// tabitem切换到考勤与矿灯记录时，执行语句，填入考勤和矿灯信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //如果切换到tabitem2且是第一次，则执行语句GetAttendanceInfo
            if (TabItem2.IsSelected && _i == 0)
            {
                //根据AttendanceObejectId字段，去考勤表中寻找相应的考勤信息
                GetAttendanceInfo(MinerInfo.Id.ToString());
                _i++;
            }
        }

        /// <summary>
        /// 获取双击选中的员工信息
        /// </summary>
        /// <param name="minerInfo"></param>
        public void SetMinerInfo(MinerInfo minerInfo)
        {
            //让MinerInfoShowControl窗口获得员工数据，填入界面中
            MinerInfoShowControl.SetMinerInfoShow(minerInfo);
        }

        /// <summary>
        /// 获取考勤信息,此界面只显示近三个月的信息
        /// </summary>
        /// <param name="attendanceObjectId"></param>
        public void GetAttendanceInfo(string attendanceObjectId)
       {
            try
            {
                //根据attendanceObjectId字段，去考勤表中寻找相应的考勤信息
                var attendanceinfo = MongoDbHelper<AllStatusHistoryInfo>.FindInfoByObjected(
                    CollectionNames.MinerAttendanceTest,
                    attendanceObjectId);
                //设置当前时间和前三个月的时间
                var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var beforetime = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd HH:mm:ss");
           
                //先填充datagrid2
                _listlampinfo = StatusHistory.LampStatusInfoShow(attendanceinfo, beforetime, now);
                DataGrid2.ItemsSource = _listlampinfo;

                //利用datagrid2的信息，填充datagrid1
                _listattendanceinfo = Attendance.AttendanceInfoShow(_listlampinfo);
                DataGrid1.ItemsSource = _listattendanceinfo;
            }
            catch (Exception)
            {
                MessageBox.Show("考勤信息获取失败，请稍后重试！");
            }
        }

        /// <summary>
        /// 导出矿灯记录表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportResultsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] str = {"状态", "时间"};
                var stringbuilder = ListToStringBuilder.ListLampInfoTosStringBuilder(_listlampinfo, str);
                var FileName = DateTime.Now.ToString("yyyy-MM-dd-HHmm") + "状态时间数据导出文件";
                Presenter.ExportResults(stringbuilder, 2, FileName);
            }
            catch
            {
                MessageBox.Show("导出数据失败，请检查后重试！");
            }
        }

    }
}