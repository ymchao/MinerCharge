using System.Collections.Generic;
using MinerLampMangement.Enum;
using MinerLampMangement.Model;
using MinerLampMangement.ViewModel;

namespace MinerLampMangement.view.viewbean
{
    //初始化界面
    class InitXaml
    {
        /// <summary>
        /// 初始化左侧部门树
        /// </summary>
        public static List<TreeviewDepartment> InitDepartmentTree()
        {
            //获取所有部门信息
            var departmentinfo = MongoDbTool.DistinctInfoString(CollectionNames.MinerInfoTest, "Department");

            if (departmentinfo.Count > 0)
            {
                List<TreeviewDepartment> itemList = new List<TreeviewDepartment>();

                foreach (var item in departmentinfo)
                {
                    //根据部门名称，获取各状态的个数
                    var departmentstatusinfo = MongoDbTool.CountTotalInfoByDepartmentAndStatus(item);

                    TreeviewDepartment node1 = new TreeviewDepartment()
                    {
                        Name = item,
                        Icon = "Image/WorkTimeQuery.png",
                    };

                    TreeviewDepartment node1Item1 = new TreeviewDepartment()
                    {
                        Name = "充电：" + departmentstatusinfo[0],
                        Icon = "Image/charging.png",
                    };
                    node1.Children.Add(node1Item1);

                    TreeviewDepartment node1Item2 = new TreeviewDepartment()
                    {
                        Name = "充满：" + departmentstatusinfo[1],
                        Icon = "Image/full.png",
                    };
                    node1.Children.Add(node1Item2);

                    TreeviewDepartment node1Item3 = new TreeviewDepartment()
                    {
                        Name = "使用：" + departmentstatusinfo[2],
                        Icon = "Image/using.png",
                    };
                    node1.Children.Add(node1Item3);

                    TreeviewDepartment node1Item4 = new TreeviewDepartment()
                    {
                        Name = "故障：" + departmentstatusinfo[3],
                        Icon = "Image/problem.png",
                    };
                    node1.Children.Add(node1Item4);
                    itemList.Add(node1);
                }
                return itemList;
            }
            else
            {
                return null;
            }
        
        }
    }
}