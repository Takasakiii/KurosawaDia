using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bot.Extensions
{
    //Classe responsavel por Obter e gerir os Modulos do bot ou outras classes concatenadas
    //  T - representa a classe mão que deriva todos as outras classes filhas / modulos
    public class ModulesConcat<T>
    {
        //Struct responsavel por organizar os comandos (metodos das classes e sua respectiva classe)
        private struct Modulos
        {
            //Index da Classe responsavel por esse metodo
            public int ClasseIndex { get; private set; }

            //Metodo em forma generica para fins de comparar e usar como Invocador 
            public MethodInfo Metodo { get; private set; }

            //Construtor da struct Modulos
            public Modulos(int classeIndex, MethodInfo metodo)
            {
                ClasseIndex = classeIndex;
                Metodo = metodo;
            }
        } 

        //Array responsavel por armazenar todos os metodos das classes filhas / comandos
        private Modulos[] MethodsModules;

        //Array responsavel por armazenar o modelo das classes filhas / modulos
        private Type[] Classes;

        //Array responsavel por armazenar os parametros necessarios para instanciar as classes filhas / modulos
        private object[] Args;

        //Contrutor da Classe ModulesConcat e responsavel por obter todos os modulos / classes filhas da classe principal e armazenar em seus devidos arrays
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

        //Metodo responsavel pela passagem dos parametros de instancia das classes filhas / metodos
        public void AddArgs(params object[] args)
        {
            Args = args;
        }

        //Metodo responsavel por instanciar uma classe filha / modulo e chamar o metodo solicitado
        public void InvokeMethod(string metodo, params object[] argumentosMetodo)
        {
            Modulos temp = Array.Find(MethodsModules, x => x.Metodo.Name == metodo);
            object instanced = Activator.CreateInstance(Classes[temp.ClasseIndex], Args);
            temp.Metodo.Invoke(instanced, argumentosMetodo);
        } 
    }
}
