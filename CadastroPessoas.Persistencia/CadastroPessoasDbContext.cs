using CadastroPessoas.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoas.Persistencia
{
    public class CadastroPessoasDbContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
