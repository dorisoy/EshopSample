using Domain.Entities;
using DomainBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IGoodsRepository : IRepository<Goods>
    {
        public Guid Key { get; set; }
    }
}
