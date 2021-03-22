using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataContextEF_Dir;

namespace WpfApp1
{
    public class DataAcessLayer
    {

        private static DataContext dataContext = new DataContext();

        public static DataContext DataContext
        {
            get
            {
                return dataContext;
            }
        }

    }
}
