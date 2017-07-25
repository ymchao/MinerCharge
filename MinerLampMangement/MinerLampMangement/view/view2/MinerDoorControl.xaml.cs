using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;

namespace MinerLampMangement.view.view2
{
    /// <summary>
    /// MinerDoorControl.xaml 的交互逻辑
    /// </summary>
    public partial class MinerDoorControl : UserControl
    {
        private readonly Presenter _presenter;
        private MinerInfo _miner;
        private DoorInfo _doorInfo;

        public MinerDoorControl(Presenter presenter)
        {
            InitializeComponent();
            this._presenter = presenter;
            //为每个grid添加悬停事件
            GridMain.MouseMove += MyMouseMoveEvent;
        }

        /// <summary>
        /// 设置柜门的显示
        /// </summary>
        /// <param name="doorInfo"></param>
        /// <param name="i"></param>
        public void SetDoorShow(DoorInfo doorInfo, int i)
        {
            this._doorInfo = doorInfo;
            //设置每个柜门的图片
            var status = doorInfo.Status;
            SetImage(status);
            //获取柜门上员工的objectId
            var objectId = doorInfo.EmployeeObjectId;
            _miner = _presenter.GetMiner(objectId);
            var name = _miner.Name;
            Minernameshow.Text = i + 1 + "【" + name + "】"; //加1是为了从1开始计   
        }

        /// <summary>
        /// 为自定义控件设置图片显示
        /// </summary>
        /// <param name="status"></param>
        public void SetImage(MinerLampStatus status)
        {
            switch (status)
            {
                case MinerLampStatus.Charging:
                    Statusimage.Source =
                        new BitmapImage(new Uri("/Image/charging.png", UriKind.Relative));
                    break;
                case MinerLampStatus.Using:
                    Statusimage.Source =
                        new BitmapImage(new Uri("/Image/using.png", UriKind.Relative));
                    break;
                case MinerLampStatus.Full:
                    Statusimage.Source =
                        new BitmapImage(new Uri("/Image/full.png", UriKind.Relative));
                    break;
                case MinerLampStatus.ChargeProblem:
                    Statusimage.Source =
                        new BitmapImage(new Uri("/Image/problem.png", UriKind.Relative));
                    break;
                case MinerLampStatus.CommuncationProblem:
                    Statusimage.Source =
                        new BitmapImage(new Uri("/Image/problem.png", UriKind.Relative));
                    break;
                case MinerLampStatus.MultiProblem:
                    Statusimage.Source =
                        new BitmapImage(new Uri("/Image/problem.png", UriKind.Relative));
                    break;
                case MinerLampStatus.UnInit:
                    Statusimage.Source =
                        new BitmapImage(new Uri("/Image/wait.png", UriKind.Relative));
                    break;
            }
        }

        /// <summary>
        /// 鼠标悬停事件 将员工信息显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MyMouseMoveEvent(object sender, MouseEventArgs e)
        {
            //            第一种方法获取员工的柜号和柜门号用于检索员工信息
            //            //获取自己悬停grid的名字
            //            var gridname = this.Name;
            //            //将grid名字的前四个字母btn砍掉就是柜门号了
            //            var doorId = gridname.Substring(4);
            //            //获取柜号
            //            var cabinetid = Presenter.GroupId;
            //第二种方法，直接获取grid格子中的MinerObjectId,然后检索员工信息
            //minernum.Text = _doorInfo.EmployeeObjectId;
            //如果为空，则不进行任何操作
            if (_miner != null)
            {
                minername.Text = "姓名：" + _miner.Name;
                minernum.Text = "工号：" + _miner.JobNumber;
                lampstyle.Text = "矿灯型号：" + _miner.LampTypes;
                chargenum.Text = "充电次数：" + _doorInfo.RemainChargeTime;
                switch (_doorInfo.Status)
                {
                    case MinerLampStatus.UnInit:
                        status.Text = "矿灯状态：未初始化";
                        break;
                    case MinerLampStatus.Charging:
                        status.Text = "矿灯状态：充电";
                        break;
                    case MinerLampStatus.Full:
                        status.Text = "矿灯状态：充满";
                        break;
                    case MinerLampStatus.Using:
                        status.Text = "矿灯状态：使用";
                        break;
                    case MinerLampStatus.ChargeProblem:
                        status.Text = "矿灯状态：充电故障";
                        break;
                    case MinerLampStatus.CommuncationProblem:
                        status.Text = "矿灯状态：通信故障";
                        break;
                    case MinerLampStatus.MultiProblem:
                        status.Text = "矿灯状态：多种故障";
                        break;
                }
                statustime.Text = "状态存在：" + _doorInfo.StatusTime;
                department.Text = "部门：" + _miner.Department;
            }
        }

        /// <summary>
        /// 为每个grid柜门设置双击事件（获取员工的信息）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinerDoorControl_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (_miner != null)
                {
                    //显示员工信息窗口
                    MinerInfoShow minerInfoShow = new MinerInfoShow();
                    //将员工信息传递给界面
                    minerInfoShow.MinerInfo = _miner;
                    minerInfoShow.Show();
                    //将员工信息显示出来
                    minerInfoShow.SetMinerInfo(_miner);
                }
                else
                {
                    MessageBox.Show("未获取有效数据，请稍后重试！");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("未获取有效数据，请稍后重试！");
            }
        }
    }
}