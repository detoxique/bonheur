using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace bonheur.UserInterface
{
    public class Button : UI
    {
        public Color BaseColor = new Color(0, 0, 0, 80);
        private RectangleShape Background = new RectangleShape();
        private Title Label;

        public Vector2f Size;

        public delegate void Action();
        public Action action;

        private Animator Fade = new Animator();

        public Sprite Icon;

        public bool MouseHover = false, Click = false, ActionDone = false;

        /// <summary>
        /// Creates a button.
        /// </summary>
        public Button()
        {
            Fade.To = 0.1f;
        }

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="Position">Button position.</param>
        /// <param name="Label">Button text.</param>
        /// <param name="Size">Button size.</param>
        public Button(Vector2f Position, Title Label, Vector2f Size)
        {
            this.Position = Position;
            this.Label = Label;
            this.Size = Size;
            Fade.To = 0.1f;
        }

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="Position">Button position.</param>
        /// <param name="Label">Button text.</param>
        /// <param name="Size">Button size.</param>
        public Button(Vector2f Position, Title Label, Vector2f Size, Action func)
        {
            this.Position = Position;
            this.Label = Label;
            this.Size = Size;
            action = func;
            Fade.To = 0.1f;
        }

        /// <summary>
        /// Updates button parameters.
        /// </summary>
        public override void Update(float DeltaTime)
        {
            Label.Update(DeltaTime);
            Background.Position = new Vector2f(Position.X, Position.Y);
            Background.Size = Size;
            if (Icon != null)
                Icon.Position = new Vector2f(Position.X + Size.X / 2 - Icon.TextureRect.Width / 2, Position.Y + Size.Y / 2 - Icon.TextureRect.Height / 2);

            Label.Position = new Vector2f(Background.Position.X + Size.X / 2 - Label.text.GetGlobalBounds().Width / 2,
                Background.Position.Y + Size.Y / 2 - Label.text.GetGlobalBounds().Height / 1.6f);

            if (MouseHover && !Click)
            {
                Background.FillColor = new Color((byte)Fade.Lerp(Background.FillColor.R, 255, DeltaTime),
                    (byte)Fade.Lerp(Background.FillColor.G, 255, DeltaTime),
                    (byte)Fade.Lerp(Background.FillColor.B, 255, DeltaTime),
                    (byte)Fade.Lerp(Background.FillColor.A, 200, DeltaTime)); // 200
                Label.BaseColor = new Color(20, 20, 20, 255);
            }
            else if (!MouseHover && !Click)
            {
                Background.FillColor = new Color((byte)Fade.Lerp(Background.FillColor.R, 0, DeltaTime),
                    (byte)Fade.Lerp(Background.FillColor.G, 0, DeltaTime),
                    (byte)Fade.Lerp(Background.FillColor.B, 0, DeltaTime),
                    (byte)Fade.Lerp(Background.FillColor.A, 200, DeltaTime)); // 0 0 0 200
                Label.BaseColor = Color.White;
            }
            if (Click && MouseHover)
            {
                Background.FillColor = new Color(255, 255, 255, 255);
                Label.BaseColor = Color.Black;

                if (action != null && !ActionDone && IsActive)
                {
                    action.Invoke();
                    ActionDone = true;
                }
            }
        }

        /// <summary>
        /// Updating a cursor position
        /// </summary>
        /// <param name="e"></param>
        public override void MouseCheck(MouseMoveEventArgs e)
        {
            if (e.X >= Position.X && e.X <= Position.X + Size.X &&
                    e.Y >= Position.Y && e.Y <= Position.Y + Size.Y)
            {
                MouseHover = true;
            }
            else
            {
                MouseHover = false;
                Click = false;
            }
        }

        /// <summary>
        /// Mouse click event
        /// </summary>
        /// <param name="e"></param>
        public override void MouseClick(MouseButtonEventArgs e)
        {
            if (MouseHover && e.Button == Mouse.Button.Left)
            {
                Click = true;
            }
            else
            {
                Click = false;
            }
        }

        /// <summary>
        /// Mouse release event
        /// </summary>
        /// <param name="e"></param>
        public override void MouseRelease(MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                Click = false;
                ActionDone = false;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (IsActive)
            {
                target.Draw(Background);
                target.Draw(Label);
                if (Icon != null)
                    target.Draw(Icon);
            }
        }
    }
}
