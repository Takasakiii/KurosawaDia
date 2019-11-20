using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using System;
using System.Threading.Tasks;
using static ConfigurationControler.Modelos.Linguagens;

namespace Bot.Extensions
{
    public static class StringCatch
    {
        public static Idiomas idiomaSelecionado { private set; get; }

        static StringCatch()
        {
            idiomaSelecionado = Idiomas.Portugues;
        }

        public static void SetIdioma(Idiomas idioma)
        {
            idiomaSelecionado = idioma;
        }


        public static async Task<string> GetStringAsync(string identificador, string respostaPadrao, params object[] addon)
        {
            Linguagens linguagens = new Linguagens(idiomaSelecionado, identificador);
            LinguagensDAO dao = new LinguagensDAO();
            var result = await dao.GetStringAsync(linguagens);

            string rest = "";
            if (result.Item1)
            {
                rest = result.Item2.texto;
            }
            else
            {
                rest = respostaPadrao;
            }

            if (addon.Length > 0)
            {
                rest = String.Format(rest, addon);
            }

            return rest;
        }
    }
}
