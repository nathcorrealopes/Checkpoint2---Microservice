using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_app_domain;
using web_app_repository;

namespace Test1
{
    public class ProdutoRepositorytest

    {
        [Fact]

        public async Task ListarValues()
        {



            var produtos = new List<Values>()
            {
                new Values()
                {
                   id = 1,
                   nome = "nathalia",
                   preco = 200
                },
                 new Values()
                {
                   id = 2,
                   nome = "gabi",
                   preco = 300
                }
            };
            var _produtoRepositoryMock = new Mock<IValueRepository>();
            _produtoRepositoryMock.Setup(u => u.ListValues()).ReturnsAsync(produtos);
            var pordutorepository = _produtoRepositoryMock.Object;

            //Act
            var result = await pordutorepository.ListValues();

            Assert.Equal(produtos, result);


        }


        [Fact]
        public async Task SaveValues()
        {
            var produto = new Values()
            {
                id = 1,
                nome = "nathalia",
                preco = 200
            };
            var produtoRepositoryMock = new Mock<IValueRepository>();
            produtoRepositoryMock.Setup(u => u.SaveValues(It.IsAny<Values>())).Returns(Task.CompletedTask);

            var produtorepository = produtoRepositoryMock.Object;

            await produtorepository.SaveValues(produto);

            produtoRepositoryMock.Verify(u => u.SaveValues(It.IsAny<Values>()), Times.Once);

        }

      
    }
}
