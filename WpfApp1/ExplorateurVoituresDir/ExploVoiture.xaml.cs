using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.ExplorateurVoituresDir
{
    /// <summary>
    /// Logique d'interaction pour ExploVoiture.xaml
    /// </summary>
    public partial class ExploVoiture : Page
    {
        public ExploVoiture()
        {

            InitializeComponent();
            this.DataContext = new VMExploVoiture();
        }
    }
}
