using System;
using MinerLampMangement.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace MinerLampMangement.Model
{
    /// <summary>
    /// 充电柜实体类
    /// </summary>
    public class DoorInfo : BaseEntity
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
        /// 获取 Tm卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        ///  矿灯状态
        /// </summary>
        public MinerLampStatus Status { get; set; }

        /// <summary>
        ///  剩余充电次数
        /// </summary>
        public int RemainChargeTime { get; set; }

        /// <summary>
        /// 矿灯状态开始时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StatusTime { get; set; }

        /// <summary>
        /// 矿工MinerObjectId
        /// </summary>
        public string EmployeeObjectId { get; set; }

    }
}