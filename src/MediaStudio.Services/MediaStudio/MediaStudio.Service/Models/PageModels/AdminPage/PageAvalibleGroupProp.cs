namespace MediaStudioService.Models.PageModels
{
    public class PageAvalibleGroupProp
    {
        public int Key { get; set; }
        public int IdTypeAudio { get; set; }
        public int IdGroupProp { get; set; }
        public string NameGroupProp { get; set; }
        public bool? AllowMultiselect { get; set; }
        public bool IsNecessary { get; set; }
    }
}
