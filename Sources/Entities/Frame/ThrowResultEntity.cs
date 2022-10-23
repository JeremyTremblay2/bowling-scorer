using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Frame
{
    public class ThrowResultEntity
    {
        public int ThrowResultId { get; set; }

        public FrameEntity FrameEntity { get; set; }
        public char Value { get; set; }

    }
}
