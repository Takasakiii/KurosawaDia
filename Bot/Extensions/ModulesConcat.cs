using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bot.Extensions
{
    public class ModulesConcat<T>
    {


        private List<Tuple<MethodInfo, Type>> MethodsModules;
        private object[] Args;


        public ModulesConcat()
        {
            Type[] tiposClasses = Assembly.GetAssembly(typeof(T)).GetTypes().Where(meutipo => meutipo.IsClass && !meutipo.IsAbstract && meutipo.IsSubclassOf(typeof(T))).ToArray();


            MethodsModules = new List<Tuple<MethodInfo, Type>>();
            for(int i = 0; i < tiposClasses.Length; i++)
            {
                MethodInfo[] metodos = tiposClasses[i].GetMethods();
                for(int j = 0; j < metodos.Length; j++)
                {
                    MethodsModules.Add(Tuple.Create(metodos[j], tiposClasses[i]));
                }
            }
            
        } 

        public void AddArgs(params object[] args)
        {
            Args = args;
        }

        public void InvokeMethod(string metodo, params object[] argumentosMetodo)
        {
            int index = MethodsModules.FindIndex(x => x.Item1.Name == metodo);
            object instanced = Activator.CreateInstance(MethodsModules[index].Item2, Args);
            MethodsModules[index].Item1.Invoke(instanced, argumentosMetodo);
        } 
    }
}
