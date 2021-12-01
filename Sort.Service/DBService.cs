using Sort.Entities.Models;
using Sort.Service.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Sort.Entities;
using Sort.Entities.Models;

namespace Sort.Service
{
    public class DBSortingService : IDBSortingService
    {

        public Context _dbcontext;

        public DBSortingService()
        {
            _dbcontext = new Context();
        }

        public List<Pessoa> GetPessoas()
        {
            var person = _dbcontext.Pessoa.ToList();
            return person;
        }
        
        public List<string> GetPessoasSorted()
        {
            var pessoas = _dbcontext.Pessoa.ToList();
            var sortedPessoas = BubbleSort(pessoas);
            return sortedPessoas.Select(x => x.Id + " - " + x.Nome).ToList();
        }
        
        public Pessoa PostPessoa(Pessoa pessoa)
        {
            var result = _dbcontext.Pessoa.Add(pessoa);
            _dbcontext.SaveChanges();
            return result.Entity;
        }

        private List<Pessoa> BubbleSort(List<Pessoa> pessoas)
        {
            for (int j = 0; j <= pessoas.Count() - 2; j++) {
                for (int i = 0; i <= pessoas.Count() - 2; i++) {
                   if (String.Compare(pessoas[i].Nome, pessoas[i + 1].Nome) > 0) {
                      var temp = pessoas[i + 1];
                      pessoas[i + 1] = pessoas[i];
                      pessoas[i] = temp;
                   }
                }
            }
            return pessoas;
        }

    }
}
