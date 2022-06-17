using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleships;
using FluentAssertions;

using Xunit;

namespace Battleships.Test
{
    public class GameTest
    {
        [Fact]
        public void TestPlay()
        {
            // var ships = new[] { "3:2,3:5" };
            var ships = new string[,] { { "3:2,3:5" }, { "5:5,5:1" }  };
            var guesses = new[] { "7:0", "3:3" };
            Game.Play(ships, guesses).Should().Be(1);
        }

        [Fact]
        public void  Game_WhenMultipleShipsHit_ReturnmorethanOne()
        {
                var ships = new string[,] { { "3:2,3:5" }, { "5:5,5:1" }  };
                var guesses = new[] { "5:1", "3:3", "5:3" };
                Game.Play(ships, guesses).Should().Be(2);
       }
    }
}
