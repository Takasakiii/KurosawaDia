using ConfigController.DAOs;
using ConfigController.EntityConfiguration;
using ConfigController.Models;
using KurosawaCore;
using KurosawaCore.Modelos;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace WpfNetCore.Views
{
    /// <summary>
    /// Interaction logic for Cantar.xaml
    /// </summary>
    public partial class Cantar : Window
    {
        private Kurosawa kurosawa;
        private BaseConfig Config;
        private ApiConfig[] ApiConfig;
        private DBConfig DbConfig;
        private StatusConfig[] StatusConfig;

        private bool AutoScroll = true;

        public Cantar()
        {
            InitializeComponent();
            Loaded += Cantar_Loaded;
        }

        private async void Cantar_Loaded(object sender, RoutedEventArgs e)
        {
            if (!await new KurosawaConfigContext().Database.EnsureCreatedAsync())
            {
                Config = await new BaseConfigDAO().Ler();

                DbConfig = await new DBConfigDAO().Ler();

                ApiConfig = await new ApiConfigDAO().Ler();

                StatusConfig = await new StatusConfigDAO().Ler();
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = (Brush)new BrushConverter().ConvertFrom("#FFAFFD84");
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = (Brush)new BrushConverter().ConvertFrom("#FFCAFFAD");
        }

        private async void Iniciar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AdicionarLogo();
            kurosawa = new Kurosawa(Config, ApiConfig, DbConfig, StatusConfig);
            kurosawa.OnLog += Kurosawa_OnLog;
            await kurosawa.Iniciar();
            
        }

        private void AdicionarLogo()
        {
            string[] logo =
            {
                @"   __ __                                                  ___    _              __   ____",
                @"  / //_/ __ __  ____ ___   ___ ___ _ _    __ ___ _       / _ \  (_) ___ _      / /  |_  /",
                @" / ,<   / // / / __// _ \ (_-</ _ `/| |/|/ // _ `/      / // / / / / _ `/     < <  _/_ < ",
                @"/_ /|_| \_,_/ /_/   \___//___/\_,_/ |__,__/ \_,_/      /____/ /_/  \_,_/       \_\/____/ ",
                ""
            };

            foreach (string linha in logo)
            {
                Paragraph paragraph = new Paragraph
                {
                    Foreground = new SolidColorBrush(Colors.Purple)
                };
                paragraph.Inlines.Add(linha);
                ConsoleLog.Blocks.Add(paragraph);
            }
        }

        private async void Kurosawa_OnLog(LogMessage e)
        {
            await Dispatcher.InvokeAsync(new Action(() =>
            {
                Paragraph paragraph = new Paragraph();

                switch (e.Level)
                {
                    case KurosawaCore.Abstracoes.NivelLog.Critical:
                        paragraph.Foreground = new SolidColorBrush(Colors.Red);
                        paragraph.Background = new SolidColorBrush(Colors.Black);
                        break;
                    case KurosawaCore.Abstracoes.NivelLog.Error:
                        paragraph.Foreground = new SolidColorBrush(Colors.Red);
                        break;
                    case KurosawaCore.Abstracoes.NivelLog.Warning:
                        paragraph.Foreground = new SolidColorBrush(Colors.Orange);
                        break;
                    case KurosawaCore.Abstracoes.NivelLog.Info:
                        paragraph.Foreground = new SolidColorBrush(Colors.Black);
                        break;
                    case KurosawaCore.Abstracoes.NivelLog.Debug:
                        paragraph.Foreground = new SolidColorBrush(Colors.LightSeaGreen);
                        break;
                    default:
                        break;
                }

                paragraph.Inlines.Add(e.ToString());
                ConsoleLog.Blocks.Add(paragraph);
            }));
        }

        private async void Desligar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            await kurosawa.DisposeAsync();
        }

        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange == 0)
            {
                if (Scroll.VerticalOffset == Scroll.ScrollableHeight)
                {
                    AutoScroll = true;
                }
                else
                {
                    AutoScroll = false;
                }
            }

            if (AutoScroll && e.ExtentHeightChange != 0)
            {
                Scroll.ScrollToVerticalOffset(Scroll.ExtentHeight);
            }
        }
    }
}
