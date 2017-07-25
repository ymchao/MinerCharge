using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;
using MessageBox = System.Windows.MessageBox;

namespace MinerLampMangement.view
{
    public class Presenter
    {
        public static int CabinetId { get; private set; } = 0; //获取充电柜的ID
        public static int CabinetNum { get; private set; } = 0; //获取充电柜的数量

        private readonly MainWindow _window;

        //一百个柜门信息
        private List<DoorInfo> _allDocument;

        public Presenter(MainWindow window)
        {
            this._window = window;
        }

        /// <summary>
        /// 根据ObjectId获取员工的信息表
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        public MinerInfo GetMiner(string objectId)
        {
            return MongoDbHelper<MinerInfo>.FindInfoByObjected(CollectionNames.MinerInfoTest, objectId);
        }

        /// <summary>
        /// 更新主界面的100个Grid的显示,给柜门id
        /// </summary>
        public void UpdateDoorShow(int cabinetId)
        {
            Presenter.CabinetId = cabinetId;
            //根据cabinetId搜集MinerLampTest集合中对应的一条柜子文档
            _allDocument = MongoDbHelper<DoorInfo>.FindPartCabinetInfo(cabinetId,
                CollectionNames.MinerLampTest);
            if (_allDocument.Count!=0)
            {
                for (int j = 0; j < 100; j++)
                {
                    var count = MongoDbHelper<DoorInfo>.DocumentCount(CollectionNames.MinerLampTest)/100;
                    //long  返回文档条数
                    if (cabinetId > count || cabinetId < 0)
                    {
                        MessageBox.Show("柜号不在范围，请检查并重试！");
                        break;
                    }
                    //设定图片和员工姓名
                    _window.Controls[j].SetDoorShow(_allDocument[j], j);
                    //显示统计的数量
                    CountStatus();
                }
            }
            else
            {
                MessageBox.Show("未获取有效数据，请稍后重试！");
            }
        }

        /// <summary>
        /// 统计各状态的数量
        /// </summary>
        public void CountStatus()
        {
            int chargingNum = 0, fullNum = 0, usingNum = 0, problemNum = 0, uninitNum = 0;
            foreach (var item in _allDocument)
            {
                var itemstatus = item.Status;
                switch (itemstatus)
                {
                    case MinerLampStatus.Charging:
                        chargingNum++;
                        break;
                    case MinerLampStatus.Full:
                        fullNum++;
                        break;
                    case MinerLampStatus.Using:
                        usingNum++;
                        break;
                    case MinerLampStatus.ChargeProblem:
                        problemNum++;
                        break;
                    case MinerLampStatus.CommuncationProblem:
                        problemNum++;
                        break;
                    case MinerLampStatus.MultiProblem:
                        problemNum++;
                        break;
                    case MinerLampStatus.UnInit:
                        uninitNum++;
                        break;
                }
                _window.TextBlockCabinetId.Text = "当前柜号：" + CabinetId + "    ";
                _window.TextBlockCharging.Text = "充电：" + chargingNum + "  ";
                _window.TextBlockFull.Text = "充满：" + fullNum + "  ";
                _window.TextBlockUsing.Text = "使用：" + usingNum + "  ";
                _window.TextBlockProblem.Text = "故障：" + problemNum + "  ";
                _window.TextBlockUnInit.Text = "未初始化：" + uninitNum + "  ";
            }
        }
        
        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="str">列头名</param>
        public static void ExportResults(StringBuilder stringBuilder, int columnsCount ,string fileName)
        {
            if (stringBuilder == null)
            {
                MessageBox.Show("没有可供导出的数据!");
                return;
            }
            var std = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
                Title = "导出文件路径",
                FileName = fileName
            };
            string s = null;
            if (std.ShowDialog() == DialogResult.OK)
            {
                s = std.FileName;
            }
            StreamWriter sw = new StreamWriter(s, false, Encoding.GetEncoding("gb2312"));
            sw.Write(stringBuilder.ToString());
            sw.Flush();
            sw.Close();
            sw.Dispose();
            MessageBox.Show("导出数据成功！");
        }


        /// <summary>
        /// 打开等待窗口
        /// </summary>
        public  static void OpenWaitingBox()
        {
            var waitingBox = new WaitingBox();
            waitingBox.Show();
        }

        #region 界面初始化工作

        #endregion


        /// <summary>
        /// TM卡转换函数
        /// </summary>
        /// <param name="cardStr"></param>
        /// <returns></returns>
        public static long CardParseLong(string cardStr)
        {
            long num = 0;
            var strs = new byte[8];
            for (var i = 0; i < 8; i++)
            {
                strs[i] = Convert.ToByte(cardStr.Substring(i * 2, 2), 16);
            }
            for (var i = 0; i <= 7; i++)
            {
                num += (strs[i] & 0xffL) << ((7 - i) * 8);
            }
            return num;
        }
    }
}