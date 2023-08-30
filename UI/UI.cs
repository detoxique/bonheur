using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace bonheur.UserInterface
{
    public class UI : Transformable, Drawable
    {
        public bool IsActive = true;

        public virtual void Update(float DeltaTime)
        {

        }

        public virtual void MouseCheck(MouseMoveEventArgs e)
        {

        }

        public virtual void MouseClick(MouseButtonEventArgs e)
        {

        }

        public virtual void MouseRelease(MouseButtonEventArgs e)
        {

        }

        public virtual void TextEntered(TextEventArgs e)
        {

        }

        public virtual void KeyPressed(KeyEventArgs e)
        {

        }

        public virtual void MouseWheel(float Delta)
        {

        }

        public virtual void Resized(SizeEventArgs e)
        {

        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            
        }
    }
}
