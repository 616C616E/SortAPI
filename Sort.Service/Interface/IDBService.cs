using Sort.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sort.Service.Interface
{
    public interface IDBSortingService
    {
        List<Pessoa> GetPessoas();
        Pessoa PostPessoa(Pessoa pessoa);
        List<string> GetPessoasSorted();
    }
}
