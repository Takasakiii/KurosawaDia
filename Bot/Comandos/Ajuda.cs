using System.Reflection;
using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Singletons;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using Bot.Modelos;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;
using static MainDatabaseControler.Modelos.Servidores;

namespace Bot.Comandos
{
    public class Ajuda : GenericModule
    {


        private PermissoesServidores Permissao = PermissoesServidores.Normal;
        public Ajuda(CommandContext contexto, params object[] args) : base(contexto, args)
        {
            VerificarPermissao().Wait();
        }

        public static InfoModule Info(){
            return new InfoModule{
                Nome = "Ajuda",
                Icon = "❓"
            };
        }

        private async Task VerificarPermissao()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            Tuple<bool, Servidores> servidor = await new ServidoresDAO().GetPermissoesAsync(new Servidores(id));
            if (servidor.Item1)
            {
                Permissao = servidor.Item2.Permissoes;
            }
        }

        public async Task convite()
        {
            await info();
        }

        public async Task help()
        {
            await ajuda();
        }

        public async Task newhelp()
        {
            EmbedBuilder embed = new EmbedBuilder();

            embed.WithTitle("Meus comandos vão te surpeender tenho certeza disso 😝");
            embed.WithDescription($"Para ver os comandos de cada módulo é so usar: `{PrefixoServidor}comandos módulo`, exemplo: `{PrefixoServidor}comandos utilidade`");
            embed.WithColor(Color.DarkPurple);
            embed.WithImageUrl("https://i.imgur.com/mQVFSrP.gif");

            string modulos = "";

            for(int i = 0; i < ModuleContexto.Classes.Length; i++)
            {
                try{
                    InfoModule info = (InfoModule) ModuleContexto.Classes[i].GetMethod("Info").Invoke(null, null);
                    modulos += $"{((info.Icon != null) ? info.Icon : null)}:{(EmojisNumberList) i }: {((info.Nome == null)? ModuleContexto.Classes[i].Name : info.Nome)} \n";
                } catch{
                    modulos += $":{(EmojisNumberList) i }: {ModuleContexto.Classes[i].Name} \n";
                }
            }

            embed.AddField("Modulos:", modulos);

            await Contexto.Channel.SendMessageAsync(embed: embed.Build());
        }
        

        public async Task ajuda()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithColor(Color.DarkPurple)
                    .WithTitle("Será um enorme prazer te ajudar 😋")
                    .WithDescription(string.Format("Eu me chamo Kurosawa Dia, sou presidente do conselho de classe, idol e também ajudo as pessoas com algumas coisinhas no Discord 😉\n"
                    + "Se você usar `{0}comandos` no chat vai aparecer tudo que eu posso fazer atualmente (isso não é demais 😁)\n"
                    + "Sério estou muito ansiosa para passar um tempo com você e também te ajudar XD\n"
                    + "Se você tem ideias de mais coisas que eu possa fazer por favor mande uma sugestão com o `{0}sugestao`\n\n"
                    + "Se você quer saber mais sobre mim, me convidar para seu servidor, ou até entrar em meu servidor de suporte use o comando `{0}info`\n\n"
                    + "E como a Mari fala Let's Go!!", PrefixoServidor))
                    .WithFooter("Kurosawa Dia é um projeto feito com amor e carinho pelos seus desenvolvedores!", "https://i.imgur.com/Cm8grM4.png")
                    .WithImageUrl("https://i.imgur.com/PC5QDiX.png")
                .Build());
        }

        enum ListaModulos { nada, ajuda, utilidade, moderacao, nsfw, weeb, imgs, cr, config, especial };
        public async Task comandos()
        {
            string[] comando = Comando;
            string msg = string.Join(" ", comando, 1, (comando.Length - 1)).ToLowerInvariant();

            if (!string.IsNullOrEmpty(msg))
            {
                ListaModulos modulo = ListaModulos.nada;
                if (int.TryParse(msg, out int result))
                {
                    modulo = (ListaModulos)result;
                }

                if (modulo == ListaModulos.ajuda || msg == "ajuda")
                {
                    await helpajuda();
                }
                else if (modulo == ListaModulos.utilidade || msg == "utilidade")
                {
                    await utilidade();
                }
                else if (modulo == ListaModulos.moderacao || msg == "moderação" || msg == "moderacao")
                {
                    await moderacao();
                }
                else if (modulo == ListaModulos.nsfw || msg == "nsfw")
                {
                    await nsfw();
                }
                else if (modulo == ListaModulos.weeb || msg == "weeb")
                {
                    await weeb();
                }
                else if (modulo == ListaModulos.imgs || msg == "imagens" || msg == "imgs")
                {
                    await img();
                }
                else if (modulo == ListaModulos.cr || msg == "reações customizadas" || msg == "reacoes customizadas")
                {
                    await customReaction();
                }
                else if (modulo == ListaModulos.config || msg == "configurações" || msg == "configuracoes")
                {
                    await configuracoes();
                }
                else if (modulo == ListaModulos.especial || msg == "especiais")
                {
                    if (Permissao == PermissoesServidores.ServidorPika)
                    {
                        await especial();
                    }
                    else
                    {
                        await modulos();
                    }
                }
                else
                {
                    await modulos();
                }
            }
            else
            {
                await modulos();
            }
        }

        public async Task info()
        {
            DiscordShardedClient client = Contexto.Client as DiscordShardedClient;
            int users = 0;
            foreach (SocketGuild servidor in client.Guilds)
            {
                users += servidor.Users.Count;
            }



            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Dia's Book:")
                    .WithDescription("Espero que não faça nada estranho com minhas informações, to zuando kkkkkk 😝")
                    .AddField("**Sobre mim:**", "__Nome:__ Kurosawa Dia (Dia - Chan)\n__Aniversário:__ 1° de Janeiro (Quero presentes)\n__Ocupação:__ Estudante e Traficante/Idol nas horas vagas", false)
                    .AddField("**As pessoas que fazem tudo isso ser possivel:**", "Takasaki#7072\nYummi#1375\nLuckShiba#0001\n\nE é claro você que acredita em meu potencial🧡", false)
                    .AddField("**Quer me ajudar????**", $"[Me adicione em seu Servidor]({InfoImportante.conviteDia})\n[Entre no meu servidor para dar suporte ao projeto]({InfoImportante.conviteServer})\n[Vote em mim no DiscordBotList para que eu possa ajudar mais pessoas]({InfoImportante.topgg})")
                    .AddField("**Informações chatas:**",  $"__Ping:__ {client.Latency}ms\n__Servidores:__ {client.Guilds.Count}\n__Usuários:__ {users}\n__Versão:__ {InfoImportante.VersaoNumb}  ({InfoImportante.VersaoName})", false)
                    .WithThumbnailUrl("https://i.imgur.com/ppXRHTi.jpg")
                    .WithImageUrl("https://i.imgur.com/qGb6xtG.jpg")
                    .WithColor(Color.DarkPurple)
                .Build());

        }

        private async Task modulos()
        {
            string modulos = ":one: ❓ Ajuda;\n:two: 🛠 Utilidade;\n:three: ⚖ Moderação;\n:four: 🔞 NSFW;\n:five: ❤ Weeb;\n:six: 🖼 Imagens;\n:seven: 💬 Reações Customizadas;\n:eight: ⚙ Configurações";

            if (Permissao.Equals(PermissoesServidores.ServidorPika))
            {
                modulos += "\n:nine: 🌟 Especiais.";
            }
            else
            {
                modulos += ".";
            }

            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Comandos atacaaaaar 😁")
                    .WithDescription($"Para ver os comandos de cada módulo é so usar: `{PrefixoServidor}{Comando[0]} módulo`, exemplo: `{PrefixoServidor}{Comando[0]} utilidade`")
                    .AddField("Módulos:", modulos)
                    .WithImageUrl("https://i.imgur.com/mQVFSrP.gif")
                    .WithColor(Color.DarkPurple)
                .Build());
        }
        private async Task helpajuda()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Módulo Ajuda (❓)")
                    .WithDescription("Este módulo tem comandos para te ajudar na ultilização do bot. \n\nNão tenha medo eles não mordem 😉")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", string.Format("`{0}ajuda`, `{0}comandos`, `{0}info`", PrefixoServidor))
                    .WithImageUrl("https://i.imgur.com/XQTVJu9.jpg")
                .Build());
        }
        private async Task utilidade()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                     .WithTitle("Módulo Utilidade (🛠)")
                     .WithDescription("Este módulo possui coisas uteis pro seu dia a dia. \n\nAaaaaaa eles são tão legais ☺")
                     .WithColor(Color.DarkPurple)
                     .AddField("Comandos:", string.Format("`{0}videochamada`, `{0}avatar`, `{0}emoji`, `{0}say`, `{0}simg`, `{0}sugestao`, `{0}bug`, `{0}perfil`, `{0}whatsify`", PrefixoServidor))
                     .WithImageUrl("https://i.imgur.com/TK7zmb8.jpg")
                 .Build());
        }
        private async Task moderacao()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Módulo Moderação (⚖)")
                    .WithDescription("Este módulo possui coisas para te ajudar a moderar seu servidor. \n\nSó não seja malvado com seus amigos 😣")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", string.Format("`{0}kick`, `{0}ban`, `{0}softban`, `{0}limparchat`", PrefixoServidor))
                    .WithImageUrl("https://i.imgur.com/hiu0Vh0.jpg")
                .Build());

        }
        private async Task nsfw()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Módulo NSFW (🔞)")
                    .WithDescription("Este módulo possui coisas para você dar orgulho para sua família. \n\nTenho medo dessas coisas 😣")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", string.Format("`{0}hentai`, `{0}hentaibomb`, `{0}anal`", PrefixoServidor))
                    .WithImageUrl("https://i.imgur.com/iGQ3SI8.png")
                .Build());
        }
        private async Task weeb()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Modulo Weeb (❤)")
                    .WithDescription("Este módulo é o mais amoroso de todos.  \n\nUse ele para distribuir o amor para seus amigos ❤")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", string.Format("`{0}hug`, `{0}slap`, `{0}kiss`, `{0}punch`, `{0}lick`, `{0}cry`, `{0}megumin`, `{0}rem`, `{0}dance`, `{0}pat`, `{0}fuck`, `{0}owoify`, `{0}bigtext`", PrefixoServidor))
                    .WithImageUrl("https://i.imgur.com/FmCmErd.png")
                .Build());
        }
        private async Task img()
        {
            string cmds = string.Format("`{0}cat`, `{0}dog`, `{0}magik`", PrefixoServidor);
            if (Permissao == PermissoesServidores.LolisEdition || Permissao == PermissoesServidores.ServidorPika)
            {
                cmds = string.Format("`{0}cat`, `{0}dog`, `{0}magik`, `{0}loli`, `{0}lolibomb`", PrefixoServidor);
            }
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Módulo Imagem (🖼)")
                    .WithDescription("Este módulo possui imagens fofinhas para agraciar seu computador.  \n\nKawaiii ❤❤❤")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", cmds)
                    .WithImageUrl("https://i.imgur.com/cQqTUl1.png")
                .Build());

        }
        private async Task customReaction()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Módulo Reações Customizadas (💬)")
                    .WithDescription("Este módulo possui comandos para você controlar as minhas Reações Customizadas. \n\nEu adoro usar elas para me divertir com vocês 😂")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", string.Format("`{0}acr`, `{0}dcr`, `{0}lcr`", PrefixoServidor))
                    .WithImageUrl("https://i.imgur.com/AUpMkBP.jpg")
                .Build());

        }
        private async Task configuracoes()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Módulo Configurações (⚙)")
                    .WithDescription("Em configurações você define preferencias de como agirei em seu servidor. \n\nTenho certeza que podemos ficar mais íntimos assim 😄")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", string.Format("`{0}setprefix`, `{0}piconf`, `{0}welcomech`, `{0}byech`, `{0}picargo`, `{0}welcomemsg`, `{0}byemsg`, `{0}erromsg`", PrefixoServidor))
                    .WithImageUrl("https://i.imgur.com/vg0z9yT.jpg")
                .Build());
        }
        private async Task especial()
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Módulo Especiais (🌟)")
                    .WithDescription("Só falo uma coisa, isso é exclusivo, e você pode ter o prazer de acessar. Não é todo mundo que tem essa chance, então aproveite.")
                    .WithColor(Color.DarkPurple)
                    .AddField("Comandos:", string.Format("`{0}insult`, `{0}criarinsulto`, `{0}fuckadd`", PrefixoServidor))
                    .WithImageUrl("https://i.imgur.com/bQGUGbB.gif")
                .Build());
        }

        public async Task MessageEventExceptions(Exception e, Servidores servidor)
        {
            if (e is NullReferenceException)
            {
                bool erroMsg = true;
                if (!Contexto.IsPrivate)
                {
                    ConfiguracoesServidor configuracoes = new ConfiguracoesServidor(new Servidores(Contexto.Guild.Id), new ErroMsg());
                    Tuple<bool, ConfiguracoesServidor> res = await new ConfiguracoesServidorDAO().GetErrorMsgAsync(configuracoes);
                    configuracoes = res.Item2;
                    if (res.Item1)
                    {
                        erroMsg = configuracoes.erroMsg.erroMsg;
                    }
                }

                if (erroMsg)
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{Contexto.User}**, esse comando não foi encontrado. Use `{new string(servidor.Prefix)}comandos` para ver os meus comandos.")
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
            }
            else
            {
                await LogEmiter.EnviarLogAsync(e);
            }
        }

        public async Task MentionMessage(Servidores servidores)
        {
            await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithDescription($"Oii {Contexto.User.Username}, meu prefixo é `{new string(servidores.Prefix)}`. Se quiser ver os meus comandos é so usar `{new string(servidores.Prefix)}comandos`!")
                .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
