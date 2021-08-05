namespace DBContext.Models
{
    public partial class GroupPropToTypeAudio
    {
        public int IdPk { get; set; }
        public int IdTypeAudio { get; set; }
        public int IdGroupProp { get; set; }
        public bool IsNecessary { get; set; }

        public virtual GroupProperties IdGroupPropNavigation { get; set; }
        public virtual TypeAudio IdTypeAudioNavigation { get; set; }
    }
}
