using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestAPI1.Services;
using TestAPI1.Models;
namespace TestAPI1.ApiControllers
{
    public class HomeController : ApiController
    {
        public int ChieuCao = 4;
        public int ChieuRong = 2;

        [HttpGet]
        public object Getlist() {                                
            TestService testService = new TestService();
           List<TestView> lsTest = testService.GetListTestId();

            return lsTest ;
        }
        [HttpGet]
        public object GetlistClass()
        {
            ClassTestService classTestService = new ClassTestService();
            List<object> lsclassTest = classTestService.GetListClassTest();
            return lsclassTest;
        }

        [HttpGet]
        public object Getdetail(string id)                                   // truyền id   (string id)
        { 
            TestService testService = new TestService();                      // khai báo test Service (bắt buộc)
            object list = testService.GetDetailid(id);
            return list;
        }

        [HttpPost]
        public string CreateTest(Test model)
        {
            using (var connect = BaseService.Connect())
            {
                connect.Open();
                using (var transaction = connect.BeginTransaction()) {
                    TestService testService = new TestService(connect);
                    ClassTestService classTestService = new ClassTestService(connect);
                    Test test = new Test();
                    test.TestId = Guid.NewGuid().ToString();
                    test.Name = model.Name;
                    test.ClassId = model.ClassId;
                    test.BirthYear = model.BirthYear;
                    test.Gender = model.Gender;
                    test.CreateTime = model.CreateTime;
                    ClassTest classTest = classTestService.GetClassTestById(model.ClassId, transaction);
                    classTest.ClassTestId = model.ClassId;
                    classTest.Quantity++;
                    testService.CreateTest(test , transaction);
                    classTestService.PlusClasQuantity(classTest, transaction);
                    transaction.Commit();
                    return "ok";
                }
            }
        }
        [HttpPost]
        public string CreateClassTest(ClassTest model)
        {
            using (var connect = BaseService.Connect())
            {
                connect.Open();
                using(var transaction = connect.BeginTransaction())
                {
                    ClassTestService classTestService = new ClassTestService(connect);
                    ClassTest classTest = new ClassTest();
                    classTest.ClassName = model.ClassName;
                    classTest.Quantity = model.Quantity;
                    classTestService.CreateClassTest(classTest, transaction);
                    transaction.Commit();
                    return "ok";
                }
            }
        }
        [HttpPost]
        public string EditTest(Test model)
        {
            using (var connect = BaseService.Connect())
            {
                connect.Open();
                using(var transaction = connect.BeginTransaction())
                {
                    TestService testService = new TestService(connect);
                    Test test = testService.GetTestById(model.TestId, transaction);
                    ClassTestService classTestService = new ClassTestService(connect);
                    if (test.ClassId != model.ClassId)
                    {
                        ClassTest classTestold = classTestService.GetClassTestById(test.ClassId, transaction);
                        classTestold.Quantity--;
                        classTestService.PlusClasQuantity(classTestold, transaction);
                        ClassTest classTestnew = classTestService.GetClassTestById(model.ClassId, transaction);
                        classTestnew.Quantity++;
                        classTestService.PlusClasQuantity(classTestnew, transaction);
                    }
                    test.Name = model.Name;
                    test.ClassId = model.ClassId;
                    test.BirthYear = model.BirthYear;
                    test.Gender = model.Gender;
                    test.CreateTime = model.CreateTime;
                    testService.UpdateTest(test, transaction);
                    transaction.Commit();
                    return "sua ok";
                }
            }
        }
        [HttpPost]
        public string DeleteTest(string testid)
        {
            using (var connect = BaseService.Connect())
            {
                connect.Open();
                using (var transaction = connect.BeginTransaction())
                {
                    TestService testService = new TestService(connect);
                    ClassTestService classTestService = new ClassTestService(connect);
                    Test test = testService.GetTestById(testid, transaction);
                    ClassTest classTest = classTestService.GetClassTestById(test.ClassId, transaction);
                    classTest.ClassTestId = test.ClassId;
                    classTest.Quantity--;
                    classTestService.PlusClasQuantity(classTest, transaction);
                    testService.RemoveTest(testid, transaction);
                    transaction.Commit();
                    return "xoa ok";
                }
            }

        }

        [HttpGet]
        public int DienTich() {
            return ChieuRong * ChieuCao;
        }

    }
}
