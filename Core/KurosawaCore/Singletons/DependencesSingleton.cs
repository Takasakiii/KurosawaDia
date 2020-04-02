using ConfigController.Models;
using System;

namespace KurosawaCore.Singletons
{
    public static class DependencesSingleton
    {
        public static ApiConfig[] ApiConfigs { set; private get; }

        public static ApiConfig GetApiWeeb()
        {
            return Array.Find(ApiConfigs, x => x.Nome.ToLower() == "weeb");
        }
    }
}
