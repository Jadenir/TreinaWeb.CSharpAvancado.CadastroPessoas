using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoas.Repositorio
{
    public interface IRepositorio<TTipo>
    {
        List<TTipo> SelecionarTodos();
        void Adicionar(TTipo objeto, Action<int> callBack);
        List<TTipo> Selecionar(Func<TTipo, bool> whereClause);
    }
}
