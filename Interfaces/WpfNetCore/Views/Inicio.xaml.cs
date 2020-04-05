using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfNetCore.Views
{
    /// <summary>
    /// Interaction logic for Inicio.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Video_MediaEnded(object sender, RoutedEventArgs e)
        {
            Video.Position = new TimeSpan(0);
        }

        #region Treinar

        private void ButtonTreinar_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonTreinar.Background = (Brush)new BrushConverter().ConvertFrom("#FFF774F7");
        }

        private void ButtonTreinar_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonTreinar.Background = (Brush)new BrushConverter().ConvertFrom("#99F774F7");
        }

        private void ButtonTreinar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Treinar treinar = new Treinar();
            Hide();
            treinar.ShowDialog();
            Show();
        }

        private void Treinar_Closed(object sender, EventArgs e)
        {
            Show();
        }

        #endregion

        #region Cantar

        private void ButtonCantar_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonCantar.Background = (Brush)new BrushConverter().ConvertFrom("#FFF774F7");
        }

        private void ButtonCantar_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonCantar.Background = (Brush)new BrushConverter().ConvertFrom("#99F774F7");
        }

        private void ButtonCantar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion
    }
}
