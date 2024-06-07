using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Views
{
    public class SoundPlayer
    {
        public readonly SoundEffect ZombieSound;
        public readonly SoundEffect MovmentSound;
        public readonly SoundEffect DethSound;
        public readonly SoundEffect AttackSound;
        public readonly SoundEffectInstance ZombieInstance;
        public readonly SoundEffect GameOverSound;
        public readonly SoundEffect ShootingSound;
        public readonly SoundEffect WinSound;



        public SoundPlayer(ContentManager content)
        {
            ZombieSound = content.Load<SoundEffect>("zombies");
            MovmentSound = content.Load<SoundEffect>("movment");
            DethSound = content.Load<SoundEffect>("player_die");
            AttackSound = content.Load<SoundEffect>("attack");
            GameOverSound = content.Load<SoundEffect>("gameOverSound");
            ShootingSound = content.Load<SoundEffect>("shooting");
            WinSound = content.Load<SoundEffect>("win");
        }

        public void PlayZombieSound()
        {
            ZombieSound.Play();
        }

        public void PlayMovmentSound()
        {
            MovmentSound.Play();
        }

        public void PlayDethSound()
        {
            DethSound.Play();
        }

        public void PlayAttackSound()
        {
            AttackSound.Play();
        }

        public void PlayGameOverSound()
        {
            GameOverSound.Play();
        }


        public void PlayShootingSound()
        {
            ShootingSound.Play();
        }

        public void PlayWinSound()
        {
            WinSound.Play();
        }

    }
}
