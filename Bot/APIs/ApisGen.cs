using Bot.DAO;
using Bot.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.APIs
{
    public class ApisGen
    {
        public ApiConfig apiConfig { get; private set; }

        public ApisGen()
        {
            apiConfig = new ApiConfig(1); // not instance in modelo
            ApiConfigDAO dao = new ApiConfigDAO(); // dnv
            apiConfig = dao.Carregar(apiConfig); // n pode ter metodos de processamento em um modelo
        }
    }
}
