using System;
using System.Collections.Generic;
using System.Text;

namespace ImageRecognition.Model.Core.Abstract
{
    public interface ITimeService
    {
        DateTime CurrentTime { get;  }
    }
}
