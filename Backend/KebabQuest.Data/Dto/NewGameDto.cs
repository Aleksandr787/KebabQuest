﻿using KebabQuest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabQuest.Data.Dto
{
    public class NewGameDto
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Plot { get; set; }
        public string? Question { get; set; }
        public Options? Options { get; set; }
        public string? Image { get; set; }
    }
}
