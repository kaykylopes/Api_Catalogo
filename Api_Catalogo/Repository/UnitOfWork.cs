using Api_Catalogo.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api_Catalogo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ProdutoRepository _produtoRepo;

        private CategoriaRepository _categoriaRepo;

        public AppDbContext _uof;

        public UnitOfWork(AppDbContext context)
        {
            _uof = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_uof);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_uof);
            }
        }
        public void Commit()
        {
            _uof.SaveChanges();
        }

        public void Dispose()
        {
            _uof.Dispose();
        }
    }
}
