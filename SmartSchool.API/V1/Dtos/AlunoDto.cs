using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.Dtos
{
    public class AlunoDto
    {
        /// <summary>
        /// Identificador e chave do banco
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Chave do aluno, para outros negocios na instituição
        /// </summary>
        public int Matricula { get; set; }
        /// <summary>
        /// Primeiro nome  e sobrenome do aluno
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// telefone do aluno
        /// </summary>
        public string Telefone { get; set; }
        /// <summary>
        /// Idade é o calculo relacionado a data de nascimento do aluno
        /// </summary>
        public int  Idade { get; set; }
        /// <summary>
        /// Data de matricula do 
        /// </summary>
        public DateTime DataMatricula { get; set; }
        /// <summary>
        /// verificar se esta ativo ou não
        /// </summary>
        public bool Ativo { get; set; }
    }
}
