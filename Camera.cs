using SFML.System;

namespace bonheur
{
    public struct Camera
    {
        public Vector2f Position;

        public Camera()
        {
            Position = new Vector2f();
        }

        public Camera(Vector2f position)
        {
            Position = position;
        }
    }
}
