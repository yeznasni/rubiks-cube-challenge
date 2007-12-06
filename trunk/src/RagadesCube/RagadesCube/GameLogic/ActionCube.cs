using System;
using Microsoft.Xna.Framework;
using RagadesCube.SceneObjects;
using RagadesCube.Controllers;
using Microsoft.Xna.Framework.Graphics;
using RagadesCube.Scenes;
using System.Collections.Generic;
using RC.Engine.Cameras;


namespace RagadesCube.GameLogic
{
    public interface IRCCubeViewer
    {
        int MoveCount { get; }
        Matrix LocalTrans { get; }
        Matrix WorldTrans { get; }
        bool IsSolved { get; }
    }

    public class RCActionCube : GameComponent, IRCCubeViewer
    {
        public delegate void ActionCubeEventHandler(RCActionCube cube);
        public event ActionCubeEventHandler RotateAnimationComplete;

        private bool _isMoving;
        private int _moveCount;
        private RCCube _myCube;
        private RCCubeController _controller;
        private RCCubeCursor _cursor;
        private Vector2 _moveVector;
        private Vector3 _xAxis;
        private Vector3 _yAxis;
 

        public RCActionCube(Game game)
            : base(game)
        {
            _moveCount = 0;
            _moveVector = new Vector2();
            _myCube = new RCCube(3, 3, 3);
            _controller = new RCCubeController();
            _cursor = new RCCubeCursor(_myCube);
            _controller.AttachToObject(_myCube);
            _myCube.AddChild(_cursor);

            _controller.OnComplete += OnAnimationComplete;

            Game.Components.Add(this);
        }

        public int MoveCount
        {
            get { return _moveCount; }
        }

        public int Length
        {
            get { return _myCube.Length; }
        }

        public int Width
        {
            get { return _myCube.Width; }
        }

        public int Height
        {
            get { return _myCube.Height; }
        }

        public void ResetMoveCount()
        {
            _moveCount = 0;
        }

        public override void Update(GameTime gameTime)
        {
            _cursor.IsVisible = !_controller.IsAnimating;
            
            if (_isMoving)
            {
                Quaternion yRot = Quaternion.CreateFromAxisAngle(_xAxis, _moveVector.X);
                Quaternion xRot = Quaternion.CreateFromAxisAngle(_yAxis, _moveVector.Y);
                Quaternion totalRot =  Quaternion.Concatenate(xRot, yRot);

                _myCube.LocalTrans *= Matrix.CreateFromQuaternion(totalRot);
                _isMoving = false;
            }
        }

        public RCCubeScene CreateScene(Viewport vp)
        {
            RCCubeScene scene = new RCCubeScene(vp, _myCube);
            return scene;
        }

        public void Select(RCCube.FaceSide selectedFace)
        {
            _cursor.SelectedFace = selectedFace;
        }

        public void Move(Vector3 xAxis, Vector3 yAxis, Vector2 where)
        {
            if (IsMoving) return;
            
            _xAxis = xAxis;
            _yAxis = yAxis;
            _moveVector = where;
            _isMoving = true;
        }

        public void Rotate(Vector3 viewVector, RCCube.RotationDirection rotationDir)
        {
            if (IsRotating) return;

            RCCube.FaceSide faceSide = _cursor.SelectedFace;
            Vector3 faceNormal = _myCube.GetWorldFaceNormal(faceSide);

            if (Vector3.Dot(viewVector, faceNormal) < 0)
            {
                if (rotationDir == RCCube.RotationDirection.Clockwise)
                    rotationDir = RCCube.RotationDirection.CounterClockwise;
                else
                    rotationDir = RCCube.RotationDirection.Clockwise;
            }

            Rotate(rotationDir);
        }

        public void Rotate(RCCube.RotationDirection rotationDir)
        {
            if (IsRotating) return;
            _controller.RotateFace(_cursor.SelectedFace, rotationDir);
            _moveCount++;
        }

        public Matrix LocalTrans
        {
            get { return _myCube.LocalTrans; }
        }
 
        public Matrix WorldTrans
        {
            get { return _myCube.WorldTrans; }
        }

        public bool IsMoving
        {
            get { return _isMoving; }
        }

        public bool IsRotating
        {
            get { return _controller.IsAnimating; }
        }

        public bool IsSolved
        {
            get
            {
#if !XBOX

                foreach(RCCube.FaceSide face in Enum.GetValues(typeof(RCCube.FaceSide)))

#else

                foreach (RCCube.FaceSide face in EnumHelper.GetValues(typeof(RCCube.FaceSide)))

#endif

                {
                    List<RCFacelet> facelets = _myCube.GetFaceletsOnFace(face);
                    string colorName = "";

                    if (facelets.Count == 0) return false;

                    foreach (RCFacelet facelet in facelets)
                    {
                        if(colorName == "")
                            colorName = facelet.Color.ToString();
                        else if(colorName != facelet.Color.ToString())
                            return false;
                    }
                }

                return true;
            }
        }

        protected void OnAnimationComplete(object sender, EventArgs e)
        {
            if (RotateAnimationComplete != null)
            {
                RotateAnimationComplete(this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Game.Components.Remove(this);

            base.Dispose(disposing);
        }
    }
}
