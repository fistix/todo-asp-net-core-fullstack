﻿using Fistix.Training.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Domain.Commands.Tasks
{
    public class CreateTaskCommandResult
    {
        //public DataModels.Task Payload { get; set; }
        public TaskDto Payload { get; set; }
    }
}