using System.Collections.Generic;
using SFML.Graphics;

namespace bonheur
{
    public class Layer : Transformable, Drawable
    {
        public bool IsActive = true;
        public List<Drawable> Objects = new List<Drawable>();

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (IsActive)
            {
                foreach (Drawable obj in Objects)
                {
                    target.Draw(obj);
                }
            }
        }
    }
}
