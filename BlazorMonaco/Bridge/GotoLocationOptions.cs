using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorMonaco.Bridge
{
    public class GotoLocationOptions
    {
        public string AlternativeDeclarationCommand { get; set; }
        public string AlternativeDefinitionCommand { get; set; }
        public string AlternativeImplementationCommand { get; set; }
        public string AlternativeReferenceCommand { get; set; }
        public string AlternativeTypeDefinitionCommand { get; set; }
        public string Multiple { get; set; }
        public string MultipleDeclarations { get; set; }
        public string MultipleDefinitions { get; set; }
        public string MultipleImplementations { get; set; }
        public string MultipleReferences { get; set; }
        public string MultipleTypeDefinitions { get; set; }
    }
}
