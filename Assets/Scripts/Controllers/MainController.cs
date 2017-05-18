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
        public Action<IList<ILayout>> CanvasRedrawCallback;
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
        }

        public void Undo()
        {
            _commands.Last().Revert();
        }

        public void Redo()
        {
            _commands.Last().Execute();
        }

        public void Save()
        {
            //string path, IProjectFormat format
            Debug.LogError("[MainController][SaveProject] save call!");
        }
    }
}