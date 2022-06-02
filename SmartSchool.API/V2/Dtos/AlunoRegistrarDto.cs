using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V2.Dtos
{
    /// <summary>
    /// Este é o DTO de aluno para cadastro.
    /// </summary>
    public class AlunoRegistrarDto
    {
        /// <summary>
        /// Identificador e chave do banco.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Chave do aluno, para outros negocios na instituição.
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        /// Primeiro nome  e sobrenome do aluno.
        /// </summary>
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataMatricula { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = null;
        public bool Ativo { get; set; } = true;
    }
}
