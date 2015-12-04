using System;
using Sitecore.Diagnostics;

namespace Sitecore.Sharedsource.NewsMover.Pipelines
{
    public class RefreshContentTree : IMoveCompletedPipelineProcessor
    {
        // Methods
        public void Process(MoveCompletedArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.Article, "Item");
            Assert.ArgumentNotNull(args.Root, "Root");

            var message = String.Format("item:refreshchildren(id={0})", args.Root.ID);
            Context.ClientPage.SendMessage(this, message);
        }
    }
}
