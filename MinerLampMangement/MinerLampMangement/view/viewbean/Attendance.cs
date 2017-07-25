using System;
using System.Collections.Generic;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view.viewbean
{
    /// <summary>
    /// 考勤记录显示的拼接类，将员工信息和考勤信息拼接在一起
    /// </summary>
    public class Attendance
    {
        /// <summary>
        /// 考勤信息的拼接方法
        /// </summary>
        /// <param name="allStatusHistoryInfo">传入一个员工的所有的考勤信息</param>
        /// <param name="minerInfo">传入一个员工信息</param>
        /// <param name="starttime">需要显示的员工考勤记录起始时间</param>
        /// <param name="endtime">需要显示的员工考勤记录结束时间</param>
        /// <returns></returns>
        public static List<Attendanceinfo> AttendanceInfoShow(List<StatusHistory.LampInfo> list)
        {
            //新建一个list用于存放拼接对象
            var attendanceinfos = new List<Attendanceinfo>();
            foreach (var item in list)
            {
                var status = item.LampStatus;
                var time = item.LampTime;
                if (attendanceinfos.Count == 0)
                {
                    switch (status)
                    {
                        case "充电":
                            //矿灯状态为充电，则矿工进入上井下班状态   
                            //如果list为空，则新建一行
                            Attendanceinfo newAttendanceinfo1 = new Attendanceinfo()
                            {
                                DownTime = "未知",
                                UpTime = time,
                                WorkTime = "未知"
                            };
                            attendanceinfos.Add(newAttendanceinfo1);
                            break;
                        case "使用中":
                            //矿灯状态为使用中，则矿工进入下井上班状态   
                            //如果list为空，则新建一行
                            Attendanceinfo newAttendanceinfo2 = new Attendanceinfo()
                            {
                                DownTime = time,
                            };
                            attendanceinfos.Add(newAttendanceinfo2);
                            break;
                    }
                }
                //如果不为空，则提取最后一条判断
                else
                {
                    Attendanceinfo lastinfo = attendanceinfos[attendanceinfos.Count - 1];
                    switch (status)
                    {
                        case "充电":
                            //矿灯状态为充电，则矿工进入上井下班状态，判断最后一条的状态
                            if (lastinfo.UpTime == null)
                            {
                                lastinfo.UpTime = time;
                                //转化时间显示的格式
                                var ts =
                                    Convert.ToDateTime(lastinfo.UpTime).Subtract(Convert.ToDateTime(lastinfo.DownTime));
                                lastinfo.WorkTime = ts.Days + "天"
                                                    + ts.Hours + "小时"
                                                    + ts.Minutes + "分钟";
                            }
                            else
                            {
                                //则新建一行
                                Attendanceinfo newAttendanceinfo = new Attendanceinfo()
                                {
                                    DownTime = "未知",
                                    UpTime = time,
                                    WorkTime = "未知"
                                };
                                attendanceinfos.Add(newAttendanceinfo);
                            }
                            break;

                        case "使用中":
                            //矿灯状态为使用中，则矿工进入下井上班状态，判断最后一条的状态
                            if (lastinfo.UpTime == null)
                            {
                                lastinfo.UpTime = "未知";
                                //转化时间显示的格式
                                lastinfo.WorkTime = "未知";
                            }

                            //则新建一行
                            Attendanceinfo newAttendanceinfo1 = new Attendanceinfo()
                            {
                                DownTime = time,
                            };
                            attendanceinfos.Add(newAttendanceinfo1);

                            break;
                    }
                }
            }
            return attendanceinfos;
        }

        //方法重载，当传入多个员工信息时,需要多次查询该员工的矿灯状态记录表
        public static List<Attendanceinfo> AttendanceInfoShow(List<MinerInfo> minerInfos, string starttime,
            string endtime)
        {
            //新建一个list用于存放拼接对象
            var attendanceinfos = new List<Attendanceinfo>();
            //首先遍历所有员工
            if (minerInfos != null)
                foreach (var minerInfo in minerInfos)
                {
                    //新建一个此员工的list
                    var oneattendanceinfo = new List<Attendanceinfo>();
                    //根据员工的ObjectId字段，去考勤表中寻找相应的考勤信息
                    var allStatusHistoryInfo = MongoDbHelper<AllStatusHistoryInfo>.FindInfoByObjected(
                        CollectionNames.MinerAttendanceTest, minerInfo.Id.ToString());

                    //首先过滤掉不在时间范围的矿灯状态信息，如果在时间范围内就执行拼接操作
                    for (int i = 0; i < allStatusHistoryInfo.StatusList.Count; i++)
                    {
                        var time = allStatusHistoryInfo.StatusList[i].Time;
                        var status = allStatusHistoryInfo.StatusList[i].Status;
                        //如果在时间范围内就执行拼接操作
                        if (DateTime.Parse(starttime) <= time && time <= DateTime.Parse(endtime))
                        {
                            switch (status)
                            {
                                case MinerLampStatus.Charging:
                                    //矿灯状态为充电，则矿工进入上井下班状态
                                    //如果list不为空
                                    if (oneattendanceinfo.Count != 0)
                                    {
                                        Attendanceinfo lastinfo = oneattendanceinfo[oneattendanceinfo.Count - 1];
                                        if (lastinfo.UpTime==null)
                                        {
                                            lastinfo.UpTime = time.ToString("yyyy-MM-dd HH:mm");
                                            //转化时间显示的格式
                                            var ts =
                                                Convert.ToDateTime(lastinfo.UpTime)
                                                    .Subtract(Convert.ToDateTime(lastinfo.DownTime));
                                            lastinfo.WorkTime = ts.Days + "天"
                                                                + ts.Hours + "小时"
                                                                + ts.Minutes + "分钟";
                                        }
                                        else
                                        {
                                            Attendanceinfo newAttendanceinfo = new Attendanceinfo()
                                            {
                                                GroupId = minerInfo.GroupId,
                                                DeviceId = minerInfo.DeviceId,
                                                Name = minerInfo.Name,
                                                JobNumber = minerInfo.JobNumber,
                                                Department = minerInfo.Department,
                                                Class = minerInfo.Classes,
                                                DownTime = "未知",
                                                UpTime = time.ToString("yyyy-MM-dd HH:mm"),
                                                WorkTime = "未知"
                                            };
                                            oneattendanceinfo.Add(newAttendanceinfo);
                                        }    
                                    }
                                    //如果list为空，则新建一行
                                    else
                                    {
                                        Attendanceinfo newAttendanceinfo = new Attendanceinfo()
                                        {
                                            GroupId = minerInfo.GroupId,
                                            DeviceId = minerInfo.DeviceId,
                                            Name = minerInfo.Name,
                                            JobNumber = minerInfo.JobNumber,
                                            Department = minerInfo.Department,
                                            Class = minerInfo.Classes,
                                            DownTime = "未知",
                                            UpTime = time.ToString("yyyy-MM-dd HH:mm"),
                                            WorkTime = "未知"
                                        };
                                        oneattendanceinfo.Add(newAttendanceinfo);
                                    }
                                    break;

                                case MinerLampStatus.Using:
                                    if (oneattendanceinfo.Count != 0)
                                    {
                                        Attendanceinfo lastinfo = oneattendanceinfo[oneattendanceinfo.Count - 1];
                                        if (lastinfo.UpTime == null)
                                        {
                                            lastinfo.UpTime = "未知";
                                            lastinfo.WorkTime = "未知";
                                        }
                                        //矿灯状态为使用，则矿工进入下井上班工作状态
                                        Attendanceinfo attendanceinfo = new Attendanceinfo()
                                        {
                                            GroupId = minerInfo.GroupId,
                                            DeviceId = minerInfo.DeviceId,
                                            Name = minerInfo.Name,
                                            JobNumber = minerInfo.JobNumber,
                                            Department = minerInfo.Department,
                                            Class = minerInfo.Classes,
                                            DownTime = time.ToString("yyyy-MM-dd HH:mm")
                                        };
                                        oneattendanceinfo.Add(attendanceinfo);
                                    }
                                    else
                                    {
                                        //矿灯状态为使用，则矿工进入下井上班工作状态
                                        Attendanceinfo attendanceinfo = new Attendanceinfo()
                                        {
                                            GroupId = minerInfo.GroupId,
                                            DeviceId = minerInfo.DeviceId,
                                            Name = minerInfo.Name,
                                            JobNumber = minerInfo.JobNumber,
                                            Department = minerInfo.Department,
                                            Class = minerInfo.Classes,
                                            DownTime = time.ToString("yyyy-MM-dd HH:mm")
                                        };
                                        oneattendanceinfo.Add(attendanceinfo);
                                    }
                                    break;
                            }
                        }
                    }
                    //拼接每个员工的考勤信息
                    attendanceinfos.AddRange(oneattendanceinfo);
                }
            return attendanceinfos;
        }

//        //方法重载，当传入多个员工信息时,需要多次查询该员工的矿灯状态记录表
//        public static List<Attendanceinfo> AttendanceInfoShow(List<MinerInfo> minerInfos)
//        {
//            //新建一个list用于存放拼接对象
//            var attendanceinfos = new List<Attendanceinfo>();
//            //首先遍历所有员工
//            if (minerInfos != null)
//                foreach (var minerInfo in minerInfos)
//                {
//                    //新建一个此员工的list
//                    var oneattendanceinfo = new List<Attendanceinfo>();
//                    //根据员工的ObjectId字段，去考勤表中寻找相应的考勤信息
//                    var allStatusHistoryInfo = MongoDbHelper<AllStatusHistoryInfo>.FindInfoByObjected(
//                        CollectionNames.MinerAttendanceTest, minerInfo.Id.ToString());
//                    //首先过滤掉不在时间范围的矿灯状态信息，如果在时间范围内就执行拼接操作
//                    for (int i = 0; i < allStatusHistoryInfo.StatusList.Count; i++)
//                    {
//                        var time = allStatusHistoryInfo.StatusList[i].Time;
//                        var status = allStatusHistoryInfo.StatusList[i].Status;
//
//                        switch (status)
//                        {
//                            case MinerLampStatus.Charging:
//                                //矿灯状态为充电，则矿工进入上井下班状态
//                                //如果list不为空
//                                if (oneattendanceinfo.Count != 0)
//                                {
//                                    Attendanceinfo lastinfo = oneattendanceinfo[oneattendanceinfo.Count - 1];
//                                    lastinfo.UpTime = time.ToString("yyyy-MM-dd HH:mm");
//                                    //转化时间显示的格式
//                                    var ts =
//                                        Convert.ToDateTime(lastinfo.UpTime)
//                                            .Subtract(Convert.ToDateTime(lastinfo.DownTime));
//                                    lastinfo.WorkTime = ts.Days + "天"
//                                                        + ts.Hours + "小时"
//                                                        + ts.Minutes + "分钟";
//                                }
//                                //如果list为空，则新建一行
//                                Attendanceinfo newAttendanceinfo = new Attendanceinfo()
//                                {
//                                    GroupId = minerInfo.GroupId,
//                                    DeviceId = minerInfo.DeviceId,
//                                    Name = minerInfo.Name,
//                                    JobNumber = minerInfo.JobNumber,
//                                    Department = minerInfo.Department,
//                                    Class = minerInfo.Classes,
//                                    DownTime = "未知",
//                                    UpTime = time.ToString("yyyy-MM-dd HH:mm"),
//                                    WorkTime = "未知"
//                                };
//                                oneattendanceinfo.Add(newAttendanceinfo);
//                                break;
//
//                            case MinerLampStatus.Using:
//                                //矿灯状态为使用，则矿工进入下井上班工作状态
//                                Attendanceinfo attendanceinfo = new Attendanceinfo()
//                                {
//                                    GroupId = minerInfo.GroupId,
//                                    DeviceId = minerInfo.DeviceId,
//                                    Name = minerInfo.Name,
//                                    JobNumber = minerInfo.JobNumber,
//                                    Department = minerInfo.Department,
//                                    Class = minerInfo.Classes,
//                                    DownTime = time.ToString("yyyy-MM-dd HH:mm")
//                                };
//                                oneattendanceinfo.Add(attendanceinfo);
//                                break;
//                        }
//                    }
//                    attendanceinfos.AddRange(oneattendanceinfo);
//                }
//            return attendanceinfos;
//        }

        public class Attendanceinfo
        {
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
            /// 下井时间
            /// </summary>
            public string DownTime { get; set; }

            /// <summary>
            /// 上井时间
            /// </summary>
            public string UpTime { get; set; }

            /// <summary>
            ///  工作时长
            /// </summary>
            public string WorkTime { get; set; }
        }
    }
}