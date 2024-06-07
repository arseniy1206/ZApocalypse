
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyGame.models;
using MyGame.Views;


namespace MyGame.controllers
{
    internal class HeroController
    {
        Hero hero;
        GameMap map;
        List<Zombie> zombies;

        private KeyboardState previousKeyboardState;
        private MouseState previousMouseState;
        public HeroController(Hero hero, GameMap map, List<Zombie> zombies)
        {
            this.hero = hero;
            this.map = map;
            this.zombies = zombies;
        }

        public bool Update(SoundPlayer soundPlayer)
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            MouseState currentMouseState = Mouse.GetState();
            if(currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed && hero.ammoCount>0)
            {
                foreach(var zombie in zombies)
                {
                    if (ClickOnZombie(zombie, currentMouseState) && ZombieOnShootingLine(zombie))
                    {
                        soundPlayer.PlayShootingSound();
                        hero.Shoot(zombie);
                        zombie.Die();
                    }
                }
            }
            

            if (currentKeyboardState.IsKeyDown(Keys.W) && !previousKeyboardState.IsKeyDown(Keys.W) && hero.CanMove(Direction.Up, map))
            {
                MoveHero(0, -1);
                soundPlayer.PlayMovmentSound();
                return true;
            }

            if (currentKeyboardState.IsKeyDown(Keys.S) && !previousKeyboardState.IsKeyDown(Keys.S) && hero.CanMove(Direction.Down, map))
            {
                MoveHero(0, 1);
                soundPlayer.PlayMovmentSound();
                return true;
            }

            if (currentKeyboardState.IsKeyDown(Keys.D) && !previousKeyboardState.IsKeyDown(Keys.D) && hero.CanMove(Direction.Right, map))
            {
                MoveHero(1, 0);
                soundPlayer.PlayMovmentSound();
                return true;
            }

            if (currentKeyboardState.IsKeyDown(Keys.A) && !previousKeyboardState.IsKeyDown(Keys.A) && hero.CanMove(Direction.Left, map))
            {
                MoveHero(-1, 0);
                soundPlayer.PlayMovmentSound();
                return true;
            }

            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
            hero.velocity = Vector2.Zero;
            return false;
        }

        private bool ClickOnZombie(Zombie zombie, MouseState mouseState)
        {
            return (zombie.position.X <= mouseState.X && mouseState.X <= zombie.position.X + 52)
                && (zombie.position.Y <= mouseState.Y && mouseState.Y <= zombie.position.Y + 72);
        }

        private bool ZombieOnShootingLine(Zombie zombie)
        {
            return (hero.X == zombie.X || hero.Y == zombie.Y);
        }

        private void MoveHero(int deltaX, int deltaY)
        {
            map.SchemeMap[hero.Y][hero.X] = 'f';
            hero.Move(deltaX, deltaY);
            hero.X += deltaX;
            hero.Y += deltaY;
            map.SchemeMap[hero.Y][hero.X] = 'p';
            previousKeyboardState = Keyboard.GetState();
        }
    }
}
