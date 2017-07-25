using System.Collections.Generic;
using Newtonsoft.Json;

namespace MinerLampMangement.Model
{
    public class AllStatusHistoryInfo : BaseEntity
    {
        public List<HistoryInfo> StatusList { get; set; }
    }
}