
using System.ComponentModel.DataAnnotations;

namespace Netby.Domain
{
    public class Formulario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Campo> Campos { get; set; }
    }
}