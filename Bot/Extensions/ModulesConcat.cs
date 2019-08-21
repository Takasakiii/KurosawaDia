using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bot.Extensions
{
    public class ModulesConcat<T>
    {
        private struct Modulos
        {
            public int ClasseIndex { get; private set; }
            public MethodInfo Metodo { get; private set; }

            public Modulos(int classeIndex, MethodInfo metodo)
            {
                ClasseIndex = classeIndex;
                Metodo = metodo;
            }
        } 

        private Modulos[] MethodsModules;
        private Type[] Classes;
        private object[] Args;


        public ModulesConcat()
        {
            Classes = Assembly.GetAssembly(typeof(T)).GetTypes().Where(meutipo => meutipo.IsSubclassOf(typeof(T)) && meutipo.IsClass && !meutipo.IsAbstract).ToArray();


            List<Modulos> temp = new List<Modulos>();
            for(int i = 0; i < Classes.Length; i++)
            {
                MethodInfo[] metodos = Classes[i].GetMethods();
                for(int j = 0; j < metodos.Length; j++)
                {
                    temp.Add(new Modulos(i, metodos[j]));
                }

                MethodsModules = temp.ToArray();
            }
            
        } 

        public void AddArgs(params object[] args)
        {
            Args = args;
        }

        public void InvokeMethod(string metodo, params object[] argumentosMetodo)
        {
            Modulos temp = Array.Find(MethodsModules, x => x.Metodo.Name == metodo);
            object instanced = Activator.CreateInstance(Classes[temp.ClasseIndex], Args);
            temp.Metodo.Invoke(instanced, argumentosMetodo);
        } 
    }
}
