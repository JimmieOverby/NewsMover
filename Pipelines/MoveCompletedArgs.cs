using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace Sitecore.Sharedsource.NewsMover.Pipelines
{
    public class MoveCompletedArgs : PipelineArgs
    {
        // Properties
        public Item Article { get; set; }
        public Item Root { get; set; }
    }
}
