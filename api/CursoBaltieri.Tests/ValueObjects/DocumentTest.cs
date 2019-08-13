using CursoBaltieri.Domain.StoreContent.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoBaltieri.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTest
    {
        private Document invalido;
        private Document valido;

        public DocumentTest()
        {
             invalido = new Document("12345678910");
             valido =   new Document("22192142811");
        }
        [TestMethod]
        public void DeveRetornarUmaNotificacaoQuandoODocumentoNaoForValido()
        {

            Assert.AreEqual(true, invalido.Invalid);
            Assert.AreEqual(1, invalido.Notifications.Count);
        }

        [TestMethod]
        public void DeveRetornarUmaNotificacaoQuandoODocumentoForValido()
        {
           
            Assert.AreEqual(false, valido.Invalid);
            Assert.AreEqual(0, valido.Notifications.Count);
        }
    }
}
