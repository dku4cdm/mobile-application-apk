using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Timers;

namespace Close_Contact
{
    public class Soldier
    {
        Random random = new Random();
        public int Power { get; set; }
        public int Reload { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public Vector2 koord;
        public Texture2D texture;
        public Color color= Color.White;
        public void AttackHighLevel(Ufo ufo, int Reload, int Damage)
        {
            ufo.Health -= Damage * Reload - 5;
            ufo.Damage -= 1;
        }
        public void AttackLowLevel(Ufo ufo, int Reload, int Damage)
        {
            ufo.Health -= Damage * Reload;
        }
        public void Repair(Infanty infanty)
        {
            infanty.Health += 10;
        }
        public Random Random()
        {
            Random random = new Random((int)DateTime.Now.Millisecond);
            return random;
        }
    }
    public class Infanty : Soldier
    {
        public int number;
        public void Create()
        {
            number++;
            Cost = 300;
            Name = "Galaxy Cat " + number;
            Power = 20;
            Health = 100;
            Reload = 2;
            IsLive = true;
            Damage = Random().Next(Power - 4, Power + 7);
        }
    }
    class Hammer : Soldier
    {
        public int number;
        Random random = new Random();
        public void Create()
        {
            number++;
            Cost = 500;
            Name = "Mercedes " + number;
            Power = 50;
            Health = 200;
            Reload = 2;
            IsLive = true;
            Damage = Random().Next(Power - 5, Power + 10);
        }
    }
    class Tank : Soldier
    {
        public int number;
        Random random = new Random();
        public void Create()
        {
            number++;
            Cost = 800;
            Name = "Leopard " + number;
            Power = 200;
            Health = 2500;
            Reload = 1;
            IsLive = true;
            Damage = Random().Next(Power - 5, Power + 20);
        }
    }
}