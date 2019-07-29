using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.DataBase.MainDB.Modelos
{
    public class Fuck
    {
        public uint cod { get; private set; }
        public string img { get; private set; }
        public bool explicitImg { get; private set; }
        public Usuarios usuario { get; private set; }

        public void SetImg(bool explicitImg, string img = "", Usuarios usuario = null, uint cod = 0)
        {
            this.img = img;
            this.explicitImg = explicitImg;
            this.usuario = usuario;
            this.cod = cod;
        }
    }
}
