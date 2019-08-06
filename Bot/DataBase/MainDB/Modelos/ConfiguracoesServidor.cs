using static ConfigurationControler.Modelos.Linguagens;

namespace Bot.DataBase.MainDB.Modelos
{
    public class ConfiguracoesServidor
    {

        public struct BemVindoGoodByeMsg
        {
            public string bemvindoMsg { private set; get; }
            public string sairMsg { private set; get; }

            public void setBemvindo(string bemvindoMsg)
            {
                this.bemvindoMsg = bemvindoMsg;
            }

            public void setSair(string sairMsg)
            {
                this.sairMsg = sairMsg;
            }
        }

        public struct Idioma
        {
            public Idiomas idioma { private set; get; }

            public Idioma(Idiomas idioma)
            {
                this.idioma = idioma;
            }
        }


        public struct PI
        {
            public bool PIConf { private set; get; }
            public double PIRate { private set; get; }
            public string MsgPIUp { private set; get; }

            public PI(bool PIConf, double PIRate = 2, string MsgPIUp = "")
            {
                this.PIConf = PIConf;
                this.PIRate = PIRate;
                this.MsgPIUp = MsgPIUp;
            }

        }

        public struct ErroMsg
        {
            public bool erroMsg { private set; get; }

            public ErroMsg(bool erroMsg)
            {
                this.erroMsg = erroMsg;
            }
        }

        public struct DiaApi
        {
            public bool diaApi { private set; get; }

            public DiaApi(bool diaApi)
            {
                this.diaApi = diaApi;
            }
        }

        public ulong cod { private set; get; }
        public Servidores servidor { private set; get; }
        public BemVindoGoodByeMsg bemvindo { private set; get; }
        public Idioma idioma { private set; get; }
        public PI pI { private set; get; }
        public DiaApi diaApi { private set; get; }
        public ErroMsg erroMsg { private set; get; }

        public ConfiguracoesServidor(Servidores servidor, BemVindoGoodByeMsg bemvindo, ulong cod = 0)
        {
            this.servidor = servidor;
            this.bemvindo = bemvindo;
            this.cod = cod;
        }

        public ConfiguracoesServidor(Servidores servidor, Idioma idioma, ulong cod = 0)
        {
            this.servidor = servidor;
            this.idioma = idioma;
            this.cod = cod;
        }

        public ConfiguracoesServidor(Servidores servidor, PI pI, ulong cod = 0)
        {
            this.servidor = servidor;
            this.pI = pI;
            this.cod = cod;
        }

        public ConfiguracoesServidor(Servidores servidor, DiaApi diaApi, ulong cod = 0)
        {
            this.servidor = servidor;
            this.diaApi = diaApi;
            this.cod = cod;
        }

        public ConfiguracoesServidor(Servidores servidor, ErroMsg erroMsg, ulong cod = 0)
        {
            this.servidor = servidor;
            this.erroMsg = erroMsg;
            this.cod = cod;
        }

    }
}
