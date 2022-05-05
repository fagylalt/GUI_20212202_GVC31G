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
        public TankType PlayerTankType { get; set; }
        public TankType EnemyTankType { get; set; }

        public event EventHandler Changed;
        public event EventHandler GameOver;

        #region Keys
        List<Key> blueKeysCurrentlyDown;
        List<Key> redKeysCurrentlyDown;
        public Key[] blueMovementKeys = { Key.W, Key.A, Key.S, Key.D };
        public Key[] redMovementKeys = { Key.Up, Key.Down, Key.Right, Key.Left };
        private Key blueMovementKey;
        private Key redMovementKey;
        #endregion

        public enum Controls
        {
            Up, Down, Left, Right, Enter, A, W, S, D, Space
        }

        public void SetUpSizes(int screenWidth, int screenHeight)
        {
            //Resizing all map elements, when window size changed...
        }

        public TankCombatLogic(int screenWidth, int screenHeight, TankType playerTankType, TankType enemyTankType)
        {
            MapFrame = new MapFrame(screenWidth, screenHeight);
            PlayerTank = new Tank(Team.Blue, playerTankType, screenWidth, screenWidth / 5, screenHeight / 2, 90);
            EnemyTank = new Tank(Team.Red, enemyTankType, screenWidth, screenWidth * 4 / 5, screenHeight / 2, 270);
            Terrains = new List<Terrain>();
            Barriers = new List<GameItem>();
            Barriers.Add(PlayerTank);
            Barriers.Add(EnemyTank);
            Barriers.Add(MapFrame);
            foreach (var terrain in Terrains)
            {
                Barriers.Add(terrain);
            }
            blueKeysCurrentlyDown = new();
            redKeysCurrentlyDown = new();
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
                case Controls.W:
                    PlayerTank.Angle = 0;
                    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
                    break;
                case Controls.S:
                    PlayerTank.Angle = 180;
                    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
                    break;
                case Controls.A:
                    PlayerTank.Angle = 270;
                    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
                    break;
                case Controls.D:
                    PlayerTank.Angle = 90;
                    PlayerTank.Move((int)PlayerTank.Angle, Barriers);
                    break;
                case Controls.Space:
                    PlayerTank.Shoot((int)PlayerTank.Angle);
                    break;
                case Controls.Up:
                    EnemyTank.Angle = 0;
                    EnemyTank.Move((int)EnemyTank.Angle, Barriers);
                    break;
                case Controls.Down:
                    EnemyTank.Angle = 180;
                    EnemyTank.Move((int)EnemyTank.Angle, Barriers);
                    break;
                case Controls.Left:
                    EnemyTank.Angle = 270;
                    EnemyTank.Move((int)EnemyTank.Angle, Barriers);
                    break;
                case Controls.Right:
                    EnemyTank.Angle = 90;
                    EnemyTank.Move((int)EnemyTank.Angle, Barriers);
                    break;
                case Controls.Enter:
                    EnemyTank.Shoot((int)EnemyTank.Angle);
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }

        public void GetLastKeyPressed()
        {
            foreach (var key in blueMovementKeys)
            {
                KeyStates currentStatus = Keyboard.GetKeyStates(key);
                if (currentStatus.HasFlag(KeyStates.Down) && !blueKeysCurrentlyDown.Contains(key))
                {
                    blueKeysCurrentlyDown.Add(key);
                }
                if (!currentStatus.HasFlag(KeyStates.Down) && blueKeysCurrentlyDown.Contains(key))
                {
                    blueKeysCurrentlyDown.Remove(key);
                }
            }

            if (blueKeysCurrentlyDown.Count > 0)
            {
                blueMovementKey = blueKeysCurrentlyDown.Last();
            }
            else
            {
                blueMovementKey = Key.None;
            }

            foreach (var key in redMovementKeys)
            {
                KeyStates currentStatus = Keyboard.GetKeyStates(key);
                if (currentStatus.HasFlag(KeyStates.Down) && !redKeysCurrentlyDown.Contains(key))
                {
                    redKeysCurrentlyDown.Add(key);
                }
                if (!currentStatus.HasFlag(KeyStates.Down) && redKeysCurrentlyDown.Contains(key))
                {
                    redKeysCurrentlyDown.Remove(key);
                }
            }

            if (redKeysCurrentlyDown.Count > 0)
            {
                redMovementKey = redKeysCurrentlyDown.Last();
            }
            else
            {
                redMovementKey = Key.None;
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
            if (blueMovementKey == Key.D)
            {
                Control(Controls.D);
            }
            else if (blueMovementKey == Key.A)
            {
                Control(Controls.A);
            }
            else if (blueMovementKey == Key.W)
            {
                Control(Controls.W);
            }
            else if (blueMovementKey == Key.S)
            {
                Control(Controls.S);
            }
            if (Keyboard.IsKeyDown(Key.Space))
            {
                Control(Controls.Space);
            }

            if (redMovementKey == Key.Right)
            {
                Control(Controls.Right);
            }
            else if (redMovementKey == Key.Left)
            {
                Control(Controls.Left);
            }
            else if (redMovementKey == Key.Up)
            {
                Control(Controls.Up);
            }
            else if (redMovementKey == Key.Down)
            {
                Control(Controls.Down);
            }
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                Control(Controls.Enter);
            }

            #region PlayerTank bullets collisions
            if (PlayerTank.Bullets.Count > 0)
            {
                foreach (var bullet in PlayerTank.Bullets.ToList())
                {
                    bullet.Move();
                    if (!EnemyTank.IsRespawning && bullet.IsCollision(EnemyTank))
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
            foreach (var bullet in EnemyTank.Bullets.ToList())
            {
                bullet.Move();
                if (!PlayerTank.IsRespawning && bullet.IsCollision(PlayerTank))
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
                    foreach (var terrain in Terrains.ToList())
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
