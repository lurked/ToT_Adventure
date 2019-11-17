using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class GameLevelScreen : Screen
    {
        public Level Level { get; set; }

        public GameLevelScreen()
        {
            Level = LevelGenerator.Generate("basic", new Vector2(9, 9));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<Vector2, Tile> tile in Level.Tileset)
            {
                spriteBatch.Draw(
                    ToT.Textures[tile.Value.ImageName],
                    tile.Key * ToT.Settings.LevelTileSize,
                    null,
                    Color.White
                );
            }
            base.Draw(spriteBatch);
        }
    }
}
