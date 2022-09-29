using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Domain.Entities;

namespace Dictionary.Application.Abstractions.Repositories
{
    public interface IEmailConfirmationRepository:IRepository<EmailConfirmation>
    {
    }
}
