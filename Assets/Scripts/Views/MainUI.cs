﻿using System;
using System.Collections.Generic;
using System.Linq;
using SVE.Models;
using SVE.Views;

using UnityEngine;
using UnityEngine.UI;

using Color = UnityEngine.Color;

namespace SVE.UI
{
    public class MainUI : MonoBehaviour
    {
        private EShapeType _currentETool;
        private Color _currentBorderColor;
        private Color _currentFillColor;
        private bool _selectedColorType;

        [SerializeField] private Button SaveProject;
        [SerializeField] private List<Button> Colors;
        [SerializeField] private Dropdown Tools;
        [SerializeField] private CanvasView CanvasView;
        [SerializeField] private Button CurrentBorderColor;
        [SerializeField] private Button CurrentFillColor;

        public Action Undo;
        public Action Redo;
        public Action Save;
        public Action<IShape> OnCreateShape;

        private void Start()
        {
            if (!CheckBindings()) return;
            Init();
        }

        private bool CheckBindings()
        {
            if (CanvasView == null)
                Debug.LogError("[MainUI][CheckBindings] CanvasView not assigned!");

            if (SaveProject == null)
                Debug.LogError("[ProjectManagerView][CheckBindings] SaveProject button not assigned!");

            return CanvasView != null && SaveProject != null;
        }

        public void Init()
        {
            CanvasView.Init(
                drawCallback: layout => OnCreateShape(layout),
                getBorderColor: () => _currentBorderColor,
                getCurrentTool: () => _currentETool,
                getFillColor:() => _currentFillColor);

            Colors.ForEach(
                color => color.onClick.AddListener(() =>
                {
                    if (_selectedColorType)
                    {
                        _currentBorderColor = color.GetComponent<Image>().color;
                        CurrentBorderColor.GetComponent<Image>().color = _currentBorderColor;
                    }
                    else
                    {
                        _currentFillColor = color.GetComponent<Image>().color;
                        CurrentFillColor.GetComponent<Image>().color = _currentFillColor;
                    }
                }));

            Tools.onValueChanged.AddListener(value => _currentETool = (EShapeType) value);

            CurrentBorderColor.onClick.AddListener(() => _selectedColorType = true);
            CurrentFillColor.onClick.AddListener(() => _selectedColorType = false);
        }

        public void RedrawCanvas(IList<IShape> shapes)
        {
            shapes.ToList().ForEach(shape => CanvasView.Draw(shape, false, false));
        }

        private void Update()
        {
            //ctrl-z
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Z) && Undo != null)
#else
            if ((Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.LeftControl)) &&
                Input.GetKeyDown(KeyCode.Z) && Undo != null)
 #endif
                Undo();

        //ctrl-shift-z
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Y) && Redo != null)
#else
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
                (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                Input.GetKeyDown(KeyCode.Z) && Redo != null)
#endif
            Redo();

        //ctrl-y
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Y) && Redo != null)
#else
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) &&
                (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                Input.GetKeyDown(KeyCode.Y) && Redo != null)
#endif
                    Redo();

        //ctrl-s
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.S) && Save != null)
#else
            if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)) &&
                Input.GetKeyDown(KeyCode.S) && Save != null)
#endif
                Save();
        }
    }
}