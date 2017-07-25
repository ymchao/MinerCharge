using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinerLampMangement.Model
{
    class TreeviewDepartment
    {
        public string Icon { get; set; }
        public string Name { get; set; }

        public List<TreeviewDepartment> Children { get; set; }

        public TreeviewDepartment()
        {
            Children  = new List<TreeviewDepartment>();
        }
    }
}
