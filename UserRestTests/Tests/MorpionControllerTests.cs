using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UserRest.Contexts;
using UserRest.Controllers;
using UserRest.Models;
using Xunit;

namespace UserRestTests.Tests
{
    public class MorpionControllerTests
    {
        private MorpionController _morpionController;

        public MorpionControllerTests()
        {
            _morpionController = new MorpionController();
        }

        [Fact]
        public void EmptyGrid()
        {
            List<Square> grid = new List<Square>() {
                new Square() { Id = 1, Owner = null}, new Square() { Id = 2, Owner = null}, new Square() { Id = 3, Owner = null},
                new Square() { Id = 4, Owner = null }, new Square() { Id = 5, Owner = null }, new Square() { Id = 6, Owner = null },
                new Square() { Id = 7, Owner = null }, new Square() { Id = 8, Owner = null }, new Square() { Id = 9, Owner = null }
            };

            Assert.Equal(null, _morpionController.CheckVictory(grid));
        }

        [Fact]
        public void NoWinner()
        {
            Pseudo j1 = new Pseudo() { Name = "Jordan", NombrePartie = 1, Victoires = 0, Avatar = "Jojo" };
            Pseudo j2 = new Pseudo() { Name = "Damien", NombrePartie = 10, Victoires = 0, Avatar = "Joker" };

            List<Square> grid = new List<Square>() {
                new Square() { Id = 1, Owner = j1}, new Square() { Id = 2, Owner = j2}, new Square() { Id = 3, Owner = j1},
                new Square() { Id = 4, Owner = j2 }, new Square() { Id = 5, Owner = j1 }, new Square() { Id = 6, Owner = j2 },
                new Square() { Id = 7, Owner = j1 }, new Square() { Id = 8, Owner = j2 }, new Square() { Id = 9, Owner = j1 }
            };

            Assert.Equal(null, _morpionController.CheckVictory(grid));
        }

        [Fact]
        public void Winner()
        {
            Pseudo j1 = new Pseudo() { Name = "Jordan", NombrePartie = 1, Victoires = 0, Avatar = "Jojo" };
            Pseudo j2 = new Pseudo() { Name = "Damien", NombrePartie = 10, Victoires = 0, Avatar = "Joker" };

            List<Square> grid = new List<Square>() {
                new Square() { Id = 1, Owner = j1}, new Square() { Id = 2, Owner = j2}, new Square() { Id = 3, Owner = j1},
                new Square() { Id = 4, Owner = j1 }, new Square() { Id = 5, Owner = j2 }, new Square() { Id = 6, Owner = j2 },
                new Square() { Id = 7, Owner = j1 }, new Square() { Id = 8, Owner = j1 }, new Square() { Id = 9, Owner = j2 }
            };

            Assert.Equal(j1, _morpionController.CheckVictory(grid));
        }
    }
}
