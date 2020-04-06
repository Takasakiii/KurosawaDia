using ConfigController.DAOs;
using ConfigController.EntityConfiguration;
using ConfigController.Models;
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

namespace WpfNetCore.Views
{
    /// <summary>
    /// Interaction logic for Treinar.xaml
    /// </summary>
    public partial class Treinar : Window
    {
        private BaseConfig Config;
        private ApiConfig[] ApiConfig;
        private DBConfig DbConfig;

        public Treinar()
        {
            InitializeComponent();
            Loaded += Treinar_Loaded;
        }

        private async void Treinar_Loaded(object sender, RoutedEventArgs e)
        {
            if (!await new KurosawaConfigContext().Database.EnsureCreatedAsync())
            {
                await BotConfigLer();

                await DbConfigLer();

                await ApisConfigLer();
            }
        }

        private async Task BotConfigLer()
        {
            Config = await new BaseConfigDAO().Ler();
            if (Config != null)
            {
                Token.Text = Config.Token;
                Prefixo.Text = Config.Prefixo;
                IdDono.Text = Config.IdDono.ToString();
            }
        }

        private async Task DbConfigLer()
        {
            DbConfig = await new DBConfigDAO().Ler();
            if (DbConfig != null)
            {
                Ip.Text = DbConfig.IP;
                Porta.Text = DbConfig.Porta.ToString();
                Database.Text = DbConfig.Database;
                User.Text = DbConfig.User;
                Senha.Password = DbConfig.Senha;
            }
        }

        private async Task ApisConfigLer()
        {
            ApiConfig = await new ApiConfigDAO().Ler();
            if (ApiConfig != null)
            {
                Apis.ItemsSource = ApiConfig;
            }
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = (Brush)new BrushConverter().ConvertFrom("#FFF774F7");
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).Background = (Brush)new BrushConverter().ConvertFrom("#FFF99B9B");
        }

        private async void SalvarBot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Token.Text != "" && Prefixo.Text != "" && IdDono.Text != "")
            {
                BaseConfig botConfig = new BaseConfig
                {
                    Token = Token.Text,
                    Prefixo = Prefixo.Text
                };

                if (ulong.TryParse(IdDono.Text, out ulong id))
                {
                    botConfig.IdDono = id;
                    await new BaseConfigDAO().Adicionar(botConfig);
                    await BotConfigLer();
                }
                else
                {
                    MessageBox.Show("Esse é mesmo um numero ?", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Acho que esqueceu de me dizer alguma coisa.", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
            }
        }

        private async void SalvarDatabase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Ip.Text != "" && Porta.Text != "" && Database.Text != "" && User.Text != "" && Senha.Password != "")
            {
                DBConfig dBConfig = new DBConfig
                {
                    IP = Ip.Text,
                    Database = Database.Text,
                    User = User.Text,
                    Senha = Senha.Password
                };

                if (uint.TryParse(Porta.Text, out uint porta))
                {
                    dBConfig.Porta = porta;
                    await new DBConfigDAO().Adicionar(dBConfig);
                    await DbConfigLer();
                }
                else
                {
                    MessageBox.Show("Esse é mesmo um numero ?", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Acho que esqueceu de me dizer alguma coisa.", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
            }
        }

        private void DataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ApiConfig apiConfig = (ApiConfig)((DataGridRow)sender).Item;
            ApiCod.Text = apiConfig.Cod.ToString();
            ApiName.Text = apiConfig.Nome;
            ApiKey.Text = apiConfig.Key;
        }

        private void Limpar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ApiCod.Text = "0";
            ApiName.Text = "";
            ApiKey.Text = "";
        }

        private async void Deletar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ApiCod.Text != "" && ApiCod.Text != "0" && ApiName.Text != "" && ApiKey.Text != "")
            {
                if (uint.TryParse(ApiCod.Text, out uint cod))
                {
                    await new ApiConfigDAO().Deletar(new ApiConfig 
                    { 
                        Cod = cod, 
                        Nome = ApiName.Text, 
                        Key = ApiKey.Text
                    });
                    await ApisConfigLer();
                }
                else
                {
                    MessageBox.Show("Esse é mesmo um numero ?", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Acho que esqueceu de me dizer alguma coisa.", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
            }
        }

        private async void SalvarApi_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ApiCod.Text == "0"  && ApiName.Text != "" && ApiKey.Text != "")
            {
                if (uint.TryParse(ApiCod.Text, out uint cod))
                {
                    await new ApiConfigDAO().Adicionar(new ApiConfig[] { new ApiConfig
                    {
                        Cod = cod,
                        Nome = ApiName.Text,
                        Key = ApiKey.Text
                    }});
                    await ApisConfigLer();
                }
                else
                {
                    MessageBox.Show("Esse é mesmo um numero ?", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Acho que esqueceu de me dizer alguma coisa.", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
            }
        }
    }
}
