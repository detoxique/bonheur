using SFML.Graphics;
using SFML.System;

namespace bonheur
{
    public class Entity : Drawable
    {
        public Vector2f Position;
        public Vector2f Size;

        private int currentAnimation = 0;

        public List<Animation> animationsList = new List<Animation>();

        public bool IsAlive = true, FlipHorizontally = false;

        public Sprite CurrentFrame;

        private float SpriteSizeMultiply = 1.0f;

        public Entity(Vector2f position, Vector2f size) 
        { 
            Position = position;
            Size = size;
        }

        public void Update(float deltaTime)
        {
            if (animationsList.Count > 0 && animationsList[currentAnimation] != null) // updating animation if it exists.
            {
                animationsList[currentAnimation].Update(deltaTime);
                CurrentFrame = animationsList[currentAnimation].GetSprite();
                if (FlipHorizontally != true)
                    CurrentFrame.Scale = new Vector2f(SpriteSizeMultiply, SpriteSizeMultiply);
                else
                    CurrentFrame.Scale = new Vector2f(-SpriteSizeMultiply, SpriteSizeMultiply);
            }
            
            CurrentFrame.Position = Position;
        }

        public void SetSpriteScale(float scale)
        {
            SpriteSizeMultiply = scale;
        }

        public void SetCurrentAnimation(int animation)
        {
            currentAnimation = animation;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (IsAlive)
            {
                target.Draw(CurrentFrame, states);
            }
        }
    }
}
