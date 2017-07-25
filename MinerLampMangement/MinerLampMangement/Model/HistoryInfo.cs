using Newtonsoft.Json;
using System;
using MinerLampMangement.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace MinerLampMangement.Model
{
    public class HistoryInfo
    {
        /// <summary>
        ///  矿灯状态
        /// </summary>
        public MinerLampStatus Status { get; set; }

        /// <summary>
        ///  矿灯状态时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Time { get; set; }

    }
}