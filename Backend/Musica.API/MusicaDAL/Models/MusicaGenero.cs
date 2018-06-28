using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaDAL.Models
{
    [BlueORM.Table("musica_genero", "codMusicaGenero")]
    [Serializable]
    public class MusicaGenero
    {
        private int codMusicaGenero;
        [BlueORM.Column("codMusicaGenero", true)]
        public int CODMUSICAGENERO
        {
            get { return codMusicaGenero; }
            set { codMusicaGenero = value; }
        }

        private int codMusica;
        [BlueORM.Column("codMusica", false)]
        public int CODMUSICA
        {
            get { return codMusica; }
            set { codMusica = value; }
        }

        private int codGenero;
        [BlueORM.Column("codGenero", false)]
        public int CODGENERO
        {
            get { return codGenero; }
            set { codGenero = value; }
        }

        public MusicaGenero()
        {
        }

    }
}
