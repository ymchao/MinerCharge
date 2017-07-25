using System;
using System.Collections.Generic;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;

namespace MinerLampMangement.view.viewbean
{
    public class StatusHistory
    {
        /// <summary>
        /// 矿灯历史状态记录的包装类
        /// </summary>
        /// <param name="allStatusHistoryInfo">数据库查到的所有状态信息</param>
        /// <param name="starttime">起始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <returns></returns>
        public static List<LampInfo> LampStatusInfoShow(AllStatusHistoryInfo allStatusHistoryInfo, string starttime,
            string endtime)
        {
            var list = new List<LampInfo>();
            allStatusHistoryInfo.StatusList.ForEach(a =>
            {
                //在时间段内，则记录进去
                if (DateTime.Parse(starttime) <= a.Time &&
                    a.Time <= DateTime.Parse(endtime))
                {
                    list.Add(new LampInfo(a));
                }
            });
            return list;
        }

        //方法的重载，不传入时间段就是返回所有的状态信息
        public static List<LampInfo> LampStatusInfoShow(AllStatusHistoryInfo allStatusHistoryInfo)
        {
            var list = new List<LampInfo>();
            allStatusHistoryInfo.StatusList.ForEach(a => { list.Add(new LampInfo(a)); });
            return list;
        }

        /// <summary>
        /// 矿灯历史记录的新类
        /// </summary>
        public class LampInfo
        {
            public LampInfo(HistoryInfo info)
            {
                switch (info.Status)
                {
                    case MinerLampStatus.Charging:
                        LampStatus = "充电";
                        break;
                    case MinerLampStatus.Full:
                        LampStatus = "充满";
                        break;
                    case MinerLampStatus.Using:
                        LampStatus = "使用中";
                        break;
                    case MinerLampStatus.ChargeProblem:
                        LampStatus = "故障";
                        break;
                    case MinerLampStatus.CommuncationProblem:
                        LampStatus = "故障";
                        break;
                    case MinerLampStatus.MultiProblem:
                        LampStatus = "故障";
                        break;
                    case MinerLampStatus.UnInit:
                        LampStatus = "未初始化";
                        break;
                    default:
                        LampStatus = "未知";
                        break;
                }
                LampTime = info.Time.ToString("yyyy-MM-dd HH:mm");
            }

            /// <summary>
            /// 矿灯状态
            /// </summary>
            public string LampStatus { get; set; }

            /// <summary>
            ///  矿灯状态时间
            /// </summary>
            public string LampTime { get; set; }
        }
    }
}