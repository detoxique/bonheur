using SFML.Graphics;
using SFML.System;

namespace bonheur
{
    public class Entity : Drawable
    {
        public AABB aabb = new AABB();
        public Vector2f Velocity = new Vector2f();
        public Vector2f VelocityMultiply = new Vector2f(0.01f, 0.1f);

        public Vector2f Scale;

        public Vector2f SpriteOffset = new Vector2f();

        private int currentAnimation = 0;
        public string Tag { get; internal set; }

        public List<Animation> animationsList = new List<Animation>();

        public bool IsAlive = true, FlipHorizontally = false, IsOnGround = false, ShowCollider = false;

        public Sprite CurrentFrame = new Sprite();

        private float SpriteSizeMultiply = 1.0f;

        public RectangleShape collider = new RectangleShape();

        public Entity() { Tag = "entity"; }

        public Entity(Vector2f position, Vector2f size) 
        { 
            aabb = new AABB(position, size);
            collider = new RectangleShape(size);
            collider.Position = position;
            collider.FillColor = Color.Transparent;
            collider.OutlineColor = Color.White;
            collider.OutlineThickness = 1;
            Tag = "entity";
        }

        public Entity(Vector2f position, Vector2f size, string tag)
        {
            aabb = new AABB(position, size);
            collider = new RectangleShape(size);
            collider.Position = position;
            collider.FillColor = Color.Transparent;
            collider.OutlineColor = Color.White;
            collider.OutlineThickness = 1;
            Tag = tag;
        }

        public Entity(Vector2f position, Vector2f size, Sprite sprite)
        {
            aabb = new AABB(position, size);
            CurrentFrame = sprite;
            collider = new RectangleShape(size);
            collider.Position = position;
            collider.FillColor = Color.Transparent;
            collider.OutlineColor = Color.White;
            collider.OutlineThickness = 1;
            Tag = "entity";
        }

        public Entity(Vector2f position, Vector2f size, Sprite sprite, string tag)
        {
            aabb = new AABB(position, size);
            CurrentFrame = sprite;
            collider = new RectangleShape(size);
            collider.Position = position;
            collider.FillColor = Color.Transparent;
            collider.OutlineColor = Color.White;
            collider.OutlineThickness = 1;
            Tag = tag;
        }

        public virtual void Update(float deltaTime, Camera camera)
        {
            if (animationsList.Count > 0 && animationsList[currentAnimation] != null) // updating animation if it exists.
            {
                animationsList[currentAnimation].Update(deltaTime);
                CurrentFrame = animationsList[currentAnimation].GetSprite();
            }

            if (FlipHorizontally != true)
            {
                CurrentFrame.Origin = new Vector2f(0, 0);
                CurrentFrame.Scale = new Vector2f(SpriteSizeMultiply, SpriteSizeMultiply);
            }
            else
            {
                CurrentFrame.Origin = new Vector2f(CurrentFrame.TextureRect.Width, 0);
                CurrentFrame.Scale = new Vector2f(-SpriteSizeMultiply, SpriteSizeMultiply);
            }

            CurrentFrame.Position = aabb.Position - camera.Position - SpriteOffset;
            collider.Position = aabb.Position - camera.Position;
        }

        public void SetTag(string tag)
        {
            Tag = tag;
        }

        public void SetSpriteScale(float scale)
        {
            SpriteSizeMultiply = scale;
            Scale = new Vector2f(scale, scale);
        }

        public void SetCurrentAnimation(int animation)
        {
            if (animationsList[currentAnimation].IsPlaying == false && currentAnimation != animation)
            {
                animationsList[currentAnimation].IsPlaying = true;
                animationsList[currentAnimation].CurrentFrame = 0;
            }
            currentAnimation = animation;
        }

        public int GetCurrentAnimation()
        {
            return currentAnimation;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (IsAlive)
            {
                target.Draw(CurrentFrame, states);
                if (ShowCollider)
                    target.Draw(collider, states);
            }
        }
    }
}
