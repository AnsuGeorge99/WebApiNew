using EmployeeDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebApiNew.Controllers;


namespace WebApiNew.UnitTests
{
    [TestClass]
    public class EmployeeControllerTests
    {
        [TestMethod]
        public void Get_ShouldReturnCorrectEmployee()
        {
            EmpDBEntities db = new EmpDBEntities();
            EmployeeController controller = new EmployeeController(db);

            //Act
            var result = controller.

        }
    }
}
