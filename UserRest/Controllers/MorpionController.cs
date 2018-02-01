﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserRest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserRest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MorpionController : Controller
    {
        // GET: api/values
        [HttpPost]
        public Joueur CheckVictory([FromBody] List<Square> grid)
        {
            Joueur winner = null;

            winner = CheckLine(new List<Joueur>() { grid[0].Owner, grid[1].Owner, grid[2].Owner });

            if (winner == null)
                winner = CheckLine(new List<Joueur>() { grid[3].Owner, grid[4].Owner, grid[5].Owner });

            if (winner == null)
                winner = CheckLine(new List<Joueur>() { grid[6].Owner, grid[7].Owner, grid[8].Owner });

            if (winner == null)
                winner = CheckLine(new List<Joueur>() { grid[0].Owner, grid[3].Owner, grid[6].Owner });

            if (winner == null)
                winner = CheckLine(new List<Joueur>() { grid[1].Owner, grid[4].Owner, grid[7].Owner});

            if (winner == null)
                winner = CheckLine(new List<Joueur>() { grid[2].Owner, grid[5].Owner, grid[8].Owner });

            if (winner == null)
                winner = CheckLine(new List<Joueur>() { grid[0].Owner, grid[4].Owner, grid[8].Owner });

            if (winner == null)
                winner = CheckLine(new List<Joueur>() { grid[2].Owner, grid[4].Owner, grid[6].Owner });

            return winner;
        }

        private Joueur CheckLine(List<Joueur> players)
        {
            Joueur retour = null;
            bool isLineComplete = true;
            bool isSamePlayer = true;
            Joueur currentPlayer = null;

            if (players != null && players.Count() > 0)
            {
                currentPlayer = players[0];

                if (currentPlayer != null)
                {
                    foreach (Joueur joueur in players)
                    {
                        isLineComplete &= (joueur != null);
                        isSamePlayer &= (currentPlayer.Equals(joueur));
                    }

                    if (isSamePlayer && isLineComplete)
                    {
                        retour = currentPlayer;
                    }
                }
            }

            return retour;
          }
    }
}
