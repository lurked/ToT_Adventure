﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace ToT_Adventure
{
    public class Camera
    {
        Vector2 position;
        Matrix viewMatrix;
        public Vector2 ScreenDimensions { get; set; }

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Camera()
        {
            ScreenDimensions = ToT.Settings.Resolution;
        }

        public Camera(int width, int height)
        {
            ScreenDimensions = new Vector2(width, height);
        }
        public void SetFocalPoint(Vector2 focalPosition)
        {
            Vector2 tPosition = position;
            tPosition.X = focalPosition.X - ScreenDimensions.X / 2;
            tPosition.Y = focalPosition.Y - ScreenDimensions.Y / 2;

            position = tPosition;
        }

        public void Update()
        {
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}
