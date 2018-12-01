using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Infrastructure.Commands.Events
{
    public class UpdateCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
