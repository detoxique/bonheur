using SFML.System;
using System;

namespace bonheur
{
    public struct AABB
    {
        public Vector2f Position;
        public Vector2f Size;
        public AABB()
        {
            Position = new Vector2f();
            Size = new Vector2f();
        }
        public AABB(Vector2f position, Vector2f size)
        {
            Position = position;
            Size = size;
        }
    }

    abstract public class Physics
    {
        private static List<Entity> staticObjects = new List<Entity>();
        private static List<Entity> dynamicObjects = new List<Entity>();

        public static bool IsGravityEnabled = true;

        private static float GravityForce = 100, GravityMultiply = 1.0f;

        public static void AddDynamicObject(Entity entity)
        {
            dynamicObjects.Add(entity);
        }

        public static void AddStaticObject(Entity entity)
        {
            staticObjects.Add(entity);
        }

        public static void Update(float dt)
        {
            //foreach (Entity entity in dynamicObjects)
            //{
            //    // X
            //    entity.Velocity = new Vector2f(entity.Velocity.X * entity.VelocityMultiply.X * 0.05f * dt, entity.Velocity.Y);
            //    if (entity.VelocityMultiply.X < 1)
            //        entity.VelocityMultiply += new Vector2f(0.01f, 0);

            //    bool collides = false;

            //    Vector2f newpos = new Vector2f(entity.aabb.Position.X + entity.Velocity.X * dt, entity.aabb.Position.Y);
            //    AABB aabb = new AABB(newpos, entity.aabb.Size);

            //    foreach (Entity entity2 in staticObjects)
            //    {
            //        if (Intersects(aabb, entity2.aabb))
            //        {
            //            collides = true;
            //            entity.Velocity = new Vector2f(entity.Velocity.X * 0.001f, entity.Velocity.Y);
            //            entity.VelocityMultiply = new Vector2f(0.01f, entity.VelocityMultiply.Y);
            //            break;
            //        }
            //    }

            //    if (!collides)
            //    {
            //        entity.aabb.Position = newpos;
            //        //entity.Velocity *= 0.05f * dt;
            //    }

            //    // Y
            //    entity.Velocity = new Vector2f(entity.Velocity.X, entity.Velocity.Y * entity.VelocityMultiply.Y * 0.05f * dt);
            //    if (entity.VelocityMultiply.Y < 1)
            //        entity.VelocityMultiply += new Vector2f(0, 0.05f);

            //    collides = false;

            //    if (entity.IsOnGround)
            //    {
            //        newpos = new Vector2f(entity.aabb.Position.X, entity.aabb.Position.Y + entity.Velocity.Y * dt * 8); 
            //        GravityMultiply = 1.0f;
            //    }
            //    else
            //    {
            //        newpos = new Vector2f(entity.aabb.Position.X, entity.aabb.Position.Y + (entity.Velocity.Y + GravityForce) * entity.VelocityMultiply.Y * dt * 8); 
            //        GravityMultiply += 0.01f;
            //    }

            //    aabb = new AABB(newpos, entity.aabb.Size);

            //    foreach (Entity entity2 in staticObjects)
            //    {
            //        if (Intersects(aabb, entity2.aabb))
            //        {
            //            collides = true;
            //            entity.IsOnGround = true;
            //            entity.Velocity = new Vector2f(entity.Velocity.X, entity.Velocity.Y * 0.001f);
            //            entity.VelocityMultiply = new Vector2f(entity.VelocityMultiply.X, 0.1f);
            //            break;
            //        }
            //    }

            //    if (!collides)
            //    {
            //        entity.IsOnGround = false;
            //        entity.aabb.Position = newpos;
            //    }
            //    Console.WriteLine(entity.VelocityMultiply.Y);
            //}

            foreach (Entity entity in dynamicObjects)
            {
                // X axis
                bool collides = false;

                Vector2f newposition = new Vector2f(entity.aabb.Position.X + entity.Velocity.X * dt, entity.aabb.Position.Y);
                AABB aabb = new AABB(newposition, entity.aabb.Size);

                foreach (Entity entity2 in staticObjects)
                {
                    if (Intersects(aabb, entity2.aabb))
                    {
                        collides = true;
                        entity.Velocity = new Vector2f(entity.Velocity.X * 0.5f, entity.Velocity.Y);
                        break;
                    }
                }

                if (!collides)
                {
                    entity.aabb.Position = newposition;
                }

                entity.Velocity = new Vector2f(entity.Velocity.X * 0.01f * dt, entity.Velocity.Y);

                // Y axis
                collides = false;

                newposition = new Vector2f(entity.aabb.Position.X, entity.aabb.Position.Y + entity.Velocity.Y * dt);
                aabb = new AABB(newposition, entity.aabb.Size);

                foreach (Entity entity2 in staticObjects)
                {
                    if (Intersects(aabb, entity2.aabb))
                    {
                        collides = true;

                        if (entity.Velocity.Y > 0)
                        {
                            entity.IsOnGround = true;
                            GravityMultiply = 0;
                        }

                        entity.Velocity = new Vector2f(entity.Velocity.X, entity.Velocity.Y * 0.5f);
                        break;
                    }
                }

                if (entity.Velocity.Y < 0)
                    GravityMultiply = 0;

                if (!collides)
                {
                    GravityMultiply += 0.1f;
                    entity.IsOnGround = false;
                    entity.aabb.Position = newposition;
                }

                entity.Velocity = new Vector2f(entity.Velocity.X, entity.Velocity.Y * 0.01f * dt + GravityForce * GravityMultiply * 150 * dt);
            }

            Console.WriteLine(dt);
        }

        public static bool Intersects(AABB aabb1, AABB aabb2)
        {
            if (aabb1.Position.X > aabb2.Position.X + aabb2.Size.X || aabb1.Position.X + aabb1.Size.X < aabb2.Position.X 
                || aabb1.Position.Y > aabb2.Position.Y + aabb2.Size.Y || aabb1.Position.Y + aabb1.Size.Y < aabb2.Position.Y)
            {
                return false;
            }

            return true;
        }
    }
}
