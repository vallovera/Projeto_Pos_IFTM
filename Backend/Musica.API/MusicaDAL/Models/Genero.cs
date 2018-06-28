using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaDAL.Models
{
    [BlueORM.Table("genero", "codGenero")]
    [Serializable]
    public partial class Genero
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        private int codGenero;
        [BlueORM.Column("codGenero", true)]
        public int CodGenero
        {
            get { return codGenero; }
            set { codGenero = value; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        private string Nome;
        [BlueORM.Column("Nome", false)]
        [Required(ErrorMessage = "Preencha o nome completo.")]
        [MaxLength(100, ErrorMessage = "O nome deve ter até {1} caracteres.")]
        [Display(Name = "Nome")]
        public string NOME
        {
            get { return Nome; }
            set { Nome = value; }
        }

        public Genero()
        {
        }

    }
}
