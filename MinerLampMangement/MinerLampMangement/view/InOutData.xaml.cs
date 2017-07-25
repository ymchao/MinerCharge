using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.view.viewbean;
using MinerLampMangement.ViewModel;
using MessageBox = System.Windows.MessageBox;

namespace MinerLampMangement.view
{
    /// <summary>
    /// InOutData.xaml 的交互逻辑
    /// </summary>
    public partial class InOutData : Window
    {
        public InOutData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 员工信息导入数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportMinerInfoBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //ImportEmployee();
        }
        //封装成函数
        public static void ImportEmployee()
        {
            var std = new OpenFileDialog()
            {
                InitialDirectory = "D:\\",
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
            };

            if (std.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //做异常处理，当需要导入的文件被打开时，将提示关闭文件
                try
                {
                    //文件路径
                    var fielpath = std.FileName;
                    var fileStream = new FileStream(fielpath, FileMode.Open, FileAccess.Read);
                    var streamReader = new StreamReader(fileStream, Encoding.GetEncoding(936)); //gb2312、简体中文
                    //记录每次读取的一行记录  
                    var strLine = streamReader.ReadLine();
                    //弹出确认框
                    MessageBoxResult result = MessageBox.Show("确认进行员工数据导入吗？", "提示",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    //如果点击确定
                    if (result == MessageBoxResult.Yes)
                    {
                        //执行写入数据库，如果不为空，则一直读
                        while (strLine.Length > 1)
                        {
                            strLine = streamReader.ReadLine();
                            //当读完最后一行时，跳出程序
                            if (strLine == null)
                            {
                                break;
                            }
                            //将excel每行的数据（都好区分的）放入一个数组中
                            string[] info = strLine.Split(new char[] { ',' });
                            //新建一个员工，并给员工信息
                            var minerInfo = new MinerInfo();
                            minerInfo.GroupId = int.Parse(info[0]);
                            minerInfo.DeviceId = int.Parse(info[1]);
                            minerInfo.Name = info[2];
                            minerInfo.JobNumber = info[3];
                            minerInfo.Sex = info[4];
                            minerInfo.BornDate = Convert.ToDateTime(info[5]);
                            minerInfo.MaritalStatus = info[6];
                            minerInfo.National = info[7];
                            minerInfo.IdentityCardId = info[8];
                            minerInfo.NativePlace = info[9];
                            minerInfo.Address = info[10];
                            minerInfo.Blood = info[11];
                            minerInfo.Image = info[12];
                            minerInfo.PoliticalStatus = info[13];
                            minerInfo.Education = info[14];
                            minerInfo.PhoneNum = info[15];
                            minerInfo.ContractTypes = info[16];
                            minerInfo.Position = info[17];
                            minerInfo.JoinTime = Convert.ToDateTime(info[18]);
                            minerInfo.Department = info[19];
                            minerInfo.EquipLampTime = Convert.ToDateTime(info[20]);
                            minerInfo.LampTypes = info[21];
                            minerInfo.Classes = info[22];
                            minerInfo.EquipSelfRescuerTime = Convert.ToDateTime(info[23]);
                            minerInfo.SelfRescuerId = info[24];
                            minerInfo.Others = info[25];
                            MongoDbHelper<MinerInfo>.Insert(CollectionNames.MinerInfoTest, minerInfo);
                           
                        }
                        streamReader.Close();
                        fileStream.Close();
                       MessageBox.Show("员工信息导入完成！");
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("文件已打开，请先关闭文件！");
                }
            }
        }

        /// <summary>
        /// 员工信息导出到CSV文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportMinerInfoBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //先获取所有员工信息
                var allminerinfo = MongoDbHelper<MinerInfo>.FindAllDocuments(CollectionNames.MinerInfoTest);
                string[] str =
                {
                    "灯柜号", "柜门号", "姓名", "工号", "性别", "出生年月", "婚否", "名族", "身份证号", "籍贯", "地址", "血型",
                    "图片", "党员", "学历", "电话", "用工性质", "职位", "参加工作时间", "部门", "矿灯配备时间", "矿灯类型", "班次", "自救器配备时间",
                    "自救器型号", "其他"
                };
                var stringbuilder = ListToStringBuilder.ListMinerInfoToStringBuilder(allminerinfo, str);
                var FileName = DateTime.Now.ToString("yyyy-MM-dd-HHmm") + "员工信息数据导出文件";
                Presenter.ExportResults(stringbuilder, 26, FileName);
            }
            catch
            {
                MessageBox.Show("导出数据失败，请检查后重试！");
            }
        }

        /// <summary>
        /// TM 卡导入文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportTmInfoBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ImportTmInfo();
        }
        //封装成方法
        public static void ImportTmInfo()
        {
            var std = new OpenFileDialog()
            {
                InitialDirectory = "D:\\",
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
            };

            if (std.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //做异常处理，当需要导入的文件被打开时，将提示关闭文件
                try
                {
                    //文件路径
                    var fielpath = std.FileName;
                    var fileStream = new FileStream(fielpath, FileMode.Open, FileAccess.Read);
                    var streamReader = new StreamReader(fileStream, Encoding.GetEncoding(936)); //gb2312、简体中文
                    //记录每次读取的一行记录  
                    var strLine = streamReader.ReadLine();
                    //弹出确认框
                    MessageBoxResult result = MessageBox.Show("确认进行Tm卡数据导入吗？", "提示",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);
                    //如果点击确定
                    if (result == MessageBoxResult.Yes)
                    {
                        //执行写入数据库，如果不为空，则一直读
                        while (strLine.Length > 1)
                        {
                            strLine = streamReader.ReadLine();
                            //当读完最后一行时，跳出程序
                            if (strLine == null)
                            {
                                break;
                            }
                            //将excel每行的数据（都好区分的）放入一个数组中
                            string[] info = strLine.Split(new char[] { ',' });
                            //写入或者是更新tm卡号
                            MongoDbHelper<DoorInfo>.UpdateTmNumberInfo(int.Parse(info[0]), int.Parse(info[1]), info[3]);
                          }
                        streamReader.Close();
                        fileStream.Close();
                        MessageBox.Show("Tm卡数据导入完成！");
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("文件已打开，请先关闭文件！");
                }
            }
        }

        /// <summary>
        /// 导出TM卡信息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportTmInfoBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //先获取所有Tm卡信息，也就是hi柜门信息
                var alldoorinfo = MongoDbHelper<DoorInfo>.FindAllDocuments(CollectionNames.MinerLampTest);
                string[] str =
                {
                    "灯柜号", "柜门号", "Tm卡编号", "Tm卡号",
                };
                var stringbuilder = ListToStringBuilder.ListTmInfoToStringBuilder(alldoorinfo, str);
                var FileName = DateTime.Now.ToString("yyyy-MM-dd-HHmm") + "TM卡数据导出文件";
                Presenter.ExportResults(stringbuilder, 4, FileName);
            }
            catch
            {
                MessageBox.Show("导出数据失败，请检查后重试！");
            }
        }

        /// <summary>
        /// 数据删除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deletebtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //只有选中，才能执行删除操作
                if (CheckBox.IsChecked == true)
                {
                    if (DatePicker.Text != "")
                    {
                        var endTime = Convert.ToDateTime(DatePicker.Text);
                        //执行删除操作
                        MongoDbHelper<DoorInfo>.DeleteLampStatusByTime(endTime);
                        MessageBox.Show("记录删除成功！");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("记录删除失败，请检查后重试！");
            }
        }
    }
}