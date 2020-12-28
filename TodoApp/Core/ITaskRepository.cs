﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core
{
    public interface ITaskRepository
    {
        Task<Domain.DataModels.Task> Create(Domain.DataModels.Task task);
        Task<List<Domain.DataModels.Task>> GetAll();
        Task<Domain.DataModels.Task> GetById(Guid id);
    }
}