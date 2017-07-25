using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinerLampMangement.Model
{
    //设置信息及卡号信息
   public class SettingInfo : BaseEntity
    {
        //万能卡号组
        public List<UniversalCard> UniversalCards { get; set; }

        //确认卡号组
        public List<ConfirmCard> ConfirmCards { get; set; }

        //用户数组
        public List<UserInfo> UserInfos { get; set; }


    }
}
