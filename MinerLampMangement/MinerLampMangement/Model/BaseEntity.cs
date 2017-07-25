using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinerLampMangement.Model
{
    /// <summary>
    /// 实体类基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }
    }
}