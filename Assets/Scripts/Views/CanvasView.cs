using System;
using SVE.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using Color = UnityEngine.Color;

namespace SVE.Views
{
    public partial class CanvasView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Action<IShape> DrawCallback;

        private Func<EShapeType> GetCurrentTool;
        private Func<Color> GetBorderColor;
        private Func<Color> GetFillColor;

        private int _canvasWidth;
        private int _canvasHeight;

        private Vector3 _startPoint;
        private Vector3 _endPoint;

        private bool _isDrawing;
        private bool _isPointerOnCanvas;

        private Texture2D _texture;

        public void Init(Action<IShape> drawCallback, Func<EShapeType> getCurrentTool,
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
            if (Input.GetMouseButtonDown(0) && _isPointerOnCanvas)
            {
                _startPoint = Input.mousePosition;
                _isDrawing = true;
            }
            if (!_isDrawing) return;

            _endPoint = Input.mousePosition;

            var layout = new BaseLayout(
                new Point(_startPoint.x, _startPoint.y),
                new Point(_endPoint.x, _endPoint.y));

            var shape = new BaseShape(
                layout,
                GetCurrentTool(),
                WrapColor(GetFillColor()),
                WrapColor(GetBorderColor()));

            Draw(shape);

            if (Input.GetMouseButtonUp(0) && _isPointerOnCanvas)
            {
                Debug.Log("draw and send shae");
                _isDrawing = false;
                Draw(shape, true, false);
            }
        }

        public void Draw(IShape shape, bool sendShape = false, bool clear = true)
        {
            if (clear)
                ClearCanvas();

            var p1 = ((BaseShape) shape).Point1;
            var p2 = ((BaseShape) shape).Point2;

            var maxX = Math.Max(p1.x, p2.x);
            var minX = Math.Min(p1.x, p2.x);
            var maxY = Math.Max(p1.y, p2.y);
            var minY = Math.Min(p1.y, p2.y);

            var deltaX = maxX - minX;
            var deltaY = maxY - minY;
            var maxDelta = Math.Max(deltaX, deltaY);
            var minDelta = Math.Min(deltaX, deltaY);
            var coff = maxDelta / minDelta;

            switch (shape.ShapeType)
            {
                case EShapeType.Line:
                    for (var i = minX; i < maxX; i++)
                        _texture.SetPixel((int) i,  (int) (i / coff), WrapColor(shape.Color));
                    break;
                case EShapeType.Rectangle:
                    for (var i = minX; i < maxX; i++)
                        for (var j = minY; j < maxY; j++)
                            _texture.SetPixel((int) i, (int) j, WrapColor(shape.Color));
                    break;
                case EShapeType.Ellipse:
                    for (double t = 0; t < 1; t += 0.001)
                    {
                        var x1 = (1 - t) * (1 - t) * p1.x + 2 * (1 - t) * t * p2.x + t * t * p2.x;
                        var y1 = (1 - t) * (1 - t) * p2.y + 2 * t * (1 - t) * p2.y + t * t * p1.y;

                        var x2 = (1 - t) * (1 - t) * p1.x + 2 * (1 - t) * t * p1.x + t * t * p2.x;
                        var y2 = (1 - t) * (1 - t) * p2.y + 2 * t * (1 - t) * p1.y + t * t * p1.y;

                        _texture.SetPixel((int) x1, (int) y1, WrapColor(shape.Color));
                        _texture.SetPixel((int) x2, (int) y2, WrapColor(shape.Color));
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            GetComponent<CanvasRenderer>().SetTexture(_texture);
            _texture.Apply();

            if (DrawCallback == null || !sendShape) return;
            Debug.Log("draw callback");
            DrawCallback(shape);
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

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOnCanvas = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerOnCanvas = false;
        }
    }
}