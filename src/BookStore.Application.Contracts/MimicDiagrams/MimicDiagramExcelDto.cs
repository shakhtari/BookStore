using System;

namespace WebNet.MimicDiagrams
{
    public class MimicDiagramExcelDto
    {
        public bool Active { get; set; }
        public string MimicDiagramName { get; set; }
        public string MimicDiagramDescription { get; set; }
        public string MimicDiagramXML { get; set; }
        public bool MimicDiagramAuthorization { get; set; }
    }
}