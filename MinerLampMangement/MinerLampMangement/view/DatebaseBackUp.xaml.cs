using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MessageBox = System.Windows.MessageBox;

namespace MinerLampMangement.view
{
    /// <summary>
    /// 数据库的备份与还原
    /// </summary>
    public partial class DatebaseBackUp : Window
    {
        //备份文件的路径
        public string Path1 = "\\" + CollectionNames.MinerAttendanceTest + ".bson";
        public string Path2 = "\\" + CollectionNames.MinerInfoTest + ".bson";
        public string Path3 = "\\" + CollectionNames.MinerLampTest + ".bson";
        public string Path4 = "\\" + CollectionNames.SettingInfoTest + ".bson";
        public string Path5 = "\\" + CollectionNames.TmCardInfoTest + ".bson";
        public string Path6 = "\\" + CollectionNames.DeviceGroup + ".bson";

        public DatebaseBackUp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 备份按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackUpBtn_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog std = new SaveFileDialog();
            std.InitialDirectory = "D:\\";
            std.FileName = DateTime.Now.ToString("yyyy-MM-dd-HHmm") + "数据库备份文件";
            if (std.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string r = std.FileName;
                Directory.CreateDirectory(r);
                StringBuilder t1 = new StringBuilder();
                t1.Append(r);
                t1.Append(Path1);
                //生成指定的.bson文件
                FileStream fs1 = new FileStream(t1.ToString(), FileMode.Create);
                fs1.Close();
                exportBson(t1.ToString(), CollectionNames.MinerAttendanceTest);


                StringBuilder t2 = new StringBuilder();
                t2.Append(r);
                t2.Append(Path2);
                //生成指定的.bson文件
                FileStream fs2 = new FileStream(t2.ToString(), FileMode.Create);
                fs2.Close();
                exportBson(t2.ToString(), CollectionNames.MinerInfoTest);


                StringBuilder t3 = new StringBuilder();
                t3.Append(r);
                t3.Append(Path3);
                //生成指定的.bson文件
                FileStream fs3 = new FileStream(t3.ToString(), FileMode.Create);
                fs3.Close();
                exportBson(t3.ToString(), CollectionNames.MinerLampTest);

                StringBuilder t4 = new StringBuilder();
                t4.Append(r);
                t4.Append(Path4);
                //生成指定的.bson文件
                FileStream fs4 = new FileStream(t4.ToString(), FileMode.Create);
                fs4.Close();
                exportBson(t4.ToString(), CollectionNames.SettingInfoTest);

                StringBuilder t5 = new StringBuilder();
                t5.Append(r);
                t5.Append(Path5);
                //生成指定的.bson文件
                FileStream fs5 = new FileStream(t5.ToString(), FileMode.Create);
                fs5.Close();
                exportBson(t5.ToString(), CollectionNames.TmCardInfoTest);

                StringBuilder t6 = new StringBuilder();
                t6.Append(r);
                t6.Append(Path6);
                //生成指定的.bson文件
                FileStream fs6 = new FileStream(t6.ToString(), FileMode.Create);
                fs6.Close();
                exportBson(t6.ToString(), CollectionNames.DeviceGroup);

                MessageBox.Show("数据库数据已备份成功！");
            }
        }

        /// <summary>
        /// 数据还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestoreBtn_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog std = new FolderBrowserDialog();
            if (std.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                String s1 = null;
                s1 = std.SelectedPath;
                s1 = s1 + Path1;
                importBson(s1, CollectionNames.MinerAttendanceTest);


                String s2 = null;
                s2 = std.SelectedPath;
                s2 = s2 + Path2;
                importBson(s2, CollectionNames.MinerInfoTest);


                String s3 = null;
                s3 = std.SelectedPath;
                s3 = s3 + Path3;
                importBson(s3, CollectionNames.MinerLampTest);

                String s4 = null;
                s4 = std.SelectedPath;
                s4 = s4 + Path4;
                importBson(s4, CollectionNames.SettingInfoTest);

                String s5 = null;
                s5 = std.SelectedPath;
                s5 = s5 + Path5;
                importBson(s5, CollectionNames.TmCardInfoTest);

                String s6 = null;
                s6 = std.SelectedPath;
                s6 = s6 + Path6;
                importBson(s6, CollectionNames.DeviceGroup);

                MessageBox.Show("数据还原成功！");
            }
        }

        public void exportBson(string file, string collectionName)
        {
            string outputFileName = file;
            var collection2 = MongoDbHelper<MinerInfo>.GetCollection2(collectionName);
            using (var streamWriter = new StreamWriter(outputFileName))
            {
                var cursor = collection2.Find(new BsonDocument()).ToCursor();
                foreach (var document in cursor.ToEnumerable())
                {
                    using (var stringWriter = new StringWriter())
                    using (var jsonWriter = new JsonWriter(stringWriter))
                    {
                        var context = BsonSerializationContext.CreateRoot(jsonWriter);
                        collection2.DocumentSerializer.Serialize(context, document);
                        var line = stringWriter.ToString();
                        streamWriter.WriteLine(line);
                    }
                }
            }
        }


        public void importBson(string file, string collectionName)
        {
            //先删除集合
            MongoDbTool.DeleteCollection(collectionName);
            string inputFileName = file;
            var collection = MongoDbHelper<MinerInfo>.GetCollection2(collectionName);
            using (var streamReader = new StreamReader(inputFileName))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    using (var jsonReader = new JsonReader(line))
                    {
                        var context = BsonDeserializationContext.CreateRoot(jsonReader);
                        var document = collection.DocumentSerializer.Deserialize(context);

                        collection.InsertOne(document);
                    }
                }
            }
        }
    }
}