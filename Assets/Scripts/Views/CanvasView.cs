using System;
using SVE.Models;
using UnityEngine;
using Color = UnityEngine.Color;

namespace SVE.Views
{
    public partial class CanvasView : MonoBehaviour
    {
        private Action<ILayout> DrawCallback;

        private Func<ETool> GetCurrentTool;
        private Func<Color> GetBorderColor;
        private Func<Color> GetFillColor;

        private int _canvasWidth;
        private int _canvasHeight;

        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private bool _isDrawing;
        private Texture2D _texture;

        public void Init(Action<ILayout> drawCallback, Func<ETool> getCurrentTool,
            Func<Color> getFillColor, Func<Color> getBorderColor)
        {
            _canvasWidth = (int) GetComponent<RectTransform>().rect.width;
            _canvasHeight = (int) GetComponent<RectTransform>().rect.height;

            _texture = new Texture2D(_canvasWidth, _canvasHeight);

            DrawCallback = drawCallback;

            GetCurrentTool = getCurrentTool;
            GetFillColor = getFillColor;
            GetBorderColor = getBorderColor;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPoint = Input.mousePosition;
                _isDrawing = true;
            }

            if (Input.GetMouseButtonUp(0)) _isDrawing = false;
            if (!_isDrawing) return;

            _endPoint = Input.mousePosition;

            var layout = new BaseLayout(
                    new Point(_startPoint.x, _startPoint.y),
                    new Point(_endPoint.x, _endPoint.y));

            var shape =
                new BaseShape(layout, WrapColor(GetBorderColor()), WrapColor(GetFillColor()));

            Draw(shape);
        }

        public void Draw(BaseShape shape)
        {
            ClearCanvas();

            var p1 = shape.Point1;
            var p2 = shape.Point2;

            var tool = GetCurrentTool();

            var maxX = Math.Max(p1.x, p2.x);
            var minX = Math.Min(p1.x, p2.x);
            var maxY = Math.Max(p1.y, p2.y);
            var minY = Math.Min(p1.y, p2.y);

            var deltaX = maxX - minX;
            var deltaY = maxY - minY;
            var maxDelta = Math.Max(deltaX, deltaY);
            var minDelta = Math.Min(deltaX, deltaY);
            var coff = maxDelta / minDelta;

            switch (tool)
            {
                case ETool.Line:
                    for (var i = minX; i < maxX; i++)
                            _texture.SetPixel((int) i,  (int) (i / coff), WrapColor(shape.Color));
                    break;
                case ETool.Rectangle:
                    for (var i = minX; i < maxX; i++)
                        for (var j = minY; j < maxY; j++)
                            _texture.SetPixel((int) i, (int) j, WrapColor(shape.Color));
                    break;
                case ETool.Circle:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GetComponent<CanvasRenderer>().SetTexture(_texture);
            _texture.Apply();

            //if (DrawCallback != null)
//                DrawCallback((ILayout) shape);
        }

        private void ClearCanvas()
        {
            var resetColor = Color.clear;
            var resetColorArray = _texture.GetPixels32();

            for (var i = 0; i < resetColorArray.Length; i++)
                resetColorArray[i] = resetColor;

            _texture.SetPixels32(resetColorArray);
            _texture.Apply();
        }
    }
}