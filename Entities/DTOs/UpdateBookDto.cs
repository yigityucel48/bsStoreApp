using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public record UpdateBookDto:ValidationBaseDto
    {
        public int Id { get; init; }
    }
}
