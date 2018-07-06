using System;
using System.Collections.Generic;

namespace MusicaDAL.Models
{
    [BlueORM.Table("musica", "codMusica")]
    [Serializable]
    public class Musica
    {
        private int codMusica;
        [BlueORM.Column("codMusica", true)]
        public int CODMUSICA
        {
            get { return codMusica; }
            set { codMusica = value; }
        }

        private string nome;
        [BlueORM.Column("nome", false)]
        public string NOME
        {
            get { return nome; }
            set { nome = value; }
        }

        private string letra;
        [BlueORM.Column("letra", false)]
        public string LETRA
        {
            get { return letra; }
            set { letra = value; }
        }

        private string generosTexto;
        [BlueORM.Column("generosTexto", false,true)]
        public string GenerosTexto
        {
            get { return generosTexto; }
            set { generosTexto = value; }
        }

        public List<Genero> generos;

        public Musica()
        {
            generos = new List<Genero>();
        }

    }
}