using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.Collections.Generic;
using System;
using System.Threading;
using System.ComponentModel;
namespace Close_Contact
{
    using System.IO;
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public static int Resource = 1000;
        private int speedResource = 100;
        SpriteFont textType;
        SpriteBatch spriteBatch;
        Texture2D bacground;
        Texture2D bacgroundGame;
        Texture2D warriorTexture;
        Texture2D alienTexture;
        private int alienTextureX=0;
        private int alienTextureY = 0;
        private int alienTextureWidth = 18;
        private int alienTextureHeigth = 43;
        private int percent = 0;
        private int warriorTextureX = 0;
        private int warriorTextureY = 0;
        private int warriorTextureWidth = 30;
        private int warriorTextureHeigth = 43;
        ClassScoreManager bestScore = new ClassScoreManager();
        Song Bay;
        Song bacgroundGameProces;
        Song MenuMusic;
        Song StartRocket;
        bool StartRocketFlag=false;
        Song ButtonMenuClick;
        private bool MenuMusicStatus = false;
        Button TimeBonus = new Button(new Vector2(0,380), Color.White);
        Button UpgradeSoilder = new Button(new Vector2(50,50),Color.White);
        Button StartButton = new Button(new Vector2(50,50),Color.White);
        Button InfinityButton = new Button(new Vector2(50, 150), Color.White);
        Button Base = new Button(new Vector2(650,150), Color.White);    
        Button OptionGameButton = new Button(new Vector2(0,0),Color.White);
        Button ExitButton = new Button(new Vector2(50, 350), Color.White);
        Button UpgradeMine = new Button(new Vector2(350,100),Color.White);
        Volume volume = new Volume(new Vector2(130,150),new Vector2(600,175),new Button(new Vector2(150,50),Color.White));
        Logic gameLogic= new Logic();
        private int Wave = 1;
        private bool bacgroundGameMusicStatus = false;
        public bool gameStatus = false;
        int GameCurentPosition = 1;
        int GamePreviousPosition = 1;
        int BaseX = 0;
        int BaseY=0;
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }
        protected override void Initialize()
        {

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MenuMusic = Content.Load<Song>("MenuMusic.mp3");
            ButtonMenuClick = Content.Load<Song>("buttonAudio.wav");
            TimeBonus.texture = Content.Load<Texture2D>("bonus.jpg");
            StartButton.texture = Content.Load<Texture2D>("btn_start.png");
            InfinityButton.texture = Content.Load<Texture2D>("btn_start_surv.png");
            ExitButton.texture = Content.Load<Texture2D>("btn_exit.png");
            Base.texture = Content.Load<Texture2D>("ship.png");
            bacground = Content.Load<Texture2D>("fon.jpg");
            bacgroundGame = Content.Load<Texture2D>("background.jpg");
            StartRocket = Content.Load<Song>("Rocket.mp3");
            warriorTexture = Content.Load<Texture2D>("soldier.png");
            alienTexture = Content.Load<Texture2D>("alien1.png");
            UpgradeSoilder.texture = Content.Load<Texture2D>("SoilderBay.png");
            textType = Content.Load<SpriteFont>("Font");
            bacgroundGameProces = Content.Load<Song>("backgroundGameProces.mp3");
            UpgradeMine.texture = Content.Load<Texture2D>("mineAdd.png");
            Bay = Content.Load<Song>("money.wav");
            StartButton.click += ClickButton;
            ExitButton.click += ClickButton;
            InfinityButton.click += ClickButton;
            StartButton.leave += LeaveButtonStart;
            ExitButton.leave += LeaveButtonExit;
            InfinityButton.leave += LeaveButtonInfinity;
            Base.click += ClickBase;
            UpgradeSoilder.click += ClickUpgradeSoilder;
            UpgradeSoilder.leave += LeaveUpgradeSoilder;
            UpgradeMine.click += ClickUpgradeMain;
            UpgradeMine.leave += LeaveUpgradeMain;
            TimeBonus.click += ClickButtonBonus;
            TimeBonus.leave += LeaveButtonBonus;
        }
        private void ClickButtonBonus(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;
            timeButton.color = Color.Gray;
            //GamePreviousPosition = GameCurentPosition;
            //GameCurentPosition = 2;
        }
        private void LeaveButtonBonus(object sender, Vector2 e)
        {
            Random rand = new Random();
            Button timeButton = (Button)sender;
            timeButton.color = Color.White;
            if (percent>=20&&Resource>=100000)
            {
                percent -= rand.Next(0, 50);
                if (percent < 0) percent = 0;
                Resource -= 100000;
            }
            //GamePreviousPosition = GameCurentPosition;
            //GameCurentPosition = 2;
        }
        private void ClickButton(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;
            timeButton.color = Color.Gray;
            //GamePreviousPosition = GameCurentPosition;
            //GameCurentPosition = 2;
        }
        private void ClickUpgradeSoilder(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;
        }

        private void LeaveUpgradeSoilder(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;
            if (Resource - 500 >= 0)
            {
                Resource -= 500;
                gameLogic.RandomCreateSoilder();

                MediaPlayer.Play(Bay);
            }
        }

        private void ClickUpgradeMain(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;

        }

        private void LeaveUpgradeMain(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;

            if (Resource - 1000 >= 0)
            {
                Resource -= 1000;
                speedResource += 150;
                MediaPlayer.Play(Bay);


            }
        }
        private void ClickBase(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;

            GamePreviousPosition = GameCurentPosition;
            GameCurentPosition = 3;

            MediaPlayer.IsRepeating = false;
            bacgroundGameMusicStatus = false;
            MediaPlayer.Stop();
        }
        private void LeaveButtonStart(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;
            timeButton.color = Color.White;
            GamePreviousPosition = GameCurentPosition;
            bestScore.ReadScores();
            gameStatus = true;
            GameCurentPosition = 2;
            MenuMusicStatus = false;
            bacgroundGameMusicStatus = false;
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(ButtonMenuClick);
             alienTextureX = 0;
             alienTextureY = 0;
             alienTextureWidth = 18;
             alienTextureHeigth = 43;
            speedResource = 100;
             percent = 0;
             StartRocketFlag = false;
             MenuMusicStatus = false;


        Resource = 1000;
            Base.koord = new Vector2(650, 150);
            gameLogic = new Logic();
            Wave = 1;
            BaseX = 0;
            BaseY = 0;

            for (int i = 0; i < 5; ++i)
            {
                Thread.Sleep(150);
                gameLogic.Hammer_Create();
            }
        }
        private void LeaveButtonInfinity(object sender, Vector2 e)
        {

            bacgroundGameMusicStatus = false;
            Button timeButton = (Button)sender;
            timeButton.color = Color.White;
            GamePreviousPosition = GameCurentPosition;
            gameStatus = true;
            GameCurentPosition = 2;
            MenuMusicStatus = false;
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(ButtonMenuClick);
        }

        private void LeaveButtonExit(object sender, Vector2 e)
        {
            Button timeButton = (Button)sender;
            timeButton.color = Color.White;

            MediaPlayer.IsRepeating = false;
            MediaPlayer.Play(ButtonMenuClick);
            this.Exit();
            MenuMusicStatus = true;
            MediaPlayer.Play(MenuMusic);
            MediaPlayer.IsRepeating = true;
        }
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                GameCurentPosition=GamePreviousPosition;

            var tc = TouchPanel.GetState();
            switch (GameCurentPosition)
            {
                case (1):
                    {
                        if (tc.Count > 0)
                        {
                            if (tc[0].Position.X > StartButton.koord.X && tc[0].Position.X < StartButton.koord.X + StartButton.texture.Width &&
                                tc[0].Position.Y > StartButton.koord.Y && tc[0].Position.Y < StartButton.koord.Y + StartButton.texture.Height)
                            {
                                StartButton.Click((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                InfinityButton.Leave((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                ExitButton.Leave((int)tc[0].Position.X, (int)tc[0].Position.Y);
                            }
                            else if (tc[0].Position.X > InfinityButton.koord.X && tc[0].Position.X < InfinityButton.koord.X + InfinityButton.texture.Width &&
                                tc[0].Position.Y > InfinityButton.koord.Y && tc[0].Position.Y < InfinityButton.koord.Y + InfinityButton.texture.Height)
                            {
                                StartButton.Leave((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                InfinityButton.Click((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                ExitButton.Leave((int)tc[0].Position.X, (int)tc[0].Position.Y);
                            }
                            else if (tc[0].Position.X > ExitButton.koord.X && tc[0].Position.X < ExitButton.koord.X + ExitButton.texture.Width &&
                                tc[0].Position.Y > ExitButton.koord.Y && tc[0].Position.Y < ExitButton.koord.Y + ExitButton.texture.Height)
                            {
                                StartButton.Leave((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                InfinityButton.Leave((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                ExitButton.Click((int)tc[0].Position.X, (int)tc[0].Position.Y);
                            }
                        }
                        else
                        {

                            StartButton.Leave(-1,-1);
                            InfinityButton.Leave(-1,-1);
                            ExitButton.Leave(-1,-1);
                        }
                        break;
                    }
                case (2):
                    {
                        if (gameStatus)
                        {
                            Random rand = new Random();
                            if(gameTime.TotalGameTime.Milliseconds%123==0)
                            switch (rand.Next(0, 5))
                            {
                                case 0:
                                    {
                                        Base.color = Color.White;
                                        break;
                                    }
                                case 1:
                                    {
                                        Base.color = Color.Blue;
                                        break;
                                    }
                                case 2:
                                    {
                                        Base.color = Color.Red;
                                        break;
                                    }
                                case 3:
                                    {
                                        Base.color = Color.Green;
                                        break;
                                    }
                                case 4:
                                    {
                                        Base.color = Color.Orange;
                                        break;
                                    }
                                case 5:
                                    {
                                        Base.color = Color.Salmon;
                                        break;
                                    }

                            }
                            if(!bacgroundGameMusicStatus)
                            {
                                bacgroundGameMusicStatus = true;
                                MediaPlayer.IsRepeating = true;
                                MediaPlayer.Play(bacgroundGameProces);
                            }
                            if (gameTime.TotalGameTime.Milliseconds%235==0)
                            {
                                ++percent;              
                            }
                            if(gameTime.TotalGameTime.Milliseconds%99==0)
                            gameLogic.Battle();
                            if(gameTime.TotalGameTime.Milliseconds%99==0)
                            Resource += speedResource;
                            gameLogic.Wave(Wave);
                            GamePreviousPosition = 1;
                            if (gameTime.TotalGameTime.Seconds % 3 == 0 && gameTime.TotalGameTime.Milliseconds % 97 == 0)
                                ++Wave;
                            gameLogic.logic();
                            if (gameLogic.alien.Count != 0)
                                if (gameLogic.alien[0].koord.X % 3 == 0)
                                {
                                    alienTextureX += alienTextureWidth + 7;
                                    if (alienTextureX >= alienTexture.Width) alienTextureX = 0;
                                }

                            if (gameLogic.ManOWar.Count != 0)
                                if ( gameTime.TotalGameTime.Milliseconds%75==0)
                                {
                                    warriorTextureX += warriorTextureWidth+5;
                                    if (warriorTextureX >= warriorTexture.Width/4) warriorTextureX = 0;
                                }
                            if (tc.Count > 0)
                            {
                                if (tc[0].Position.X > Base.koord.X && tc[0].Position.X < Base.koord.X + Base.texture.Width &&
                                    tc[0].Position.Y > Base.koord.Y && tc[0].Position.Y < Base.koord.Y + Base.texture.Height/2)
                                {
                                    Base.Click((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                    TimeBonus.Leave(-1, -1);
                                }
                                if (tc[0].Position.X > TimeBonus.koord.X && tc[0].Position.X < TimeBonus.koord.X + TimeBonus.texture.Width &&
                                   tc[0].Position.Y > TimeBonus.koord.Y && tc[0].Position.Y < TimeBonus.koord.Y + Base.texture.Height / 2)
                                {
                                    TimeBonus.Click((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                }
                                else TimeBonus.Leave(-1, -1);
                            }
                            else TimeBonus.Leave(-1, -1); 
                        }
                        if (gameLogic.alien.Count != 0)
                            if (gameLogic.alien[0].koord.X >= 550|| percent==100)
                        {
                            EndGameScript();
                        }
                        //EndGameScript();

                        break;
                    }


                case (3):
                    {

                        if (tc.Count > 0)
                        {
                            if (tc[0].Position.X > UpgradeSoilder.koord.X && tc[0].Position.X < UpgradeSoilder.koord.X + UpgradeSoilder.texture.Width &&
                                tc[0].Position.Y > UpgradeSoilder.koord.Y && tc[0].Position.Y < UpgradeSoilder.koord.Y + UpgradeSoilder.texture.Height)
                            {

                                UpgradeMine.Leave(-1, -1);
                                UpgradeSoilder.Click((int)tc[0].Position.X, (int)tc[0].Position.Y);
                            }
                            else if(tc[0].Position.X > UpgradeMine.koord.X && tc[0].Position.X < UpgradeMine.koord.X + UpgradeMine.texture.Width &&
                                tc[0].Position.Y > UpgradeMine.koord.Y && tc[0].Position.Y < UpgradeMine.koord.Y + UpgradeMine.texture.Height)
                            {

                                UpgradeMine.Click((int)tc[0].Position.X, (int)tc[0].Position.Y);
                                UpgradeSoilder.Leave(-1,-1);
                            }
                            else 
                            {
                                UpgradeMine.Leave(-1, -1);
                                UpgradeSoilder.Leave(-1, -1);
                            }
                        }

                        else
                        {
                            UpgradeMine.Leave(-1, -1);
                            UpgradeSoilder.Leave(-1, -1);
                        }
                        break;
                    }

                case (4):
                    {

                        break;
                    }
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.White);
            switch (GameCurentPosition)
            {
                case (1):
                    {
                        DrawMenu();
                        break;
                    }

                case (2):
                    {
                        DrawGameProces();
                        break;
                    }

                case (3):
                    {
                        DrawShop();
                        break;
                    }
            }
            base.Draw(gameTime);
        }
        private void DrawMenu()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(bacground, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(StartButton.texture,StartButton.koord,StartButton.color);
            spriteBatch.Draw(InfinityButton.texture,InfinityButton.koord,InfinityButton.color);
            spriteBatch.Draw(ExitButton.texture, ExitButton.koord,ExitButton.color);
            spriteBatch.End();
            if(!MenuMusicStatus)
            {
                MenuMusicStatus = true;
                MediaPlayer.Play(MenuMusic);
                MediaPlayer.IsRepeating = true;
            }
        }
        private void DrawGameProces()
        {
            int n = gameLogic.ManOWar.Count;
            int m = gameLogic.alien.Count;
            spriteBatch.Begin();
            spriteBatch.Draw(bacgroundGame,new Vector2(0,0), Color.White);
            spriteBatch.Draw(TimeBonus.texture, TimeBonus.koord, TimeBonus.color);
            spriteBatch.DrawString(textType,"Speed:" + Convert.ToString(speedResource), new Vector2(550, 0), Color.Black);
            spriteBatch.DrawString(textType, "Soilder:" + Convert.ToString(gameLogic.ManOWar.Count), new Vector2(450,0), Color.Black);
            spriteBatch.Draw(Base.texture, Base.koord, new Rectangle(BaseX,BaseY,Base.texture.Width,Base.texture.Height/2), Base.color);
            spriteBatch.DrawString(textType,"Percent: "+Convert.ToString(percent), new Vector2(650, 450), Color.Red);

            for (int i=0;i<n;++i)
                {
                    spriteBatch.Draw(warriorTexture,
                        gameLogic.ManOWar[i].koord,
                        new Rectangle(warriorTextureX, warriorTextureY, warriorTextureWidth, warriorTextureHeigth),
                        gameLogic.ManOWar[i].color,
                        0,
                        Vector2.Zero,
                        0.8f,
                        SpriteEffects.FlipHorizontally,
                        0);
                }
                for (int i=0;i<m;++i)
                {
                spriteBatch.Draw(alienTexture,
                    gameLogic.alien[i].koord,
                    new Rectangle(alienTextureX,alienTextureY,alienTextureWidth,alienTextureHeigth),
                    gameLogic.alien[i].color,
                        0,
                        Vector2.Zero, 
                        1f,
                        SpriteEffects.FlipHorizontally,
                        0);
                }
            spriteBatch.DrawString(textType, "Resource:" + Convert.ToString(Resource), new Vector2(225, 0), Color.Black);
            spriteBatch.End();

        }
        private void EndGameScript()
        {
            //gameStatus = false;
            Base.koord -= new Vector2(0, 3);
            BaseY = Base.texture.Height / 2;
            gameStatus = false;
            if (!StartRocketFlag)
            {
                StartRocketFlag = true;
                MediaPlayer.IsRepeating = false;
                MediaPlayer.Play(StartRocket);
            } 
            if (Base.koord.Y+Base.texture.Height/2<0)
            {
                /* if (bestScore.Score.Value < Resource)
                 {
                     bestScore.Score.Value = Resource;
                     bestScore.WriteScores();
                 }*/
                //DrawEndGame();
                GameCurentPosition = 1;
            }
        }
        private void DrawEndGame()
        {

        }
        private void DrawShop()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(UpgradeSoilder.texture,UpgradeSoilder.koord,UpgradeSoilder.color);
            spriteBatch.DrawString(textType, "Price: 500 \n" + "Count:" + Convert.ToString(gameLogic.ManOWar.Count),new Vector2(100,UpgradeSoilder.texture.Height+UpgradeSoilder.koord.Y),Color.Black);
            spriteBatch.DrawString(textType,"Resource:" + Convert.ToString(Resource), new Vector2(335,25 ), Color.Black);
            spriteBatch.DrawString(textType, "Price: 1000 \n" + "Speed:" + Convert.ToString(speedResource), new Vector2(390, UpgradeMine.texture.Height + UpgradeMine.koord.Y), Color.Black);
            spriteBatch.Draw(UpgradeMine.texture,UpgradeMine.koord,UpgradeMine.color);
            spriteBatch.End();
        }
    }
}
