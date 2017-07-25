using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.view.viewbean;
using MinerLampMangement.ViewModel;
using CheckBox = System.Windows.Controls.CheckBox;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using MongoDB.Bson;
using Window = System.Windows.Window;

namespace MinerLampMangement.view
{
    /// <summary>
    /// MinerInfoMaintain.xaml 的交互逻辑
    /// </summary>
    public partial class MinerInfoSet : Window
    {
        //做一个判断是不是备用充电位的标签
        private int _tab = 0;

        public MinerInfoSet()
        {
            InitializeComponent();
            //在datagrid首列加id
            this.DataGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(DataGridFirstColum.DataGrid_LoadingRow);
            MinerInfoShowControl8.TmNumberBox.IsReadOnly = true; //让Tm卡号无法编辑
            MinerInfoShowControl8.CabinetIdBox.IsReadOnly = true; //让柜号无法编辑
            MinerInfoShowControl8.DoorIdBox.IsReadOnly = true; //让柜门号无法编辑
            MinerInfoShowControl8.NameBox.IsReadOnly = true; //让姓名无法编辑
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
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Searchbtn_OnClick(object sender, RoutedEventArgs e)
        {
            var fieldandvalue = new List<string>();
            foreach (var checkBox in Grid.Children.OfType<CheckBox>())
            {
                if (checkBox.IsChecked == true)
                {
                    fieldandvalue.Add(checkBox.Name);
                    switch (checkBox.Name)
                    {
                        case "Name":
                            fieldandvalue.Add(Namebox.Text);
                            break;
                        case "JobNumber":
                            fieldandvalue.Add(Jobidbox.Text);
                            break;
                        case "IdentityCardId":
                            fieldandvalue.Add(Identityidbox.Text);
                            break;
                        case "GroupId":
                            fieldandvalue.Add(CabinetIdbox.Text);
                            break;
                        case "DeviceId":
                            fieldandvalue.Add(DoorIdbox.Text);
                            break;
                        case "Department":
                            fieldandvalue.Add(Departmentbox.Text);
                            break;
                    }
                }
            }
            //如果键值对的字段长度不到2，说明不是一个完整的field和value字段，是无法完成查询操作的
            if (fieldandvalue.Count >= 2)
            {
                //执行查询操作
                var list = MongoDbHelper<MinerInfo>.FindInfoByField(CollectionNames.MinerInfoTest, fieldandvalue);
                //如果count为0,则代表未检索到任何对象
                if (list.Count != 0)
                {
                    //将查询结果给datagrid用于显示
                    DataGrid.ItemsSource = list;
                }
                else
                {
                    MessageBox.Show("未查询到任何符合条件的信息，请重新设定条件！");
                }
            }
            else
            {
                MessageBox.Show("未输入任何条件，无法查询！");
            }
        }

        /// <summary>
        /// DataGrid双击事件，双击后，将员工信息传给MinerInfoShowControl8显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //minerinfo 为双击选中的员工信息对象
            var minerInfodoubleselect = DataGrid.SelectedItem as MinerInfo;
            if (minerInfodoubleselect != null)
            {
                //将选中的员工信息显示在MinerInfoShowControl8上
                MinerInfoShowControl8.SetMinerInfoShow(minerInfodoubleselect);
                if (minerInfodoubleselect.Name == "备用")
                {
                    _tab = 1;
                    MinerInfoShowControl8.NameBox.IsReadOnly = false; //让姓名可以编辑
                }
                else
                {
                    _tab = 0;
                    MinerInfoShowControl8.NameBox.IsReadOnly = true; //让姓名无法编辑
                }
            }
        }

        /// <summary>
        /// 修改按钮的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_tab == 1)
            {
                MessageBox.Show("此充电位是备用充电位，无法修改信息，只允许添加操作！");
            }
            else
            {
                try
                {
                    //通过页面，拿到输入的员工信息
                    var minerinfo = MinerInfoShowControl8.GetMinerInfo();
                    //执行更新操作，根据柜号和柜门号搜索
                    MongoDbTool.UpdateMinerInfo(minerinfo.GroupId, minerinfo.DeviceId, minerinfo);
                    //修改员工信息，包括部门信息后，告知服务器
                    var url = "/operate/devices/update/" + minerinfo.GroupId +
                              "/" + minerinfo.DeviceId + "?name=true&department=true&cardnumber=true";
                    var result = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(result == "success" ? "员工信息修改成功，服务器收到正确数据！" : "服务器未受到正确数据！");
                }
                catch (Exception)
                {
                    MessageBox.Show("员工信息修改失败！");
                }
            }
        }

        /// <summary>
        /// 删除员工按钮事件,删除相应柜号的员工后，此充电位变为备用状态
        /// 其实也就是新建一个备用员工员工，并删除旧员工的OBJECTID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //不是备用充电位，才能进行删除操作
            if (_tab == 0)
            {
                try
                {
                //通过页面，拿到此充电位的员工位置信息，删除此员工的objectid和柜号，柜门号，并备注辞职。
                var oldminerinfo = MinerInfoShowControl8.GetMinerInfo();
                //将此人的柜子变为备用状态
                var newminerinfo = new MinerInfo()
                {
                    //以下就是删除员工，将此充电位变为备用状态，以下几个信息是不能变化的
                    //次充电位员工姓名变为备用
                    Name = "备用",
                    //柜号不变
                    GroupId = oldminerinfo.GroupId,
                    //柜门号不变
                    DeviceId = oldminerinfo.DeviceId,
                    //objectid不变
                    Id = oldminerinfo.Id,
                };
                //将此人信息修改,不分配柜号和柜门号了
                oldminerinfo.GroupId = 0;
                oldminerinfo.DeviceId = 0;
                //给此员工新生成一个objectid
                oldminerinfo.Id = ObjectId.GenerateNewId();
                //然后将此人重新插入到员工数据库
                MongoDbHelper<MinerInfo>.Insert(CollectionNames.MinerInfoTest, oldminerinfo);

                //将备用更新到数据库
                MongoDbTool.UpdateMinerInfo(newminerinfo.GroupId, newminerinfo.DeviceId, newminerinfo);
                //去相应的柜门上更新位备用状态(20170525,此操作不用执行)
                //MongoDbTool.UpdateCabinetInfo(oldminerinfo.GroupId, oldminerinfo.DeviceId);
                //清空此人的考勤记录（删除当前时间的所有信息）
                MongoDbHelper<AllStatusHistoryInfo>.DeleteLampStatusByTime(DateTime.Now, newminerinfo.Id);
                //告知服务器柜门数据更新，服务器下发新数据到板子
               var url = "/operate/devices/update/" + 1 +
                          "/" + 2 + "?name=true&department=true&cardnumber=true";
                var result = HttpClient.HttpClient.HttpGet(url, "text/plain");
                MessageBox.Show(result == "success" ? "柜门更新成功，服务器收到正确数据！" : "服务器未受到正确数据！");
                }
                catch (Exception)
                {
                    MessageBox.Show("员工删除失败！");
                }
            }
            else
            {
                MessageBox.Show("此充电位已是备用状态！");
            }
        }

        /// <summary>
        /// 添加员工按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //如果双击选中的是备用充电位，则可以进行员工信息添加
            if (_tab == 1)
            {
                try
                {
                    //通过页面，拿到输入的员工信息
                    var minerinfo = MinerInfoShowControl8.GetMinerInfo();
                    //执行更新操作，根据柜号和柜门号搜索
                    MongoDbTool.UpdateMinerInfo(minerinfo.GroupId, minerinfo.DeviceId, minerinfo);
                    //告知服务器更新操作
                    var url = "/operate/devices/update/" + minerinfo.GroupId +
                              "/" + minerinfo.DeviceId + "?name=true&department=true&cardnumber=true&maxgroupid=true";
                    var result = HttpClient.HttpClient.HttpGet(url, "text/plain");
                    MessageBox.Show(result == "success" ? "柜门添加员工成功，服务器收到正确数据！" : "服务器未受到正确数据！");
                }
                catch (Exception)
                {
                    MessageBox.Show("充电位添加员工失败！");
                }
            }
            else
            {
                MessageBox.Show("此充电位不是备用充电位，无法添加添加员工，请选择备用充电位！");
            }
        }

        /// <summary>
        /// 导出员工信息到word模板事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportResultsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            string urlstring = @"D:\VSproject\MinerLampMangement\员工信息模板.doc";
            if (!File.Exists(urlstring))
            {
                MessageBox.Show("员工信息模板不存在！");
            }
            else
            {
                SaveFileDialog std = new SaveFileDialog();
                //保存路径，初始化打开D盘
                std.InitialDirectory = "D:\\";
                std.Filter = "doc文件(*.doc)|*.doc";
                std.FileName = MinerInfoShowControl8.NameBox.Text + " 个人信息";
                if (std.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string s = std.FileName;
                    File.Copy(urlstring, s, true);
                    object filename = s;
                    Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                    Document wordDoc = new Document();
                    Object Nothing = System.Reflection.Missing.Value;
                    wordDoc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                        ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                        ref Nothing, ref Nothing, ref Nothing, ref Nothing);
                    Dictionary<string, string> DList = new Dictionary<string, string>();
                    DList.Add("Name", MinerInfoShowControl8.NameBox.Text);
                    DList.Add("Sex", MinerInfoShowControl8.SexBox.Text);
                    DList.Add("BornDate", MinerInfoShowControl8.BornDateBox.Text);
                    DList.Add("MaritalStatus", MinerInfoShowControl8.MaritalStatusBox.Text);
                    DList.Add("NativePlace", MinerInfoShowControl8.NativePlaceBox.Text);
                    DList.Add("PoliticalStatus", MinerInfoShowControl8.PoliticalStatusBox.Text);
                    DList.Add("Education", MinerInfoShowControl8.EducationBox.Text);
                    DList.Add("Blood", MinerInfoShowControl8.BloodBox.Text);
                    DList.Add("IdentityCardId", MinerInfoShowControl8.IdentityCardIdBox.Text);
                    DList.Add("PhoneNum", MinerInfoShowControl8.PhoneNumbox.Text);
                    DList.Add("Address", MinerInfoShowControl8.AddressBox.Text);
                    DList.Add("Others", MinerInfoShowControl8.OthersBox.Text);
                    DList.Add("CardNumber", MinerInfoShowControl8.TmNumberBox.Text);
                    DList.Add("GroupId", MinerInfoShowControl8.CabinetIdBox.Text);
                    DList.Add("DeviceId", MinerInfoShowControl8.DoorIdBox.Text);
                    DList.Add("Position", MinerInfoShowControl8.PositionBox.Text);
                    DList.Add("JobNumber", MinerInfoShowControl8.JobNumberBox.Text);
                    DList.Add("Department", MinerInfoShowControl8.DepartmentBox.Text);
                    DList.Add("ContractTypes", MinerInfoShowControl8.ContractTypesBox.Text);
                    DList.Add("LampTypes", MinerInfoShowControl8.LampTypesBox.Text);
                    DList.Add("Classes", MinerInfoShowControl8.ClassesBox.Text);
                    DList.Add("JoinTime", MinerInfoShowControl8.JoinTimeBox.Text);
                    DList.Add("EquipLampTime", MinerInfoShowControl8.EquipLampTimeBox.Text);
                    DList.Add("SelfRescuerNum", MinerInfoShowControl8.SelfRescuerIdBox.Text);
                    DList.Add("EquipSelfRescuerTime", MinerInfoShowControl8.EquipSelfRescuerTypeBox.Text);
                    DList.Add("LampChargingCount", MinerInfoShowControl8.LampChargingCountBox.Text);
                    object what = WdGoToItem.wdGoToBookmark;
                    object WordMarkName;
                    foreach (var item in DList)
                    {
                        WordMarkName = item.Key;
                        wordDoc.ActiveWindow.Selection.GoTo(ref what, ref Nothing, ref Nothing, ref WordMarkName);
                        wordDoc.ActiveWindow.Selection.TypeText(item.Value);
                    }
                    object IsSave = true;
                    wordDoc.Close(ref IsSave, ref Nothing, ref Nothing);
                    wordApp.Quit();
                    MessageBox.Show("导出成功！！");
                }
            }
        }
    }
}