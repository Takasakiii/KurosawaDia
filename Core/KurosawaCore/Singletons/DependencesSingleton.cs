using ConfigController.Models;
using System;

namespace KurosawaCore.Singletons
{
    internal static class DependencesSingleton
    {
        internal static ApiConfig[] ApiConfigs { set; private get; }


        internal static ApiConfig GetApiWeeb()
        {
            return Array.Find(ApiConfigs, x => x.Nome.ToLower() == "weeb");
        }
    }
}
