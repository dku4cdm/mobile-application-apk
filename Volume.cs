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
    class Volume
    {
        public Vector2 koordLine;
        public Vector2 koordSlider;
        public Vector2 koordButtonOffOn;
        public Button ButtonOffOn;
        public Texture2D Line;
        public Texture2D Slider;
        public float VolumeSound;
        public bool SoundStatus = true;
        bool touchSlider = false;
        public delegate void SliderEvent(Object sender, Vector2 koord);
        public event SliderEvent SvipeSlider;
        public event SliderEvent leaveSlider;
        public Volume()
        {

        }
        public Volume( Vector2 _koordLine,
             Vector2 _koordSlider,
            Button _buttonOffOn)
        {
            koordLine = _koordLine;
            koordSlider = _koordSlider;
            ButtonOffOn = _buttonOffOn;
        }
    }
}