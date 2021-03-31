using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp1.ExplorateurVoituresDir.FilterDir
{
    public class FilterDataGrid : ContextMenu
    {

        public DataGridTextColumn Column { get; private set; }

        public string DataBindProp;
        public List<FrameworkElement> menuItems { get; } = new List<FrameworkElement>();
        public FilterManager Manager;

        public FilterDataGrid(string dataBindProp, FilterManager filterManager)
        {
            Manager = filterManager;
            DataBindProp = dataBindProp;

            CreateMenuItems();
            InitSortItems();
        }

        internal void Open(Button sender)
        {
            this.IsOpen = true;
            this.StaysOpen = true;
            Column = (DataGridTextColumn)((System.Windows.Controls.Primitives.DataGridColumnHeader)(ContentControl)((FrameworkElement)((FrameworkElement)((sender as Button).TemplatedParent as ContentPresenter).Parent).Parent).TemplatedParent).Column;
            UpdateRowSelection();
        }


        private void CreateMenuItems()
        {
            AddSortItems();
            InitRowSelection();

            this.ItemsSource = menuItems;
        }

        private void UpdateColumn()
        {
            Manager.RowCollectionView.Refresh();
        }

        #region Filter Item

        #region Sort

        public Nullable<SortDescription> SortDescription { get; set; }

        List<MenuItem> GroupSort = new List<MenuItem>();

        private void InitSortItems()
        {
        }

        private void AddSortItems()
        {
            MenuItem sortAZ = new MenuItem();
            sortAZ.Header = "Trier de A à Z";
            sortAZ.Click += SortAZ_Click;

            MenuItem sortZA = new MenuItem();
            sortZA.Header = "Trier de Z à A";
            sortZA.Click += SortZA_Click; ;

            menuItems.Add(sortAZ);
            menuItems.Add(sortZA);
            GroupSort.Add(sortZA);
            GroupSort.Add(sortAZ);
        }

        private void SortZA_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            SortAction(menuItem, ListSortDirection.Descending);
            e.Handled = true;
        }

        private void SortAZ_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            SortAction(menuItem, ListSortDirection.Ascending);
            e.Handled = true;
        }

        private void SortAction(MenuItem menuItem, ListSortDirection direction)
        {
            if (ChangeMenuItemCheckedSort(menuItem))
                SetSort(new SortDescription(DataBindProp, direction));
            else
                RemoveSort();

            UpdateColumn();
        }

        private void SetSort(SortDescription sortDescription)
        {
            RemoveSort();
            Manager.RowCollectionView.SortDescriptions.Add(sortDescription);
        }

        private void RemoveSort()
        {
            var existingSortDescription = Manager.RowCollectionView.SortDescriptions.FirstOrDefault(a => a.PropertyName == DataBindProp);
            Manager.RowCollectionView.SortDescriptions.Remove(existingSortDescription);
        }

        private bool ChangeMenuItemCheckedSort(MenuItem menuItem)
        {
            bool state = !menuItem.IsChecked;
            GroupSort.ForEach(a => a.IsChecked = false);
            menuItem.IsChecked = state;
            return state;
        }

        #endregion

        #region RowSelection

        public UC_RowSelection RowSelection { get; private set; }

        private void InitRowSelection()
        {
            RowSelection = new UC_RowSelection();

            RowSelection.AllSelection.Click += AllSelection_Click;

            menuItems.Add(new Separator());

            CreateCheckBox();
            CheckBoxList.ForEach(a => RowSelection.MainStackPanel.Children.Add(a));
            Manager.CollectionViewSource.Filter += new FilterEventHandler(RowSelection_Filter);

            //menuItems.Add(grid);
            menuItems.Add(RowSelection);
        }

        private void AllSelection_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (!(bool)checkBox.IsChecked)   // The user unchecked
                CheckBoxList.ForEach(a => a.IsChecked = false);
            else  // The user checked
                CheckBoxList.ForEach(a => a.IsChecked = true);
        }

        private void UpdateRowSelection()
        {

        }

        private List<CheckBox> CheckBoxList;
        private bool DoRowSel_Filter { get; set; } = false;

        private void CreateCheckBox()
        {
            CheckBoxList = new List<CheckBox>();

            var dataList = new List<string>();
            foreach (var item in Manager.RowCollectionView)
            {
                try
                {
                    dataList.Add(item.GetType().GetProperty(DataBindProp).GetValue(item)?.ToString());
                }
                catch { }
            }
            var type = dataList.Distinct();
            foreach (var item in type)
            {
                if (item == null) continue;

                var menuItem = new CheckBox();
                menuItem.Content = item;
                menuItem.Click += MenuItem_Click_RowSelection;
                menuItem.IsChecked = true;
                CheckBoxList.Add(menuItem);
            }
        }

        private void MenuItem_Click_RowSelection(object sender, RoutedEventArgs e)
        {
            UpdateRowFilterState();
            ManageAllSelectionDynamic();
            UpdateColumn();
        }

        private void UpdateRowFilterState()
        {
            DoRowSel_Filter = CheckBoxList.Any(a => a.IsChecked == false);
        }

        private void ManageAllSelectionDynamic()
        {
            if (CheckBoxList.Any(c => c.IsChecked == false))
                RowSelection.AllSelection.IsChecked = false;
            if (CheckBoxList.All(c => c.IsChecked == true))
                RowSelection.AllSelection.IsChecked = true;
        }

        private void RowSelection_Filter(object sender, FilterEventArgs e)
        {
            if (!DoRowSel_Filter)
                return;

            Object obj = e.Item;
            object value = obj.GetType().GetProperty(DataBindProp)?.GetValue(obj);
            if (CheckBoxList.Any(a => a.Content.ToString() == value?.ToString() && !(bool)a.IsChecked))
                e.Accepted = false;
            //else
            //    e.Accepted = false;
            
        }

        #endregion

        #endregion

    }
}
