using CursoBaltieri.Domain.StoreContent.Entities;
using CursoBaltieri.Domain.StoreContent.Enuns;
using CursoBaltieri.Domain.StoreContent.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoBaltieri.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private Customer _customer;
        private Order _order;

        private Product produto1;
        private Product produto2;
        private Product produto3;
        private Product produto4;

        public OrderTests()
        {
            var name = new Name("Evandro", "Casimiro");
            var document = new Document("22192142811");
            var email = new Email("quitosp@hotmail.com");
            _customer = new Customer(name, document, email, "17996056382");
            _order = new Order(_customer);

            produto1 = new Product("title", "Descricao", "image", 100, 10);
            produto2 = new Product("title", "Descricao", "image", 100, 10);
            produto3 = new Product("title", "Descricao", "image", 100, 10);
            produto4 = new Product("title", "Descricao", "image", 100, 10);

        }
        //consigo criar um pedido
        [TestMethod]
        public void CriarUmPedidoQuandoForValido()
        {
           Assert.AreEqual(false, _order.Invalid);
        }

        //ao criar um pedido, o status deve ser created
        [TestMethod]
        public void MudarStatusParaCreated()
        {
            Assert.AreEqual(EOrderStatus.Create, _order.Status);
        }

        //ao adicionar um novo item, a quantidade deve mudar
        [TestMethod]
        public void DeveRetornarDoisQuandoAdicionadoDoisItens()
        {
            _order.AddItem(produto1, 5);
            _order.AddItem(produto2, 5);

            Assert.AreEqual(2, _order.Items.Count);
        }

        //ao adicionar um novo item, deve subtrair a quantidade do produto
        [TestMethod]
        public void DeveRetornarCincoQuandoCompradoCincoItem()
        {
            _order.AddItem(produto2, 5);

            Assert.AreEqual(produto2.QuantityOnHand,5);
        }

        //ao confirmar pedido, deve gerar um numero
        [TestMethod]
        public void AoGerarUmPedidoDeveRetornarUmNumero()
        {
            _order.Place();
            Assert.AreNotEqual("", _order.Number);
        }

        //ao pagar um pedido, o status deve ser pago
        [TestMethod]
        public void DeveRetornarPagoQuandoOPedidoForPago()
        {
            _order.Pay();
            Assert.AreEqual(EOrderStatus.Pad, _order.Status);
        }

        //dados mais 10 produtos, devem haver duas entregas
        [TestMethod]
        public void DeveRetornarDoisQuandoComprarDezProdutos()
        {
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);

            _order.Ship();
            Assert.AreEqual(2, _order.Deliveries.Count);
        }

        //ao cancelar o pedido, o status deve ser cancelado
        [TestMethod]
        public void MudarStatusParaCanceladoQuandoPedidoForCancelado()
        {
            _order.Cancel();
            Assert.AreEqual(EOrderStatus.Canceled, _order.Status);
        }

        //ao cancelar o pedido, deve cancelar as entregas
        [TestMethod]
        public void MudarStatusParaCanceladoQuandoEntregaForCancelado()
        {
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);
            _order.AddItem(produto1, 1);

            _order.Cancel();

            foreach (var item in _order.Deliveries)
            {
                Assert.AreEqual(EDeliveryStatus.Canceled, item.Status);
            }
        }
    }
}
