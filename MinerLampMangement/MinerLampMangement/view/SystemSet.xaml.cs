using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace MinerLampMangement.view
{
    /// <summary>
    /// SystemSet.xaml 的交互逻辑
    /// </summary>
    public partial class SystemSet : Window
    {
        public SystemSet()
        {
            InitializeComponent();
        }

        //        /// <summary>         
        //        /// 重写OnClosing事件 解决窗口关闭不能再开的bug。         
        //        /// </summary>         
        //        /// <param name="e"></param>         
        //        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //        {
        //            this.Hide();
        //            e.Cancel = true;
        //        }


        /// <summary>
        /// 当选中TM卡号管理时，自动查询数据库，填写相应信息到textbox中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //如果选中Tm卡号管理，则直接通过服务器获取数据并显示在界面中
                if (TabItem2.IsSelected)
                {
                    //在group1中填充万能卡号
                    var url = "/info/freecard";
                    var listcard = HttpClient.HttpClient.HttpGet(url, "application/json");
                    List<string> type = new List<string>();
                    DataContractJsonSerializer json = new DataContractJsonSerializer(type.GetType());
                    MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(listcard));
                    var card = json.ReadObject(stream);
                    var list = (from object item in (IEnumerable)card select item.ToString()).ToList();
                    var i = 0;
                    foreach (var textbox in Grid1.Children.OfType<TextBox>())
                    {
                        if (i < list.Count)
                        {
                            textbox.Text = list[i];
                            i++;
                        }
                        else
                        {
                            textbox.Text = "";
                        }
                    }

                    //在group2中填充确认卡号
                    var url2 = "/info/confirmcard";
                    var listcard2 = HttpClient.HttpClient.HttpGet(url2, "application/json");
                    List<string> type2 = new List<string>();
                    DataContractJsonSerializer json2 = new DataContractJsonSerializer(type2.GetType());
                    MemoryStream stream2 = new MemoryStream(Encoding.ASCII.GetBytes(listcard2));
                    var card2 = json2.ReadObject(stream2);
                    var list2 = (from object item in (IEnumerable)card2 select item.ToString()).ToList();
                    var j = 0;
                    foreach (var textbox in Grid2.Children.OfType<TextBox>())
                    {
                        if (j < list2.Count)
                        {
                            textbox.Text = list2[j];
                            j++;
                        }
                        else
                        {
                            textbox.Text = "";
                        }

                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("获取数据库卡号失败，请检查后重试！");
            }
          
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// 万能卡写入充电柜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UniversalCardSave_OnClick(object sender, RoutedEventArgs e)
        {
            //           //自己将卡号更新到数据库
            //           var listcard = new List<UniversalCard>();
            //           foreach (var textbox in Grid1.Children.OfType<TextBox>())
            //            {
            //                if (textbox.Text != "")
            //                {
            //                    var cardNumber = Presenter.CardParseLong(textbox.Text);
            //                    var universalCard = new UniversalCard {CardNumber = cardNumber};
            //                    listcard.Add(universalCard);
            //                }
            //            }
            //           //更新到数据库
            //            MongoDbHelper<SettingInfo>.UpdateUniversalCardInfo(listcard);

            //通过Http协议POST方法，将数据发给服务器，服务器更新到数据库和柜门
            try
            {
                var listcard = new List<string>();
                foreach (var textbox in Grid1.Children.OfType<TextBox>())
                {
                    if (textbox.Text != "")
                    {
                        listcard.Add(textbox.Text);
                    }
                }
                //将list<string>转换成json格式
                DataContractJsonSerializer json = new DataContractJsonSerializer(listcard.GetType());
                string tmJason = "";
                //序列化
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, listcard);
                    tmJason = Encoding.UTF8.GetString(stream.ToArray());
                }
                var result = HttpClient.HttpClient.HttpPost("/operate/freecard/255/255", tmJason);
                MessageBox.Show(result == "success" ? "写入成功！" : "写入失败，请稍检查后重试！");
            }
            catch (Exception)
            {
                MessageBox.Show("写入失败，请稍后重试！");
            }
        }

        /// <summary>
        /// 确认卡写入充电柜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmCardSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var listcard = new List<string>();
                foreach (var textbox in Grid2.Children.OfType<TextBox>())
                {
                    if (textbox.Text != "")
                    {
                        listcard.Add(textbox.Text);
                    }
                }
                //将list<string>转换成json格式
                DataContractJsonSerializer json = new DataContractJsonSerializer(listcard.GetType());
                string tmJason = "";
                //序列化
                using (MemoryStream stream = new MemoryStream())
                {
                    json.WriteObject(stream, listcard);
                    tmJason = Encoding.UTF8.GetString(stream.ToArray());
                }
                //通过Http协议，将数据传给服务器，让服务器写入数据库
                var result = HttpClient.HttpClient.HttpPost("/info/confirmcard", tmJason);
                MessageBox.Show(result == "success" ? "写入成功！" : "写入失败，请稍检查后重试！");
            }
            catch (Exception)
            {
                MessageBox.Show("写入失败，请稍后重试！");
            }
        }
    }
}