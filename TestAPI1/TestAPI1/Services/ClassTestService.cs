using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;
using TestAPI1.Models;

namespace TestAPI1.Services
{
    public class ClassTestService:BaseService
    {
        public ClassTestService() : base() { }                      // khai báo kết nối database
        public ClassTestService(IDbConnection db) : base(db) { }

        public List<object> GetListClassTest()
        {
            string query = "select * from class_test";
            List<object> ls = this._connection.Query<object>(query, null).ToList();
            return ls;
        }
        public bool CreateClassTest(ClassTest model, IDbTransaction transaction = null)
        {
            string query = "INSERT INTO [dbo].[class_test] ([ClassName],[Quantity]) VALUES (@ClassName,@Quantity)";
            int status = this._connection.Execute(query, model, transaction);
            return status > 0;
        }
        public ClassTest GetClassTestById(int id, IDbTransaction transaction = null)
        {
            string query = "select * from class_test where classtestid=@id";
            return this._connection.Query<ClassTest>(query, new { id }, transaction).FirstOrDefault();
        }
        public bool PlusClasQuantity(ClassTest model, IDbTransaction transaction= null)
        {
            string query = "UPDATE [dbo].[class_test] SET [Quantity] = @Quantity where [Classtestid]=@classtestid";
            int status = this._connection.Execute(query, model, transaction);
            return status > 0;
        }
    }
}