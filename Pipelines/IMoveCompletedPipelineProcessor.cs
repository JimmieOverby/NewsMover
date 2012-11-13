using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sitecore.Sharedsource.NewsMover.Pipelines
{
    public interface IMoveCompletedPipelineProcessor
    {
        // Methods
        void Process(MoveCompletedArgs args);
    }
}
