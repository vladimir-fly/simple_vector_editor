using System;
using System.Collections.Generic;
using System.Linq;
using SVE.Models;
using UnityEngine;
using Color = SVE.Models.Color;

namespace SVE.Controllers
{
    public class MainController
    {
        public Action<IList<IShape>> CanvasRedrawCallback;
        public Action<Color> PaletteSwitchColor;
        public Action<List<string>> ObjectListShow;

        private List<ICommand> _commands = new List<ICommand>();
        public IProject Project { get; private set; }

        public MainController(IProject project)
        {
            Project = project;
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
            _commands.Last().Execute();

            CanvasRedrawCallback(Project.Shapes);
        }

        public void Undo()
        {
            _commands.Last().Revert();
            Debug.Log("UNDO");

            Debug.Log("shapes = " + Project.Shapes.Count);

            CanvasRedrawCallback(Project.Shapes);
        }

        public void Redo()
        {
            _commands.Last().Execute();
            Debug.Log("REDO");
            Debug.Log("shapes = " + Project.Shapes.Count);
            CanvasRedrawCallback(Project.Shapes);
        }

        public void Save()
        {
            Debug.LogError("[MainController][SaveProject] save call!");
        }
    }
}