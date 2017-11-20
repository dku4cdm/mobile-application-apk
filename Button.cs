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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Close_Contact
{
    class Button
    {
        public Vector2 koord;
        public Texture2D texture;
        public Color color;
        public bool PressStatus = false;
        public delegate void Event(Object sender, Vector2 _koord);
        public event Event click;
        public event Event leave;
        public bool click_ = false;
        public Button()
        {

        }
        public Button(Vector2 _koord, Color _color)
        {
            koord = _koord;
            color = _color;
        }
        public void Click(int x, int y)
        {
            click_ = true;
            click(this,new Vector2(x,y));
        }

        public void Leave(int x, int y)
        {
            if (click_)
            {
                click_ = false;
                leave(this, new Vector2(x, y));
            }
        }
    }
}