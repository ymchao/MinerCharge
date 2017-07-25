using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.view.viewbean;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view
{
    /// <summary>
    /// QueryTimeout.xaml 的交互逻辑
    /// </summary>
    public partial class QueryTimeout : Window
    {
        private List<DoorInfoStatus.DoorStatusInfo> listdDoorStatusInfos;

        public QueryTimeout()
        {
            InitializeComponent();
            //在datagrid首列加id
            this.DataGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(DataGridFirstColum.DataGrid_LoadingRow);
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //小时需要乘以60分钟
                var time = int.Parse(TimeTextBox.Text) * 60;
                //找到超时柜门的信息
                var list1 = MongoDbHelper<DoorInfo>.FindDoorInfoByTime(CollectionNames.MinerLampTest,
                    MinerLampStatus.Using.ToString(), time);
                //查找对应状态的员工信息
                var list2 = new List<MinerInfo>();
                foreach (DoorInfo doorInfo in list1)
                {
                    var minerobjectid = doorInfo.EmployeeObjectId;
                    MinerInfo minerInfo =
                        MongoDbHelper<MinerInfo>.FindInfoByObjected(CollectionNames.MinerInfoTest,
                            minerobjectid);
                    list2.Add(minerInfo);
                }
                //调用DoorInfoStatus的DoorSatusInfoShow方法，拼接list1和list2,显示到dategrid上
                listdDoorStatusInfos = DoorInfoStatus.DoorStatusInfoShow(list1, list2);
                DataGrid.ItemsSource = listdDoorStatusInfos;
            }
            catch
            {
                MessageBox.Show("输入数据不符合要求，无法查询！");
            }
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportResultsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] str = {"灯柜号", "灯位号", "姓名", "工号", "部门", "班次", "下井时间", "超时总计"};
                var stringbuilder = ListToStringBuilder.ListDoorInfoTosStringBuilder(listdDoorStatusInfos, str);
                var FileName = DateTime.Now.ToString("yyyy-MM-dd-HHmm") + "下井超时数据导出文件";
                Presenter.ExportResults(stringbuilder, 2, FileName);
                MessageBox.Show("导出数据成功！");
            }
            catch
            {
                MessageBox.Show("导出数据失败，请检查后重试！");
            }
        }
    }
}