using System.IO;
using Sitecore.Mvc.Pipelines.Response.RenderRendering;
using Sitecore.Mvc.Presentation;

namespace Specflow.Web.Pipelines.MvcRenderRendering
{
    public class AddSpecflowTag : ExecuteRenderer
    {
        protected override bool Render(Renderer renderer, TextWriter writer, RenderRenderingArgs args)
        {
            if (Sitecore.Context.Site.Name == "kitchensink")
            {
                writer.WriteLine($"<span class=\"renderingInfo\" data-renderingid=\"{args.Rendering.RenderingItem.ID}\" />");
            }

            return base.Render(renderer, writer, args);
        }
    }
}