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
        private StatusConfig[] StatusConfig;

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

                await StatusConfigLer();
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

        private async Task StatusConfigLer()
        {
            StatusConfig = await new StatusConfigDAO().Ler();
            if (StatusConfig != null)
            {
                Status.ItemsSource = StatusConfig;
                StatusTipo.ItemsSource = new string[] { "Jogando (0)", "Live (1)", "Ouvindo (2)", "Assistindo (3)" };
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

        private void ApiDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ApiConfig apiConfig = (ApiConfig)((DataGridRow)sender).Item;
            ApiCod.Text = apiConfig.Cod.ToString();
            ApiName.Text = apiConfig.Nome;
            ApiKey.Text = apiConfig.Key;
        }

        private void LimparApi_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ApiCod.Text = "0";
            ApiName.Text = "";
            ApiKey.Text = "";
        }

        private async void DeletarApi_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ApiCod.Text != "0")
            {
                if (uint.TryParse(ApiCod.Text, out uint cod))
                {
                    await new ApiConfigDAO().Deletar(new ApiConfig 
                    { 
                        Cod = cod
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

        private void StatusDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StatusConfig statusConfigs = (StatusConfig)((DataGridRow)sender).Item;
            StatusCod.Text = statusConfigs.Cod.ToString();
            StatusName.Text = statusConfigs.StatusJogo;
            StatusUrl.Text = statusConfigs.StatusUrl;
            StatusTipo.SelectedIndex = (int)statusConfigs.TipoDeStatus;
        }

        private void LimparStatus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StatusCod.Text = "0";
            StatusName.Text = "";
            StatusUrl.Text = "";
            StatusTipo.SelectedIndex = -1;
        }

        private async void DeletarStatus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (StatusCod.Text != "0")
            {
                if (uint.TryParse(StatusCod.Text, out uint cod))
                {
                    await new StatusConfigDAO().Deletar(new StatusConfig
                    {
                        Cod = cod
                    });
                    await StatusConfigLer();
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

        private async void SalvarStatus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (StatusCod.Text == "0" && StatusName.Text != "" && StatusTipo.SelectedIndex != -1)
            {
                if (StatusTipo.SelectedIndex == 1 && StatusUrl.Text == "")
                {
                    MessageBox.Show("Acho que esqueceu de me dizer alguma coisa.", "Kurosawa Dia - Alerta", MessageBoxButton.OK);
                    return;
                }

                if (uint.TryParse(StatusCod.Text, out uint cod))
                {
                    await new StatusConfigDAO().Adicionar(new StatusConfig[] { new StatusConfig
                    {
                        Cod = cod,
                        StatusJogo = StatusName.Text,
                        StatusUrl = StatusUrl.Text,
                        TipoDeStatus = (TipoDeStatus)StatusTipo.SelectedIndex
                    }});
                    await StatusConfigLer();
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
