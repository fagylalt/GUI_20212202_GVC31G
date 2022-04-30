using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public Bullet SingleBullet { get; set; }
        public TankType PlayerTankType { get; set; }
        public TankType EnemyTankType { get; set; }

        public event EventHandler Changed;
        public event EventHandler GameOver;

        #region Keys
        List<Key> keysCurrentlyDown;
        public Key[] movementKeys = { Key.Up, Key.Down, Key.Right, Key.Left };
        //private KeyStates[] oldStates = new KeyStates[mov];
        private List<KeyStates> oldStates;
        private Key movementKey;
        #endregion

        public enum Controls
        {
            Up, Down, Left, Right, Space
        }

        public void SetUpSizes(int screenWidth, int screenHeight)
        {
            //Resizing all map elements, when window size changed...
        }

        public TankCombatLogic(int screenWidth, int screenHeight, TankType playerTankType, TankType enemyTankType)
        {
            MapFrame = new MapFrame(screenWidth, screenHeight);
            PlayerTank = new Tank(Team.Blue, playerTankType, screenWidth, screenWidth / 5, screenHeight / 2, 90);
            EnemyTank = new Tank(Team.Red, enemyTankType, screenWidth, screenWidth * 4 / 5, screenHeight / 2, 180);
            Terrains = new List<Terrain>();
            Barriers = new List<GameItem>();
            Barriers.Add(PlayerTank);
            Barriers.Add(EnemyTank);
            Barriers.Add(MapFrame);
            foreach (var terrain in Terrains)
            {
                Barriers.Add(terrain);
            }
            oldStates = new List<KeyStates>();
            foreach (var _movementKey in movementKeys)
            {
                oldStates.Add(Keyboard.GetKeyStates(_movementKey));
            }
            keysCurrentlyDown = new();
        }
        #endregion

        #region Logic
        public void Control(Controls control)
        {
            //if (control == Controls.Up)
            //{
            //    PlayerTank.Angle = 0;
            //    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
            //}
            //if (control == Controls.Down)
            //{
            //    PlayerTank.Angle = 180;
            //    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
            //}
            //if (control == Controls.Left)
            //{
            //    PlayerTank.Angle = 270;
            //    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
            //}
            //if (control == Controls.Right)
            //{
            //    PlayerTank.Angle = 90;
            //    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
            //}

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

        public void GetLastKeyPressed()
        {
            foreach (var key in movementKeys)
            {
                KeyStates currentStatus = Keyboard.GetKeyStates(key);
                if (currentStatus.HasFlag(KeyStates.Down) && !keysCurrentlyDown.Contains(key))
                {
                    keysCurrentlyDown.Add(key);
                }
                if (!currentStatus.HasFlag(KeyStates.Down) && keysCurrentlyDown.Contains(key))
                {
                    keysCurrentlyDown.Remove(key);
                }
            }

            if (keysCurrentlyDown.Count > 0)
            {
                movementKey = keysCurrentlyDown.Last();
            }
            else
            {
                movementKey = Key.None;
            }

            //bool isAnyKeyPressed = false;
            ////bool isAnyKeyReleased = false;

            //for (int i = 0; i < movementKeys.Length; i++)
            //{
            //    KeyStates newState = Keyboard.GetKeyStates(movementKeys[i]);

            //    if (!newState.HasFlag(KeyStates.Down) && oldStates[i].HasFlag(KeyStates.Down))
            //    {
            //        movementKey = Key.None;
            //        //isAnyKeyReleased = true;
            //    }
            //    if (newState.HasFlag(KeyStates.Down) && oldStates[i].HasFlag(KeyStates.Down))
            //    {
            //        movementKey = movementKeys[i];
            //    }
            //    //if (newState == KeyStates.Down)
            //    //{
            //    //    isAnyKeyPressed = true;
            //    //}

            //    oldStates[i] = newState;
            //}
            ////if (!isAnyKeyPressed)
            ////{
            ////    movementKey = Key.None;
            ////}
        }

        public void TimeStep()
        {
            GetLastKeyPressed();
            if (movementKey == Key.Right)
            {
                Control(Controls.Right);
            }
            else if (movementKey == Key.Left)
            {
                Control(Controls.Left);
            }
            else if (movementKey == Key.Up)
            {
                Control(Controls.Up);
            }
            else if (movementKey == Key.Down)
            {
                Control(Controls.Down);
            }
            if (Keyboard.IsKeyDown(Key.Space))
            {
                Control(Controls.Space);
            }

            #region PlayerTank bullets collisions
            if (PlayerTank.Bullets.Count > 0)
            {
                foreach (var bullet in PlayerTank.Bullets.ToList())
                {
                    bullet.Move();
                    if (bullet.IsCollision(EnemyTank))
                    {
                        PlayerTank.Bullets.Remove(bullet);
                        EnemyTank.GotHit(PlayerTank.Damage);
                        if (EnemyTank.Lives <= 0)
                        {
                            GameOver?.Invoke(this, null);
                        }

                    }
                    else if (bullet.IsCollision(MapFrame))
                    {
                        PlayerTank.Bullets.Remove(bullet);
                    }
                    else
                    {
                        foreach (var terrain in Terrains.ToList())
                        {
                            if (bullet.IsCollision(terrain))
                            {
                                PlayerTank.Bullets.Remove(bullet);
                                terrain.GotHit(PlayerTank.Damage);
                                if (terrain.Hp <= 0)
                                {
                                    Terrains.Remove(terrain);
                                    Barriers.Remove(terrain);
                                }
                            }
                        }
                    }
                    
                }
            }
            #endregion

            #region EnemyTank bullets collisions
            foreach (var bullet in EnemyTank.Bullets)
            {
                bullet.Move();
                if (bullet.IsCollision(PlayerTank))
                {
                    EnemyTank.Bullets.Remove(bullet);
                    PlayerTank.GotHit(EnemyTank.Damage);
                    if (PlayerTank.Lives <= 0)
                    {
                        GameOver?.Invoke(this, null);
                    }
                }
                else if (bullet.IsCollision(MapFrame))
                {
                    EnemyTank.Bullets.Remove(bullet);
                }
                else
                {
                    foreach (var terrain in Terrains)
                    {
                        if (bullet.IsCollision(terrain))
                        {
                            EnemyTank.Bullets.Remove(bullet);
                            terrain.GotHit(PlayerTank.Damage);
                            if (terrain.Hp <= 0)
                            {
                                Terrains.Remove(terrain);
                                Barriers.Remove(terrain);
                            }
                        }
                    }
                }
            }
            #endregion
        }
        #endregion
    }
}
