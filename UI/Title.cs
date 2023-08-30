using SFML.Graphics;
using SFML.System;

namespace bonheur.UserInterface
{
    public class Title : UI
    {
        public Text text;
        public Color BaseColor = Color.White;

        public Title(string title, Font font)
        {
            text = new Text(title, font);
            text.FillColor = BaseColor;
        }

        public Title(string title, Font font, Vector2f position)
        {
            text = new Text(title, font);
            text.Position = position;
            text.FillColor = BaseColor;
        }

        public void SetText(string title)
        {
            text.DisplayedString = title;
        }

        public void SetCharacterSize(uint size)
        {
            text.CharacterSize = size;
        }

        public override void Update(float DeltaTime)
        {
            text.Position = Position;
            text.Rotation = Rotation;
            text.FillColor = BaseColor;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (IsActive)
                target.Draw(text);
        }
    }
}
