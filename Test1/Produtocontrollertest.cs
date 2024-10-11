using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Common.Telemetry;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_inventario.Controllers;
using web_app_repository;

namespace Test1
{
    public class Produtocontrollertest
    {
        private readonly Mock<IValueRepository> _produtoRepositoryMock;
        private readonly ValuesController _controller;

        public Produtocontrollertest()
        {
            _produtoRepositoryMock = new Mock<IValueRepository>();
            _controller = new ValuesController(_produtoRepositoryMock.Object);
        }

        [Fact]

        public async Task Get_ListarValuesOk()
        {
            var produtos = new List<Values>()
            {
                new Values()
                {
                   id = 1,
                   nome = "nathalia",
                   preco = 200
                }
            };
            _produtoRepositoryMock.Setup(r => r.ListValues()).ReturnsAsync(produtos);

            //act
            var result = await _controller.GetValues();

            //Asserts
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(JsonConvert.SerializeObject(produtos), JsonConvert.SerializeObject(okResult.Value));
             

        }

        [Fact] 
       
        public async Task Get_ListarRetornaNotFound()
        {
            _produtoRepositoryMock.Setup(u => u.ListValues()).ReturnsAsync((IEnumerable<Values>)null);
            var result = await _controller.GetValues();

            Assert.IsType<NotFoundResult>(result);
        }

        public async Task Post_salvarproduto()
        {
            //arrange

            var produto = new Values()
            {
                id = 1,
                nome = "nathalia lopes",
                preco = 200
            };

            _produtoRepositoryMock.Setup(u => u.SaveValues(It.IsAny<Values>())).Returns(Task.CompletedTask);

            //act

            var result = await _controller.Post(produto);

            //asserts

            _produtoRepositoryMock.Verify(u => u.SaveValues(It.IsAny<Values>()), Times.Once);
            Assert.IsType<OkResult>(result);


                 
        }

    }
}
