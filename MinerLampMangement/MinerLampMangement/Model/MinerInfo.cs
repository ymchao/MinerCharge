using System;
using MongoDB.Bson.Serialization.Attributes;

namespace MinerLampMangement.Model
{
    /// <summary>
    /// 矿工实体类
    /// </summary>
    public class MinerInfo : BaseEntity
    {
        /// <summary>
        /// 获取 柜号
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 获取 柜门号
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
        /// 获取 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 获取出生日期
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime BornDate { get; set; }

        /// <summary>
        /// 婚姻状况
        ///  </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// 名族
        /// </summary>
        public string National { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityCardId { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string Blood { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        public string PoliticalStatus { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractTypes { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 参加工作时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime JoinTime { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 矿灯配备时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EquipLampTime { get; set; }

        /// <summary>
        /// 矿灯型号
        /// </summary>
        public string LampTypes { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        public string Classes { get; set; }

        /// <summary>
        /// 自救器配备时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EquipSelfRescuerTime { get; set; }

        /// <summary>
        /// 自救器型号
        /// </summary>
        public string SelfRescuerId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Others { get; set; }

        /// <summary>
        /// 考勤信息ObjectId
        /// </summary>
//        public string AttendanceInfoObjectId { get; set; }


        /// <summary>
        /// 重写tostring 将文档输出为jason格式
        /// </summary>
        /// <returns></returns>
//        public override string ToString()
//        {
//            return JsonConvert.SerializeObject(this);
//        }
    }
}