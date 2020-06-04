using CadastroPessoas.Dominio;
using CadastroPessoas.Persistencia;
using CadastroPessoas.Persistencia.NHibernate.Maps;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CadastroPessoas.Repositorio
{
    public class PessoaRepositorio : IRepositorio<Pessoa>
    {
        //private ISessionFactory _sessionFactory;

        //*********************************************************************
        //NHibernate
        //public PessoaRepositorio()
        //{
        //    Configuration config = new Configuration();
        //    config.Configure();
        //    config.AddAssembly(typeof(Pessoa).Assembly);
        //    HbmMapping mapping = CreateMappings();
        //    config.AddDeserializedMapping(mapping, null);
        //    _sessionFactory = config.BuildSessionFactory();
        //}
        //private HbmMapping CreateMappings()
        //{
        //    ModelMapper mapper = new ModelMapper();
        //    mapper.AddMapping(typeof(PessoaMap));
        //    return mapper.CompileMappingForAllExplicitlyAddedEntities();
        //}
        //public List<Pessoa> SelecionarTodos()
        //{
        //    using (ISession sessao = _sessionFactory.OpenSession())
        //    {
        //        IQuery consulta = sessao.CreateQuery("FROM Pessoa");
        //        return consulta.List<Pessoa>().ToList();
        //    }
        //}
        //public int Adicionar(Pessoa objeto)
        //{
        //    using (ISession sessao = _sessionFactory.OpenSession())
        //    {
        //        using(var transacao = sessao.BeginTransaction())
        //        {
        //            sessao.Save(objeto);
        //            transacao.Commit();
        //            return 0;
        //        }
        //    }
        //}

        //*********************************************************************
        //Entity Framwork
        public List<Pessoa> SelecionarTodos()
        {
            CadastroPessoasDbContext contexto = new CadastroPessoasDbContext();
            List<Pessoa> pessoas =
                contexto.Pessoas
                .AsParallel()
                .OrderBy(o => o.Id)
                .ToList();
            contexto.Dispose();
            return pessoas;
        }
        public async void Adicionar(Pessoa objeto, Action<int> callBack)
        {
            CadastroPessoasDbContext contexto = new CadastroPessoasDbContext();
            contexto.Pessoas.Add(objeto);
            Thread.Sleep(1000);
            await contexto.SaveChangesAsync().ContinueWith((taskAnterior) =>
            {
                int linhasAfetatas = taskAnterior.Result;
                callBack(linhasAfetatas);
            });
        }

        public List<Pessoa> Selecionar(Func<Pessoa, bool> whereClause)
        {
            CadastroPessoasDbContext contexto = new CadastroPessoasDbContext();
            List<Pessoa> pessoas =
                contexto.Pessoas
                .AsParallel()
                .Where(whereClause)
                .ToList();
            contexto.Dispose();
            return pessoas;
        }
    }
}
