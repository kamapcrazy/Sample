namespace Sample.PDF.Services.Models
{
    public class DocSettings
    {
        public DocSettings(ColorEnum color, OrientationEnum orientation, PaperKindEnum paperKind)
        {
            Color = color;
            Orientation = orientation;
            PaperKind = paperKind;
        }

        public ColorEnum Color { get; set; }
        public OrientationEnum Orientation { get; set; }
        public PaperKindEnum PaperKind { get; set; }
    }
}
