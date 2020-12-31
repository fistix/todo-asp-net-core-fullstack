﻿using Fistix.Training.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fistix.Training.Core
{
    public interface IProfileRepository
    {
        Task<Profile> Create(Profile profile);
    }
}