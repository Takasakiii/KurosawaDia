using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
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


        public static string GetString(string identificador, string respostaPadrao)
        {
            Linguagens linguagens = new Linguagens(idiomaSelecionado, identificador);
            LinguagensDAO dao = new LinguagensDAO();
            var result = dao.GetString(linguagens);
            if (result.Item1)
            {
                return result.Item2.texto;
            }
            else
            {
                return respostaPadrao;
            }
        }
    }
}
