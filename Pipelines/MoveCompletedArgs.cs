using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace Sitecore.Sharedsource.NewsMover.Pipelines
{
    public class MoveCompletedArgs : PipelineArgs
    {
        // Methods
        public MoveCompletedArgs() { }

        // Properties
        public Item Article { get; set; }
        public Item Root { get; set; }
    }
}
