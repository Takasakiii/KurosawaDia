using ConfigController.Models;
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
        private BaseConfig Config { get; set; }

        public Inicio()
        {
            InitializeComponent();
            Initialized += Inicio_Initialized;
        }

        private async void Inicio_Initialized(object sender, EventArgs e)
        {

        }
    }
}
