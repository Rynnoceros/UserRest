using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRest.Controllers;
using UserRest.Contexts;
using UserRest.Models;
using Xunit;

namespace UserRestTests.Tests
{
    public class PseudosControllerTests
    {
        private UserContext _context;
        private PseudosController _pseudoController;

        public PseudosControllerTests()
        {
            var options = new DbContextOptionsBuilder<UserContext>().UseInMemoryDatabase("test").Options;
            var context = new UserContext(options);
            context.SaveChanges();
            _context = context;
            _pseudoController = new PseudosController(_context);
        }

        [Fact]
        public void GetAll()
        {
            var results = _pseudoController.Get() as OkObjectResult;
            var pseudos = results.Value as IList;
            Assert.Equal(200, results.StatusCode);
            Assert.NotNull(pseudos);
        }

        [Theory]
        [InlineData(new object[] { "Jojo", "Jojo.com" })]
        [InlineData(new object[] { "Titi", "Titi.com" })]
        [InlineData(new object[] { "Link", "Link.com" })]
        public void PostGetPseudoMultiple(string name, string avatar)
        {
            var results = _pseudoController.Get() as OkObjectResult;
            var listPseudos = results.Value as IList;
            Pseudo pseudo = new Pseudo() { Name = name, Avatar = avatar };

            int beforeAdd = listPseudos.Count;
            var result = _pseudoController.Post(pseudo) as CreatedAtActionResult;

            Assert.Equal(201, result.StatusCode);

            results = _pseudoController.Get(beforeAdd + 1) as OkObjectResult;
            pseudo = results.Value as Pseudo;

            Assert.Equal(beforeAdd + 1, pseudo.Id);
        }

        [Fact]
        public void GetNotFound()
        {
            var result = _pseudoController.Get(99999) as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void DeleteFound()
        {
            var result = _pseudoController.Delete(1) as AcceptedResult;

            Assert.Equal(202, result.StatusCode);
        }

        [Fact]
        public void DeleteNotFound()
        {
            var result = _pseudoController.Delete(99999) as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void PutModified()
        {
            Pseudo pseudo = new Pseudo() { Name = "Grosminet", Avatar = "Grosminet.com" };

            var result = _pseudoController.Put(2, pseudo) as AcceptedResult;
            Assert.Equal(202, result.StatusCode);

            var result1 = _pseudoController.Get(2) as OkObjectResult;
            var pseudoMaj = result1.Value as Pseudo;

            Assert.Equal("Grosminet", pseudoMaj.Name);
        }

        [Fact]
        public void PutNotFound()
        {
            Pseudo pseudo = new Pseudo() { Name = "Perceval", Avatar = "Perceval.com" };

            var result = _pseudoController.Put(99999, pseudo) as NotFoundResult;

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void PutConflict()
        {
            Pseudo pseudo = new Pseudo() { Name = "Perceval", Avatar = "Perceval.com" };

            var result = _pseudoController.Post(pseudo) as CreatedAtActionResult;
            var pseudoInserted = result.Value as Pseudo;

            var result1 = _pseudoController.Put(pseudoInserted.Id, pseudo) as ObjectResult;
            var error = result1.Value as Message;
            Assert.Equal(409, result1.StatusCode);
            Assert.Equal("Pseudo already exists", error.Detail);

        }


    }
}
