using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Close_Contact
{
    public class Logic
    {
        public void Ufo_1Create()
        {
            Random rand = new Random();
            Ufo_1 ufo = new Ufo_1();
            ufo.Create();
            alien.Add(ufo);
            ufo.koord.Y = rand.Next(0, 450);
            ufo.koord.X = -1 * rand.Next(10, 20);
            /*ufo.speed.X = rand.Next(3, 8);
            ufo.speed.Y = 0;*/
        }
        public void Ufo_2Create()
        {
            Random rand = new Random();
            Ufo_2 ufo = new Ufo_2();
            ufo.Create();
            alien.Add(ufo);
            ufo.koord.Y = rand.Next(0, 450);
            ufo.koord.X = -1 * rand.Next(10, 20);
            // ufo.speed.X = rand.Next(3, 8);
            //ufo.speed.Y = 0;
        }
        public void Ufo_3Create()
        {
            Random rand = new Random();
            Ufo_3 ufo = new Ufo_3();
            ufo.Create();
            alien.Add(ufo);
            ufo.koord.Y = rand.Next(0, 450);
            ufo.koord.X = -1 * rand.Next(10, 20);
        }
        public void RandomCreateAlien()
        {
            Random rand = new Random();
            switch (rand.Next(1, 3))
            {
                case 1:
                    Ufo_1Create();
                    break;
                case 2:
                    Ufo_2Create();
                    break;
                case 3:
                    Ufo_3Create();
                    break;
                default:
                    break;
            }
        }
        public void Infanty_Create()
        {
            Random rand = new Random();
            Infanty inf = new Infanty();
            inf.Create();
            ManOWar.Add(inf);
            inf.koord.X = rand.Next(400, 500);
            inf.koord.Y = rand.Next(400, 500);
        }
        public void Hammer_Create()
        {
            Random rand = new Random();
            Hammer ham = new Hammer();
            ham.Create();
            ManOWar.Add(ham);
            ham.koord.X = rand.Next(400, 500);
            ham.koord.Y = rand.Next(20, 400);
        }
        public void Tank_Create()
        {
            Random rand = new Random();
            Tank tank = new Tank();
            tank.Create();
            ManOWar.Add(tank);
            tank.koord.X = rand.Next(400, 500);
            tank.koord.Y = rand.Next(400, 500);
        }
        public void RandomCreateSoilder()
        {
            Random rand = new Random();
            switch (rand.Next(1, 3))
            {
                case 1:
                    Infanty_Create();
                    break;
                case 2:
                    Hammer_Create();
                    break;
                case 3:
                    Tank_Create();
                    break;
                default:
                    break;
            }
        }
        public void Wave(int waveLevel)
        {
            int counterAlien = alien.Count;
            for (int i = counterAlien; i < waveLevel; i++)
            {
                RandomCreateAlien();
            }
        }
        public void UfoAttack()
        {
            if (alien.Count > 0 && ManOWar.Count > 0)
            {
                for (int counter = 0; counter < alien.Count; counter++)
                {
                    alien[counter].MassiveAttack(ManOWar[counter % ManOWar.Count], alien[counter].Damage);
                }
            }
        }
        public void UserAttack()
        {
            if (ManOWar.Count > 0 && alien.Count > 0)
            {
                for (int counter = 0; counter < ManOWar.Count; counter++)
                {
                    ManOWar[counter].AttackLowLevel(alien[counter % alien.Count], 1, ManOWar[counter].Damage);
                    ManOWar[counter].AttackHighLevel(alien[counter % alien.Count], 1, ManOWar[counter].Damage);
                }
            }
        }
        public void ColorDie()
        {
            foreach (var unit in alien)
            {
                if (unit.Health <= 0)
                {
                    unit.color = Color.Red;
                }
            }
            foreach (var unit in ManOWar)
            {
                if (unit.Health <= 0)
                {
                    unit.color = Color.Red;
                }
            }
        }
        public void KilledRemove()
        {
            for (int unit = 0; unit < alien.Count; unit++)
            {
                if (alien[unit].color == Color.Red)
                {
                    alien.RemoveAt(unit);
                    Game.Resource += 100;
                }
            }
            for (int unit = 0; unit < ManOWar.Count; unit++)
            {
                if (ManOWar[unit].color == Color.Red)
                {
                    ManOWar.RemoveAt(unit);
                    unit--;
                }
            }
        }
        public void Battle()
        {
            if (ManOWar.Count > 0 && alien.Count > 0)
            {
                UserAttack();
                UfoAttack();
                ColorDie();
                KilledRemove();
            }
        }
        public List<Soldier> ManOWar = new List<Soldier>();
        public List<Ufo> alien = new List<Ufo>();
        public void logic()
        {
            foreach (var ufo in alien)
            {
                ufo.koord += ufo.speed;
            }
        }
    }
}