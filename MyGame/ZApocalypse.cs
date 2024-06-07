using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.controllers;
using MyGame.models;
using MyGame.Views;
using MyGame.Views.MyGame.Views;
using System.Collections.Generic;
namespace MyGame
{
    public class ZApocalypse : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SoundPlayer soundPlayer;
        ContentLoader gameContent;
        SpriteFont font;
        GameMap map;
        Hero hero;
        List<Zombie> zombies;
        HeroController controller;
        View view;
        HeroView heroView;
        MapView mapView;
        ZombieView zombieView;
        string message;
        bool playersTurn;
        bool gameOver;
        int level;

        public ZApocalypse()
        {
            playersTurn = true;
            level = 1;
            gameOver = false;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();

            soundPlayer = new SoundPlayer(Content);
            gameContent = new ContentLoader(Content);
            InitializeViews();
            map = new GameMap(10, "level" + level.ToString(), mapView.SchemeMap);
            hero = new Hero();
            zombies = map.FindZombies();
            controller = new HeroController(hero, map, zombies);
            message = "Press R to go to restart level";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        private void InitializeViews()
        {
            view = new View(gameContent.Sprites);
            heroView = new HeroView(gameContent.Sprites);
            zombieView = new ZombieView(gameContent.Sprites);
            mapView = new MapView(10, gameContent.TileSet, "level" + level.ToString());
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (!gameOver)
            {
                if (!playersTurn)
                {
                    HandleZombieTurn(gameTime);
                }
                else
                {
                    HandlePlayerTurn();
                }
                base.Update(gameTime);
            }

            CheckForRestart();
        }

        private void HandleZombieTurn(GameTime gameTime)
        {
            if (!hero.IsAlive(zombies))
            {
                HandleHeroDeath(gameTime);
            }
            else
            {
                MoveZombies();
                playersTurn = true;
            }
        }

        private void HandleHeroDeath(GameTime gameTime)
        {
            hero.Die();
            soundPlayer.PlayAttackSound();
            soundPlayer.PlayDethSound();
            soundPlayer.PlayGameOverSound();
            gameOver = true;
            Draw(gameTime);
        }

        private void MoveZombies()
        {
            foreach (var zombie in zombies)
            {
                if (zombie.IsAlive())
                {
                    zombie.Move(hero, map);
                    soundPlayer.PlayZombieSound();
                }
            }
        }

        private void HandlePlayerTurn()
        {
            if (controller.Update(soundPlayer))
            {
                if (hero.CanExit())
                {
                    HandleHeroExit();
                }
                else
                {
                    playersTurn = false;
                }
            }
        }

        private void HandleHeroExit()
        {
            soundPlayer.PlayWinSound();
            level += 1;
            message = "Press R to go to the next level";
            gameOver = true;
        }

        private void CheckForRestart()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                ReloadGame();
            }
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            mapView.DrawMap(_spriteBatch);
            heroView.DrawHero(_spriteBatch, hero);
            view.DrawAmmoCount(_spriteBatch, hero, gameContent.Font);
            foreach(var zombie in zombies)
                zombieView.DrawZombie(_spriteBatch, zombie);
            if (gameOver && !hero.IsAlive(zombies))
                view.DrawGameOver(_spriteBatch, gameContent.GameOverTexture);
            view.DrawMessage(_spriteBatch, message, gameContent.Font);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ReloadGame()
        {
            InitializeViews();
            map = new GameMap(10, "level" + level.ToString(), mapView.SchemeMap);
            hero = new Hero();
            zombies = map.FindZombies();
            controller = new HeroController(hero, map, zombies);
            playersTurn = true;
            gameOver = false;
            message = "Press R to restart level";
        }


    }
}
