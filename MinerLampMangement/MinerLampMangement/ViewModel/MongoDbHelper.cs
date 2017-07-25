using System;
using System.Collections.Generic;
using System.Windows;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MinerLampMangement.ViewModel
{
    /// <summary>
    /// 数据访问层的基类
    /// </summary>
    public class MongoDbHelper<T> where T : BaseEntity
    {
        #region 数据库基本连接操作

        /// <summary>
        /// 链接字符串
        /// </summary>
        private const string Connectstring = "mongodb://202.193.56.226:27017";

        /// <summary>
        /// GUET数据库  
        /// </summary>
        private static string DbName = "ChargeDevice";

        /// <summary>
        /// Mongo客户端
        /// </summary>
        public static IMongoClient Client;

        /// <summary>
        /// 数据库
        /// </summary>
        //private static IMongoDatabase _database;
        /// <summary>
        /// 数据库连接
        /// </summary>
        public static IMongoCollection<T> Collection;

        //数据备份还原专用
        public static IMongoCollection<BsonDocument> Collection2;

        /// <summary>
        /// 根据数据库名创建MongoDatabase对象
        /// </summary>
        /// <param name="databaseName">数据库名称，默认空为GUET</param>
        /// <returns></returns>
        public static IMongoDatabase GetDatabase()
        {
            Client = new MongoClient(Connectstring);
            if (Client != null)
            {
                // 默认连接GUET数据库 ，没做判断，如果Mogo中没有相应的GUET
                return Client.GetDatabase(DbName);
            }
            else
            {
                MessageBox.Show("无法连接到数据库!");
                return null;
            }
        }

        /// <summary>
        /// 获取操作对象的IMongoCollection集合,强类型对象集合
        /// </summary>
        /// <returns></returns>
        public static IMongoCollection<T> GetCollection(string collectionName)
        {
            return GetDatabase().GetCollection<T>(collectionName);
        }

        //数据库备份还原专用
        public static IMongoCollection<BsonDocument> GetCollection2(string collectionName)
        {
            return GetDatabase().GetCollection<BsonDocument>(collectionName);
        }

        #endregion

        #region 数据库插入

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="model">数据对象</param>
        public static void Insert(string collectionName, T model)
        {
            Collection = GetCollection(collectionName);
            Collection.InsertOne(model);
        }

        #endregion

        #region 数据库查询

        /// <summary>
        /// 查询所有文档
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static List<T> FindAllDocuments(string collectionName)
        {
            Collection = GetCollection(collectionName);
            return Collection.Find(new BsonDocument()).ToList();
        }

        /// <summary>
        /// 根据柜号搜寻对应的100个柜门的信息
        /// </summary>
        /// <param name="cabinetId"></param>
        /// <returns></returns>
        public static List<T> FindPartCabinetInfo(int cabinetId, string collectionName)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq("GroupId", cabinetId);
            return Collection.Find(filter).ToList();
        }

        /// <summary>
        /// 根据objectId查找一条资料
        /// </summary>
        /// <returns></returns>
        public static T FindInfoByObjected(string collectionName, string objectId)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(objectId));
            var a = Collection.Find(filter).First();
            return a;
        }

        /// <summary>
        /// 根据传入的字段(可以传入多个键值对)，查找对应的信息，可以是一条或者多条
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="fieldandvalue">这是一个list,里面是字段+值+字段+值。。。。</param>
        /// <returns></returns>
        public static List<T> FindInfoByField(string collectionName, List<string> fieldandvalue)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq(fieldandvalue[0], fieldandvalue[1]);
            for (var i = 2; i < fieldandvalue.Count; i = i + 2)
            {
                filter = RepeatFilterDefinition(filter, fieldandvalue[i], fieldandvalue[i + 1]);
            }
            return Collection.Find(filter).ToList();
        }

        internal static List<T> FindInfoByField(string collectionName, string v1, int v2)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq(v1, v2);
            return Collection.Find(filter).ToList();
        }

//        //更具员工id，在柜门集合中搜索数据,只返回状态列
//        public static List<BsonDocument> FindDoorInfoByEmployeeId(string collectionName, string objectid)
//        {
//            Collection = GetCollection(collectionName);
//            var prejection = Builders<T>.Projection.Include("Status");
//            var filter = Builders<T>.Filter.Eq("EmployeeObjectId", objectid);
//            return Collection.Find(filter).Project(prejection).ToList();
//        }

        /// <summary>
        /// 根据传入的字段搜索数据库中相应列的信息，也就是获取任意列的信息
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="field">可以传入多个想要获取列的字段</param>
        /// <returns></returns>
        public static List<BsonDocument> FindPartFieldInfo(string collectionName, params string[] field)
        {
            var prejection = Builders<T>.Projection.Include(field[0]);
            if (field.Length > 1)
            {
                for (int i = 1; i < field.Length; i++)
                {
                    prejection = RepeatProjectionDefinition(prejection, field[i]);
                }
            }
            return GetCollection(collectionName).Find(new BsonDocument()).Project(prejection).ToList();
        }

        /// <summary>
        /// 根据状态持续时间查找柜门信息集合
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="field"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static List<T> FindDoorInfoByTime(string collectionName, string field, int time)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Lt("StatusTime", DateTime.Now.AddMinutes(-time)) &
                         Builders<T>.Filter.Eq("Status", field);
            return Collection.Find(filter).ToList();
        }

        /// <summary>
        /// 更具充电次数查询集合
        /// </summary>
        /// <param name="minnumber"></param>
        /// <param name="maxnumber"></param>
        /// <returns></returns>
        public static List<T> FindDoorInfoByChargingNumber(int minnumber, int maxnumber)
        {
            Collection = GetCollection(CollectionNames.MinerLampTest);
            var filter = Builders<T>.Filter.Gt("RemainChargeTime", minnumber) &
                         Builders<T>.Filter.Lt("RemainChargeTime", maxnumber);
            return Collection.Find(filter).ToList();
        }

        /// <summary>
        /// 根据部门名称条件查询信息，只返回员工Objectid列
        /// </summary>
        /// <returns></returns>
        public static List<BsonDocument> FindOneFieldValueInfoByField(string collectionName, string field)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq("Department", field);
            var prejection = Builders<T>.Projection.Include("_id");
            return Collection.Find(filter).Project(prejection).ToList();
        }

        /// <summary>
        /// 去重操作,当字段类型是string时
        /// </summary>
        /// <returns></returns>
        public static List<string> DistinctInfoString(string collectionName, string field)
        {
            Collection = GetCollection(collectionName);
            var filter = new BsonDocument();
            var info = Collection.Distinct<string>(field, filter);
            var listinfo = new List<string>();
            while (info.MoveNext())
            {
                var current = info.Current;
                foreach (var item in current)
                {
                    if (item != null)
                    {
                        listinfo.Add(item);
                    }
                }
            }
            return listinfo;
        }

        /// <summary>
        /// 去重操作,当字段类型是int时
        /// </summary>
        /// <returns></returns>
        public static List<int> DistinctInfoInt(string collectionName, string field)
        {
            Collection = GetCollection(collectionName);
            var filter = new BsonDocument();
            var info = Collection.Distinct<int>(field, filter);
            var listinfo = new List<int>();
            while (info.MoveNext())
            {
                var current = info.Current;
                foreach (var item in current)
                {
                    listinfo.Add(item);
                }
            }
            return listinfo;
        }

        #endregion

        #region 数据库更新

        /// <summary>
        /// 更新一个员工信息 , 根据柜号和柜门号作为索引
        /// </summary>
        /// <param name="cabinetid"></param>
        /// <param name="doorid"></param>
        /// <param name="item">传入的员工对象</param>
        public static void UpdateMinerInfo(int cabinetid, int doorid, T item)
        {
            Collection = GetCollection(CollectionNames.MinerInfoTest);
            var filter = Builders<T>.Filter.Eq("GroupId", cabinetid) & Builders<T>.Filter.Eq("DeviceId", doorid);
            Collection.FindOneAndReplace(filter, item);
        }

        /// <summary>
        /// 根据柜号与柜门号，更新对应的Tm卡号字段
        /// </summary>
        /// <param name="cabinetid"></param>
        /// <param name="doorid"></param>
        /// <param name="tmNumber"></param>
        public static void UpdateTmNumberInfo(int cabinetid, int doorid, string tmNumber)
        {
            Collection = GetCollection(CollectionNames.MinerLampTest);
            var filter = Builders<T>.Filter.Eq("GroupId", cabinetid) & Builders<T>.Filter.Eq("DeviceId", doorid);
            var updated = Builders<T>.Update.Set("CardNumber", tmNumber);
            Collection.UpdateOne(filter, updated);
        }

        //方法重载，整柜更新
        public static void UpdateTmNumberInfo(int cabinetid, string tmNumber)
        {
            Collection = GetCollection(CollectionNames.MinerLampTest);
            var filter = Builders<T>.Filter.Eq("GroupId", cabinetid);
            var updated = Builders<T>.Update.Set("CardNumber", tmNumber);
            Collection.UpdateOne(filter, updated);
        }

        /// <summary>
        /// 根据员工的objectid,去对应的柜门上更新状态（当充电柜的状态变为备用时）(20170525停用
        /// </summary>
        /// <param name="cabinetid"></param>
        /// <param name="doorid"></param>
        /// <param name="item"></param>
        public static void UpdateCabinetInfo(int cabinetid, int doorid)
        {
            Collection = GetCollection(CollectionNames.MinerLampTest);
            //过滤器的一种写法
            var filter = Builders<T>.Filter.Eq("GroupId", cabinetid) & Builders<T>.Filter.Eq("GroupId", doorid);
            //var update = new BsonDocument("$set",new BsonDocument("DoorInfo." + doorid + ".StatusTime", nowtime ));
            var update1 = Builders<T>.Update.Set("StatusTime", DateTime.Now).Set("Status", 4);
            Collection.UpdateOne(filter, update1);
        }

        /// <summary>
        /// 更新万能卡和确认卡号
        /// </summary>
        public static void UpdateUniversalCardInfo(List<UniversalCard> list)
        {
            Collection = GetCollection(CollectionNames.SettingInfoTest);
            var update1 = Builders<T>.Update.Set("UniversalCards", list);
            Collection.UpdateOne("{}", update1);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        public static void UpdateUserInfo(List<UserInfo> list)
        {
            Collection = GetCollection(CollectionNames.SettingInfoTest);
            var update1 = Builders<T>.Update.Set("UserInfos", list);
            Collection.UpdateOne("{}", update1);
        }

        #endregion

        #region 根据条件删除

        /// <summary>
        /// 根据objectid删除数据
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="objectId"></param>
        public static void DeleteByObjectid(string collectionName, string objectId)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(objectId));
            Collection.DeleteOne(filter);
        }

        /// <summary>
        /// 根据指定的时间，删除矿灯记录,记录表的所有，没加过滤器
        /// </summary>
        /// <param name="time"></param>
        public static void DeleteLampStatusByTime(DateTime time)
        {
            Collection = GetCollection(CollectionNames.MinerAttendanceTest);
            //var filter = Builders<T>.Filter.Lt("StatusList.Time", time);
            //lte 小于或等于
            //db.getCollection('MinerAttendanceTest').updateMany(
            //{ },
            //{ $pull: { 'StatusList': { Status: 2 } } });
            var update = Builders<T>.Update.PullFilter("StatusList",
                Builders<T>.Filter.Lt("Time", time));
            var resut = Collection.UpdateMany("{}", update);
        }
        //加过滤器
        public static void DeleteLampStatusByTime(DateTime time,ObjectId objectId)
        {
            Collection = GetCollection(CollectionNames.MinerAttendanceTest);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            //lte 小于或等于
            //db.getCollection('MinerAttendanceTest').updateMany(
            //{ },
            //{ $pull: { 'StatusList': { Status: 2 } } });
            var update = Builders<T>.Update.PullFilter("StatusHistoryInfos",
                Builders<T>.Filter.Lt("MinerLampStatusTime", time));
            var resut = Collection.UpdateMany(filter, update);
        }


        /// <summary>
        /// 清空集合数据操作
        /// </summary>
        /// <param name="collectionName"></param>
        public static void Clear(string collectionName)
        {
            Collection = GetCollection(collectionName);
            var resut = Collection.DeleteMany("{}");
        }

        /// <summary>
        /// 删除集合操作
        /// </summary>
        /// <param name="collectionName"></param>
        public static void Delete(string collectionName)
        {
            Collection = GetCollection(collectionName);
            Collection.Database.DropCollection(collectionName);
        }

        #endregion

        #region 数据库其他操作

//        public static bool a(string collectionName)
//        {
//            Collection = GetCollection(collectionName);
//            Collection.
//        }

        /// <summary>
        /// 统计文档的条数
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static long DocumentCount(string collectionName)
        {
            Collection = GetCollection(collectionName);
            return Collection.Count(new BsonDocument());
        }

        /// <summary>
        /// 统计各状态总数,条件值为string
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="field">过滤的字段</param>
        /// <param name="value">字段的值</param>
        /// <returns></returns>
        public static long DocumentCount(string collectionName, params string[] fieldandvalue)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq(fieldandvalue[0], fieldandvalue[1]);
            for (var i = 2; i < fieldandvalue.Length; i = i + 2)
            {
                filter = RepeatFilterDefinition(filter, fieldandvalue[i], fieldandvalue[i + 1]);
            }
            return Collection.Count(filter);
        }

        /// <summary>
        /// 统计各状态总数，条件值为int
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="field">过滤的字段</param>
        /// <param name="value">字段的值</param>
        /// <returns></returns>
        public static long DocumentCount(string collectionName, string field, int value)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq(field, value);
            return Collection.Count(filter);
        }

        public static long DocumentCount(string collectionName, string field1, string value1, string field2, int value2)
        {
            Collection = GetCollection(collectionName);
            var filter = Builders<T>.Filter.Eq(field1, value1) & Builders<T>.Filter.Eq(field2, value2);
            return Collection.Count(filter);
        }

        #endregion

        /// <summary>
        /// 为了能够复用filter方法，也就是叠加多个过滤器
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static FilterDefinition<T> RepeatFilterDefinition(FilterDefinition<T> a, string b, string c)
        {
            return a & Builders<T>.Filter.Eq(b, c);
        }

        /// <summary>
        /// 为了复用include方法,也就是叠加多个include
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static ProjectionDefinition<T> RepeatProjectionDefinition(ProjectionDefinition<T> a, string b)
        {
            return a.Include(b);
        }
    }
}