using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.view.viewbean;
using MinerLampMangement.ViewModel;
using MongoDB.Bson;

namespace MinerLampMangement.view
{
    /// <summary>
    /// LampQuery.xaml 的交互逻辑
    /// </summary>
    public partial class LampQuery : Window
    {
        private List<ChargeTimes.ChargeTime> listinfo;
        private List<DoorInfoStatus.DoorStatusInfo> listDoorInfoStatus;

        public LampQuery()
        {
            InitializeComponent();
            //在datagrid首列加id
            this.DataGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(DataGridFirstColum.DataGrid_LoadingRow);
        }

        /// <summary>
        /// 矿灯状态查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
//        private void QueryBtn1_OnClick(object sender, RoutedEventArgs e)
//        {
//            //设置界面显示，隐藏一列，显示两列
//            DataGrid.Columns[5].Visibility = Visibility.Hidden;
//            DataGrid.Columns[6].Visibility = Visibility.Visible;
//            DataGrid.Columns[7].Visibility = Visibility.Visible;
//            var field1 = "";
//            var field2 = "";
//            //查看哪个radiobutton被选中
//            foreach (RadioButton radioButton in Grid1.Children.OfType<RadioButton>())
//            {
//                if (radioButton.IsChecked == true)
//                {
//                    field1 = radioButton.Tag.ToString();
//                }
//            }
//            //查看哪个radiobutton被选中
//            foreach (RadioButton radioButton in Grid2.Children.OfType<RadioButton>())
//            {
//                if (radioButton.IsChecked == true)
//                {
//                    field2 = radioButton.Tag.ToString();
//                }
//            }
//            var a =
//                MongoDbHelper<minerInfo>.FindPartFieldInfo(CollectionNames.MinerInfoTest, field2);
//            MessageBox.Show(a.ToJson());
//        }

        /// <summary>
        /// 查询矿灯充电次数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryBtn2_OnClick(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[5].Visibility = Visibility.Visible;
            DataGrid.Columns[6].Visibility = Visibility.Hidden;
            DataGrid.Columns[7].Visibility = Visibility.Hidden;
            DataGrid.Columns[8].Visibility = Visibility.Hidden;
            try
            {
                //查找员工信息和充电柜信息
                var minnumber = Int32.Parse(TextBox1.Text);
                var maxnumber = Int32.Parse(TextBox2.Text);
                //查找次数复合要求的充电柜
                var listDoorInfo = MongoDbHelper<DoorInfo>.FindDoorInfoByChargingNumber(minnumber, maxnumber);
                //查找充电柜对应的员工信息
                var listMinerInfo = new List<MinerInfo>();
                foreach (var doorinfo in listDoorInfo)
                {
                    //获取一条员工信息
                    var minerinfo = MongoDbHelper<MinerInfo>.FindInfoByObjected(CollectionNames.MinerInfoTest,
                        doorinfo.EmployeeObjectId);
                    //装入list
                    listMinerInfo.Add(minerinfo);
                }
                //把员工list和柜门list进行组装
                listinfo = ChargeTimes.ChargeTimeInfoShow(listDoorInfo,listMinerInfo);
                //显示
                DataGrid.ItemsSource = listinfo;
            }
            catch (Exception)
            {
                MessageBox.Show("输入数据不符合要求，无法查询！");
            }
        }

        /// <summary>
        /// 查询状态超过时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryBtn3_OnClick(object sender, RoutedEventArgs e)
        {
            //设置界面显示，隐藏一列，显示两列
            DataGrid.Columns[5].Visibility = Visibility.Hidden;
            DataGrid.Columns[6].Visibility = Visibility.Visible;
            DataGrid.Columns[7].Visibility = Visibility.Visible;
            DataGrid.Columns[8].Visibility = Visibility.Visible;
            try
            {
                //查找对应状态的矿灯信息
                var field = "";
                //查看哪个radiobutton被选中
                foreach (RadioButton radioButton in Grid3.Children.OfType<RadioButton>())
                {
                    if (radioButton.IsChecked == true)
                    {
                        field = radioButton.Tag.ToString();
                    }
                }
                var time = int.Parse(TimeTextBox.Text);
                var list1 = MongoDbHelper<DoorInfo>.FindDoorInfoByTime(CollectionNames.MinerLampTest, field, time);
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
                listDoorInfoStatus = DoorInfoStatus.DoorStatusInfoShow(list1, list2);
                DataGrid.ItemsSource = listDoorInfoStatus;
            }
            catch (Exception)
            {
                MessageBox.Show("输入数据不符合要求，无法查询！");
            }
        }

        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportResultsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TabItem1.IsSelected == true)
                {
                    string[] str = {"灯柜号", "灯位号", "姓名", "工号", "部门", "充电次数"};
                    //将list数据变成stringbuilder
                    var stringbuilder = ListToStringBuilder.ListMinerInfoTosStringBuilder(listinfo, str);
                    var FileName = DateTime.Now.ToString("yyyy-MM-dd-HHmm") + "充电次数数据导出文件";
                    Presenter.ExportResults(stringbuilder, 6,FileName);
                }
                else
                {
                    string[] str = {"灯柜号", "灯位号", "姓名", "工号", "部门", "班次", "开始时间", "持续时间"};
                    var stringbuilder = ListToStringBuilder.ListDoorInfoTosStringBuilder(listDoorInfoStatus, str);
                    var FileName = DateTime.Now.ToString("yyyy-MM-dd-HHmm") + "状态持续时间数据导出文件";
                    Presenter.ExportResults(stringbuilder, 8, FileName);
                }
            }
            catch
            {
                MessageBox.Show("导出数据失败，请检查后重试！");
            }
        }
    }
}