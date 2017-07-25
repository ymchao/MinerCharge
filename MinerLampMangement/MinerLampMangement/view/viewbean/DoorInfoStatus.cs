using System;
using System.Collections.Generic;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;

namespace MinerLampMangement.view.viewbean
{
    /// <summary>
    /// 矿灯状态查询的list拼接方法
    /// </summary>
    public class DoorInfoStatus
    {
        /// <summary>
        /// 传入柜门信息和员工信息，拼接的方法
        /// </summary>
        /// <param name="doorInfo"></param>
        /// <param name="minerInfo"></param>
        /// <returns></returns>
        public static List<DoorStatusInfo> DoorStatusInfoShow(List<DoorInfo> doorInfo, List<MinerInfo> minerInfo)
        {
            var list = new List<DoorStatusInfo>();
            for (int i = 0; i < doorInfo.Count; i++)
            {
                list.Add(new DoorStatusInfo(doorInfo[i],minerInfo[i]));
            }
            return list;
        }

        /// <summary>
        /// 用于矿灯状态查询的拼接的新类
        /// </summary>
        public class DoorStatusInfo
        {
            public DoorStatusInfo(DoorInfo doorInfo, MinerInfo minerInfo)
            {
                GroupId = minerInfo.GroupId;
                DeviceId = minerInfo.DeviceId;
                MinerLampStatus = doorInfo.Status;
                StatusTime = doorInfo.StatusTime.ToString("yyyy-MM-dd HH:mm");
                Name = minerInfo.Name;
                JobNumber = minerInfo.JobNumber;
                Department = minerInfo.Department;
                Class = minerInfo.Classes;
                //转化时间显示的格式
                TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(StatusTime));
                DurationTime = ts.Days + "天"
                               + ts.Hours + "小时"
                               + ts.Minutes + "分钟";
            }

            /// <summary>
            /// 柜号
            /// </summary>
            public int GroupId { get; set; }

            /// <summary>
            /// 柜门号
            /// </summary>
            public int DeviceId { get; set; }

            /// <summary>
            /// 获取 员工姓名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 获取 员工编号
            /// </summary>
            public string JobNumber { get; set; }

            /// <summary>
            /// 部门
            /// </summary>
            public string Department { get; set; }

            /// <summary>
            /// 班次
            /// </summary>
            public string Class { get; set; }

            /// <summary>
            ///  矿灯状态
            /// </summary>
            public MinerLampStatus MinerLampStatus { get; set; }

            /// <summary>
            /// 矿灯状态开始时间
            /// </summary>
            public string StatusTime { get; set; }

            /// <summary>
            ///  矿灯状态持续时间
            /// </summary>
            public string DurationTime { get; set; }
        }
    }
}