using FluentArch.Arch.Layer;
using FluentArch.DTO;

namespace FluentArch.Filters
{
    public class IntermediaryFilter
    {
        private readonly IEnumerable<ClassEntityDto> _classes;
        public IntermediaryFilter(IEnumerable<ClassEntityDto> classes)
        {
            _classes = classes;
        }

        public Layer DefineAsLayer()
        {
            return new Layer(_classes, "");
        }

        public Filter And()
        {
            return new Filter(_classes);
        }
    }
}
