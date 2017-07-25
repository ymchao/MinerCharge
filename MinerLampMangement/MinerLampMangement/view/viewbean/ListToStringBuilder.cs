using System;
using System.Collections.Generic;
using System.Text;
using MinerLampMangement.Model;

namespace MinerLampMangement.view.viewbean
{
    class ListToStringBuilder
    {
        /// <summary>
        /// 矿灯充电次数查询datagrid,数据导出excel
        /// </summary>
        /// <param name="list"></param>
        /// <param name=""></param>
        /// <param name="str">列头字段</param>
        /// <returns></returns>
        public static StringBuilder ListMinerInfoTosStringBuilder(List<ChargeTimes.ChargeTime> list, string[] str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                stringBuilder.Append(str[i] + ",");
            }
            stringBuilder.Append(Environment.NewLine);
            foreach (var Info in list)
            {
                stringBuilder.Append(Info.GroupId + ",");
                stringBuilder.Append(Info.DeviceId + ",");
                stringBuilder.Append(Info.Name + ",");
                stringBuilder.Append(Info.JobNumber + ",");
                stringBuilder.Append(Info.Department + ",");
                stringBuilder.Append(Info.RemainChargeTime + ",");
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder;
        }

        /// <summary>
        /// 矿灯状态时间查询datagrid,导出EXCEL
        /// </summary>
        /// <param name="list"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StringBuilder ListDoorInfoTosStringBuilder(List<DoorInfoStatus.DoorStatusInfo> list, string[] str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                stringBuilder.Append(str[i] + ",");
            }
            stringBuilder.Append(Environment.NewLine);
            foreach (var DoorInfo in list)
            {
                stringBuilder.Append(DoorInfo.GroupId + ",");
                stringBuilder.Append(DoorInfo.DeviceId + ",");
                stringBuilder.Append(DoorInfo.Name + ",");
                stringBuilder.Append(DoorInfo.JobNumber + ",");
                stringBuilder.Append(DoorInfo.Department + ",");
                stringBuilder.Append(DoorInfo.Class + ",");
                stringBuilder.Append(DoorInfo.StatusTime + ",");
                stringBuilder.Append(DoorInfo.DurationTime + ",");
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder;
        }

        /// <summary>
        /// 员工显示的矿灯状态datagrid 导出
        /// </summary>
        /// <returns></returns>
        public static StringBuilder ListLampInfoTosStringBuilder(List<StatusHistory.LampInfo> list, string[] str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                stringBuilder.Append(str[i] + ",");
            }
            stringBuilder.Append(Environment.NewLine);
            foreach (var LampInfo in list)
            {
                stringBuilder.Append(LampInfo.LampStatus + ",");
                stringBuilder.Append(LampInfo.LampTime + ",");
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder;
        }

        /// <summary>
        /// 员工显示的考勤信息datagrid 导出
        /// </summary>
        /// <returns></returns>
        public static StringBuilder ListAttendanceInfoTosStringBuilder(List<Attendance.Attendanceinfo> list,
            string[] str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                stringBuilder.Append(str[i] + ",");
            }
            stringBuilder.Append(Environment.NewLine);
            foreach (var LampInfo in list)
            {
                stringBuilder.Append(LampInfo.GroupId + ",");
                stringBuilder.Append(LampInfo.DeviceId + ",");
                stringBuilder.Append(LampInfo.Name + ",");
                stringBuilder.Append(LampInfo.JobNumber + ",");
                stringBuilder.Append(LampInfo.Department + ",");
                stringBuilder.Append(LampInfo.Class + ",");
                stringBuilder.Append(LampInfo.DownTime + ",");
                stringBuilder.Append(LampInfo.UpTime + ",");
                stringBuilder.Append(LampInfo.WorkTime + ",");
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder;
        }

        /// <summary>
        /// 员工信息导出
        /// </summary>
        /// <param name="list"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StringBuilder ListMinerInfoToStringBuilder(List<MinerInfo> list, string[] str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                stringBuilder.Append(str[i] + ",");
            }
            stringBuilder.Append(Environment.NewLine);
            foreach (var minerInfo in list)
            {
                stringBuilder.Append(minerInfo.GroupId + ",");
                stringBuilder.Append(minerInfo.DeviceId + ",");
                stringBuilder.Append(minerInfo.Name + ",");
                stringBuilder.Append(minerInfo.JobNumber + ",");
                stringBuilder.Append(minerInfo.Sex + ",");
                stringBuilder.Append(minerInfo.BornDate + ",");
                stringBuilder.Append(minerInfo.MaritalStatus + ",");
                stringBuilder.Append(minerInfo.National + ",");
                stringBuilder.Append(minerInfo.IdentityCardId + ",");
                stringBuilder.Append(minerInfo.NativePlace + ",");
                stringBuilder.Append(minerInfo.Address + ",");
                stringBuilder.Append(minerInfo.Blood + ",");
                stringBuilder.Append(minerInfo.Image + ",");
                stringBuilder.Append(minerInfo.PoliticalStatus + ",");
                stringBuilder.Append(minerInfo.Education + ",");
                stringBuilder.Append(minerInfo.PhoneNum + ",");
                stringBuilder.Append(minerInfo.ContractTypes + ",");
                stringBuilder.Append(minerInfo.Position + ",");
                stringBuilder.Append(minerInfo.JoinTime + ",");
                stringBuilder.Append(minerInfo.Department + ",");
                stringBuilder.Append(minerInfo.EquipLampTime + ",");
                stringBuilder.Append(minerInfo.LampTypes + ",");
                stringBuilder.Append(minerInfo.Classes + ",");
                stringBuilder.Append(minerInfo.EquipSelfRescuerTime + ",");
                stringBuilder.Append(minerInfo.SelfRescuerId + ",");
                stringBuilder.Append(minerInfo.Others + ",");
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder;
        }

        /// <summary>
        ///Tm卡号导出CSV
        /// </summary>
        /// <param name="alldoorinfo"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static StringBuilder ListTmInfoToStringBuilder(List<DoorInfo> alldoorinfo, string[] str)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                stringBuilder.Append(str[i] + ",");
            }
            stringBuilder.Append(Environment.NewLine);
            foreach (var doorInfo in alldoorinfo)
            {
                stringBuilder.Append(doorInfo.GroupId + ",");
                stringBuilder.Append(doorInfo.DeviceId + ",");
                stringBuilder.Append(doorInfo.GroupId*1000+doorInfo.DeviceId + ",");
                stringBuilder.Append(doorInfo.CardNumber + ",");
                stringBuilder.Append(Environment.NewLine);
            }
            return stringBuilder;
        }
    }
}