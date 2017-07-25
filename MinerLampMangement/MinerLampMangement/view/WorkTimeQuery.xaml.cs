using System;
using System.Collections.Generic;
using MinerLampMangement.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.view.viewbean;

namespace MinerLampMangement.view
{
    /// <summary>
    /// WorkTimeQuery.xaml 的交互逻辑
    /// </summary>
    public partial class WorkTimeQuery : Window
    {
        private List<Attendance.Attendanceinfo> _allinfolist;
        private List<MinerInfo> _minerinfolist;
      
        public WorkTimeQuery()
        {
            InitializeComponent();
            //在datagrid首列加id
            this.DataGrid1.LoadingRow += new EventHandler<DataGridRowEventArgs>(DataGridFirstColum.DataGrid_LoadingRow);
            this.DataGrid2.LoadingRow += new EventHandler<DataGridRowEventArgs>(DataGridFirstColum.DataGrid_LoadingRow);
            //将radiobutton click 添加到事件中
            DepartmentRadionButton.Checked += new RoutedEventHandler(RadioButton_Checked1);
            CabinetRadionButton.Checked += new RoutedEventHandler(RadioButton_Checked1);
        }

        /// <summary>
        /// radiobutton的checked事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked1(object sender, RoutedEventArgs e)
        {
            RadioButton btn = sender as RadioButton;
            if (btn == null)
                return;
            //如果选中的是部门Radiobutton
            if (btn.Name == "DepartmentRadionButton")
            {
                //获取去重后的部门信息
                var listinfo = MongoDbTool.DistinctInfoString(CollectionNames.MinerInfoTest, "Department");
                DataGridTextColumn1.Header = "部门";
                DataGridTextColumn1.Binding = new Binding("Infovalue");
                var list = Toinfo.ToInfos(listinfo);
                DataGrid1.ItemsSource = list;
            }
            //如果选中的是柜门Radiobutton
            else if (btn.Name == "CabinetRadionButton")
            {
                //获取去重后的柜号信息
                var listinfo = MongoDbTool.DistinctInfoInt(CollectionNames.MinerInfoTest, "GroupId");
                DataGridTextColumn1.Header = "柜号";
                DataGridTextColumn1.Binding = new Binding("Infovalue");
                var list = Toinfo.ToInfos(listinfo);
                DataGrid1.ItemsSource = list;
            }
        }


        /// <summary>
        /// datagrid1的双击事件，更新右侧的datagrid2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid1_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //minerinfo 为双击选中的员工信息对象
            var datagrid1Selected = DataGrid1.CurrentItem as Toinfo.Info;
            if (datagrid1Selected != null)
            {
                //获取双击选中行的值
                var selectedvalue = datagrid1Selected.Infovalue;
                //填充datagrid2,先判断时间选中没
                if (DatePicker1.Text != "" && DatePicker2.Text != "")
                {
                    if (DatePicker1.Text != DatePicker2.Text)
                    {
                        //首先获取选中的柜号或者部门的所有员工信息
                        if (DataGridTextColumn1.Header.ToString() == "部门")
                        {
                            //首先将字段和值放入list
                            List<string> _fieldandvalue = new List<string>();
                            _fieldandvalue.Add("Department");
                            _fieldandvalue.Add(selectedvalue);
                            //获取所有员工信息
                            _minerinfolist = MongoDbHelper<MinerInfo>.FindInfoByField(CollectionNames.MinerInfoTest,
                                _fieldandvalue);
                        }
                        else if (DataGridTextColumn1.Header.ToString() == "柜号")
                        {
                            //获取所有员工信息
                            _minerinfolist = MongoDbHelper<MinerInfo>.FindInfoByField(CollectionNames.MinerInfoTest,
                                "GroupId", int.Parse(selectedvalue));
                        }
                        //获取DatePicker的时间段
                        var datePicker1 = Convert.ToDateTime(DatePicker1.Text).ToString("yyyy-MM-dd HH:mm:ss");
                        var datePicker2 = Convert.ToDateTime(DatePicker2.Text).ToString("yyyy-MM-dd HH:mm:ss");
                        //将员工信息和时间段信息全部传入方法中
                        _allinfolist = Attendance.AttendanceInfoShow(_minerinfolist, datePicker1, datePicker2);
                        if (_allinfolist.Count == 0)
                        {
                            MessageBox.Show("未查询到符合条件的值！");
                        }
                        else
                        {
                            DataGrid2.ItemsSource = _allinfolist;
                        }
                    }
                    else
                    {
                        MessageBox.Show("时间区间不能设置相同！");
                    }
                }
                else
                {
                    MessageBox.Show("时间区间未设置正确！");
                }
            }
        }

        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportDate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] str = {"灯柜号", "灯位号", "姓名", "工号", "部门", "班次", "下井时间", "上井时间", "工作时长"};
                var stringbuilder = ListToStringBuilder.ListAttendanceInfoTosStringBuilder(_allinfolist, str);
                var FileName = DateTime.Now.ToString("yyyy-MM-dd-HHmm") + "考勤数据导出文件";
                Presenter.ExportResults(stringbuilder, 9, FileName);
            }
            catch
            {
                MessageBox.Show("导出数据失败，请检查后重试！");
            }
        }

        /// <summary>
        /// 将去重后的list<string>包装成list<info>
        /// </summary>
        public class Toinfo
        {
            //当传入的是list<string>时
            public static List<Info> ToInfos(List<string> info)
            {
                var list = new List<Info>();
                info.ForEach(a => { list.Add(new Info(a)); });
                return list;
            }

            //当传入的是list<int>时
            public static List<Info> ToInfos(List<int> info)
            {
                var list = new List<Info>();
                info.ForEach(a => { list.Add(new Info(a.ToString())); });
                return list;
            }

            public class Info
            {
                public Info(string a)
                {
                    Infovalue = a;
                }

                public string Infovalue { get; set; }
            }
        }


        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //判断radion哪个选中
            if (NameRadioButton.IsChecked == true)
            {

                //首先将字段和值放入list
                List<string> _fieldandvalue = new List<string>();
                _fieldandvalue.Add("Name");
                _fieldandvalue.Add(NameTextBox.Text);
                //查找此人信息
                _minerinfolist = MongoDbHelper<MinerInfo>.FindInfoByField(CollectionNames.MinerInfoTest, _fieldandvalue);
            }
            else if (IdRadioButton.IsChecked == true)
            {
                //首先将字段和值放入list
                List<string> _fieldandvalue = new List<string>();
                _fieldandvalue.Add("JobNumber");
                _fieldandvalue.Add(IdNumberTextBox.Text);
                //查找此人信息
                _minerinfolist = MongoDbHelper<MinerInfo>.FindInfoByField(CollectionNames.MinerInfoTest, _fieldandvalue);
            }
            //填充datagrid2,先判断时间选中没
            if (DatePicker3.Text != "" && DatePicker4.Text != "")
            {
                if (DatePicker3.Text != DatePicker4.Text)
                {
                    //获取DatePicker的时间段
                    var datePicker3 = Convert.ToDateTime(DatePicker3.Text).ToString("yyyy-MM-dd HH:mm:ss");
                    var datePicker4 = Convert.ToDateTime(DatePicker4.Text).ToString("yyyy-MM-dd HH:mm:ss");
                    //将员工信息和时间段信息全部传入方法中
                    if (_minerinfolist.Count != 0)
                    {
                        _allinfolist = Attendance.AttendanceInfoShow(_minerinfolist, datePicker3, datePicker4);
                        if (_allinfolist.Count == 0)
                        {
                            MessageBox.Show("未查询到此员工相应时间段的信息！");
                        }
                        else
                        {
                            DataGrid2.ItemsSource = _allinfolist;
                        }
                    }
                    else
                    {
                        MessageBox.Show("未找到此员工！");
                    }
                }
                else
                {
                    MessageBox.Show("时间区间不能设置相同！");
                }
            }
            //如果事件设置相同，则查询此所有信息
//            else if(DatePicker3.Text == "" && DatePicker4.Text == "")
//            {
//                //将员工信息和时间段信息全部传入方法中
//                if (_minerinfolist.Count != 0)
//                {
//                    _allinfolist = Attendance.AttendanceInfoShow(_minerinfolist);
//                    if (_allinfolist.Count == 0)
//                    {
//                        MessageBox.Show("未查询到此员工相应时间段的信息！");
//                    }
//                    else
//                    {
//                        DataGrid2.ItemsSource = _allinfolist;
//                    }
//                }
//                else
//                {
//                    MessageBox.Show("未找到此员工！");
//                }
//            }
            else
            {
                MessageBox.Show("时间区间未设置完整！");
            }
        }
    }
}