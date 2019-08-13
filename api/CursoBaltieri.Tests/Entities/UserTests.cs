using CursoBaltieri.Domain.UserContent.Entities;
using CursoBaltieri.Shared.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace CursoBaltieri.Tests.Entities
{
    [TestClass]
    public class UserTests
    {
        Usuario user;
        EmailObjectShared email;

        public UserTests()
        {
            
        }
        //cadastro usuario valido
        [TestMethod]
        public void CriarUmUsuarioQuandoForValido()
        {
            //var _email = new EmailObjectShared("quitosp@hotmail.com");
            var _user = new Usuario("evandro","dias","quitosp@hotmail.com","123456789");

            Assert.AreEqual(true, _user.Valid);
        }

        //cadastro usuario valido
        [TestMethod]
        public void CriarUmUsuarioQuandoForInValido()
        {
            //var _email = new EmailObjectShared("quitosp@hotmail.com");
            var _user = new Usuario("evandro", "dias", "quitosp@hotmail.com", "123456789");

            Assert.AreEqual(false, _user.Invalid);
        }
    }
}
