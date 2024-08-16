
using System.ComponentModel.DataAnnotations;

namespace Netby.Domain
{
    public class Campo
    {
        [Key]
        public int Id { get; set; }
        public int FormularioId { get; set; }
        public string NombreCampo { get; set; }
        public string TipoCampo { get; set; }
        public bool EsRequerido { get; set; }
        public Formulario Formulario { get; set; }
    }
}