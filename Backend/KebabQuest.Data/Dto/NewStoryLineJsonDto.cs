﻿using KebabQuest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Dto
{
    public class NewStoryLineJsonDto
    {
        public string Title { get; set; } = "";
        public string Plot { get; set; } = "";
        public string GameColors { get; set; } = "";
        public MainPlayer MainPlayer { get; set; } = null!;
        public string Question { get; set; } = null!;
        public Options Options { get; set; } = null!;
    }
}
