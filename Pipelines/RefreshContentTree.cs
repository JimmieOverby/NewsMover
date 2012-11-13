using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            string message = String.Format("item:refreshchildren(id={0})", args.Root.ID.ToString());
            Sitecore.Context.ClientPage.SendMessage(this, message);
        }
    }
}
