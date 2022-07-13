using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using TestAPI1.Models;

namespace TestAPI1.Services
{
    public class TestService : BaseService
    {
        public TestService() : base() { }                      // khai báo kết nối database
        public TestService(IDbConnection db) : base(db) { }

        public List<TestView> GetListTestId() {                
            string query = "select * from Test t left join class_test ct on t.ClassId=ct.ClassTestId";    // join bảng
            List<TestView> ls = this._connection.Query<TestView>(query, null).ToList();    // co the lay nhung cai khac thay vi null
            return ls;
        }
     

        public object GetDetailid(string testid)
        {
            string query = "select TestId,Name,ClassId,BirthYear,Gender,CreateTime from Test where TestId=@testid";
            object ls = this._connection.Query<object>(query, new { testid }).ToList();
            return ls;
        }
        public bool CreateTest(Test model, IDbTransaction transaction = null)
        {
            string query = "INSERT INTO [dbo].[Test] ([TestId],[Name],[ClassId],[BirthYear],[Gender],[CreateTime]) VALUES (@TestId,@Name,@ClassId,@BirthYear,@Gender,getdate())";
            int status = this._connection.Execute(query, model, transaction);
            return status > 0;
        }
        public Test GetTestById(string id, IDbTransaction transaction = null)
        {
            string query = "select * from Test where TestId=@id";
            return this._connection.Query<Test>(query, new { id }, transaction).FirstOrDefault();
        }
        public bool UpdateTest(Test model, IDbTransaction transaction = null)
        {
            string query = "UPDATE [dbo].[Test] SET [Name] = @Name , [ClassId] = @ClassId, [BirthYear] = @BirthYear, [Gender] = @Gender, [CreateTime] = @CreateTime where [TestId]=@testid";
            int status = this._connection.Execute(query, model, transaction);
            return status > 0;
        }
        public Test RemoveTest(string testid, IDbTransaction transaction = null)
        {
            string query = "DELETE from test where Testid=@testid";
            return this._connection.Query<Test>(query, new { testid }, transaction).FirstOrDefault();
        }
    }
}