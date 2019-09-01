using Bot.Singletons;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    //struturas
    public class BotCadastro
    {
        private struct CadastrosSessions
        {
            public Task processo { private set; get; }
            public CommandContext contextoReferencia { private set; get; }

            public CadastrosSessions(Task processo, CommandContext contextoReferencia)
            {
                this.processo = processo;
                this.contextoReferencia = contextoReferencia;
            }
        }

        
        //static Class

        private static List<CadastrosSessions> sessoes = new List<CadastrosSessions>();
        
        public static void AdicionarCadastro(CommandContext contexto)
        {
            if (!contexto.IsPrivate)
            {
                Task Sessao = new Task(() =>
                {
                    new Servidores_UsuariosDAO().inserirServidorUsuario(new Servidores_Usuarios(new Servidores(contexto.Guild.Id, contexto.Guild.Name), new Usuarios(contexto.User.Id, contexto.User.ToString())));
                    new BotCadastro(null, null).GarbageColectorSessao();
                });

                Sessao.Start();
                sessoes.Add(new CadastrosSessions(Sessao, contexto));
            }
        }


        
        //object Class

        private Action processoFinalizar;
        private CommandContext contextoObj;


        private void GarbageColectorSessao()
        {
            new Thread(() =>
            {
                for (int i = 0; i < sessoes.Count; i++)
                {
                    if (sessoes[i].processo.IsCompleted)
                    {
                        sessoes.RemoveAt(i);
                    }
                }
            }).Start();
        }
        public BotCadastro(Action readyFunction, CommandContext contexto)
        {
            processoFinalizar = readyFunction;
            contextoObj = contexto;
        }

        public void EsperarOkDb()
        {
            try
            {
                int sessaoIndex = sessoes.FindIndex(x => x.contextoReferencia == contextoObj);
                if (sessaoIndex >= 0)
                {
                    Task threadCadastrando = sessoes[sessaoIndex].processo;
                    threadCadastrando.Wait(); 
                }
            }
            catch (IndexOutOfRangeException)
            {
                processoFinalizar.Invoke();
            }
            catch(Exception e)
            {
                MethodInfo metodo = SingletonLogs.tipo.GetMethod("Log");
                object[] parms = new object[1];
                parms[0] = e.ToString();
                metodo.Invoke(SingletonLogs.instanced, parms);
            }
            finally
            {
                processoFinalizar.Invoke();
            }
        }        
    }
}
