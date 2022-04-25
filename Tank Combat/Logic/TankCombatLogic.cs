using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank_Combat.Models;

namespace Tank_Combat.Logic
{
    internal class TankCombatLogic : IGameModel
    {
        #region Setup
        public Tank PlayerTank { get; set; }
        public Tank EnemyTank { get; set; }
        public MapFrame MapFrame { get; set; }
        public List<Terrain> Terrains { get; set; }
        public List<GameItem> Barriers { get; set; }
        public Bullet SingleBullet { get; set ; }

        public event EventHandler Changed;
        public event EventHandler GameOver;

        public enum Controls
        {
            Up, Down, Left, Right, Space
        }

        public void SetUpSizes(int screenWidth, int screenHeight)
        {
            //Resizing all map elements, when window size changed...
        }

        public TankCombatLogic(int screenWidth, int screenHeight)
        {
            MapFrame = new MapFrame(screenWidth, screenHeight);
            PlayerTank = new Tank(screenWidth / 5, screenHeight / 2, 10, 10);
            EnemyTank = new Tank(screenWidth * 4 / 5, screenHeight / 2, 10, 10);
            Terrains = new List<Terrain>();
            Barriers = new List<GameItem>();
            Barriers.Add(PlayerTank);
            Barriers.Add(EnemyTank);
            Barriers.Add(MapFrame);
            foreach (var item in Terrains)
            {
                Barriers.Add(item);
            }
        }
        #endregion

        #region Logic
        public void Control(Controls control)
        {
            switch (control)
            {
                
                case Controls.Up:
                    PlayerTank.Angle = 0;
                    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
                        break;
                case Controls.Down:
                    PlayerTank.Angle = 180;
                    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
                    break;
                case Controls.Left:
                    PlayerTank.Angle = 270;
                    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
                    break;
                case Controls.Right:
                    PlayerTank.Angle = 90;
                    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
                    break;
                case Controls.Space:
                    PlayerTank.Shoot((int)PlayerTank.Angle);
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }

        public void TimeStep()
        {
            #region PlayerTank bullets collisions
            foreach (var bullet in PlayerTank.Bullets)
            {
                bullet.Move();
                foreach (var terrain in Terrains)
                {
                    if (bullet.IsCollision(terrain))
                    {
                        PlayerTank.Bullets.Remove(bullet);
                        terrain.GotHit();
                        if (terrain.Hp <= 0)
                        {
                            Terrains.Remove(terrain);
                        }
                    }
                }
                if (bullet.IsCollision(EnemyTank))
                {
                    PlayerTank.Bullets.Remove(bullet);
                    EnemyTank.GotHit();
                    if (EnemyTank.Hp <= 0)
                    {
                        GameOver?.Invoke(this, null);
                    }

                }
                if (bullet.IsCollision(MapFrame))
                {
                    PlayerTank.Bullets.Remove(bullet);
                }
            }
            #endregion

            #region EnemyTank bullets collisions
            foreach (var bullet in EnemyTank.Bullets)
            {
                bullet.Move();
                foreach (var terrain in Terrains)
                {
                    if (bullet.IsCollision(terrain))
                    {
                        EnemyTank.Bullets.Remove(bullet);
                        terrain.GotHit();
                        if (terrain.Hp <= 0)
                        {
                            Terrains.Remove(terrain);
                        }
                    }
                }
                if (bullet.IsCollision(PlayerTank))
                {
                    EnemyTank.Bullets.Remove(bullet);
                    PlayerTank.GotHit();
                    if (PlayerTank.Hp <= 0)
                    {
                        GameOver?.Invoke(this, null);
                    }
                }
                if (bullet.IsCollision(MapFrame))
                {
                    EnemyTank.Bullets.Remove(bullet);
                }
            }
            #endregion
        }
        #endregion
    }
}
