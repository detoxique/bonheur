using SFML.Graphics;
using SFML.System;

namespace bonheur
{
    internal class Entity : Drawable
    {
        public Vector2f Position;
        public Vector2f Size;

        private int currentAnimation = 0;

        public List<Animation> animationsList = new List<Animation>();

        public bool IsAlive = true;

        public Sprite CurrentFrame;

        public Entity() 
        { 

        }

        public void Update(float deltaTime)
        {
            animationsList[currentAnimation].Update(deltaTime);
            CurrentFrame = animationsList[currentAnimation].GetSprite();
            CurrentFrame.Position = Position;
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
