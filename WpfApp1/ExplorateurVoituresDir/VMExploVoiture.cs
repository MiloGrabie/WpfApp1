using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp1.DataContextEF_Dir;
using WpfApp1.ExplorateurVoituresDir.FilterDir;

namespace WpfApp1.ExplorateurVoituresDir
{
    public class VMExploVoiture : ViewModelBase
    {

        public VMExploVoiture()
        {
            InitCommand();
            InitDataGrid();
        }


        #region Ajout Voiture

        public Command Ajouter { get; set; }

        private void InitCommand()
        {
            Ajouter = new Command(Ajouter_Func);
		}

		private void Ajouter_Func()
        {
			DataAcessLayer.DataContext.Voiture.Add(new Voiture
			{
				Marque = voitureMarque,
				Modele = voitureModele,
				Couleur = voitureCouleur,
				VitesseMax = voitureVMax,
				Prix = voiturePrix
			});
			DataAcessLayer.DataContext.SaveChanges();
			MessageBox.Show("Voiture enregistrée", "Enregistrement de voiture", MessageBoxButton.OK, MessageBoxImage.Information);
			UpdateDataGrid();
			OnPropertyChanged("RowCollectionView");
		}

        private string voitureMarque;
		private string voitureModele;
		private string voitureCouleur;
		private int voitureVMax;
		private decimal voiturePrix;

		public string VoitureMarque
		{
			get
			{
				return voitureMarque;
			}
			set
			{
				if (voitureMarque != value)
				{
					voitureMarque = value;
					OnPropertyChanged("VoitureMarque");
				}
			}
		}
		public string VoitureModele
		{
			get
			{
				return voitureModele;
			}
			set
			{
				if (voitureModele != value)
				{
					voitureModele = value;
					OnPropertyChanged("VoitureModele");
				}
			}
		}
		public string VoitureCouleur
		{
			get
			{
				return voitureCouleur;
			}
			set
			{
				if (voitureCouleur != value)
				{
					voitureCouleur = value;
					OnPropertyChanged("VoitureCouleur");
				}
			}
		}
		public int VoitureVMax
		{
			get
			{
				return voitureVMax;
			}
			set
			{
				if (voitureVMax != value)
				{
					voitureVMax = value;
					OnPropertyChanged("VoitureVMax");
				}
			}
		}
		public decimal? Prix
		{
			get
			{
				return voiturePrix;
			}
			set
			{
				if (voiturePrix != value)
				{
					voiturePrix = (decimal)value;
					OnPropertyChanged("Prix");
				}
			}
		}

		#endregion

		#region DataGrid


		private List<VMExploVoiture_Row> rowList;
        private ICollectionView rowCollectionView;
        public List<VMExploVoiture_Row> RowList { get => rowList; set => rowList = value; }
        public CollectionViewSource CollectionViewSource { get; set; }
        public ICollectionView RowCollectionView { get => rowCollectionView; set => rowCollectionView = value; }
		public FilterManager FilterManager;

		private void InitDataGrid()
        {
            RowList = new List<VMExploVoiture_Row>();

			UpdateDataGrid();

			CollectionViewSource = new CollectionViewSource() { Source = RowList };

			rowCollectionView = CollectionViewSource.View;
			FilterManager = new FilterManager(rowCollectionView, CollectionViewSource);
			OnPropertyChanged("RowCollectionView");
		}


        private void UpdateDataGrid()
        {
			rowList.Clear();
			List<Voiture> voitures = DataAcessLayer.DataContext.Voiture.ToList();
			foreach (var voiture in voitures)
			{
				rowList.Add(new VMExploVoiture_Row(voiture));
			}

            if (RowCollectionView != null)
                RowCollectionView.Refresh();
        }

        public VMExploVoiture_Row SelectedItem { get; set; }



		#endregion
	}
}
