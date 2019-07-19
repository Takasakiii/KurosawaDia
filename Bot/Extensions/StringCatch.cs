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


        public static string GetString(string identificador, string respostaPadrao, params object[] addon)
        {
            Linguagens linguagens = new Linguagens(idiomaSelecionado, identificador);
            LinguagensDAO dao = new LinguagensDAO();
            var result = dao.GetString(linguagens);

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
