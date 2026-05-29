using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameProject.Scenes
{
    public abstract class Scene : IDisposable
    {
        protected ContentManager Content { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { }

        public virtual void Initialize()
        {
            LoadContent();
        }

        public virtual void LoadContent() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }
    }
}
