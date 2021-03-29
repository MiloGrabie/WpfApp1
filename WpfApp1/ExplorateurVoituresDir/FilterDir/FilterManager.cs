using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.ExplorateurVoituresDir.FilterDir
{
    public class FilterManager
    {

        List<FilterDataGrid> FilterDataGrids = new List<FilterDataGrid>();
        public ICollectionView RowCollectionView { get; set; }

        public FilterManager(ICollectionView rowCollectionView)
        {
            RowCollectionView = rowCollectionView;
            InitFilter();
        }

        private void InitFilter()
        {
            foreach (var column in ((System.Windows.Data.ListCollectionView)RowCollectionView).ItemProperties.Select(a => a.Name))
                FilterDataGrids.Add(new FilterDataGrid(column, this));
        }

        internal void OpenContextMenu(Button sender)
        {
            if (sender.ContextMenu == null)
                AffectContextMenu(sender);
            (sender.ContextMenu as FilterDataGrid).Open(sender);
        }

        private void AffectContextMenu(Button sender)
        {
            string dataBindProp = ((System.Windows.Controls.Primitives.DataGridColumnHeader)(ContentControl)((FrameworkElement)((FrameworkElement)((sender as Button).TemplatedParent as ContentPresenter).Parent).Parent).TemplatedParent).Column.SortMemberPath;
            sender.ContextMenu = FilterDataGrids.FirstOrDefault(a => a.DataBindProp == dataBindProp);
        }
    }
}
