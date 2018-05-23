using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaledServices
{
    public enum MenuType
    {
        None,//开始为空
        Super,//需要把下面的权限全部加进去才可以
        Self,
        Bga_Repair,
        Repair,
        TestALL,
        Test1,
        Test2,
        Recieve_Return,
        Outlook,
        Store,
        Running,
        Obe,
        Package,
        Other,//把不能分类的都放在这里
    }
}
