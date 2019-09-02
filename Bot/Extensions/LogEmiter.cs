using Bot.Singletons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    //Classe responsavel por criar custom logs nas interfaces graficas
    public class LogEmiter
    {
        
        //Struct Responsavel por gerenciar as Cores do Log
        public struct TipoLog
        {
            //Enumerate dos tipos de cores diferentes para cada tipo de Log
            public enum TipoCor
            {
                Info = 0,
                Erro = 1,
                Debug = 2,
                Generic = 3
            }

            //Atributo contem o tipo de log que essa cor guarda
            public TipoCor Tipo { private set; get; }

            //Atributo guarda a cor para interface grafica
            public Color CorNoDrawing { private set; get; }

            //Atributo guarda a cor para interface de texto
            public ConsoleColor CorNoConsole { private set; get; }


            //Contrutor da struct
            public TipoLog(TipoCor tipo, Color corNoDrawing, ConsoleColor corNoConsole)
            {
                Tipo = tipo;
                CorNoDrawing = corNoDrawing;
                CorNoConsole = corNoConsole;
            }

        }

        //Guarda o Metodo da interface responsavel pelo Log
        private static Action<TipoLog, string> MetodoLog;

        //Array constante contendo todas as variações de cores do Log
        private static readonly TipoLog[] CoresLog = {
            new TipoLog(TipoLog.TipoCor.Generic, Color.Black, ConsoleColor.White),
            new TipoLog(TipoLog.TipoCor.Info, Color.Blue, ConsoleColor.Blue),
            new TipoLog(TipoLog.TipoCor.Erro, Color.Red, ConsoleColor.Red),
            new TipoLog(TipoLog.TipoCor.Debug, Color.Magenta, ConsoleColor.Magenta)
        };

        //Metodo privado estatico reponsavel por Selecionar o TipoLog responsavel pela TipoCor
        private static TipoLog PegarCor (TipoLog.TipoCor Cor)
        {
            return Array.Find(CoresLog, x => x.Tipo == Cor);
        } 

        //Metodo responsavel por atribuir o MetodosLog
        public static void SetMetodoLog(Action<TipoLog, string> metodoLog)
        {
            MetodoLog = metodoLog;
        }

        //Metodo Async que envia a interface a excessão a partir de uma Exeption
        public static Task EnviarLogAsync(Exception e)
        {
            return new LogEmiter().LogAsync(PegarCor(TipoLog.TipoCor.Erro), e.ToString()); 
        }

        //Metodo Async que envia a interface a excessão a partir de uma string
        public static Task EnviarLogAsync(TipoLog.TipoCor tipoException, string e)
        {
             return new LogEmiter().LogAsync(PegarCor(tipoException), e);
        }

        //Metodo interno Async que gerencia as excessões
        private async Task LogAsync(TipoLog tipoLog, string exception)
        {
            if(MetodoLog != null)
            {
                await Task.Run(() =>
                {
                    MetodoLog.Invoke(tipoLog, $"[{tipoLog.Tipo.ToString()}] {exception}");
                });
            }
        }
    }
}
