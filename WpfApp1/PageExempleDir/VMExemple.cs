using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp1.PageExempleDir
{
    public class VMExemple : ViewModelBase
    {
        public List<Fruit> ListFruits = new List<Fruit>();
        public ICollectionView ListFruitsView { get; set; }
        public Command AjouterPomme { get; set; }

        public VMExemple()
        {
            ListFruits.Add(new Fruit());
            ListFruitsView = CollectionViewSource.GetDefaultView(ListFruits);
            OnPropertyChanged("ListFruitsView");

            AjouterPomme = new Command(AjouterPomme_Func);
        }

        private void AjouterPomme_Func()
        {
            ListFruits.Add(new Fruit());
            ListFruitsView.Refresh();
            //OnPropertyChanged("ListFruitsView");
        }



    }

    public class Fruit
    {
        public string Nom
        {
            get
            {
                return "Pomme";
            }
        }
    }
}
