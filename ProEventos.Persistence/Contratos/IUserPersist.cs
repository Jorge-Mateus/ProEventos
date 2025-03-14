﻿using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Contratos
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> GetUserByIdAsync(int Id);
        Task<User> GetUserByUserNameAsync(string userName);

    }
}
