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
   public class Ufo
    {
        public int Power { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public Vector2 speed = new Vector2(0.5f,0);
        public Vector2 koord;
        public Texture2D texture;
        public Color color=Color.White;

        public void MassiveAttack(Soldier soldier, int Damage)
        {
            soldier.Health -= Damage;
        }
        public void OneAttack(int Damage)
        {

        }
        public Random Random()
        {
            Random random = new Random((int)DateTime.Now.Millisecond);
            return random;
        }
    }
    public class Ufo_1 : Ufo
    {
        public int number;
        public void Create()
        {
            number++;
            Name = "Star Warrior Light " + number;
            Power = 20;
            Health = 60;
            IsLive = true;
            Damage = Random().Next(Power - 2, Power + 2);
        }
    }
    class Ufo_2 : Ufo
    {
        public int number;
        Random random = new Random();
        public void Create()
        {
            number++;
            Name = "Star Warrior Medium " + number;
            Power = 40;
            Health = 200;
            IsLive = true;
            Damage = Random().Next(Power - 2, Power + 20);
        }
    }
    class Ufo_3 : Ufo
    {
        public int number;
        Random random = new Random();
        public void Create()
        {
            number++;
            Name = "Star Warrior Heavy " + number;
            Power = 80;
            Health = 400;
            IsLive = true;
            Damage = Random().Next(Power - 2, Power + 10);
        }
    }
}