using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_Combat.Models
{
    internal interface IGameModel
    {
        event EventHandler Changed;
        event EventHandler GameOver;

        Tank PlayerTank { get; set; }
        Tank EnemyTank { get; set; }
        MapFrame MapFrame { get; set; }
        List<Terrain> Terrains { get; set; }
    }
}
