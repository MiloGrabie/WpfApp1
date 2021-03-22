using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataContextEF_Dir;

namespace WpfApp1.ExplorateurVoituresDir
{
    public class VMExploVoiture_Row : ViewModelBase
    {

        public VMExploVoiture_Row(Voiture voiture)
        {
            Voiture = voiture;
        }

        public Voiture Voiture { get; }

        public string Marque
        {
            get
            {
                return Voiture.Marque;
            }
        }
        public string Modele
        {
            get
            {
                return Voiture.Modele;
            }
        }
        public string Couleur
        {
            get
            {
                return Voiture.Couleur;
            }
        }
        public int VMax
        {
            get
            {
                return (int)Voiture.VitesseMax;
            }
        }
        public decimal Prix
        {
            get
            {
                return Voiture.Prix.GetValueOrDefault();
            }
        }
    }
}
