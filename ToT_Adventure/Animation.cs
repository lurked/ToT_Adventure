using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ToT_Adventure
{
    public class Animation
    {
        public string ImageName { get; set; }
        public Vector2 Frames { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Origin { get; set; }
        public int FX { get; set; }
        public int FY { get; set; }
        public int LastFX { get; set; }

        private int lastFY;

        public int GetLastFY()
        {
            return lastFY;
        }

        public void SetLastFY(int value)
        {
            lastFY = value;
        }

        public bool Active { get; set; }
        public Toolbox.AnimationType Type { get; set; }
        public Rectangle SourceRect { get; set; }
        Vector2 BaseFrames;
        private int frameCounter;
        private readonly int switchFrame;


        public int FrameWidth
        {
            get
            {
                if (Frames.X != 0f)
                    return ToT.Textures[ImageName].Width / (int)Frames.X;
                else
                    return ToT.Textures[ImageName].Width;
            }
        }

        public int FrameHeight
        {
            get
            {
                if (Frames.Y != 0f)
                    return ToT.Textures[ImageName].Height / (int)Frames.Y;
                else
                    return ToT.Textures[ImageName].Height;
            }
        }


        public Animation(Toolbox.AnimationType tType = Toolbox.AnimationType.None, string imageName = "colorwheel32")
        {
            FX = 0;
            FY = 0;
            LastFX = 0;
            lastFY = 0;

            Type = tType;
            BaseFrames = new Vector2(1, 1);

            frameCounter = 0;
            switchFrame = 100;
            ImageName = imageName;
        }

        public void Init()
        {
            if (ImageName != "")
                InitAnim();
        }

        public void InitAnim()
        {
            if (Frames != BaseFrames)
                SourceRect = new Rectangle(FX * FrameWidth, FY * FrameHeight, FrameWidth, FrameHeight);
            else
                SourceRect = new Rectangle(0, 0, ToT.Textures[ImageName].Width, ToT.Textures[ImageName].Height);

            Size = new Vector2(SourceRect.Width, SourceRect.Height);

            Origin = Size / 2;
        }

        internal Rectangle LevelRectangle(Vector2 levelPosition, Vector2 levelTileSize)
        {
            return new Rectangle((int)(levelPosition.X * levelTileSize.X), (int)(levelPosition.Y * levelTileSize.Y), SourceRect.Width, SourceRect.Height);
        }

        public void Update()
        {
            switch (Type)
            {
                case Toolbox.AnimationType.Spritesheet:
                    if (Active)
                    {
                        frameCounter += (int)(1000f / ToT.Settings.FPScap);
                        if (Frames.X > 1)
                        {
                            if (frameCounter >= switchFrame)
                            {
                                frameCounter = 0;
                                FX++;

                                if (FX >= Frames.X)
                                    FX = 0;
                            }

                        }
                    }
                    else
                    {
                        frameCounter = 0;
                        FX = 1;
                    }
                    if (LastFX != FX || lastFY != FY)
                    {
                        SourceRect = new Rectangle(FX * (int)Size.X, FY * (int)Size.Y, (int)Size.X, (int)Size.Y);
                        LastFX = FX;
                        lastFY = FY;
                    }

                    SourceRect = new Rectangle((int)FX * FrameWidth, (int)FY * FrameHeight,
                        FrameWidth, FrameHeight);
                    break;
                default:

                    break;
            }
        }
    }
}
