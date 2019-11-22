using Bot.Extensions;
using Bot.GenericTypes;
using Discord.Commands;
using MainDatabaseControler.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Comandos
{
    /// <summary>
    /// Modulo Para exemplificar um novo tipo de abordagem para a vadia ver
    /// </summary>
    public class ExempleModuleUP1 : GenericModule
    {
        /// <summary>
        /// Adiciona uma opc de Autor no modulo
        /// </summary>
        private string Autor;
        private bool Permissoes = false;

        //Contrutor padrao + extras
        public ExempleModuleUP1 (CommandContext contexto, params object[] args): base(contexto, args)
        {
            //vc pode pegar propriedades basicas diretamente do modulo e usalo como quizer no msm
            Autor = contexto.User.ToString();

            //Adiciona verificador de permissao para owner
            VerificarOwner().Wait();
            
        }

        private async Task VerificarOwner()
        {
            if ((await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                Permissoes = true;
            }
        }



        public async Task exemple()
        {
            if (Permissoes)
            {
                await Contexto.Channel.SendMessageAsync("msg1: ");
            }
            else
            {
                //isso faz o Handler emitir um comando n encontrado
                throw new NullReferenceException();
            }
        }

        public async Task exemple2()
        {
            if (Permissoes)
            {
                await Contexto.Channel.SendMessageAsync("msg2: ");
            }
            else
            {
                throw new NullReferenceException();
            }
        }


        public async Task exempleErro()
        {
            if (Permissoes)
            {
                await Contexto.Channel.SendMessageAsync("aaa");
            }
            else
            {
                await Erro.EnviarErroAsync("gordo viado", new ErrorExtension.DadosErro("viado", "gay"));                
            }
        }

    }
}
