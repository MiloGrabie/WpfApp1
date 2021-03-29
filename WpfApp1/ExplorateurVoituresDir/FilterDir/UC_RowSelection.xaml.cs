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

namespace WpfApp1.ExplorateurVoituresDir.FilterDir
{
    /// <summary>
    /// Logique d'interaction pour UC_RowSelection.xaml
    /// </summary>
    public partial class UC_RowSelection : UserControl
    {
        public UC_RowSelection()
        {
            InitializeComponent();
        }

        public CheckBox AllSelection
        {
            get
            {
                return MainStackPanel.Children.OfType<CheckBox>().First();
            }
        }


    }
}
