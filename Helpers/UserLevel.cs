using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yubay_Drone_team.Helpers
{


    public enum UserLevel
    {

        /// <summary> 僅新增跟修改功能 </summary>
        Normal = 1,

        /// <summary>
        /// 有刪除功能
        /// </summary>
        Supervisor = 2
    }

}