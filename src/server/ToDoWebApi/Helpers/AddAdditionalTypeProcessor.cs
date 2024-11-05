using Namotion.Reflection;
using NSwag.Generation.Processors.Contexts;
using NSwag.Generation.Processors;

namespace TodoWebApi.Helpers
{
    public sealed class AddAdditionalTypeProcessor<T> : IDocumentProcessor where T : class
    {
        public void Process(DocumentProcessorContext context)
        {
            context.SchemaGenerator.Generate(typeof(T).ToContextualType(), context.SchemaResolver);
        }
    }
}
