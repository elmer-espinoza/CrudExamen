using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudExamen.Models
{

    [Table(name: "Cliente")]
    public class Cliente
    {
        [Key]
        public int? ClienteId { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? TipoCliente { get; set; }

        public string? SituacionLaboral { get; set; }

        public string? Estado { get; set; }

    }

    [Table(name: "Cliente_compras")]

    public class ClienteCompras
    {
        [Key]
        public int? ClienteId { get; set; }

        public string? Nombres { get; set; }

        public string? Apellidos { get; set; }

        public string? TipoCliente { get; set; }

        public string? SituacionLaboral { get; set; }

        public string? Estado { get; set; }

        public int? NroCompras { get; set; }
    }


    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<ClienteCompras> Cliente_Compras { get; set; }
    }



}
