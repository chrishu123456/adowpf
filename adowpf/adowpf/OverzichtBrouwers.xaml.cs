using AdoGemeenschap;
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


             brouwerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("brouwerViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // brouwerViewSource.Source = [generic data source]

                   var manager = new BrouwerManager();
                   brouwersOb = manager.BrouwersMetBeginLetters(TextBoxZoeken.Text);


                    brouwerViewSource.Source = brouwersOb;
            goUpdate();
                  LabelTotalRowCount.Content = brouwerDataGrid.Items.Count-1;


        }


        private void goUpdate()
        {
                     goToPreviousButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);

                      goToFirstButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);

                    goToNextButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 2);

                  goToLastButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 2);

                if (brouwerDataGrid.Items.Count != 0)
                  {
                      if (brouwerDataGrid.SelectedItem != null)
                      {
                          brouwerDataGrid.ScrollIntoView(brouwerDataGrid.SelectedItem);
                    ListBoxBrouwers.ScrollIntoView(brouwerDataGrid.SelectedItem);
                      }
                  }

               TextBoxGo.Text = (brouwerViewSource.View.CurrentPosition + 1).ToString();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource brouwerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("brouwerViewSource")));

            VulDeGrid();

            //var manager = new BrouwerManager();

            

            //brouwersOb = manager.BrouwersMetBeginLetters(TextBoxZoeken.Text);

            

            //brouwerViewSource.Source = brouwersOb;

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
            if (position >0 && position <=brouwerDataGrid.Items.Count - 1)
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

        private void CheckBoxPostcode0_Click(object sender, RoutedEventArgs e)
        {
            Binding binding1 = BindingOperations.GetBinding(TextBoxPostCode, TextBox.TextProperty);
            binding1.ValidationRules.Clear();

            Binding binding2 = (postCodeColumn as DataGridBoundColumn).Binding as Binding;
            binding2.ValidationRules.Clear();

            brouwerDataGrid.RowValidationRules.Clear();

            switch (CheckBoxPostcode0.IsChecked)
            {
                case true:
                    binding1.ValidationRules.Add(new PostCodeRangeRule0());
                    binding2.ValidationRules.Add(new PostCodeRangeRule0());
                    brouwerDataGrid.RowValidationRules.Add(new PostCodeRangeRule0());
                    break;
                case false:
                    binding1.ValidationRules.Add(new PostCodeRangeRule());
                    binding2.ValidationRules.Add(new PostCodeRangeRule());
                    brouwerDataGrid.RowValidationRules.Add(new PostCodeRangeRule());
                    break;
                default:
                    binding1.ValidationRules.Add(new PostCodeRangeRule());
                    binding2.ValidationRules.Add(new PostCodeRangeRule());
                    brouwerDataGrid.RowValidationRules.Add(new PostCodeRangeRule());
                    break;
            }

            binding1.ValidationRules[0].ValidatesOnTargetUpdated = true;
            binding1.ValidationRules[0].ValidationStep = ValidationStep.UpdatedValue;

            binding2.ValidationRules[0].ValidatesOnTargetUpdated = true;
            binding2.ValidationRules[0].ValidationStep = ValidationStep.UpdatedValue;

            brouwerDataGrid.RowValidationRules[0].ValidatesOnTargetUpdated = true;
            brouwerDataGrid.RowValidationRules[0].ValidationStep = ValidationStep.UpdatedValue;
        }

        private void TestOpFouten_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            bool foutgevonden = false;
            foreach (var c in gridDetail.Children)
            {
                if (c is AdornerDecorator)
                {
                    if (Validation.GetHasError( ( (AdornerDecorator)c).Child))
                  {
                        foutgevonden = true;
                    }
                }
                else if (Validation.GetHasError((DependencyObject)(c) ))
                    {
                    foutgevonden = true;
                }
                
            }
            

            foreach (var c in brouwerDataGrid.ItemsSource)
            {
                var d = brouwerDataGrid.ItemContainerGenerator.ContainerFromItem(c);
                if (d is DataGridRow)
                {
                    if (Validation.GetHasError(d))
                    {
                        foutgevonden = true;
                    }
                }
               

            }
            if (foutgevonden)
            { e.Handled = true; }
        }
    }
}
