using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pariveda.Domain.Messages;

namespace Pariveda.Domain.Commands
{
    [Serializable]
    public abstract class Command : Message
    {
        public int Version { get; private set; }

        public Command(int version)
        {
            // Commands create a new version so we need to increment the version for the new command
            Version = version + 1;
        }
    }
}
