using System.Collections.Generic;
using MinerLampMangement.Model;

namespace MinerLampMangement.view.viewbean
{
    //充电时间查询拼接类
    public class ChargeTimes
    {
        /// <summary>
        /// 传入柜门信息和员工信息，拼接的方法
        /// </summary>
        /// <param name="doorInfo"></param>
        /// <param name="minerInfo"></param>
        /// <returns></returns>
        public static List<ChargeTime> ChargeTimeInfoShow(List<DoorInfo> doorInfo, List<MinerInfo> minerInfo)
        {
            var list = new List<ChargeTime>();
            for (int i = 0; i < doorInfo.Count; i++)
            {
                list.Add(new ChargeTime(doorInfo[i], minerInfo[i]));
            }
            return list;
        }

        /// <summary>
        /// 用于矿灯充电次数查询的拼接的新类
        /// </summary>
        public class ChargeTime
        {
            public ChargeTime(DoorInfo doorInfo, MinerInfo minerInfo)
            {
                GroupId = minerInfo.GroupId;
                DeviceId = minerInfo.DeviceId;
                Name = minerInfo.Name;
                JobNumber = minerInfo.JobNumber;
                Department = minerInfo.Department;
                RemainChargeTime = doorInfo.RemainChargeTime;
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
            ///  矿灯充电次数
            /// </summary>
            public int RemainChargeTime { get; set; }
        }
    }
}