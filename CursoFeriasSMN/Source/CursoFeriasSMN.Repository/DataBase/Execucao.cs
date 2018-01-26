using System.Data;
using System.Data.SqlClient;

namespace CursoFeriasSMN.Repository.DataBase
{
    public class Execucao
    {
        private readonly Conexao _conexao;
        private SqlCommand _command;

        public Execucao(Conexao conexao)
        {
            _conexao = conexao;
        }

        //Monta o comando
        public void ExecuteProcedure(object procName)
        {
            _command = new SqlCommand(procName.ToString(),
                _conexao.Connection, _conexao.Transaction)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = int.MaxValue
            };
        }

        //Adiciona os parâmetros na procedure caso seja necessário
        public void AddParameter(string parameterName, object parameterValue)
        {
            _command.Parameters.AddWithValue(parameterName, parameterValue);
        }

        /*Adiciona um parâmetro de saída na procedure, esse método geralmente será utilizado
         com o método ExecuteNonQueryWithReturn */
        protected void AddParameterOutput(string parameterName, object parameterValue, DbType parameterType)
        {
            _command.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.InputOutput,
                Value = parameterValue,
                DbType = parameterType
            });
        }

        protected string GetParameterOutput(string parameter) => _command.Parameters[parameter].Value.ToString();

        //Método para executar procedure que tem nenhum retorno (Insert, Delete)
        public void ExecuteNonQuery()
        {
            _conexao.Open();
            _command.ExecuteNonQuery();
        }

        protected void AddParameterReturn(string parameterName = "@RETURN_VALUE", DbType parameterType = DbType.Int16)
        {
            _command.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.ReturnValue,
                DbType = parameterType
            });
        }

        public int ExecuteNonQueryWithReturn()
        {
            AddParameterReturn();
            _conexao.Open();
            _command.ExecuteNonQuery();
            return int.Parse(_command.Parameters["@RETURN_VALUE"].Value.ToString());
        }

        //Método utilizado para executar procedures que retorna valores (Select)
        public SqlDataReader ExecuReader()
        {
            _conexao.Open();
            return _command.ExecuteReader();
        }

        public void BeginTransaction()
        {
            _conexao.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _conexao.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _conexao.RollBackTransaction();
        }
    }
}
