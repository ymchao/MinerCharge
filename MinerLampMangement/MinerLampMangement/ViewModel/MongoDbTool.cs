using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.view;
using MongoDB.Bson;
using static System.DateTime;
using MessageBox = System.Windows.MessageBox;

namespace MinerLampMangement.ViewModel
{
    /// <summary>
    /// 对数据库的具体操作
    /// </summary>
    public class MongoDbTool
    {
        #region 生成集合数据，用作测试用

        /// <summary>
        /// 插入20个充电柜文档（测试用,信息自拟）
        /// </summary>
        public static void InsertDeviceInfo()
        {
            var random = new Random();

            List<MinerLampStatus> suij = new List<MinerLampStatus>()
            {
                MinerLampStatus.Using,
                MinerLampStatus.Full,
                MinerLampStatus.Charging,
                MinerLampStatus.ChargeProblem,
                MinerLampStatus.UnInit
            };
            for (var i = 1; i <= 20; i++)
            {
                for (var j = 1; j <= 100; j++)
                {
                    var suijishu = random.Next(5);
                    var item = new DoorInfo()
                    {
                        GroupId = i,
                        DeviceId = j,
                        Status = suij[suijishu],
                        CardNumber = "5615ab56145" + i + j,
                        RemainChargeTime = i * 3 + j,
                        StatusTime = Now.AddMinutes(j),
                        EmployeeObjectId = j.ToString()
                    };
                    MongoDbHelper<DoorInfo>.Insert(CollectionNames.MinerLampTest, item);
                }
            }
        }

        /// <summary>
        /// 插入20个充电柜文档（数据库初始化用,信息备用）
        /// </summary>
        /// <param name="num">充电柜数量</param>
        public static void InsertDeviceInfo2(int num)
        {
            for (var i = 1; i <= num; i++)
            {
                for (var j = 1; j <= 100; j++)
                {
                    var item = new DoorInfo()
                    {
                        GroupId = i,
                        DeviceId = j,
                        RemainChargeTime = 500
                    };
                    MongoDbHelper<DoorInfo>.Insert(CollectionNames.MinerLampTest, item);
                }
            }
        }

        /// <summary>
        /// 插入2000个员工的考勤记录（测试用）
        /// </summary>
        public static void InsertAttendanceTest()
        {
            int x = 0;
            // MinerWorkStatus b;
            var random = new Random();
            List<MinerLampStatus> suij = new List<MinerLampStatus>()
            {
                MinerLampStatus.Using,
                MinerLampStatus.Full,
                MinerLampStatus.Charging,
                MinerLampStatus.ChargeProblem,
                MinerLampStatus.UnInit
            };

            for (var i = 1; i <= 2000; i++)
            {
                var item = new AllStatusHistoryInfo()
                {
                    StatusList = new List<HistoryInfo>()
                };
                for (int j = 1; j <= 100; j++)
                {
                    if (j % 3 == 1)
                    {
                        var a = new HistoryInfo()
                        {
                            Status = MinerLampStatus.Charging,
                            Time = Now.AddHours(x),
                        };
                        item.StatusList.Add(a);
                    }
                    else if (j % 3 == 2)
                    {
                        var a = new HistoryInfo()
                        {
                            Status = MinerLampStatus.Full,
                            Time = Now.AddHours(x),
                        };
                        item.StatusList.Add(a);
                    }
                    else
                    {
                        var a = new HistoryInfo()
                        {
                            Status = MinerLampStatus.Using,
                            Time = Now.AddHours(x),
                        };
                        item.StatusList.Add(a);
                    }
                    //随机他们的时间
                    int[] numbers = {6, 8, 10, 12};
                    int index = random.Next(0, 4);
                    x = x + numbers[index];
                }
                MongoDbHelper<AllStatusHistoryInfo>.Insert(CollectionNames.MinerAttendanceTest, item);
            }
        }

        /// <summary>
        /// 插入2000个员工，并与考勤表进行绑定
        /// </summary>
        public static void InsertPeopleTest()
        {
            //只需要_id列的信息
//            var objectidlist = MongoDbHelper<AllStatusHistoryInfo>.FindPartFieldInfo(
//                CollectionNames.MinerAttendanceTest,
//                "_id");
            int x = 0;
            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 100; j++)
                {
                    if (j % 15 == 0)
                    {
                        var item = new MinerInfo()
                        {
//                            Id = ObjectId.Parse(objectidlist[x].GetValue("_id").ToString()),
                            GroupId = i,
                            DeviceId = j,
                            Name = "备用",
                            Others = "此灯位备用！"
                        };
                        MongoDbHelper<MinerInfo>.Insert(CollectionNames.MinerInfoTest, item);
                    }
                    else
                    {
                        Random ran = new Random();
                        int Randnum = ran.Next(0, 11);

                        string[] listsurname =
                        {
                            "王",
                            "李",
                            "赵",
                            "高",
                            "冯",
                            "马",
                            "张",
                            "刘",
                            "杨",
                            "陈",
                            "孙",
                            "梅"
                        };

                        string[] listname =
                        {
                            "超",
                            "亮",
                            "帅",
                            "伟",
                            "强",
                            "鹏",
                            "文昊 ",
                            "正豪",
                            "翰飞",
                            "国源",
                            "俊明",
                            "建辉"
                        };

                        string[] listdepartment =
                        {
                            "保卫科",
                            "机电一队",
                            "挖掘队",
                            "防突队",
                            "掘进二队",
                            "评估队",
                            "综采二队",
                            "信息办",
                            "运输队",
                            "调度室",
                            "放炮队",
                            "物管队"
                        };

                        var item = new MinerInfo()
                        {
//                            Id = ObjectId.Parse(objectidlist[x].GetValue("_id").ToString()),
                            GroupId = i,
                            DeviceId = j,
                            Name = getRandomName(),
                            JobNumber = "PMJT" + i + j,
                            Sex = "男",
                            BornDate = Now,
                            MaritalStatus = "已婚",
                            National = GetRandomNation(),
                            IdentityCardId = "54224465456131",
                            NativePlace = "河南",
                            Address = "金鸡岭路一号",
                            Blood = "A",
                            Image = "照片",
                            PoliticalStatus = "党员",
                            Education = "本科",
                            PhoneNum = "152365899562",
                            ContractTypes = "合同工",
                            Department = listdepartment[Randnum],
                            Position = "经理",
                            JoinTime = Now,
                            EquipLampTime = Now,
                            LampTypes = "NB666",
                            Classes = "早班",
                            EquipSelfRescuerTime = Now,
                            SelfRescuerId = "NB555",
                            Others = "大家发了看到手机！",
                        };
                        MongoDbHelper<MinerInfo>.Insert(CollectionNames.MinerInfoTest, item);
                    }
                    x = x + 1;
                }
            }
        }

        /// <summary>
        /// 新建员工的考勤表，用员工的objectid当objectid
        /// </summary>
        /// <param name="list"></param>
//        public static void insertStatusTable()
//        {
//            //只需要_id列的信息
//            var objectidlist = MongoDbHelper<MinerInfo>.FindPartFieldInfo(
//                CollectionNames.MinerInfoTest, "_id");
//            //新建一个历史记录类
//            var item = new AllStatusHistoryInfo()
//            {
//                StatusList = new List<HistoryInfo>()
//            };
//            foreach (var objectid in objectidlist)
//            {
//            }
//        }
        /// <summary>
        /// 更新柜门表的objectId字段，将员工ObjectId写到柜门上
        /// </summary>
        public static void UpdateObjectId()
        {
            //只需要CabinetId和DoorId字段。
            var partDocument = MongoDbHelper<DoorInfo>.FindPartFieldInfo(CollectionNames.MinerInfoTest, "GroupId",
                "DeviceId");
            //获取柜子数量
            var groupnumber = MongoDbHelper<DoorInfo>.DocumentCount(CollectionNames.MinerLampTest);

            for (var i = 0; i < groupnumber; i++)
            {
                var cabinetid = partDocument[i].GetValue("GroupId").ToInt32();
                var doorid = partDocument[i].GetValue("DeviceId").ToInt32();
                var objectid = partDocument[i].GetValue("_id").ToString();
                var filter = "{ GroupId: " + cabinetid + ",DeviceId:" + doorid + "}";

                var update = new BsonDocument("$set",
                    new BsonDocument("EmployeeObjectId", objectid));
                MongoDbHelper<DoorInfo>.GetCollection(CollectionNames.MinerLampTest)
                    .UpdateOne(filter, update);
            }
        }

        #endregion

        /// <summary>
        /// 根据集合名称，清空集合数据
        /// </summary>
        /// <param name="collectionNames"></param>
        public static void ClearCollection(params string[] collectionNames)
        {
            foreach (var collectionName in collectionNames)
            {
                MongoDbHelper<MinerInfo>.Clear(collectionName);
            }
        }

        /// <summary>
        /// 根据集合名称，删除集合
        /// </summary>
        /// <param name="collectionName"></param>
        public static void DeleteCollection(params string[] collectionNames)
        {
            foreach (var collectionName in collectionNames)
            {
                MongoDbHelper<MinerInfo>.Delete(collectionName);
            }
        }

        /// <summary>
        /// 根据cabinetid和doorid更新、删除、添加相应的员工信息
        /// </summary>
        /// <param name="cabinetid"></param>
        /// <param name="doorid"></param>
        /// <param name="item"></param>
        public static void UpdateMinerInfo(int cabinetid, int doorid, MinerInfo item)
        {
            MongoDbHelper<MinerInfo>.UpdateMinerInfo(cabinetid, doorid, item);
        }

        /// <summary>
        /// 根据员工的objectid ，去修改对应柜门上的状态时间信息
        /// </summary>
        /// <param name="cabinetid"></param>
        /// <param name="doorid"></param>
        /// <param name="item"></param>
        public static void UpdateCabinetInfo(int cabinetid, int doorid)
        {
            MongoDbHelper<DoorInfo>.UpdateCabinetInfo(cabinetid, doorid);
        }

        /// <summary>
        /// 根据柜门上矿灯的状态，统计汇总信息查询
        /// </summary>
        public static List<long> CountTotalInfoByStatus()
        {
            List<long> list = new List<long>();
            //柜门总数
            list.Add(MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest));
            //员工总数(也就是柜门总数)-备用名称的
            list.Add(MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest) -
                     MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Name", "备用"));
            //未初始化总数
            list.Add(MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Status", 0));
            //使用总数
            list.Add(MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Status", 1));
            //充电总数
            list.Add(MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Status", 2));
            //充满总数
            list.Add(MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Status", 3));
            //故障总数
            var problemmnum = MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Status", 4) +
                              MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Status", 5) +
                              MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Status", 6);
            list.Add(problemmnum);
            //备用
            list.Add(MongoDbHelper<MinerInfo>.DocumentCount(CollectionNames.MinerLampTest, "Name", "备用"));
            return list;
        }

        /// <summary>
        /// 根据部门名称和矿灯状态，统计汇总信息查询
        /// </summary>
        public static List<int> CountTotalInfoByDepartmentAndStatus(string department)
        {
            var objectidlist = MongoDbHelper<MinerInfo>.FindOneFieldValueInfoByField(CollectionNames.MinerInfoTest,
                department);

            int chargingNum = 0,
                fullNum = 0,
                usingNum = 0,
                problemNum = 0;
            foreach (var item in objectidlist)
            {
                List<string> fieldandvalue = new List<string>();
                fieldandvalue.Add("EmployeeObjectId");
                fieldandvalue.Add(item.GetValue(0).ToString());
                var status = MongoDbHelper<DoorInfo>.FindInfoByField(CollectionNames.MinerLampTest,
                    fieldandvalue);

                if (status.Count != 0)
                {
                    switch (status[0].Status)
                    {
                        case MinerLampStatus.Charging:
                            chargingNum++;
                            break;
                        case MinerLampStatus.Full:
                            fullNum++;
                            break;
                        case MinerLampStatus.Using:
                            usingNum++;
                            break;
                        case MinerLampStatus.ChargeProblem:
                            problemNum++;
                            break;
                        case MinerLampStatus.CommuncationProblem:
                            problemNum++;
                            break;
                        case MinerLampStatus.MultiProblem:
                            problemNum++;
                            break;
                    }
                }
            }
            List<int> list = new List<int>();
            //充电总数
            list.Add(chargingNum);
            //充满总数
            list.Add(fullNum);
            //使用总数
            list.Add(usingNum);
            //问题总数
            list.Add(problemNum);
            return list;
        }

        /// <summary>
        /// 去重数据统计,string类型
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static List<string> DistinctInfoString(string collectionName, string field)
        {
            return MongoDbHelper<MinerInfo>.DistinctInfoString(collectionName, field);
        }

        /// <summary>
        /// 去重数据统计，int类型
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static List<int> DistinctInfoInt(string collectionName, string field)
        {
            return MongoDbHelper<MinerInfo>.DistinctInfoInt(collectionName, field);
        }

        #region 随机姓名生成器

        private static string firstName = @"赵,钱,孙,李,周,吴,郑,王,冯,陈,褚,卫,蒋,
            沈,韩,杨,朱,秦,尤,许,何,吕,施,张,孔,曹,严,华,金,魏,陶,姜, 戚,谢,邹,喻,
            柏,水,窦,章,云,苏,潘,葛,奚,范,彭,郎,鲁,韦,昌,马,苗,凤,花,方,俞,任,袁,柳,
            丰,鲍,史,唐, 费,廉,岑,薛,雷,贺,倪,汤,滕,殷,罗,毕,郝,邬,安,常,乐,于,时,
            傅,皮,卞,齐,康,伍,余,元,卜,顾,孟,平,黄, 和,穆,萧,尹,姚,邵,湛,汪,祁,毛,
            禹,狄,米,贝,明,臧,计,伏,成,戴,谈,宋,茅,庞,熊,纪,舒,屈,项,祝,董,梁, 杜,
            阮,蓝,闵,席,季,麻,强,贾,路,娄,危,江,童,颜,郭,梅,盛,林,刁,钟,徐,丘,骆,高,
            夏,蔡,田,樊,胡,凌,霍, 虞,万,支,柯,昝,管,卢,莫,经,房,裘,缪,干,解,应,宗,丁,
            宣,贲,邓,郁,单,杭,洪,包,诸,左,石,崔,吉,钮,龚, 程,嵇,邢,滑,裴,陆,荣,翁,荀,
            羊,於,惠,甄,麴,家,封,芮,羿,储,靳,汲,邴,糜,松,井,段,富,巫,乌,焦,巴,弓, 牧,
            隗,山,谷,车,侯,宓,蓬,全,郗,班,仰,秋,仲,伊,宫,宁,仇,栾,暴,甘,钭,厉,戌,祖,
            武,符,刘,景,詹,束,龙, 叶,幸,司,韶,郜,黎,蓟,薄,印,宿,白,怀,蒲,邰,从,鄂,索,
            咸,籍,赖,卓,蔺,屠,蒙,池,乔,阴,郁,胥,能,苍,双, 闻,莘,党,翟,谭,贡,劳,逢,姬,
            申,扶,堵,冉,宰,郦,雍,郤,璩,桑,桂,濮,牛,寿,通,边,扈,燕,冀,郏,浦,尚,农, 温,
            别,庄,晏,柴,瞿,阎,充,慕,连,茹,习,宦,艾,鱼,容,向,古,易,慎,戈,廖,庾,终,暨,
            居,衡,步,都,耿,满,弘, 匡,国,文,寇,广,禄,阙,东,欧,殳,沃,利,蔚,越,菱,隆,师,
            巩,厍,聂,晃,勾,敖,融,冷,訾,辛,阚,那,简,饶,空, 曾,毋,沙,乜,养,鞠,须,丰,巢,
            关,蒯,相,查,后,荆,红,游,竺,权,逯,盖,益,桓,公, 万俟,司马,上官,欧阳,夏侯,
            诸葛,闻人,东方,赫连,皇甫,尉迟,公羊,澹台,公冶,宗政,濮阳,淳于,单于,太叔,
            申屠,公孙,仲孙,轩辕,令狐,钟离,宇文,长孙,慕容,司徒,司空";

        private static string lastName = @"努,迪,立,林,维,吐,丽,新,涛,米,亚,克,湘,明,
            白,玉,代,孜,霖,霞,加,永,卿,约,小,刚,光,峰,春,基,木,国,娜,晓,兰,阿,伟,英,元,
            音,拉,亮,玲,木,兴,成,尔,远,东,华,旭,迪,吉,高,翠,莉,云,华,军,荣,柱,科,生,昊,
            耀,汤,胜,坚,仁,学,荣,延,成,庆,音,初,杰,宪,雄,久,培,祥,胜,梅,顺,涛,西,库,康,
            温,校,信,志,图,艾,赛,潘,多,振,伟,继,福,柯,雷,田,也,勇,乾,其,买,姚,杜,关,陈,
            静,宁,春,马,德,水,梦,晶,精,瑶,朗,语,日,月,星,河,飘,渺,星,空,如,萍,棕,影,南,北";
        private static string nationName = @"汉族,壮族,满族,回族,苗族,维吾尔族,土家族,
            彝族,蒙古族,藏族,布依族,侗族,瑶族,朝鲜族,白族,哈尼族,哈萨克族,黎族,傣族,畲族,
            傈僳族,仡佬族,东乡族,高山族,拉祜族,水族,佤族,纳西族,羌族,土族,仫佬族,锡伯族,
            柯尔克孜族,达斡尔族,景颇族,毛南族,撒拉族,布朗族,塔吉克族,阿昌族,普米族,鄂温克族
            ,怒族,京族,基诺族,德昂族,保安族,俄罗斯族,裕固族,乌兹别克族,门巴族,鄂伦春族,
            独龙族,塔塔尔族,赫哲族,珞巴族";
        static Random rnd = new Random((int) DateTime.Now.ToFileTimeUtc());

        public static string getRandomName()
        {
            int namelength = 0;
            namelength = rnd.Next(2, 4);
            firstName = firstName.Replace("\n", "");
            firstName = firstName.Replace("\r", "");
            firstName = firstName.Replace(" ", "");
            lastName = lastName.Replace("\r", "");
            lastName = lastName.Replace("\n", "");
            lastName = lastName.Replace(" ", "");
            string name = "";
            string[] FirstName = firstName.Split(',');
            string[] LastName = lastName.Split(',');
            if (namelength == 2)
            {
                name = FirstName[rnd.Next(0, FirstName.Length)] + LastName[rnd.Next(0, LastName.Length)];
            }
            else if (namelength == 3)
            {
                name = FirstName[rnd.Next(0, FirstName.Length)] + LastName[rnd.Next(0, LastName.Length)] +
                       LastName[rnd.Next(0, LastName.Length)];
            }

            return name;
        }

        public static string GetRandomNumber(int startnumber, int endnumber)
        {
            string strNumber = rnd.Next(startnumber, endnumber).ToString();
            return strNumber;
        }

        public static string GetRandomNation()
        {
            nationName = nationName.Replace("\n", "");
            nationName = nationName.Replace("\r", "");
            nationName = nationName.Replace(" ", "");
            string[] nationname = nationName.Split(',');
            string nation = nationname[rnd.Next(0, 55)];
            return nation;
        }

        #endregion
    }
}