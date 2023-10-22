using SFML.Graphics;
using SFML.System;
using bonheur;

namespace bonheur
{
    public enum AnimationDirection
    {
        Right, Down
    }

    public class Animation
    {
        public IntRect FrameSize;
        public IntRect Frame;
        public Texture SpriteSheet;
        public Sprite result = new Sprite();
        public int Step = 0;
        private Vector2f startPosition = new Vector2f();
        public Vector2f Origin { get; set; }

        public bool IsPlaying = true, Loop = false;

        public int CurrentFrame = 0;

        public int Flip, FramesCount;

        public Clock Timer = new Clock();

        public float Speed;

        public AnimationDirection Direction { get; set; } = AnimationDirection.Right;

        public Animation(Texture SpriteSheet, IntRect TextureRect, float Speed, int FramesCount, bool Loop, AnimationDirection animationDirection)
        {
            this.SpriteSheet = SpriteSheet;
            FrameSize = TextureRect;
            this.Speed = Speed;
            this.FramesCount = FramesCount;
            this.Loop = Loop;
            result.Texture = SpriteSheet;
            startPosition = new Vector2f(TextureRect.Left, TextureRect.Top);
            Direction = animationDirection;
        }

        /// <summary>
        /// Updating an animation
        /// </summary>
        public void Update(float deltatime)
        {
            if (IsPlaying)
            {
                if (Timer.ElapsedTime.AsMilliseconds() >= Speed)
                {
                    CurrentFrame++;
                    Timer.Restart();
                }

                if (CurrentFrame > FramesCount && !Loop)
                {
                    IsPlaying = false;
                }

                if (Loop && CurrentFrame > FramesCount)
                {
                    CurrentFrame = 0;
                }

                if (!IsPlaying)
                {
                    CurrentFrame = FramesCount;
                }
            }
            if (Direction == AnimationDirection.Right)
                Frame = new IntRect((int)startPosition.X + CurrentFrame * (FrameSize.Width + Step), FrameSize.Top, FrameSize.Width, FrameSize.Height);
            else
                Frame = new IntRect(FrameSize.Left, (int)startPosition.Y + CurrentFrame * (FrameSize.Height + Step), FrameSize.Width, FrameSize.Height);
        }

        /// <summary>
        /// Makes the animation play
        /// </summary>
        public void Play()
        {
            IsPlaying = true;
        }

        /// <summary>
        /// Makes the animation pause
        /// </summary>
        public void Pause()
        {
            IsPlaying = false;
        }

        /// <summary>
        /// Next animation frame
        /// </summary>
        public void NextFrame()
        {
            if (CurrentFrame < FramesCount)
                CurrentFrame++;
            else
                CurrentFrame = 0;
        }

        /// <summary>
        /// Previous animation frame
        /// </summary>
        public void PreviousFrame()
        {
            if (CurrentFrame - 1 > 0)
                CurrentFrame--;
            else
                CurrentFrame = FramesCount;
        }

        /// <summary>
        /// Returns current sprite
        /// </summary>
        /// <returns>Current sprite</returns>
        public Sprite GetSprite()
        {
            result.TextureRect = Frame;
            //result.Origin = Origin;
            return result;
        }
    }
}