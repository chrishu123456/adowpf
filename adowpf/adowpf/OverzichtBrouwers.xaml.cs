﻿using AdoGemeenschap;
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
using System.Windows.Shapes;

namespace adowpf
{
    /// <summary>
    /// Interaction logic for OverzichtBrouwers.xaml
    /// </summary>
    public partial class OverzichtBrouwers : Window
    {
        public List<Brouwer> brouwersOb = new List<Brouwer>();

        private CollectionViewSource brouwerViewSource;

        public OverzichtBrouwers()
        {
            InitializeComponent();
        }

        private void VulDeGrid()
        {

            //System.Windows.Data.CollectionViewSource brouwerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("brouwerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // brouwerViewSource.Source = [generic data source]

            var manager = new BrouwerManager();
            brouwersOb = manager.BrouwersMetBeginLetters(TextBoxZoeken.Text);
            

            brouwerViewSource.Source = brouwersOb;
            //goUpdate();
            LabelTotalRowCount.Content = brouwerDataGrid.Items.Count;


        }

        private void goUpdate()
        {
            goToPreviousButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);

            goToFirstButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);

            goToNextButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1);

            goToLastButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1);

            if (brouwerDataGrid.Items.Count != 0)
            {
                if (brouwerDataGrid.SelectedItem != null)
                {
                    brouwerDataGrid.ScrollIntoView(brouwerDataGrid.SelectedItem);
                }
            }

            TextBoxGo.Text = (brouwerViewSource.View.CurrentPosition + 1).ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            VulDeGrid();
            TextBoxZoeken.Focus();


        }


        private void ButtonZoeken_Click(object sender, RoutedEventArgs e)
        {
            VulDeGrid();
        }

        private void TextBoxZoeken_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VulDeGrid();
            }
        }

        private void goToFirstButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToFirst();
            goUpdate();
        }

        private void goToPreviousButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToPrevious();
            goUpdate();

        }

        private void goToNextButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToNext();
            goUpdate();
        }

        private void goToLastButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToLast();
            goUpdate();

        }

        private void Buttongo_Click(object sender, RoutedEventArgs e)
        {
            int position;
            int.TryParse(TextBoxGo.Text, out position);
            if (position >0 && position <=brouwerDataGrid.Items.Count)
            {
                brouwerViewSource.View.MoveCurrentToPosition(position - 1);

            }
            else
            {
                MessageBox.Show("The input index is not valid.");
            }
            goUpdate();
        }

        private void brouwerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            goUpdate();
        }
    }
}